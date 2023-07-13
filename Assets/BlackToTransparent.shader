Shader "Custom/BlackToTransparentSprite"
{
    Properties
    {
        _MainTex ("Sprite Texture", 2D) = "white" {}
    }
    
    SubShader
    {
        Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        Lighting Off
        ZWrite Off
        
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            
            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };
            
            struct v2f_t
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };
            
            sampler2D _MainTex;
            
            v2f_t vert(appdata_t v)
            {
                v2f_t o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }
            
            fixed4 frag(v2f_t i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);

                if((col.r + col.g + col.b)/3.0 <= 0.1){
                    discard;
                }
                
                return col;
            }
            
            ENDCG
        }
    }
}
