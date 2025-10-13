Shader "Custom/WorldObjectTransparentShaderURP"
{
	Properties
	{
		_PhysicTexture ("Physic Texture", 2D) = "white" {}
		_AstralTexture ("Astral Texture", 2D) = "white" {}
		_ShadowColor ("Shadow Color", Color) = (0,0,0,1)
		_HighlightColor ("Highlight Color", Color) = (1,1,1,1)
		_FresnelColor ("Fresnel Color", Color) = (1,1,1,1)
		_FresnelThreshold ("Fresnel Threshold", Range(0, 1)) = 0.2
		_FresnelHardness ("Fresnel Hardness", Range(0.01, 5)) = 1.0
		_ShadowThreshold ("Shadow Threshold", Range(0, 1)) = 0.5
		_HighlightThreshold ("Highlight Threshold", Range(0, 1)) = 0.8
		_IsAstralWorld("Is Astral World", Range(0, 1)) = 0

		/* Peep Cylinder */
		_IsCylinderActive ("Is Cylinder Active", Range(0, 1)) = 0
		_CylinderDirection ("Cylinder Direction", Vector) = (0,1,0,0)
		_CylinderRadius ("Cylinder Radius", Float) = 0
		_CylinderOrigin ("Cylinder Origin", Vector) = (0,0,0,0)

		/* Synthesis Sphere */
		_IsSphereActive ("Is Sphere Active", Range(0, 1)) = 0
		_SphereRadius ("Sphere Radius", Float) = 0
		_SphereOrigin ("Sphere Origin", Vector) = (0,0,0,0)
	}

	SubShader
	{
		Tags { "RenderPipeline"="UniversalPipeline" "RenderType"="Transparent" "Queue"="Transparent" }
		Blend SrcAlpha OneMinusSrcAlpha
		LOD 100

		Pass
		{
			Name "ToonLitWithAstral"
			Tags { "LightMode"="UniversalForward" }

			Cull Off

			HLSLPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 2.0

			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

			struct Attributes
			{
				float4 positionOS : POSITION;
				float3 normalOS : NORMAL;
				float2 uv : TEXCOORD0;
			};

			struct Varyings
			{
				float4 positionHCS : SV_POSITION;
				float3 positionWS : TEXCOORD0;
				float3 normalWS : TEXCOORD1;
				float2 uvPhysic : TEXCOORD2;
				float2 uvAstral : TEXCOORD3;
			};

			TEXTURE2D(_PhysicTexture);
			TEXTURE2D(_AstralTexture);
			SAMPLER(sampler_PhysicTexture);
			SAMPLER(sampler_AstralTexture);

			CBUFFER_START(UnityPerMaterial)
				half4 _BaseColor;
				half4 _ShadowColor;
				half4 _HighlightColor;
				half4 _FresnelColor;
				float _FresnelThreshold;
				float _FresnelHardness;
				float _ShadowThreshold;
				float _HighlightThreshold;
				float4 _PhysicTexture_ST;
				float4 _AstralTexture_ST;
				float _IsAstralWorld;

				float _IsCylinderActive;
				float3 _CylinderDirection;
				float _CylinderRadius;
				float3 _CylinderOrigin;

				float _IsSphereActive;
				float _SphereRadius;
				float3 _SphereOrigin;
			CBUFFER_END

			Varyings vert (Attributes IN)
			{
				Varyings OUT;
				VertexPositionInputs posInputs = GetVertexPositionInputs(IN.positionOS.xyz);
				VertexNormalInputs normInputs = GetVertexNormalInputs(IN.normalOS);

				OUT.positionHCS = posInputs.positionCS;
				OUT.positionWS = posInputs.positionWS;
				OUT.normalWS = normInputs.normalWS;

				OUT.uvPhysic = TRANSFORM_TEX(IN.uv, _PhysicTexture);
				OUT.uvAstral = TRANSFORM_TEX(IN.uv, _AstralTexture);

				return OUT;
			}

			half4 frag (Varyings IN, bool isFrontFace : SV_IsFrontFace) : SV_Target
			{
				float3 normal = normalize(IN.normalWS);
				float3 viewDirection = normalize(_WorldSpaceCameraPos - IN.positionWS);

				/* NdotL */
				Light mainLight = GetMainLight();
				float3 lightDirection = normalize(mainLight.direction);
				float NdotL = saturate(dot(normal, lightDirection));

				/* Textures Sampling */
				half4 physicalColor = SAMPLE_TEXTURE2D(_PhysicTexture, sampler_PhysicTexture, IN.uvPhysic);
				half4 astralColor = SAMPLE_TEXTURE2D(_AstralTexture, sampler_AstralTexture, IN.uvAstral);

				/* Cylinder Effect */
				float3 pointToAxis = IN.positionWS - _CylinderOrigin;
				float3 proj = dot(pointToAxis, _CylinderDirection) * _CylinderDirection;
				float3 radial = pointToAxis - proj;

				float isInsideCylinder = step(length(radial), _CylinderRadius);
				float applyCylinderEffect = isInsideCylinder * _IsCylinderActive;

				/* Sphere Effect */
				float distanceToSphereCenter = pow(IN.positionWS.x - _SphereOrigin.x, 2) + pow(IN.positionWS.y - _SphereOrigin.y, 2) + pow(IN.positionWS.z - _SphereOrigin.z, 2);
				float isInsideSphere = step(distanceToSphereCenter, pow(_SphereRadius, 2));
				float applySphereEffect = isInsideSphere * _IsSphereActive;

				/* Astral Effect */
				float applyAstralEffect = abs(applySphereEffect - abs(applyCylinderEffect - _IsAstralWorld));
				half4 texColor = lerp(physicalColor, astralColor, applyAstralEffect);

				/* Toon Effect */
				float shadowMask = 1.0 - smoothstep(_ShadowThreshold - 0.05, _ShadowThreshold + 0.05, NdotL);
				float highlightMask = smoothstep(_HighlightThreshold - 0.02, _HighlightThreshold + 0.02, NdotL);
				float midRangeMask = 1.0 - shadowMask - highlightMask;

				half4 shadowColor = shadowMask * (texColor * 0.4 + _ShadowColor * 0.6);
				half4 highlightColor = highlightMask * (texColor * 0.2 + _HighlightColor * 0.8);
				half4 midRangeColor = midRangeMask * texColor;

				half4 color = shadowColor + highlightColor + midRangeColor;

				/* Fresnel Effect */
				float fresnel = pow(1.0 - saturate(dot(normal, viewDirection)), _FresnelHardness);
				fresnel *= saturate(NdotL); /* Show only in lit areas */
				fresnel = smoothstep(_FresnelThreshold - 0.05, _FresnelThreshold + 0.05, fresnel);
				color.rgb += fresnel * _FresnelColor.rgb;

				color.a = texColor.a;

				if (!isFrontFace)
					return half4(0, 0, 0, texColor.a);

				return half4(color);
			}

			ENDHLSL
		}
	}

	FallBack Off
}
