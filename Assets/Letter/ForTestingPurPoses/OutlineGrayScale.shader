// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Outline&GrayScale"
//properties = 인스펙터에서 설정 가능한 파라미터 리스트
{Properties
{
	_MainTex("Sprite Texture", 2D) = "white" {}
	_Color("Tint", Color) = (1,1,1,1)
	_OutlineColor("Outline", Color) = (1, 1, 1, 1)
	_OutlineThickness("OutlineThinkness", Float) = 1
	[MaterialToggle] PixelSnap("Pixel snap", Float) = 0
	_EffectAmount("Effect Amount", Range(0, 1)) = 1.0
}

//shader에 대한 자세한 설명
SubShader
	{
		Tags
		{
			"Queue" = "Transparent"
			"IgnoreProjector" = "True"
			"RenderType" = "Transparent"
			"PreviewType" = "Plane"
			"CanUseSpriteAtlas" = "True"
		}
		//renderer state set-up
		Cull Off
		Lighting Off
		ZWrite Off
		Fog { Mode Off }
		Blend SrcAlpha OneMinusSrcAlpha

		//Causes the geometry of a GameObject to be rendered once
		Pass
		{
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile DUMMY PIXELSNAP_ON
			#include "UnityCG.cginc"

			struct appdata_t
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				fixed4 color : COLOR;
				half2 texcoord  : TEXCOORD0;
			};

			fixed4 _Color;

			v2f vert(appdata_t IN)
			{
				v2f OUT;
				float expand = 1.1f;
				IN.vertex.xyz *= expand;
				
				OUT.vertex = UnityObjectToClipPos(IN.vertex);
				OUT.texcoord = (IN.texcoord - 0.5f) * expand + 0.5f;

				//OUT.texcoord = IN.texcoord;
				OUT.color = IN.color * _Color;
				#ifdef PIXELSNAP_ON
				OUT.vertex = UnityPixelSnap(OUT.vertex);
				#endif

				return OUT;
			}

			sampler2D _MainTex;
			uniform float _EffectAmount;
			fixed4 _OutlineColor;
			float _OutlineThickness;

			fixed4 frag(v2f IN) : SV_Target
			{
				// Texcoord distance from the center of the quad.
				float2 fromCenter = abs(IN.texcoord - 0.5f);
				// Signed distance from the horizontal & vertical edges.
				float2 fromEdge = fromCenter - 0.5f;

				// Use screenspace derivatives to convert to pixel distances.
				fromEdge.x /= length(float2(ddx(IN.texcoord.x), ddy(IN.texcoord.x)));
				fromEdge.y /= length(float2(ddx(IN.texcoord.y), ddy(IN.texcoord.y)));

				// Compute a nicely rounded distance from the edge.
				float distance = abs(min(max(fromEdge.x,fromEdge.y), 0.0f) + length(max(fromEdge, 0.0f)));

				// Sample our texture for the interior.
				fixed4 texcol = tex2D(_MainTex, IN.texcoord);
				// Clip out the part of the texture outside our original 0...1 UV space.
				texcol.a *= step(max(fromCenter.x, fromCenter.y), 0.5f);

				// Blend in our outline within a controllable thickness of the edge.
				texcol = lerp(texcol, _OutlineColor, saturate(_OutlineThickness - distance));

				texcol.rgb = lerp(texcol.rgb, dot(texcol.rgb, float3(0.3, 0.59, 0.11)), _EffectAmount);
				texcol = texcol * IN.color;
				return texcol;
			}

		ENDCG
		}
	}
		Fallback "Sprites/Default"
}
