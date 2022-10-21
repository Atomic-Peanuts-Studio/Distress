Shader "Unlit/FadeCloneShader"
{
    Properties
    {
        _Color ("Tint", Color) = (0, 0, 0, 1)
        _MainTex ("Texture", 2D) = "white" {}
        _AnimateXY("Animate X Y", Vector) = (0,0,0,0)
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }

        Pass
        {
            Cull Off
            ZWrite Off
            ZTest Off
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct MeshData
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                fixed4 color : COLOR;
            };

            struct Interpolators
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                fixed4 color : COLOR;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _AnimateXY;

            Interpolators vert (MeshData v)
            {
                Interpolators o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.color = v.color;

                //For animating sprite texture
                o.uv += frac(_AnimateXY.xy * _Time.yy);
				return o;
            }

            fixed4 frag (Interpolators i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);

                //Glitchy effect
                i.uv.x = abs(frac(i.uv.y + _Time.y * 1) *2 -1);

                //Mask
                float2 newUV = i.uv * 2 - 1;
                float circle = length(newUV);
                //Hard-coded radius, no feathering
                float mask = 1 - smoothstep(1 , 1 , circle);

                //Add picker color
				col *= i.color;

                //Transparency towards the bottom
				col *= i.uv.y;

                return col * mask;
            }
            ENDCG
        }
    }
}
