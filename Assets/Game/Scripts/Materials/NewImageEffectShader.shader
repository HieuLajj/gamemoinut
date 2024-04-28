Shader "Cơ/UI_Mask_Texture"
{
	Properties
	{
		_MainTex("Base (RGB)", 2D) = "white" {}
		_MaskTex("Mask Texture", 2D) = "white" {}
		_Alpha("Alpha",float) = 1
	    _Alpha_Border("Alpha Viền",float) = 1
	}
		SubShader
	{
		Tags { "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
		LOD 200

		Blend SrcAlpha OneMinusSrcAlpha
		ZWrite Off
		Cull Off
		Lighting Off
		Fog { Mode Off }

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fog

			#include "UnityCG.cginc"

			struct appdata_t
			{
				float4 vertex : POSITION;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f
			{
				float2 texcoord : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			sampler2D _MaskTex;
			float4 _MainTex_ST;
			float _Alpha;
			float _Alpha_Border;

			v2f vert(appdata_t v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				fixed4 baseColor = tex2D(_MainTex, i.texcoord);
			    fixed4 tempBaseColor = tex2D(_MainTex, i.texcoord);
				fixed4 maskColor = tex2D(_MaskTex, i.texcoord);		
				if (baseColor.a < 0.1)
				{
					discard;
				}
				
				if (maskColor.a < 0.5)
					discard;				
				baseColor *= maskColor;	
				if (tempBaseColor.r <= 0.5 && tempBaseColor.g <= 0.5 && tempBaseColor.b <= 0.5)
				{
					baseColor.r = 1 - tempBaseColor.r;
					baseColor.g = 1 - tempBaseColor.g;
					baseColor.b = 1 - tempBaseColor.b;
					baseColor.a = _Alpha_Border;
				}
				else
				{
					baseColor.a = _Alpha;
				}
				
				
					
				return baseColor;
			}
			ENDCG
		}
	}
}