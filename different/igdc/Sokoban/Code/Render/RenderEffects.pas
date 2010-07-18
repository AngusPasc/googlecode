// PART OF ORBITAL ENGINE 2.0 SOURCE CODE
unit RenderEffects;
//==============================================================================
// Unit: RenderTextures.pas
// Desc: �������� ��������
//       �2006 .gear
//==============================================================================
interface
uses
  Windows, SysUtils, GameMain, D3D9, D3DX9Def, D3DX9Link;

type
  TEffect= record
    Name    : string;
    Effect  : ID3DXEffect;
  end;

  function LoadEffectFromFile(Name: string; FileName: string): HRESULT;
  function FindEffect(Name: string): ID3DXEffect;
  function FindTechnique(Name: string): ID3DXEffect;
  procedure OnResetDevice;

var
  Effects  : array of TEffect;
  nEffects : integer;
  hr       : HRESULT;

implementation
uses
  RenderMain;
//==============================================================================
// Name: LoadEffectFromFile
// Desc: ��������� ������ �� �����
//==============================================================================
  function LoadEffectFromFile(Name: string; FileName: string): HRESULT;
  var Error: ID3DXBuffer;
  begin
       inc(nEffects);
       SetLength(Effects, nEffects);
       Effects[nEffects-1].Name := Name;
       hr := D3DXCreateEffectFromFile(
                  Device,
                  PChar(GameWorkDir+'Data\'+FileName),
                  nil, nil,
                  D3DXFX_NOT_CLONEABLE,
                  nil,
                  Effects[nEffects-1].Effect,
                  @Error);
       if FAILED(hr) then
       begin
            Result := hr;
            Exit;
       end;
       Result := D3D_OK;
  end;

//==============================================================================
// Name: FindEffect
// Desc: ������� ������ �� �����
//==============================================================================
  function FindEffect(Name: string): ID3DXEffect;
  var i: integer;
  begin
       Result := nil;
       for i := 0 to nEffects-1 do
       if Effects[i].Name = Name then
       begin
            Result := Effects[i].Effect;
            Exit;
       end;
  end;

//==============================================================================
// Name: FindTechnique
// Desc: ������� ������, ���������� ������ �������
//==============================================================================
  function FindTechnique(Name: string): ID3DXEffect;
  var i: integer; Technique: TD3DXHandle;
  begin
       Result := nil;
       for i := 0 to nEffects-1 do
       begin
            Technique := Effects[i].Effect.GetTechniqueByName(PChar(Name));
            if Technique <> nil then
            begin
                 Result := Effects[i].Effect;
                 Exit;
            end;
       end;
  end;

//==============================================================================
// Name: OnResetDevice
// Desc: ��������������� ������� ����� ������������ ����������
//==============================================================================
  procedure OnResetDevice;
  var i: integer;
  begin
       for i := 0 to nEffects-1 do
       begin
            hr := Effects[i].Effect.OnLostDevice;
            hr := Effects[i].Effect.OnResetDevice;
       end;
  end;

end.
