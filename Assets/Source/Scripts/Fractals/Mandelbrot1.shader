Shader "Unlit/Mandelbrot1"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Mod ("Modifier", Range(1,255)) = 127.0
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100
		Cull off
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag 
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};


			sampler2D _MainTex;
			float4 _MainTex_ST;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			int _Mod;
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				float2 c = (i.uv - 01.f);
				float2 z;
				float iter;
				for (iter = 0; iter < (int)_Mod; iter++) {
					z = float2(z.x*z.x - z.y*z.y, 2 * z.x*z.y) + c;
					if (length(z) > 2)
						break;
				}
				return iter / (int)_Mod;
			}
			ENDCG
		}
	}
}
