Shader "Unlit/squares"
{
	Properties
	{
		_Density("Density", Range(2,50)) = 30
		_OFFSET("Vert Offset",Float) = 10

		/*R("_Red", Float) = 1
		G("_Green", Float) = 1
		B("_Blue", Float) = 0*/
	}
		SubShader
	{
		//Offset[_OFFSET],[_OFFSET]

		//Color_ [_COLOR], [_COLOR]

		Pass
		{
			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
				//float4 color : COLOR;
			};

			float _Density;
			float _OFFSET;
			/*float R;
			float G;
			float B;*/

			v2f vert(float4 pos : POSITION, float2 uv : TEXCOORD0)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(pos);
				o.uv = uv * _Density;
				o.uv[1] += _OFFSET;
				//o.color = float4(R, G, B, 1);
				return o;
			}

			fixed4 frag(v2f i) : COLOR
			{
				float2 c = i.uv;
				c = floor(c) / 2;
				fixed4 checker = frac(c.x + c.y) * 2; /** i.color*//*half4(R,G,B,1)*/
				return checker;
			}
			ENDCG
		}
	}
}