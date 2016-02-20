Shader "Unlit/circle"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_CircleRatio ("CircleRation", Float) = 0.0
	}
	SubShader
	{
		Tags { "RenderType" = "Transparent" "Queue"="Transparent" }
		Blend SrcAlpha OneMinusSrcAlpha

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

			sampler2D _MainTex;
			float _CircleRatio;

			// 画面サイズからの今の位置必要
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = v.uv;

				return o;
			}
			
			float4 frag (v2f i) : SV_Target
			{
				float2 center = fixed2(0.5, 0.5);

				// カラーと比較したいのでintで
				int isCircle = step(length(i.uv - center), 0.5);

				float4 col = tex2D(_MainTex, i.uv);
				col = col * isCircle;

				if (i.vertex.y < _CircleRatio)
				{
					col.a = 0.0;
				}

				return col;
			}
			ENDCG
		}
	}
}
