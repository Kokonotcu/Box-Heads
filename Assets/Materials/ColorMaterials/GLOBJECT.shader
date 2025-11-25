Shader "Unlit/GLOBJECT"
{
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Pass
        {
            ZWrite Off
            Cull Off
            Fog { Mode Off }
            Lighting Off
            BindChannels
            {
                Bind "vertex", vertex
                Bind "color", color
            }
        }
    }
}
