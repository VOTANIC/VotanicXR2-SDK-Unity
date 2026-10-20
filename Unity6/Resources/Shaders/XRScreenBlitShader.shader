Shader "Hidden/Blit/XRScreenBlitShader"
{
    Properties
    {
        _ColorTex("Blit Texture", 2D) = "white" {}
        _ObjectSpacePosTex ("ObjectSpacePosition", 2D) = "white" {}
    }

    HLSLINCLUDE
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
        // #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/SpaceTransforms.hlsl"
        #include "Packages/com.unity.render-pipelines.core/Runtime/Utilities/Blit.hlsl"

        sampler2D _ColorTex;
        sampler2D _ObjectSpacePosTex;
        float4x4 _ObjectToScreenMatrix;

        float4 frag (Varyings input) : SV_Target
        {
            // UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);
            float4 objPos = tex2D(_ObjectSpacePosTex, input.texcoord);

            float4 clipPos = mul(_ObjectToScreenMatrix, objPos);
            clipPos = clipPos / clipPos.w / 2 + 0.5;
            
            return tex2D(_ColorTex, clipPos.xy);
        }
    ENDHLSL

    SubShader
    {
        // No culling or depth
        Cull Off
        ZWrite Off
        ZTest Always

        Pass
        {
            Name "XRScreenBlitShader"

            Tags { "RenderPipeline" = "UniversalPipeline" }

            HLSLPROGRAM

            #pragma vertex Vert
            #pragma fragment frag

            ENDHLSL
        }
    }
}
