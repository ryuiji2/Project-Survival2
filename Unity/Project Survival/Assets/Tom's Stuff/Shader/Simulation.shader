Shader "Custom/Simulation" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Normal ("Normal Map", 2D) = "bump" {}
		[Toggle] _Alpha ("Alpha toggle" , float) = 0
		
		_TexOne ("Simulation Mask One", 2D) = "white" {}
		_SliderOne ("Slider Mask One", range(0,1)) = 1
		_TexTwo ("Simulation Mask Two", 2D) = "white" {}
		_SliderTwo ("Slider Mask Two", range(0,1)) = 0

		_Simulation ("Simulation Texture", 2D) = "white" {}
		_SSX ("Scroll Speed X", float) = -1.0
		_SSY ("Scroll Speed Y", float) = 0.5
		
	}
	SubShader {
		Tags{ "Queue" = "Transparent" "RenderType" = "Transparent" }
		
		LOD 200

		CGPROGRAM

		#pragma surface surf Standard fullforwardshadows alpha
		#pragma multi_compile ALPHA_ON ALPHA_OFF

		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _Normal;
		sampler2D _Simulation;
		sampler2D _TexOne;
		sampler2D _TexTwo;

		struct Input {
			float2 uv_MainTex;
			float2 uv_Normal;
			float2 uv_Simulation;
			float2 uv_TexOne;
			float2 uv_TexTwo;
		};

		fixed4 _Color;
		float _Alpha;
		float _SSX;
		float _SSY;

		half _SliderOne;
		half _SliderTwo;

		void surf (Input IN, inout SurfaceOutputStandard o) {
			fixed2 scrollingUV = IN.uv_Simulation;
			fixed SSSX = _SSX * _Time;
			fixed SSSY = _SSY * _Time;
			scrollingUV += fixed2(SSSX, SSSY);

			
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			fixed4 m1 = tex2D (_TexOne, IN.uv_TexOne) * _SliderOne;
			fixed4 m2 = tex2D (_TexTwo, IN.uv_TexTwo) * _SliderTwo;
			fixed4 s = tex2D (_Simulation, scrollingUV) * (m1 + m2);
			o.Albedo = c.rgb + s.rgb;
			o.Emission = s.rgb;
			
			o.Metallic = 0;
			o.Smoothness = 0;
			o.Normal = UnpackNormal(tex2D(_Normal, IN.uv_Normal));
			o.Alpha = !_Alpha;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
