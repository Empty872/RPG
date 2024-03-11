Shader "Custom/WeaponUIMask"
{
    SubShader
    {
        Tags
        {
            "RenderTYpe" = "Opaque" "Queue" = "Geometry-1"
        } // Write to the stencil buffer before drawing any geometry to the screen
        LOD 100
//        ColorMask 0 // Don't write to any colour channels
        Blend Zero One
        ZWrite Off // Don't write to the Depth buffer
        Pass
        {
            Stencil
            {
                Ref 1
                Comp Always
                Pass Replace
            }

        }
    }
}