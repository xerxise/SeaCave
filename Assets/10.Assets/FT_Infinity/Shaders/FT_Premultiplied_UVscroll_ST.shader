// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.29 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.29;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:7,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:True,fgod:False,fgor:False,fgmd:0,fgcr:0,fgcg:0,fgcb:0,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True;n:type:ShaderForge.SFN_Final,id:4795,x:33868,y:32570,varname:node_4795,prsc:2|emission-2641-OUT,alpha-7883-OUT;n:type:ShaderForge.SFN_Tex2d,id:6074,x:31852,y:32521,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:_MainTex,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-8916-OUT;n:type:ShaderForge.SFN_VertexColor,id:2053,x:32235,y:32772,varname:node_2053,prsc:2;n:type:ShaderForge.SFN_Multiply,id:9759,x:32589,y:32595,varname:node_9759,prsc:2|A-2053-RGB,B-9243-OUT,C-6265-OUT,D-6074-A,E-2053-A;n:type:ShaderForge.SFN_Multiply,id:669,x:32589,y:32790,varname:node_669,prsc:2|A-6074-A,B-2053-A;n:type:ShaderForge.SFN_ValueProperty,id:6265,x:32235,y:32940,ptovrint:False,ptlb:Emission,ptin:_Emission,varname:node_6265,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_DepthBlend,id:7531,x:32768,y:32997,varname:node_7531,prsc:2|DIST-4229-OUT;n:type:ShaderForge.SFN_Multiply,id:7883,x:33003,y:32764,varname:node_7883,prsc:2|A-7468-OUT,B-7531-OUT;n:type:ShaderForge.SFN_Slider,id:4229,x:32225,y:33054,ptovrint:False,ptlb:Depth,ptin:_Depth,varname:node_4229,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_SwitchProperty,id:9243,x:32252,y:32447,ptovrint:False,ptlb:Desaturate,ptin:_Desaturate,varname:node_9243,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:False|A-6074-RGB,B-4001-OUT;n:type:ShaderForge.SFN_Desaturate,id:4001,x:32046,y:32347,varname:node_4001,prsc:2|COL-6074-RGB;n:type:ShaderForge.SFN_TexCoord,id:8259,x:30878,y:32395,varname:node_8259,prsc:2,uv:0;n:type:ShaderForge.SFN_ValueProperty,id:8189,x:30425,y:32470,ptovrint:False,ptlb:X_Speed,ptin:_X_Speed,varname:_X_Offset_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_ValueProperty,id:1973,x:30415,y:32568,ptovrint:False,ptlb:Y_Speed,ptin:_Y_Speed,varname:_Y_Offset_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Add,id:535,x:31305,y:32241,varname:node_535,prsc:2|A-8259-UVOUT,B-8618-OUT;n:type:ShaderForge.SFN_Multiply,id:8341,x:30878,y:32565,varname:node_8341,prsc:2|A-9586-OUT,B-556-T;n:type:ShaderForge.SFN_Time,id:556,x:30660,y:32646,varname:node_556,prsc:2;n:type:ShaderForge.SFN_Append,id:9586,x:30649,y:32489,varname:node_9586,prsc:2|A-8189-OUT,B-1973-OUT;n:type:ShaderForge.SFN_Add,id:8916,x:31663,y:32392,varname:node_8916,prsc:2|A-535-OUT,B-7782-OUT;n:type:ShaderForge.SFN_Tex2d,id:6684,x:32438,y:33335,ptovrint:False,ptlb:ClipTex,ptin:_ClipTex,varname:node_6684,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-6695-OUT;n:type:ShaderForge.SFN_Tex2d,id:9989,x:31677,y:33036,ptovrint:False,ptlb:Distortion Tex,ptin:_DistortionTex,varname:node_9989,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-7485-OUT;n:type:ShaderForge.SFN_TexCoord,id:2851,x:31937,y:33133,varname:node_2851,prsc:2,uv:0;n:type:ShaderForge.SFN_Add,id:6695,x:32202,y:33281,varname:node_6695,prsc:2|A-2851-UVOUT,B-9071-OUT;n:type:ShaderForge.SFN_Multiply,id:2641,x:32808,y:32614,varname:node_2641,prsc:2|A-9759-OUT,B-2644-OUT;n:type:ShaderForge.SFN_Multiply,id:7468,x:32808,y:32790,varname:node_7468,prsc:2|A-669-OUT,B-2644-OUT;n:type:ShaderForge.SFN_ValueProperty,id:9398,x:31025,y:33144,ptovrint:False,ptlb:Dist Strength,ptin:_DistStrength,varname:node_9398,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Multiply,id:7782,x:31608,y:32638,varname:node_7782,prsc:2|A-9989-R,B-9398-OUT;n:type:ShaderForge.SFN_Multiply,id:9071,x:31937,y:33309,varname:node_9071,prsc:2|A-9989-R,B-9398-OUT;n:type:ShaderForge.SFN_Add,id:7485,x:31344,y:32504,varname:node_7485,prsc:2|A-8259-UVOUT,B-4597-OUT;n:type:ShaderForge.SFN_ValueProperty,id:4483,x:30778,y:32837,ptovrint:False,ptlb:Dist Speed Mult,ptin:_DistSpeedMult,varname:node_4483,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:2;n:type:ShaderForge.SFN_Multiply,id:4597,x:31027,y:32680,varname:node_4597,prsc:2|A-8341-OUT,B-4483-OUT;n:type:ShaderForge.SFN_ValueProperty,id:9571,x:32394,y:33190,ptovrint:False,ptlb:Clip Strength,ptin:_ClipStrength,varname:node_9571,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Power,id:2644,x:32615,y:33167,varname:node_2644,prsc:2|VAL-6684-R,EXP-9571-OUT;n:type:ShaderForge.SFN_Vector1,id:2753,x:30842,y:32288,varname:node_2753,prsc:2,v1:0;n:type:ShaderForge.SFN_SwitchProperty,id:8618,x:31095,y:32315,ptovrint:False,ptlb:Scroll Distortion Only,ptin:_ScrollDistortionOnly,varname:node_8618,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:False|A-8341-OUT,B-2753-OUT;proporder:6074-6684-9989-6265-9243-4229-8189-1973-9398-4483-9571-8618;pass:END;sub:END;*/

