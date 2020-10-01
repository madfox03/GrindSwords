Shader "GFM/BillboardGlowRamp"
{

	Properties{
		_MainTex("Base texture", 2D) = "black" {}
		_FadeOutDistNear("Near fadeout dist", float) = 10
		_FadeOutDistFar("Far fadeout dist", float) = 10000
		_VerticalBillboarding("Vertical billboarding amount", Range(0,1)) = 1
		_ViewerOffset("Viewer offset", float) = 0
		_Color("Color", COLOR) = (0,0,0,0)
		_Intencity("Intencity", float) = 0
	}


		SubShader{
		Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }

		Blend DstColor One //SrcAlpha OneMinusSrcAlpha
		Cull off 
		Lighting off 
		ZWrite Off 
		Fog{ Color(0,0,0,0) }

		LOD 100

		CGINCLUDE
#include "UnityCG.cginc"
	sampler2D _MainTex;

	half _FadeOutDistNear;
	half _FadeOutDistFar;
	half _VerticalBillboarding;
	half _ViewerOffset;
	half _ColorPos;
	half _Intencity;
	uniform half4 _Color;

	struct v2f {
		half4	pos	: SV_POSITION;
		half2	uv		: TEXCOORD0;
		fixed4	color : TEXCOORD1;
	};

	void CalcOrthonormalBasis(half3 dir,out half3 right,out half3 up)
	{
		up = abs(dir.y) > 0.999f ? half3(0,0,1) : half3(0,1,0);
		right = normalize(cross(up,dir));
		up = cross(dir,right);
	}

	half CalcFadeOutFactor(half dist)
	{
		half		nfadeout = saturate(dist / _FadeOutDistNear);
		half		ffadeout = 1 - saturate(max(dist - _FadeOutDistFar,0) * 0.2);

		ffadeout *= ffadeout;

		nfadeout *= nfadeout;
		nfadeout *= nfadeout;

		nfadeout *= ffadeout;

		return nfadeout;
	}

	half CalcDistScale(half dist)
	{
		half	distScale = min(max(dist - 1,0) / 1,1);

		return distScale * distScale;
	}


	v2f vert(appdata_full v)
	{
		v2f o;
#if 0
		// cheap view space billboarding
		half3	centerOffs = half3(half(0.5).xx - v.color.rg,0) * v.texcoord1.xyy;
		half3	BBCenter = v.vertex + centerOffs.xyz;
		half3	viewPos = mul(UNITY_MATRIX_MV, half4(BBCenter, 1)) - centerOffs; // UnityObjectToViewPos(half4(BBCenter, 1)) - centerOffs; //mul(UNITY_MATRIX_MV,half4(BBCenter,1)) - centerOffs;
#else

		half3	centerOffs = half3(half(0.5).xx - v.color.rg,0) * v.texcoord1.xyy;
		half3	centerLocal = v.vertex.xyz + centerOffs.xyz;
		half3	viewerLocal = mul(unity_WorldToObject,half4(_WorldSpaceCameraPos,1));
		half3	localDir = viewerLocal - centerLocal;
		localDir[1] = lerp(0,localDir[1],_VerticalBillboarding);

		half	localDirLength = length(localDir);
		half3	rightLocal;
		half3	upLocal;

		CalcOrthonormalBasis(localDir / localDirLength,rightLocal,upLocal);

		half	distScale = CalcDistScale(localDirLength)*v.color.a;
		half3	BBNormal = rightLocal * v.normal.x + upLocal * v.normal.y;
		half3	BBLocalPos = centerLocal - (rightLocal * centerOffs.x + upLocal * centerOffs.y) + BBNormal * distScale;

		BBLocalPos += _ViewerOffset * localDir;
#endif

		o.uv = v.texcoord.xy;
		o.pos = UnityObjectToClipPos(half4(BBLocalPos,1));
		o.color = CalcFadeOutFactor(localDirLength);

		return o;
	}
	ENDCG


	Pass{
		CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#pragma fragmentoption ARB_precision_hint_fastest		
	fixed4 frag(v2f i) : COLOR
	{
		fixed4 result = tex2D(_MainTex, i.uv.xy);
		result *= _Color * _Intencity;
		return result;
	}
		ENDCG
	}
	}

}