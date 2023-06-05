Shader "Custom/LavaFlow"
{
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
        _Speed ("Speed", Range(0, 10)) = 1
        _Distortion ("Distortion", Range(0, 1)) = 0.1
        _DistortionSpeed ("Distortion Speed", Range(0, 10)) = 1
        _DistortionScale ("Distortion Scale", Range(0, 10)) = 5
        _DistortionStrength ("Distortion Strength", Range(0, 10)) = 1
        _Emission ("Emission", Range(0, 1)) = 1
        _Color ("Color", Color) = (1, 1, 1, 1)
    }

    SubShader {
        Tags {"RenderType"="Opaque"}
        LOD 200

        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fog

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
            float _Speed;
            float _Distortion;
            float _DistortionSpeed;
            float _DistortionScale;
            float _DistortionStrength;
            float _Emission;
            float4 _Color;

            v2f vert (appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float4 SampleDistortion(float2 uv) {
                float4 noise = frac(sin(dot(uv, float2(12.9898, 78.233))) * 43758.5453);
                noise = noise * 2.0 - 1.0;
                noise.x *= _DistortionScale;
                noise.y *= _DistortionScale;
                return noise;
            }

            float4 SampleLava(float2 uv) {
                float2 lavaUV = uv;
                lavaUV.x += _Speed * _Time.y * 0.1;
                lavaUV.y += _Speed * _Time.y * 0.05;
                lavaUV += SampleDistortion(uv).xy * _Distortion * _DistortionStrength;
                float4 sample = tex2D(_MainTex, lavaUV);
                sample.rgb *= _Color.rgb;
                sample.a = _Color.a;
                sample.rgb += _Emission;
                return sample;
            }

            float4 frag (v2f i) : SV_Target {
                float4 sample = SampleLava(i.uv);
                return sample;
            }
            ENDCG
        }
    }

    FallBack "Diffuse"
}


