Shader "Unlit/InstantiateSample"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _FadeAmount ("Fade Amount", Range(0, 1)) = 0.5
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 200

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            sampler2D _MainTex;
            float _FadeAmount;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            half4 frag (v2f i) : SV_Target
            {
              // Adjust UVs to range from 0 to 1
                float2 uv = i.uv;

                // Use the Y coordinate for vertical fade
                float distance = 1-uv.y;

                // Apply the fade effect based on the distance
                float edge = smoothstep(_FadeAmount - 0.01, _FadeAmount, distance);

                half4 col = tex2D(_MainTex, i.uv);
                col.a *= 1.0 - edge; // Invert the edge to fade from bottom to top
                return col;
            }
            ENDCG
        }
    }
}
