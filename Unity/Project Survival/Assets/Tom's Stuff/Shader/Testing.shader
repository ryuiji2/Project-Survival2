Shader "Custom/Testing" {
	Properties {
		_TexOne ("Tex One (RGB)", 2D) = "white" {}
		_SliderOne ("Test Slider", range(0,1)) = 1
		_TexTwo ("Tex Two (RGB)", 2D) = "white" {}
		_SliderTwo ("Test Slider", range(0,1)) = 0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM
		
		#pragma surface surf Standard fullforwardshadows

		#pragma target 3.0

		sampler2D _TexOne;
		sampler2D _TexTwo;

		struct Input {
			float2 uv_TexOne;
			float2 uv_TexTwo;
		};

		half _SliderOne;
		half _SliderTwo;

		void surf (Input IN, inout SurfaceOutputStandard o) {
			

			fixed4 d1 = tex2D (_TexOne, IN.uv_TexOne) * _SliderOne;
			fixed4 d2 = tex2D (_TexTwo, IN.uv_TexTwo) * _SliderTwo;
			
			o.Albedo = d1.rgb + d2.rgb;
			o.Metallic = 0;
			o.Smoothness = 0;
			o.Alpha = 1;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
