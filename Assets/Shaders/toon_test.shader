Shader "Custom/TestToonShaderURP"
{
	Properties
	{
		_BaseMap ("Main Texture", 2D) = "white" {}
		_ShadowColor ("Shadow Color", Color) = (0,0,0,1)
		_HighlightColor ("Highlight Color", Color) = (1,1,1,1)
		_FresnelColor ("Fresnel Color", Color) = (1,1,1,1)
		_FresnelThreshold ("Fresnel Threshold", Range(0, 1)) = 0.2
		_FresnelHardness ("Fresnel Hardness", Range(0.01, 5)) = 1.0
		_ShadowThreshold ("Shadow Threshold", Range(0, 1)) = 0.5
		_HighlightThreshold ("Highlight Threshold", Range(0, 1)) = 0.8
	}

	SubShader
	{
		Tags { "RenderType"="Opaque" "RenderPipeline"="UniversalRenderPipeline" }
		LOD 100

		Pass
		{
			Name "ForwardLit"
			Tags { "LightMode"="UniversalForward" }

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
				float2 uv : TEXCOORD2;
			};

			// Texture and Sampler
			TEXTURE2D(_BaseMap);
			SAMPLER(sampler_BaseMap);

			CBUFFER_START(UnityPerMaterial)
				half4 _BaseColor;
				half4 _ShadowColor;
				half4 _HighlightColor;
				half4 _FresnelColor;
				float _FresnelThreshold;
				float _FresnelHardness;
				float _ShadowThreshold;
				float _HighlightThreshold;
				float4 _BaseMap_ST;
			CBUFFER_END

			Varyings vert (Attributes IN)
			{
				Varyings OUT;
				VertexPositionInputs posInputs = GetVertexPositionInputs(IN.positionOS.xyz);
				VertexNormalInputs normInputs = GetVertexNormalInputs(IN.normalOS);

				OUT.positionHCS = posInputs.positionCS;
				OUT.positionWS = posInputs.positionWS;
				OUT.normalWS = normInputs.normalWS;

				OUT.uv = TRANSFORM_TEX(IN.uv, _BaseMap);
				return OUT;
			}

			half4 frag (Varyings IN) : SV_Target
			{
				float3 normal = normalize(IN.normalWS);
				float3 viewDir = normalize(_WorldSpaceCameraPos - IN.positionWS);

				Light mainLight = GetMainLight();
				float3 lightDir = normalize(mainLight.direction);
				float NdotL = saturate(dot(normal, lightDir));

				half4 texColor = SAMPLE_TEXTURE2D(_BaseMap, sampler_BaseMap, IN.uv);

				half4 color;
				float shadowMask = step(NdotL, _ShadowThreshold);
				float highlightMask = step(_HighlightThreshold, NdotL);
				float midRangeMask = 1.0 - shadowMask - highlightMask;

				half4 shadowColor = shadowMask * (texColor * 0.4 + _ShadowColor * 0.6);
				half4 highlightColor = highlightMask * (texColor * 0.4 + _HighlightColor * 0.6);
				half4 midRangeColor = midRangeMask * texColor;

				color = shadowColor + highlightColor + midRangeColor;

				float fresnel = pow(1.0 - saturate(dot(normal, viewDir)), _FresnelHardness);
				fresnel = step(_FresnelThreshold, fresnel);
				color.rgb += fresnel * _FresnelColor.rgb;

				return half4(color.rgb, 1.0);
			}

			ENDHLSL
		}
	}

	FallBack Off
}
