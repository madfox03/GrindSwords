Shader "GFM/Billboard"
{

	Properties{
		_MainTex("Base texture", 2D) = "black" {}
		_FadeOutDistNear("Near fadeout dist", float) = 10
		_FadeOutDistFar("Far fadeout dist", float) = 10000
		_VerticalBillboarding("Vertical billboarding amount", Range(0,1)) = 1
		_ViewerOffset("Viewer offset", float) = 0
		_Color("Color", Color) = (1,1,1,1)
		_Intencity("Intencity", Range(0.0, 1.0)) = 1
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

	float _FadeOutDistNear;
	float _FadeOutDistFar;
	float _VerticalBillboarding;
	float _ViewerOffset;
	float4 _Color;
	float _Intencity;

	struct v2f {
		float4	pos	: SV_POSITION;
		float2	uv		: TEXCOORD0;
		fixed4	color : TEXCOORD1;
	};

	void CalcOrthonormalBasis(float3 dir,out float3 right,out float3 up)
	{
		up = abs(dir.y) > 0.999f ? float3(0,0,1) : float3(0,1,0);
		right = normalize(cross(up,dir));
		up = cross(dir,right);
	}

	float CalcFadeOutFactor(float dist)
	{
		float		nfadeout = saturate(dist / _FadeOutDistNear);
		float		ffadeout = 1 - saturate(max(dist - _FadeOutDistFar,0) * 0.2);

		ffadeout *= ffadeout;

		nfadeout *= nfadeout;
		nfadeout *= nfadeout;

		nfadeout *= ffadeout;

		return nfadeout;
	}

	float CalcDistScale(float dist)
	{
		float	distScale = min(max(dist - 1,0) / 1,1);

		return distScale * distScale;
	}


	v2f vert(appdata_full v)
	{
		v2f o;
#if 0
		// cheap view space billboarding
		float3	centerOffs = float3(float(0.5).xx - v.color.rg,0) * v.texcoord1.xyy;
		float3	BBCenter = v.vertex + centerOffs.xyz;
		float3	viewPos = mul(UNITY_MATRIX_MV, float4(BBCenter, 1)) - centerOffs; // UnityObjectToViewPos(float4(BBCenter, 1)) - centerOffs; //mul(UNITY_MATRIX_MV,float4(BBCenter,1)) - centerOffs;
#else

		float3	centerOffs = float3(float(0.5).xx - v.color.rg,0) * v.texcoord1.xyy;
		float3	centerLocal = v.vertex.xyz + centerOffs.xyz;
		float3	viewerLocal = mul(unity_WorldToObject,float4(_WorldSpaceCameraPos,1));
		float3	localDir = viewerLocal - centerLocal;
		localDir[1] = lerp(0,localDir[1],_VerticalBillboarding);

		float	localDirLength = length(localDir);
		float3	rightLocal;
		float3	upLocal;

		CalcOrthonormalBasis(localDir / localDirLength,rightLocal,upLocal);

		float	distScale = CalcDistScale(localDirLength)*v.color.a;
		float3	BBNormal = rightLocal * v.normal.x + upLocal * v.normal.y;
		float3	BBLocalPos = centerLocal - (rightLocal * centerOffs.x + upLocal * centerOffs.y) + BBNormal * distScale;

		BBLocalPos += _ViewerOffset * localDir;
#endif

		o.uv = v.texcoord.xy;
		o.pos = UnityObjectToClipPos(float4(BBLocalPos,1));
		o.color = CalcFadeOutFactor(localDirLength) *_Color * _Intencity;

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
		return tex2D(_MainTex, i.uv.xy) *i.color;
	}
		ENDCG
	}
	}

}