Shader "FT/Premultiplied_UVscroll_Standartd" {
    Properties {
        _MainTex ("MainTex", 2D) = "white" {}
        _ClipTex ("ClipTex", 2D) = "white" {}
        _DistortionTex ("Distortion Tex", 2D) = "white" {}
        _Emission ("Emission", Float ) = 1
        [MaterialToggle] _Desaturate ("Desaturate", Float ) = 0
        _Depth ("Depth", Range(0, 1)) = 0
        _X_Speed ("X_Speed", Float ) = 0
        _Y_Speed ("Y_Speed", Float ) = 0
        _DistStrength ("Dist Strength", Float ) = 1
        _DistSpeedMult ("Dist Speed Mult", Float ) = 2
        _ClipStrength ("Clip Strength", Float ) = 1
        [MaterialToggle] _ScrollDistortionOnly ("Scroll Distortion Only", Float ) = 0
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend One OneMinusSrcAlpha
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform sampler2D _CameraDepthTexture;
            uniform float4 _TimeEditor;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float _Emission;
            uniform float _Depth;
            uniform fixed _Desaturate;
            uniform float _X_Speed;
            uniform float _Y_Speed;
            uniform sampler2D _ClipTex; uniform float4 _ClipTex_ST;
            uniform sampler2D _DistortionTex; uniform float4 _DistortionTex_ST;
            uniform float _DistStrength;
            uniform float _DistSpeedMult;
            uniform float _ClipStrength;
            uniform fixed _ScrollDistortionOnly;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 vertexColor : COLOR;
                float4 projPos : TEXCOORD1;
                UNITY_FOG_COORDS(2)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.pos = UnityObjectToClipPos(v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float sceneZ = max(0,LinearEyeDepth (UNITY_SAMPLE_DEPTH(tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.projPos)))) - _ProjectionParams.g);
                float partZ = max(0,i.projPos.z - _ProjectionParams.g);
////// Lighting:
////// Emissive:
                float4 node_556 = _Time + _TimeEditor;
                float2 node_8341 = (float2(_X_Speed,_Y_Speed)*node_556.g);
                float2 node_7485 = (i.uv0+(node_8341*_DistSpeedMult));
                float4 _DistortionTex_var = tex2D(_DistortionTex,TRANSFORM_TEX(node_7485, _DistortionTex));
                float2 node_8916 = ((i.uv0+lerp( node_8341, 0.0, _ScrollDistortionOnly ))+(_DistortionTex_var.r*_DistStrength));
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(node_8916, _MainTex));
                float2 node_6695 = (i.uv0+(_DistortionTex_var.r*_DistStrength));
                float4 _ClipTex_var = tex2D(_ClipTex,TRANSFORM_TEX(node_6695, _ClipTex));
                float node_2644 = pow(_ClipTex_var.r,_ClipStrength);
                float3 emissive = ((i.vertexColor.rgb*lerp( _MainTex_var.rgb, dot(_MainTex_var.rgb,float3(0.3,0.59,0.11)), _Desaturate )*_Emission*_MainTex_var.a*i.vertexColor.a)*node_2644);
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,(((_MainTex_var.a*i.vertexColor.a)*node_2644)*saturate((sceneZ-partZ)/_Depth)));
                UNITY_APPLY_FOG_COLOR(i.fogCoord, finalRGBA, fixed4(0,0,0,1));
                return finalRGBA;
            }
            ENDCG
        }
    }
    CustomEditor "ShaderForgeMaterialInspector"
}
