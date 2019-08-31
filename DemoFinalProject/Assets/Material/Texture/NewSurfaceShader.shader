Shader "DM/Ripple Shader" {
    Properties {

       _horizonColor ("Horizon color", COLOR)  = ( .172 , .463 , .435 , 0)
    _WaveScale ("Wave scale", Range (0.02,0.15)) = .07
    [NoScaleOffset] _ColorControl ("Reflective color (RGB) fresnel (A) ", 2D) = "" { }
    [NoScaleOffset] _BumpMap ("Waves Normalmap ", 2D) = "" { }
    WaveSpeed ("Wave speed (map1 x,y; map2 x,y)", Vector) = (19,9,-16,-7)


        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Texture", 2D) = "white" {}
        _Scale ("Scale", Range(0.5,500.0)) = 3.0
        _Speed ("Speed", Range(-50,50.0)) = 1.0
    }

    CGINCLUDE

#include "UnityCG.cginc"

uniform float4 _horizonColor;

uniform float4 WaveSpeed;
uniform float _WaveScale;
uniform float4 _WaveOffset;

struct appdata {
    float4 vertex : POSITION;
    float3 normal : NORMAL;
};

struct v2f {
    float4 pos : SV_POSITION;
    float2 bumpuv[2] : TEXCOORD0;
    float3 viewDir : TEXCOORD2;
    UNITY_FOG_COORDS(3)
};

v2f vert(appdata v)
{
    v2f o;
    float4 s;

    o.pos = UnityObjectToClipPos(v.vertex);

    // scroll bump waves
    float4 temp;
    float4 wpos = mul (unity_ObjectToWorld, v.vertex);
    temp.xyzw = wpos.xzxz * _WaveScale + _WaveOffset;
    o.bumpuv[0] = temp.xy * float2(.4, .45);
    o.bumpuv[1] = temp.wz;

    // object space view direction
    o.viewDir.xzy = normalize( WorldSpaceViewDir(v.vertex) );

    UNITY_TRANSFER_FOG(o,o.pos);
    return o;
}

ENDCG


    SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 200
        Cull Off
        CGPROGRAM
        #pragma surface surf Lambert
        #include "UnityCG.cginc"
        
        half4 _Color;
        half _Scale;
        half _Speed;
        sampler2D _MainTex;
        
        struct Input {
            float2 uv_MainTex;
        };

        void surf (Input IN, inout SurfaceOutput o) {
            half2 uv = (IN.uv_MainTex - 0.5) * _Scale;
            half r = sqrt (uv.x*uv.x + uv.y*uv.y);
            half z = sin (r+_Time[1]*_Speed) / r;
            o.Albedo = _Color.rgb * tex2D (_MainTex, IN.uv_MainTex+z).rgb;
            o.Alpha = _Color.a;
            o.Normal = (z, z, z);
        }
        ENDCG

        Pass {

CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#pragma multi_compile_fog

sampler2D _BumpMap;
sampler2D _ColorControl;

half4 frag( v2f i ) : COLOR
{
    half3 bump1 = UnpackNormal(tex2D( _BumpMap, i.bumpuv[0] )).rgb;
    half3 bump2 = UnpackNormal(tex2D( _BumpMap, i.bumpuv[1] )).rgb;
    half3 bump = (bump1 + bump2) * 0.5;
    
    half fresnel = dot( i.viewDir, bump );
    half4 water = tex2D( _ColorControl, float2(fresnel,fresnel) );
    
    half4 col;
    col.rgb = lerp( water.rgb, _horizonColor.rgb, water.a );
    col.a = _horizonColor.a;

    UNITY_APPLY_FOG(i.fogCoord, col);
    return col;
}





ENDCG
    }

    } 
    FallBack "Diffuse"
}