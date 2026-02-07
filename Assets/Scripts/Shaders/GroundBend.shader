Shader "Custom/GroundBend"
{
    Properties
    {
        _BaseColor ("Base Color", Color) = (0.5, 0.8, 0.5, 1)
        _BendStrength ("Bend Strength", Range(-1, 1)) = 0
        _BendStartZ ("Bend Start Z", Float) = 0
    }

    SubShader
    {
        Tags
        {
            "RenderPipeline"="UniversalPipeline"
            "RenderType"="Opaque"
            "Queue"="Geometry"
        }

        Pass
        {
            Name "UniversalForward"
            Tags { "LightMode"="UniversalForward" }

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 2.0

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            CBUFFER_START(UnityPerMaterial)
                float4 _BaseColor;
                float _BendStrength;
                float _BendStartZ;
            CBUFFER_END

            struct Attributes
            {
                float3 positionOS : POSITION;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
            };

            Varyings vert (Attributes v)
            {
                Varyings o;

                float3 pos = v.positionOS;

                float z = max(0, pos.z - _BendStartZ);
                pos.y += z * z * _BendStrength;

                o.positionHCS = TransformObjectToHClip(pos);
                return o;
            }

            half4 frag (Varyings i) : SV_Target
            {
                return _BaseColor;
            }
            ENDHLSL
        }
    }
}
