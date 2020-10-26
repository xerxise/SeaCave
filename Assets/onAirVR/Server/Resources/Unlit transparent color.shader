/***********************************************************

  Copyright (c) 2017-present Clicked, Inc.

  Licensed under the MIT license found in the LICENSE file 
  in the Docs folder of the distributed package.

 ***********************************************************/

Shader "onAirVR/Unlit transparent color" 
{
	Properties
	{
		_Color ("Main Color", Color) = (1, 1, 1, 1)
	}

    SubShader
    {
        Tags { "RenderType" = "Transparent" "Queue"="Transparent" "IgnoreProjector"="True"}
        LOD 100
        Fog { Mode Off }
        Zwrite Off
        Blend SrcAlpha OneMinusSrcAlpha
        Color [_Color]

        Pass {}
    }
}
