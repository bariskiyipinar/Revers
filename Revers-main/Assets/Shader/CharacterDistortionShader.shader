Shader"Custom/DistortionShader"
{
    Properties
    {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _DistortionStrength ("Distortion Strength", Range(0, 1)) = 0.5
        _DistortionSpeed ("Distortion Speed", Range(0, 10)) = 1
        _TimeFactor ("Time Factor", Range(0, 10)) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

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
};

struct v2f
{
    float2 uv : TEXCOORD0;
    float4 vertex : SV_POSITION;
};

float _DistortionStrength;
float _DistortionSpeed;
float _TimeFactor;
sampler2D _MainTex;

v2f vert(appdata v)
{
    v2f o;
    o.vertex = UnityObjectToClipPos(v.vertex);

    // Apply distortion effect based on sine wave
    float time = _Time.y * _DistortionSpeed * _TimeFactor; // Time-based distortion
    o.uv = v.uv + float2(sin(time) * _DistortionStrength, 0); // Distort UVs in X direction

    return o;
}

half4 frag(v2f i) : SV_Target
{
    half4 col = tex2D(_MainTex, i.uv);
    return col;
}
            ENDCG
        }
    }

Fallback"Diffuse"
}
