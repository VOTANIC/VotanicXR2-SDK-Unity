Shader "VXR/ProximityWarning"
{
    Properties
    {
        _Color("Color", Color) = (0, 0, 0, 1)
        _MainTex("Main Texture", 2D) = "white" {}
        _WarningDistance("Warning Distance", float) = 0.6
        _DistanceRamp("Distance Ramp", float) = 0.2
        _EffectStrength("Effect Strength", float) = 4
        _AlphaRangeMin("Alpha Range Min", Range(0, 1)) = 0.5
        _AlphaRangeMax("Alpha Range Max", Range(0, 1)) = 1
    }
    SubShader
    {
        Tags
        {
            "RenderType"="Transparent"
            "Queue"="Transparent"
        }
        LOD 100
        
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        ZTest Off

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
                // float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
                float4 cave_position : POSITION1;
            };

            float4 _Color;
            float4x4 _ReferenceOriginWorldToLocal;
            float _WarningDistance;
            float _DistanceRamp;
            float _EffectStrength;
            float _AlphaRangeMin;
            float _AlphaRangeMax;

            int _TrackedObjectsCount;
            float4 _TrackedObjects[64];

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                // o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                o.cave_position = mul(_ReferenceOriginWorldToLocal, mul(unity_ObjectToWorld, v.vertex));
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float dist = _WarningDistance;
                for (int index = 0; index < _TrackedObjectsCount; index++)
                {
                    dist = min(dist, distance(_TrackedObjects[index], i.cave_position));
                }
                if (dist == _WarningDistance) discard;

                dist = (_WarningDistance - dist) / _DistanceRamp;
                dist = pow(dist, 1 / _EffectStrength);
                dist = smoothstep(_AlphaRangeMin, _AlphaRangeMax, dist);
                // return fixed4(dist.xxx, 1);
                fixed4 col = fixed4(_Color.xyz, dist);
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
