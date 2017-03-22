Shader "Custom/DayAndNightTransition"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_NightColor("night color",COLOR) = (0.2,0.2,0.2,1)
		_boolDay("bool_isDay",Int) = 0
		_depthInfluence("depth influence factor",Range(0,10)) = 0.5
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			sampler2D _MainTex;
			float4 _NightColor;
			float _depthInfluence;

            uniform sampler2D _CameraDepthTexture;

			float4 frag (v2f i) : SV_Target
			{
				float4 col = tex2D(_MainTex, i.uv);

				float2 tmp = float2(i.uv.x,1-i.uv.y);
				float depth = UNITY_SAMPLE_DEPTH(tex2D(_CameraDepthTexture, tmp));
				//return 1-depth;
				col *= _NightColor*((1-depth)*_depthInfluence);
				return col;
			}
			ENDCG
		}
	}
}
