Shader "Custom/Simulation" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Normal ("Normal Map", 2D) = "bump" {}
		
		_Simulation ("Simulation Texture", 2D) = "white" {}
		_SimMask ("Simulation Mask", 2D) = "black" {}
		_SSX ("Scroll Speed X", float) = -1.0
		_SSY ("Scroll Speed Y", float) = 0.5
		
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM

		#pragma surface surf Standard fullforwardshadows

		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _Normal;
		sampler2D _Simulation;
		sampler2D _SimMask;

		struct Input {
			float2 uv_MainTex;
			float2 uv_Normal;
			float2 uv_Simulation;
			float2 uv_SimMask;
		};

		fixed4 _Color;
		float _SSX;
		float _SSY;

		void surf (Input IN, inout SurfaceOutputStandard o) {
			fixed2 scrollingUV = IN.uv_Simulation;
			fixed SSSX = _SSX * _Time;
			fixed SSSY = _SSY * _Time;
			scrollingUV += fixed2(SSSX, SSSY);

			
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			fixed4 s = tex2D (_Simulation, scrollingUV) * tex2D (_SimMask, IN.uv_SimMask);
			o.Albedo = c.rgb + s.rgb;
			o.Emission = s.rgb;
			
			o.Metallic = 0;
			o.Smoothness = 0;
			o.Normal = UnpackNormal(tex2D(_Normal, IN.uv_Normal));
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
