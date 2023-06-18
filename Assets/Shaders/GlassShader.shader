Shader "Custom/GlassShader" {
    Properties {
        _Color ("Color", Color) = (1,1,1,1)
        _Glossiness ("Glossiness", Range(0, 1)) = 0.5
        _Refraction ("Refraction", Range(0, 1)) = 0.5
        _Opacity ("Opacity", Range(0, 1)) = 0.5
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
    }

    SubShader {
        Tags {"Queue"="Transparent" "RenderType"="Opaque"}

        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _Color;
            float _Glossiness;
            float _Refraction;
            float _Opacity;

            v2f vert (appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float4 frag (v2f i) : COLOR {
                float4 col = tex2D(_MainTex, i.uv);
                col.rgb *= _Color.rgb;
                col.a *= _Opacity;
                col.a *= pow(1.0 / length(1.0 - _Refraction * (col.a - 1.0)), 1.5 * _Glossiness);
                return col;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
