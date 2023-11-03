Shader "Custom/Cage_Shader"
{
    Properties
    {
        _LeftTex ("Left texture", 2D) = "white" {}
        _RightTex ("Right texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100
 
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
                float2 uv1 : TEXCOORD1;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };
 
            sampler2D _LeftTex;
            float4 _LeftTex_ST;
 
            sampler2D _RightTex;
            float4 _RightTex_ST;
                   
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _LeftTex);
                o.uv1 = TRANSFORM_TEX(v.uv, _RightTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }
       
            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col;
                fixed4 l = tex2D(_LeftTex, i.uv);
                fixed4 r = tex2D(_RightTex, i.uv1);
 
                col = r * unity_StereoEyeIndex + l * (1 - unity_StereoEyeIndex);
 
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}