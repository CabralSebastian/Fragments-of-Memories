Shader "Stylized/Sky Astral Blend"
{
  Properties
  {
    [Header(Astralization)]
    _Astralization ("Astralization", Range(0, 1)) = 0

    /* PHYSICAL */
    [Header(Physical Sun Disc)]
    _SunDiscColor_Physical ("Color", Color) = (1, 1, 1, 1)
    _SunDiscMultiplier_Physical ("Multiplier", float) = 25
    _SunDiscExponent_Physical ("Exponent", float) = 125000

    [Header(Physical Sun Halo)]
    _SunHaloColor_Physical ("Color", Color) = (0.8970588, 0.7760561, 0.6661981, 1)
    _SunHaloExponent_Physical ("Exponent", float) = 125
    _SunHaloContribution_Physical ("Contribution", Range(0, 1)) = 0.75

    [Header(Physical Horizon Line)]
    _HorizonLineColor_Physical ("Color", Color) = (0.9044118, 0.8872592, 0.7913603, 1)
    _HorizonLineExponent_Physical ("Exponent", float) = 4
    _HorizonLineContribution_Physical ("Contribution", Range(0, 1)) = 0.25
    
    [Header(Physical Sky Gradient)]
    _SkyGradientTop_Physical ("Top", Color) = (0.172549, 0.5686274, 0.6941177, 1)
    _SkyGradientBottom_Physical ("Bottom", Color) = (0.764706, 0.8156863, 0.8509805)
    _SkyGradientExponent_Physical ("Exponent", float) = 2.5

    /* ASTRAL */
    [Header(Astral Sun Disc)]
    _SunDiscColor_Astral ("Color", Color) = (1, 1, 1, 1)
    _SunDiscMultiplier_Astral ("Multiplier", float) = 25
    _SunDiscExponent_Astral ("Exponent", float) = 125000

    [Header(Astral Sun Halo)]
    _SunHaloColor_Astral ("Color", Color) = (0.8970588, 0.7760561, 0.6661981, 1)
    _SunHaloExponent_Astral ("Exponent", float) = 125
    _SunHaloContribution_Astral ("Contribution", Range(0, 1)) = 0.75

    [Header(Astral Horizon Line)]
    _HorizonLineColor_Astral ("Color", Color) = (0.9044118, 0.8872592, 0.7913603, 1)
    _HorizonLineExponent_Astral ("Exponent", float) = 4
    _HorizonLineContribution_Astral ("Contribution", Range(0, 1)) = 0.25
    
    [Header(Astral Sky Gradient)]
    _SkyGradientTop_Astral ("Top", Color) = (0.172549, 0.5686274, 0.6941177, 1)
    _SkyGradientBottom_Astral ("Bottom", Color) = (0.764706, 0.8156863, 0.8509805)
    _SkyGradientExponent_Astral ("Exponent", float) = 2.5
  }

  SubShader
  {
    Tags { "RenderType" = "Background" "Queue" = "Background" }
    LOD 100

    Pass
    {
      CGPROGRAM
      #pragma vertex vert
      #pragma fragment frag
      #include "UnityCG.cginc"

      /* Astralization */
      float _Astralization;

      /* Physical */
      float3 _SunDiscColor_Physical;
      float _SunDiscMultiplier_Physical;
      float _SunDiscExponent_Physical;
      float3 _SunHaloColor_Physical;
      float _SunHaloExponent_Physical;
      float _SunHaloContribution_Physical;
      float3 _HorizonLineColor_Physical;
      float _HorizonLineExponent_Physical;
      float _HorizonLineContribution_Physical;
      float3 _SkyGradientTop_Physical;
      float3 _SkyGradientBottom_Physical;
      float _SkyGradientExponent_Physical;

      /* Astral */
      float3 _SunDiscColor_Astral;
      float _SunDiscMultiplier_Astral;
      float _SunDiscExponent_Astral;
      float3 _SunHaloColor_Astral;
      float _SunHaloExponent_Astral;
      float _SunHaloContribution_Astral;
      float3 _HorizonLineColor_Astral;
      float _HorizonLineExponent_Astral;
      float _HorizonLineContribution_Astral;
      float3 _SkyGradientTop_Astral;
      float3 _SkyGradientBottom_Astral;
      float _SkyGradientExponent_Astral;

      struct appdata
      {
        float4 vertex : POSITION;
        float2 uv : TEXCOORD0;
      };

      struct v2f
      {
        float4 vertex : SV_POSITION;
        float3 worldPosition : TEXCOORD0;
      };

      v2f vert (appdata v)
      {
        v2f o;
        o.vertex = UnityObjectToClipPos(v.vertex);
        o.worldPosition = mul(unity_ObjectToWorld, v.vertex).xyz;
        return o;
      }

      fixed4 frag (v2f i) : SV_Target
      {
        // Interpolated parameters (Physical <-> Astral)
        float3 _SunDiscColor = lerp(_SunDiscColor_Physical, _SunDiscColor_Astral, _Astralization);
        float _SunDiscMultiplier = lerp(_SunDiscMultiplier_Physical, _SunDiscMultiplier_Astral, _Astralization);
        float _SunDiscExponent = lerp(_SunDiscExponent_Physical, _SunDiscExponent_Astral, _Astralization);

        float3 _SunHaloColor = lerp(_SunHaloColor_Physical, _SunHaloColor_Astral, _Astralization);
        float _SunHaloExponent = lerp(_SunHaloExponent_Physical, _SunHaloExponent_Astral, _Astralization);
        float _SunHaloContribution = lerp(_SunHaloContribution_Physical, _SunHaloContribution_Astral, _Astralization);

        float3 _HorizonLineColor = lerp(_HorizonLineColor_Physical, _HorizonLineColor_Astral, _Astralization);
        float _HorizonLineExponent = lerp(_HorizonLineExponent_Physical, _HorizonLineExponent_Astral, _Astralization);
        float _HorizonLineContribution = lerp(_HorizonLineContribution_Physical, _HorizonLineContribution_Astral, _Astralization);

        float3 _SkyGradientTop = lerp(_SkyGradientTop_Physical, _SkyGradientTop_Astral, _Astralization);
        float3 _SkyGradientBottom = lerp(_SkyGradientBottom_Physical, _SkyGradientBottom_Astral, _Astralization);
        float _SkyGradientExponent = lerp(_SkyGradientExponent_Physical, _SkyGradientExponent_Astral, _Astralization);

        // Lighting
        float3 dir = normalize(i.worldPosition);
        float maskHorizon = dot(dir, float3(0, 1, 0));
        float maskSunDir = dot(dir, _WorldSpaceLightPos0.xyz);

        // Sun disc
        float maskSun = pow(saturate(maskSunDir), _SunDiscExponent);
        maskSun = saturate(maskSun * _SunDiscMultiplier);

        // Sun halo
        float3 sunHaloColor = _SunHaloColor * _SunHaloContribution;
        float bellCurve = pow(saturate(maskSunDir), _SunHaloExponent * saturate(abs(maskHorizon)));
        float horizonSoften = 1 - pow(1 - saturate(maskHorizon), 50);
        sunHaloColor *= saturate(bellCurve * horizonSoften);

        // Horizon line
        float3 horizonLineColor = _HorizonLineColor * saturate(pow(1 - abs(maskHorizon), _HorizonLineExponent));
        horizonLineColor = lerp(0, horizonLineColor, _HorizonLineContribution);

        // Sky gradient
        float3 skyGradientColor = lerp(_SkyGradientTop, _SkyGradientBottom, pow(1 - saturate(maskHorizon), _SkyGradientExponent));

        // Combine
        float3 finalColor = lerp(saturate(sunHaloColor + horizonLineColor + skyGradientColor), _SunDiscColor, maskSun);
        return float4(finalColor, 1);
      }
      ENDCG
    }
  }
}
