﻿// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Cel Shader"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_Treshold("Cel treshold", Range(1., 20.)) = 5.
		_Ambient("Ambient intensity", Range(0., 0.5)) = 0.1

		[Header(Outline)]
		_OutlineVal("Outline value", Range(0., 5.)) = 1.
		_OutlineCol("Outline color", color) = (1., 1., 1., 1.)
		[Header(Texture)]
		_MainTex("Texture", 2D) = "white" {}
		_Zoom("Zoom", Range(0.5, 20)) = 1
		_SpeedX("Speed along X", Range(-1, 1)) = 0
		_SpeedY("Speed along Y", Range(-1, 1)) = 0
	}
		SubShader
		{
			Tags { "RenderType" = "Opaque" "LightMode" = "ForwardBase" }

			Pass
			{
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag

				#include "UnityCG.cginc"

				struct v2f
				{
					float4 pos : SV_POSITION;
					float2 uv : TEXCOORD0;
					float3 worldNormal : NORMAL;
				};

				float _Treshold;

				float LightToonShading(float3 normal, float3 lightDir)
				{
					float NdotL = max(0.0, dot(normalize(normal), normalize(lightDir)));
					return floor(NdotL * _Treshold) / (_Treshold - 0.5);
				}

				sampler2D _MainTex;
				float4 _MainTex_ST;

				v2f vert(appdata_full v)
				{
					v2f o;
					o.pos = UnityObjectToClipPos(v.vertex);
					o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
					o.worldNormal = mul(v.normal.xyz, (float3x3) unity_WorldToObject);
					return o;
				}

				fixed4 _LightColor0;
				half _Ambient;

				fixed4 frag(v2f i) : SV_Target
				{
					fixed4 col = tex2D(_MainTex, i.uv);
					col.rgb *= saturate(LightToonShading(i.worldNormal, _WorldSpaceLightPos0.xyz) + _Ambient) * _LightColor0.rgb;
					return col;
				}
				ENDCG
			}

			Tags { "Queue" = "Geometry" "RenderType" = "Opaque" }

			Pass
			{
				Cull Front

				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#include "UnityCG.cginc"

				struct v2f {
					float4 pos : SV_POSITION;
				};

				float _OutlineVal;

				v2f vert(appdata_base v) {
					v2f o;

					// Convert vertex to clip space
					o.pos = UnityObjectToClipPos(v.vertex);

					// Convert normal to view space (camera space)
					float3 normal = mul((float3x3) UNITY_MATRIX_IT_MV, v.normal);

					// Compute normal value in clip space
					normal.x *= UNITY_MATRIX_P[0][0];
					normal.y *= UNITY_MATRIX_P[1][1];

					// Scale the model depending the previous computed normal and outline value
					o.pos.xy += _OutlineVal * normal.xy;
					return o;
				}

				fixed4 _OutlineCol;

				fixed4 frag(v2f i) : SV_Target {
					return _OutlineCol;
				}

				ENDCG
			}

			/*Pass
			{
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#include "UnityCG.cginc"

				float4 vert(appdata_base v) : SV_POSITION
				{
					return UnityObjectToClipPos(v.vertex);
				}

				sampler2D _MainTex;
				float _Zoom;
				float _SpeedX;
				float _SpeedY;

				fixed4 frag(float4 i : VPOS) : SV_Target
				{
					// Screen space texture
					return tex2D(_MainTex, ((i.xy / _ScreenParams.xy) + float2(_Time.y * _SpeedX, _Time.y * _SpeedY)) / _Zoom);
				}

				ENDCG
			}*/
		}
}