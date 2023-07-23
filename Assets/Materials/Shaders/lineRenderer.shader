Shader "Custom/lineRenderer"
{
    Properties {
        _MainColor("Main Color", Color) = (1,1,1,1)
        _GlowColor("Glow Color", Color) = (0,1,0,1)
        _GlowIntensity("Glow Intensity", Range(0, 1)) = 0.5
        _LineTexture("Line Texture", 2D) = "white" {}
    }
 
    SubShader {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 200
 
        CGPROGRAM
        #pragma surface surf Lambert alpha
 
        sampler2D _LineTexture;
        fixed4 _MainColor;
        fixed4 _GlowColor;
        half _GlowIntensity;
 
        struct Input {
            float2 uv_MainTex;
        };
 
        void surf (Input IN, inout SurfaceOutput o) {
            fixed4 mainColor = tex2D(_LineTexture, IN.uv_MainTex) * _MainColor;
            o.Albedo = mainColor.rgb;
            o.Alpha = mainColor.a;
            o.Emission = _GlowColor.rgb * _GlowIntensity;
        }
 
        ENDCG
    }
 
    FallBack "Diffuse"
}
