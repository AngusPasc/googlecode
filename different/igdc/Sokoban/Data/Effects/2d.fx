// � ���� ����� ���������� ���� ��������, ������������ � ����, � � ���
// ������������� ����������� ������� �� ������������(��������� ���� ����� 
// ������� ����� �������). � ���������� �� ������������, ���� ����������� 
// ����� ������������, �� ������ ��������� ����� ����� �������� ����
// (�������� ��������� �����). ������ ��� ������������ C-�������� ���,
// ������������ � ����. ������ ��������������� ��������������� ���������
// ������ �� ���������� � ��������. �� ��� DirectX, ������������ ������ 
// HLSL(High Level Shader Language). ��� � ���� ������ Cg(������� nVIDIA, ��
// ������ ��� GeForce), � GLSL ��� OpenGL.

texture t0;

sampler ClampSampler = sampler_state
{ 
    Texture = <t0>; 
    AddressU = Clamp; 
    AddressV = Clamp; 
    MinFilter = Point;    
    MipFilter = Point; 
    MagFilter = Point; 
};

sampler WrapSampler = sampler_state
{ 
    Texture = <t0>; 
    AddressU = Wrap; 
    AddressV = Wrap;
    MipFilter = Point; 
    MinFilter = Point; 
    MagFilter = Point; 
};

sampler LinearClampSampler = sampler_state
{ 
    Texture = <t0>; 
    AddressU = Clamp; 
    AddressV = Clamp; 
    MipFilter = Linear; 
    MinFilter = Linear; 
    MagFilter = Linear; 
};

// ��� ���� 2D-����, �� ��������� ������ ���� �� ��, �� � �� ����������.
// ��, ��� �� ������ ��� ��� ��������� ���������� ������� ������ �������� � 
// ����������� �������

void vNormal(in  float4 iPos: POSITION,
             in  float4 iCol: COLOR0,
             in  float4 iTex: TEXCOORD0,
             out float4 oPos: POSITION,
             out float4 oCol: COLOR0,
             out float4 oTex: TEXCOORD0)
{
	oPos = iPos;
	oCol = iCol;
	oTex = iTex;
}

// ��� ���������� ���������� �������, ���������� �� "����������" ����� �������,
// ��������� ������ ��� � �������� �����������(������������ �������� ����� ������� ����� 
// ��� ���� ����?)
 
void pNormal(in  float2 iTex: TEXCOORD0,
             in  float4 iCol: COLOR0,
             out float4 oCol: COLOR0)
{
	oCol = tex2D(ClampSampler, iTex)*iCol;
}

void pGrid(in  float2 iTex: TEXCOORD0,
           in  float4 iCol: COLOR0,
           out float4 oCol: COLOR0)
{
	oCol = tex2D(WrapSampler, iTex)*iCol;
}

void pAngle(in  float2 iTex: TEXCOORD0,
            in  float4 iCol: COLOR0,
            out float4 oCol: COLOR0)
{
	oCol = tex2D(LinearClampSampler, iTex)*iCol;
}

// ������ "���������" �������� ������� � ������� ����

void pTest(in  float2 iTex: TEXCOORD0,
           in  float4 iCol: COLOR0,
           out float4 oCol: COLOR0)
{
     oCol = tex2D(ClampSampler, iTex);
     if (oCol.r > iCol.w) oCol = 0; else
     if (oCol.g > iCol.w) oCol = 0; else
     if (oCol.b > iCol.w) oCol = 0;
}

// ��� ������� ��� ������ ������� �������� � ���������� ��������.
// ������ � �����������. ��� ������ ������ ��������: ��������� 
// ������������ ���� ��������.

float3 C = { 0.2125f, 0.7154f, 0.0721f };

void pBlackAndWhite(in  float2 iTex: TEXCOORD0,
                    in  float4 iCol: COLOR0,
                    out float4 oCol: COLOR0)
{
     oCol = tex2D(ClampSampler, iTex);
     oCol.rgb = dot(oCol.rgb, C);
     oCol.a = oCol.a*iCol.a;
}

void pBlackAndWhiteSmooth(in  float2 iTex: TEXCOORD0,
                          in  float4 iCol: COLOR0,
                          out float4 oCol: COLOR0)
{
     oCol = tex2D(LinearClampSampler, iTex);
     oCol.rgb = dot(oCol.rgb, C);
     oCol.a = oCol.a*iCol.a;
}

// � ��� ��� �������...����� ��� ������� :) ������ ��������� ������
// ��� "��������"? ������ ������� "����������" - �� ����� ������ �� 
// ��������� :) ������ ������������ � ��������������, ������ ����� �� 
// ��������� :)

extern float a;
extern float b;

void pWaves(in  float2 iTex: TEXCOORD0,
            in  float4 iCol: COLOR0,
            out float4 oCol: COLOR0)
{
      iTex.y += 0.0015*sin(50*iTex.x+b)*iCol.a;
	iTex.x += 0.003*sin(50*iTex.y+a)*iCol.a;
	oCol = tex2D(LinearClampSampler, iTex);
}

technique Normal
{
      pass P0
	{
		VertexShader = compile vs_1_1 vNormal();
		PixelShader = compile ps_1_1 pNormal();
      }
}

technique Grid
{
	pass P0
	{
		VertexShader = compile vs_1_1 vNormal();
		PixelShader = compile ps_1_1 pGrid();
	}
}

technique Angle
{
	pass P0
	{
		VertexShader = compile vs_1_1 vNormal();
		PixelShader = compile ps_1_1 pAngle();
	}
}

technique Test
{
	pass P0
	{
		VertexShader = compile vs_1_1 vNormal();
		PixelShader = compile ps_1_4 pTest();
	}
}

technique BlackAndWhite
{
	pass P0
	{
		VertexShader = compile vs_1_1 vNormal();
		PixelShader = compile ps_1_1 pBlackAndWhite();
	}
}

technique BlackAndWhiteSmooth
{
	pass P0
	{
		VertexShader = compile vs_1_1 vNormal();
		PixelShader = compile ps_1_1 pBlackAndWhiteSmooth();
	}
}

technique Blur
{
	pass P0
	{
		VertexShader = compile vs_1_1 vNormal();
		PixelShader = compile ps_2_0 pWaves();
	}
}