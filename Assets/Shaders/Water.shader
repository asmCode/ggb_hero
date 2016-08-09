// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Unlit/Water"
{
	Properties
	{
		_ColorTop ("Color Top", Color) = (1, 1, 1, 1)
		_ColorBottom ("Color Bottom", Color) = (1, 1, 1, 1)
	}
		SubShader
		{
			///Tags{ "Queue" = "Transparent" "RenderType" = "Transparent" }
			Tags{ "RenderType" = "Opaque" }
			LOD 100
		//Blend SrcAlpha OneMinusSrcAlpha

			Pass
			{
				//Blend SrcAlpha OneMinusSrcAlpha
				//Tags{ "Queue" = "Transparent" "RenderType" = "Transparent" }
				CGPROGRAM
		//Blend SrcAlpha OneMinusSrcAlpha
	//Tags{ "Queue" = "Transparent" "RenderType" = "Transparent" }
				#pragma vertex vert
				#pragma fragment frag

				#include "UnityCG.cginc"

				static const int SegmentsCount = 512;

			struct appdata
			{
				float4 position : POSITION;
			};

			struct v2f
			{
				float4 position_mvp: SV_POSITION;
				float4 position_base: TEXCOORD0;
			};

			half4 _ColorTop;
			half4 _ColorBottom;
			float _Heights[SegmentsCount];

			v2f vert(appdata v)
			{
				v2f o;
				o.position_mvp = mul(UNITY_MATRIX_MVP, v.position);
				//o.position_base = mul(unity_ObjectToWorld, v.position);
				o.position_base = v.position;
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				float normalized_x_coord = i.position_base * 0.5 + 0.5;
				int index = int(clamp(normalized_x_coord * float(SegmentsCount), 0, SegmentsCount));
				float height = _Heights[index];

				fixed4 col = fixed4(0.2, 0.2, 1, 1);
				if (i.position_base.y > height)
					discard;
				if (i.position_base.y > height - 0.03)
					col = fixed4(0.5, 0.5, 0.9, 0.8f);
				else
				{
					float color_lerp = (height - i.position_base.y) / 2;
					color_lerp *= 10.0;
					color_lerp = int(color_lerp) / 10.0;
					col = lerp(_ColorTop, _ColorBottom, color_lerp);
				}
				return col;
			}
			ENDCG
		}
	}
}
