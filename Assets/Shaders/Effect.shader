Shader "Custom/Effect"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (0, 0, 0, 1)
        _Radius("Radius", Range(0,1)) = 0
        _Feather("Feather", Range(0,1)) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "HLSLSupport.cginc"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct meshdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct interpolators
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Color;
            float4 _AnimateXY;
            float _Radius;
            float _Feather;

            interpolators vert (meshdata v)
            {
                interpolators o;
                o.vertex = TransformObjectToHClip(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (interpolators i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);

                //Vignette Effect
                float2 centered = i.uv * 2 - 1;
                float circle = length(centered);
                float mask = 1 - smoothstep(_Radius , _Radius + _Feather , circle *abs(_CosTime.w));
                float invertMask = 1 - mask;

                float3 displayColor = col.rgb * mask;
                float3 vignetteColor = col.rgb * invertMask * _Color;

                return fixed4(displayColor + vignetteColor , 1);
            }
            ENDHLSL
        }
    }
    Fallback "Unlit/Color"
}
