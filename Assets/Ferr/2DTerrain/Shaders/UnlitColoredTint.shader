// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Ferr/Unlit Tinted Textured Vertex Color" {
	Properties {
		_MainTex("Texture (RGB)", 2D   ) = "white" {}
		_Color  ("Tint",          Color) = (1,1,1,1)
	}
	SubShader {
		Tags {"IgnoreProjector"="True" "RenderType"="Opaque"}
		Blend Off
		
		LOD 100
		Cull      Off
		Lighting  Off
		Fog {Mode Off}
		
		
		Pass {
			CGPROGRAM
			#pragma vertex         vert
			#pragma fragment       frag
			#pragma fragmentoption ARB_precision_hint_fastest
			#include "UnityCG.cginc"

			sampler2D _MainTex;
			float4    _MainTex_ST;
			fixed4    _Color;

			struct appdata_ferr {
			    float4 vertex   : POSITION;
			    float4 texcoord : TEXCOORD0;
			    fixed4 color    : COLOR;
			};
			struct VS_OUT {
				float4 position : SV_POSITION;
				fixed4 color    : COLOR;
				float2 uv       : TEXCOORD0;
			};

			VS_OUT vert (appdata_ferr input) {
				VS_OUT result;
				result.position = UnityObjectToClipPos (input.vertex);
				result.uv       = TRANSFORM_TEX (input.texcoord, _MainTex);
				result.color    = input.color;

				return result;
			}

			fixed4 frag (VS_OUT input) : COLOR {
				fixed4 color = tex2D(_MainTex, input.uv);
				return (input.color * _Color) * color;
			}
			ENDCG
		}
	}
}
