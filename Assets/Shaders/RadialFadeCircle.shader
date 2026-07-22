Shader "Custom/RadialFadeCircle"
{
    Properties
    {
        _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
        _InnerRadius ("Inner Radius (fully opaque)", Range(0,0.5)) = 0.2
        _OuterRadius ("Outer Radius (fully transparent)", Range(0,0.5)) = 0.5
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" "PreviewType"="Plane" "CanUseSpriteAtlas"="True" }
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

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                fixed4 color : COLOR;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                fixed4 color : COLOR;
            };

            sampler2D _MainTex;
            fixed4 _Color;
            float _InnerRadius;
            float _OuterRadius;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.color = v.color; // SpriteRenderer's own tint/alpha (e.g. from Color property or fade tweens)
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 tex = tex2D(_MainTex, i.uv);

                // Distance from center of the UV square (0.5, 0.5), 0 = center, ~0.707 = corner
                float dist = distance(i.uv, float2(0.5, 0.5));

                // smoothstep gives a soft, non-linear fade between inner and outer radius
                float fade = 1.0 - smoothstep(_InnerRadius, _OuterRadius, dist);

                fixed4 col = tex * _Color * i.color;
                col.a *= fade;
                return col;
            }
            ENDCG
        }
    }
}
