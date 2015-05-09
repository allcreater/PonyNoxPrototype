Shader "Custom/ForceFieldShader" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Cube ("Cubemap", CUBE) = "" {}
	}
	SubShader {
		Tags { "RenderType"="Transparent" "Queue" = "Transparent" "ForceNoShadowCasting" = "True"}
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows alpha:auto 
		//

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		samplerCUBE _Cube;
		
		struct Input
		{
			float3 viewDir;
			float2 uv_MainTex;
			float3 worldNormal;
			float3 worldRefl;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

		void surf (Input IN, inout SurfaceOutputStandard o)
		{
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) + _Color;
			o.Albedo = c.rgb;
			
			float p = pow(1.0 - saturate(dot(normalize(IN.viewDir), IN.worldNormal)), 2.0f);
			o.Emission = texCUBE (_Cube, IN.worldRefl).rgb * p * 0.3;
			o.Alpha = p;//c.a;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
