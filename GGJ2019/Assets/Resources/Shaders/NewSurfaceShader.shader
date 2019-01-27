Shader "Custom/Dissolve"
{
	Properties{
		_MainTex("Tex", 2D) = "white" {}
		_BaseColor("Albedo", 2D) = "white" {}
		_AO("AO", 2D) = "white" {}
		_Color("Color", Color) = (1, 1, 1, 1)
		_Metallic("Metallic", 2D) = "white" {}
		_MetallicFactor("Metallic Strength", Range(0,2)) = 1
		_Roughness("Roughness", 2D) = "white" {}
		_RoughnessFactor("Roughness Strength", Range(0,2)) = 1

		_SliceGuide("Slice Guide (RGB)", 2D) = "white" {}
		_BurnSize("Burn Size", Range(0.0, 1.0)) = 0.1
		_BurnRamp("Burn Ramp (RGB)", 2D) = "white" {}
		_Scale("Scale", float) = 0.25
		_DissolveAmount("Disolve Amount", Range(0.0, 1)) = 0.2
	}
		SubShader{
			Tags{ "RenderType" = "Opaque" }
			Cull Off
			CGPROGRAM
			#include "UnityPBSLighting.cginc"
			#pragma surface surf Standard addshadow

			struct Input {
				float2 uv_BaseColor;
				float2 uv_AO;
				float2 uv_Metallic;
				float2 uv_Roughness;

				float2 uv_SliceGuide;
				float3 worldPos;
				float3 worldNormal; INTERNAL_DATA
			};

			sampler2D _MainTex;
			sampler2D _BaseColor;
			sampler2D _AO;
			sampler2D _Metallic;
			sampler2D _Roughness;
			half _MetallicFactor;
			half _RoughnessFactor;

			sampler2D _SliceGuide;
			sampler2D _BurnRamp;
			float _BurnSize;
			half _Scale;
			float4 _Color;
			float _DissolveAmount;

			void surf(Input IN, inout SurfaceOutputStandard o) {
				float2 UV;
				fixed4 c;

				if (abs(IN.worldNormal.x) > 0.5) {
					UV = IN.worldPos.zy; // side
					c = tex2D(_BaseColor,UV);
				}
	 else if (abs(IN.worldNormal.z) > 0.5) {
	  UV = IN.worldPos.xy; // front
	  c = tex2D(_BaseColor,UV);
  }
else {
 UV = IN.worldPos.xz; // top
 c = tex2D(_BaseColor,UV);
}
UV.x *= _Scale;
UV.y *= _Scale;

clip(tex2D(_SliceGuide,UV).rgb - _DissolveAmount);
o.Albedo = tex2D(_BaseColor, IN.uv_BaseColor).rgb;
o.Albedo *= tex2D(_AO,IN.uv_AO).rgb;
o.Metallic = tex2D(_Metallic,IN.uv_Metallic).rgb * _MetallicFactor;
o.Smoothness = 1 - tex2D(_Roughness,IN.uv_Roughness).rgb * _RoughnessFactor;

half test = tex2D(_SliceGuide, UV).rgb - _DissolveAmount;
if (test < _BurnSize && _DissolveAmount > 0 && _DissolveAmount < 1) {
	o.Emission = tex2D(_BurnRamp, float2(test *(1 / _BurnSize), 0));
	o.Albedo *= o.Emission;
}
}
ENDCG
		}
			Fallback "Diffuse"
}