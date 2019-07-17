Shader "Winter/Grey Ghost Shader"
{

Properties
	{

		_Color("Main Color", Color) = (0.5, 0.5, 0.5, 1)
		_Threshold("Opacity Inclusion Threshold", Range(0.00, 1.00)) = 0.82
		_MainTex("Diffuse (RGB) Trans (A)", 2D) = "white" {}
		_BumpMap("Normal Map", 2D) = "bump" {}
		_SpecLevel("Specular Level", Range(0.0, 1.0)) = 0.5
		_SpecPinch("Spec Pinch", Range(0.00, 1.00)) = 0.05
		_GlossLevel("Gloss Enhancement Level", Range(0.0, 1.0)) = 0.5
		_GlossPinch("Gloss Pinch", Range(0.00, 1.00)) = 0.48
		_SpecTex("Specular Map", 2D) = "black" {}

	}

SubShader
{

	Tags
		{
			"RenderType" = "Transparent"
			"Queue" = "Transparent"
		}

	Pass
		{
			ColorMask 0

			CGPROGRAM
				#pragma vertex Vertex_ScaleAndOffsetTexture
				#pragma fragment Fragment_ClipBelowThreshold
				#pragma target 3.0

				#include "UnityCG.cginc"

				struct VertexAndFragmentShaderInfo
				{
					float4 TransformedPosition : SV_POSITION;
					float2 UVCoordinate : TEXCOORD0;
				};

				struct VertexInfo
				{
					float4 Position : POSITION;
					float4 UVCoordinate : TEXCOORD0;
				};

				float4 _Color;
				float _Threshold;
				sampler2D _MainTex;
				float4 _MainTex_ST = 0.0;

				VertexAndFragmentShaderInfo Vertex_ScaleAndOffsetTexture(VertexInfo vertexInfo)
				{
					VertexAndFragmentShaderInfo result;
				result.TransformedPosition = UnityObjectToClipPos(vertexInfo.Position);
				result.UVCoordinate = TRANSFORM_TEX(vertexInfo.UVCoordinate, _MainTex);
				return result;
			}

			fixed4 Fragment_ClipBelowThreshold(VertexAndFragmentShaderInfo input) : COLOR
			{
				if (_Color.a > 0)
				{
					fixed alpha = tex2D(_MainTex, input.UVCoordinate).a;
					fixed threshold = alpha - (1 - ((1 - _Threshold) * _Color.a));

					if (threshold > 0)
					{
						clip(threshold);
					}
					else
					{
						clip(-1);
					}
				}

				return fixed4(0.0, 0.0, 0.0, 0.0);
			}

		ENDCG
	}

	ZWrite Off
Blend SrcAlpha OneMinusSrcAlpha

CGPROGRAM
#pragma surface shadeSurface Custom keepalpha
#pragma target 3.0

struct Input
			{
				float2 uv_MainTex;
				float2 uv_BumpMap;
				float2 uv_SpecTex;
			};

			fixed4 _Color;
			sampler2D _MainTex;
			sampler2D _BumpMap;
			float _SpecLevel;
			float _SpecPinch;
			float _GlossLevel;
			float _GlossPinch;
			sampler2D _SpecTex;

			void shadeSurface(Input input,
				inout SurfaceOutput surfaceOutput)
			{
				float4 diffuse = tex2D(_MainTex, input.uv_MainTex) * _Color;
				float4 shiny = tex2D(_SpecTex, input.uv_SpecTex);
				surfaceOutput.Albedo = diffuse.rgb;
				surfaceOutput.Normal = UnpackNormal(tex2D(_BumpMap, input.uv_BumpMap));
				surfaceOutput.Alpha = diffuse.a;
				surfaceOutput.Specular = shiny.r;
				surfaceOutput.Gloss = shiny.a;
			}

			fixed4 LightingCustom(SurfaceOutput surfaceOutput,
				fixed3 lightDirection,
				fixed3 viewDirection,
				fixed attenuation)
			{
				fixed4 result;
				float normalsByDirections = saturate(dot(surfaceOutput.Normal, normalize(lightDirection + viewDirection)));
				float specularity = saturate(pow(normalsByDirections, _SpecPinch * 100) * surfaceOutput.Specular * _SpecLevel);
				float glossiness = saturate(pow(normalsByDirections, _GlossPinch * 100) * surfaceOutput.Gloss * _GlossLevel);
				specularity = max(specularity, glossiness);
				attenuation *= surfaceOutput.Alpha;
				float normalsByLightDirection = saturate(dot(surfaceOutput.Normal, lightDirection));
				result.rgb = (surfaceOutput.Albedo * _LightColor0.rgb * normalsByLightDirection + _LightColor0.rgb * specularity) * attenuation;
				result.a = surfaceOutput.Alpha;
				return result;
			}
			ENDCG

}



FallBack "Legacy Shaders/Bumped Specular"

}

