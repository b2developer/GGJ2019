Shader "Outlined/CustomSelectable" {
	Properties {
		_Color ("Main Color", Color) = (.5,.5,.5,1)
		_OutlineColor ("Outline Color", Color) = (0,0,0,1)
		_OutlineAlpha ("Outline Alpha", Range(0, 1)) = 0
		_Outline ("Outline width", Range (0, 1)) = .1
		_MainTex ("Base (RGB)", 2D) = "white" { }
	}
	
	////////////////////////////////////////////////////////////////////////////////////////////
	// Standard Shader
	CGINCLUDE
	#include "UnityCG.cginc"
 
	struct appdata {
		float4 vertex : POSITION;
		float3 normal : NORMAL;
	};
 
	struct v2f {
		float4 pos : POSITION;
		float4 color : COLOR;
	};
 
	uniform float _Outline;
	uniform float4 _OutlineColor;
	uniform float _OutlineAlpha;

	v2f vert(appdata v) {
		// just make a copy of incoming vertex data but scaled according to normal direction
		v2f o;

		v.vertex *= ( 1 + _Outline);

		o.pos = UnityObjectToClipPos(v.vertex);
 		o.color = float4(_OutlineColor.rgb, _OutlineAlpha);
		return o;
	}
	ENDCG
	
	////////////////////////////////////////////////////////////////////////////////////////////
	// Outline Shader
	SubShader {
		CGPROGRAM
		#pragma surface surf Lambert
 
		sampler2D _MainTex;
		fixed4 _Color;
 
		struct Input {
			float2 uv_MainTex;
		};
 
		void surf (Input IN, inout SurfaceOutput o) {
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
		
		// note that a vertex shader is specified here but its using the one above
		Pass {
			Name "OUTLINE"
			Tags { "LightMode" = "Always" }
			Cull Front
			ZWrite On
			ColorMask RGB
			Blend SrcAlpha OneMinusSrcAlpha
 
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			half4 frag(v2f i) :COLOR { return i.color; }
			ENDCG
		}
	}

	////////////////////////////////////////////////////////////////////////////////////////////
	// Combine Shader
	SubShader {
		CGPROGRAM
		#pragma surface surf Lambert
 
		sampler2D _MainTex;
		fixed4 _Color;
 
		struct Input {
			float2 uv_MainTex;
		};
 
		void surf (Input IN, inout SurfaceOutput o) {
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
 
		Pass {
			Name "OUTLINE"
			Tags { "LightMode" = "Always" }
			Cull Front
			ZWrite On
			ColorMask RGB
			Blend SrcAlpha OneMinusSrcAlpha
 
			CGPROGRAM
			#pragma vertex vert
			#pragma exclude_renderers gles xbox360 ps3
			ENDCG
			SetTexture [_MainTex] { combine primary }
		}
	}
 
	Fallback "Diffuse"
}