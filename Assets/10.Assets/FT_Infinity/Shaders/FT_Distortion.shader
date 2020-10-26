// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Shader created with Shader Forge v1.29 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.29;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:3,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:True,hqlp:False,rprd:True,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:2865,x:33020,y:32712,varname:node_2865,prsc:2|emission-9677-OUT,alpha-1176-OUT,refract-7038-OUT;n:type:ShaderForge.SFN_Multiply,id:153,x:32317,y:32980,varname:node_153,prsc:2|A-3769-OUT,B-7900-OUT;n:type:ShaderForge.SFN_Multiply,id:7900,x:32109,y:33046,varname:node_7900,prsc:2|A-5906-OUT,B-5609-OUT;n:type:ShaderForge.SFN_Vector1,id:5609,x:31899,y:33127,varname:node_5609,prsc:2,v1:0.2;n:type:ShaderForge.SFN_Slider,id:5906,x:31652,y:32976,ptovrint:False,ptlb:Dist Strength,ptin:_DistStrength,varname:node_5906,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Lerp,id:9677,x:32083,y:32676,varname:node_9677,prsc:2|A-2770-OUT,B-7238-RGB,T-5906-OUT;n:type:ShaderForge.SFN_Tex2d,id:7238,x:31809,y:32704,ptovrint:False,ptlb:DistortionTex,ptin:_DistortionTex,varname:node_7238,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:c6cbaafa87f25c74faf118b59a1b25f7,ntxv:3,isnm:False|UVIN-2688-OUT;n:type:ShaderForge.SFN_Vector3,id:2770,x:31809,y:32571,varname:node_2770,prsc:2,v1:0,v2:0,v3:1;n:type:ShaderForge.SFN_ComponentMask,id:3769,x:32083,y:32826,varname:node_3769,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-7238-RGB;n:type:ShaderForge.SFN_TexCoord,id:8489,x:31369,y:32561,varname:node_8489,prsc:2,uv:0;n:type:ShaderForge.SFN_ValueProperty,id:8516,x:30985,y:32825,ptovrint:False,ptlb:X_Speed,ptin:_X_Speed,varname:node_3133,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_ValueProperty,id:8882,x:30974,y:32923,ptovrint:False,ptlb:Y_Speed,ptin:_Y_Speed,varname:node_7570,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Multiply,id:4545,x:31442,y:32876,varname:node_4545,prsc:2|A-7772-OUT,B-807-T;n:type:ShaderForge.SFN_Time,id:807,x:31219,y:33001,varname:node_807,prsc:2;n:type:ShaderForge.SFN_Append,id:7772,x:31208,y:32844,varname:node_7772,prsc:2|A-8516-OUT,B-8882-OUT;n:type:ShaderForge.SFN_Add,id:2688,x:31619,y:32679,varname:node_2688,prsc:2|A-8489-UVOUT,B-4545-OUT;n:type:ShaderForge.SFN_Vector1,id:1176,x:32515,y:33101,varname:node_1176,prsc:2,v1:0;n:type:ShaderForge.SFN_Multiply,id:7038,x:32551,y:32944,varname:node_7038,prsc:2|A-153-OUT,B-7238-A;proporder:7238-5906-8516-8882;pass:END;sub:END;*/

Shader "FT/Distortion" {
    Properties {
        _DistortionTex ("DistortionTex", 2D) = "bump" {}
        _DistStrength ("Dist Strength", Range(0, 1)) = 0
        _X_Speed ("X_Speed", Float ) = 0
        _Y_Speed ("Y_Speed", Float ) = 0
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        GrabPass{ }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform sampler2D _GrabTexture;
            uniform float4 _TimeEditor;
            uniform float _DistStrength;
            uniform sampler2D _DistortionTex; uniform float4 _DistortionTex_ST;
            uniform float _X_Speed;
            uniform float _Y_Speed;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float4 screenPos : TEXCOORD3;
                UNITY_FOG_COORDS(4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos(v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                o.screenPos = o.pos;
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                #if UNITY_UV_STARTS_AT_TOP
                    float grabSign = -_ProjectionParams.x;
                #else
                    float grabSign = _ProjectionParams.x;
                #endif
                i.normalDir = normalize(i.normalDir);
                i.screenPos = float4( i.screenPos.xy / i.screenPos.w, 0, 0 );
                i.screenPos.y *= _ProjectionParams.x;
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float4 node_807 = _Time + _TimeEditor;
                float2 node_2688 = (i.uv0+(float2(_X_Speed,_Y_Speed)*node_807.g));
                float4 _DistortionTex_var = tex2D(_DistortionTex,TRANSFORM_TEX(node_2688, _DistortionTex));
                float2 sceneUVs = float2(1,grabSign)*i.screenPos.xy*0.5+0.5 + ((_DistortionTex_var.rgb.rg*(_DistStrength*0.2))*_DistortionTex_var.a);
                float4 sceneColor = tex2D(_GrabTexture, sceneUVs);
////// Lighting:
////// Emissive:
                float3 emissive = lerp(float3(0,0,1),_DistortionTex_var.rgb,_DistStrength);
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(lerp(sceneColor.rgb, finalColor,0.0),1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "Meta"
            Tags {
                "LightMode"="Meta"
            }
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_META 1
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #include "UnityMetaPass.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform float _DistStrength;
            uniform sampler2D _DistortionTex; uniform float4 _DistortionTex_ST;
            uniform float _X_Speed;
            uniform float _Y_Speed;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityMetaVertexPosition(v.vertex, v.texcoord1.xy, v.texcoord2.xy, unity_LightmapST, unity_DynamicLightmapST );
                return o;
            }
            float4 frag(VertexOutput i) : SV_Target {
                UnityMetaInput o;
                UNITY_INITIALIZE_OUTPUT( UnityMetaInput, o );
                
                float4 node_807 = _Time + _TimeEditor;
                float2 node_2688 = (i.uv0+(float2(_X_Speed,_Y_Speed)*node_807.g));
                float4 _DistortionTex_var = tex2D(_DistortionTex,TRANSFORM_TEX(node_2688, _DistortionTex));
                o.Emission = lerp(float3(0,0,1),_DistortionTex_var.rgb,_DistStrength);
                
                float3 diffColor = float3(0,0,0);
                float specularMonochrome;
                float3 specColor;
                diffColor = DiffuseAndSpecularFromMetallic( diffColor, 0, specColor, specularMonochrome );
                o.Albedo = diffColor;
                
                return UnityMetaFragment( o );
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
