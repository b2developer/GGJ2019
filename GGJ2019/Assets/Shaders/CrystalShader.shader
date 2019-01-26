// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Pseudo Refraction"
{
	Properties
	{
		_Color("Tint", Color) = (1, 1, 1, 1)
		_Alpha ("Alpha", Range(0, 1)) = 1
		_Fresnel("Fresnel Coefficient", float) = 5.0
		_Reflectance("Reflectance", float) = 1.0
		_Refraction("Refraction", float) = 0.1
	}
	SubShader
	{
		Tags{ "RenderType" = "Transparent" "Queue" = "Transparent" }

		GrabPass{ "_GrabTexture" }


		CGPROGRAM
		#pragma surface surf PseudoRefraction vertex:vert

		half4 LightingPseudoRefraction(SurfaceOutput s, half3 lightDir, half atten) {
			half4 c;
			c.rgb = s.Emission;
			c.a = s.Alpha;
			return c;
		}

		struct Input {
			half3 viewDir;
			half3 worldNormal;
			half4 pos : POSITION;
			half4 grabPos : TEXCORRD0;
		};

		sampler2D _GrabTexture;
		half _Fresnel;
		half _Reflectance;
		half _Refraction;
		float _Alpha;
		half4 _Color;

		void vert(inout appdata_full v,out Input o){
			UNITY_INITIALIZE_OUTPUT(Input,o);
			o.pos = UnityObjectToClipPos(v.vertex);
			o.grabPos = ComputeGrabScreenPos(o.pos);
		}

		void surf(Input IN, inout SurfaceOutput o) {
			half3 n = normalize(IN.worldNormal);
			half3 v = normalize(IN.viewDir);
			half fr = pow(1.0 - dot(v, n), _Fresnel) * _Refraction;
			half nfr = pow(dot(v,n),_Fresnel) * _Refraction;

			half3 reflectDir = reflect(-v, n);
			half3 reflectColor = tex2D(_GrabTexture,reflectDir).rgb;

			IN.grabPos -= half4(reflectDir, 0) * _Refraction;
			fixed4 col = tex2Dproj(_GrabTexture,UNITY_PROJ_COORD(IN.grabPos));

			o.Emission = (col * _Color) * (_Alpha + nfr) + fr;
			o.Albedo = _Color;
		}



		ENDCG
	}
		FallBack Off
}