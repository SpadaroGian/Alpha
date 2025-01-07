Shader "Unlit/fog"
{
    Properties
    {
        
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "RenderPipeline" = "UniversalPipeline"}

        Pass
        {
            HLSLPROGRAM
            #pragma vertex Vert
            #pragma fragment frag

            #pragma multi_compile_fog

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.core/Runtime/Utilities/Blit.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DeclareDepthTexture.hlsl"

            half4 frag(Varyings IN) : SV_Target
            {
                return 1.0 - SAMPLE_TEXTURE2D(_BlitTexture, sampler_LinearClamp, IN.texcoord);
            }
            ENDHLSL
        }
    }
}
