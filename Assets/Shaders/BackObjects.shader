Shader "Custom/BackObjects"
{
    SubShader
    {
        Pass
        {
            Stencil
            {
                Ref 1
                Comp Equal
            }
        }

    }
}
