unit GuiSplash;
//==============================================================================
// Unit: GuiSplash.pas
// Desc: �������� �� Game Developers Contest-�������� � ������ ����
//==============================================================================
interface
uses Windows, Game, RenderMain;

  function Initialize: HRESULT;
  function OnRender: HRESULT;

  function WMKeyDown(Key: Word): HRESULT;
  function WMTimer: HRESULT;
  function WMClick: HRESULT;
  
implementation
const
  Appearance    = 0; // ���������
  Disappearance = 1; // ���������
  Wait          = 1; // ����� �������� � ��������
var
  State : Byte = Appearance;
  Alpha : Single = 0;
  Time  : Single = 0;

  function Max(a,b: Single): Single;
  begin
       if a>b then Result := a else Result := b;
  end;

  function Min(a,b: Single): Single;
  begin
       if a<b then Result := a else Result := b;
  end;

//==============================================================================
// Name: Initialize
//==============================================================================
  function Initialize: HRESULT;
  begin
       LoadTextureFromFile('GDCLogo', 'textures\GDCLogo.tga', 0);
       Result := S_OK;
  end;

//==============================================================================
// Name: OnRender
// Desc: ��������������� ������
//==============================================================================
  function OnRender: HRESULT;
  begin
       DrawSprite('GDCLogo', 256, 128, 512, 512, 0, 0, 1, 1, (Round(Alpha*2.55) shl 24) or $00FFFFFF);
       Result := S_OK;
  end;

//==============================================================================
// Name: WMTimer
// Desc: ���������� 40 ��� � �������
//==============================================================================
  function WMTimer: HRESULT;
  begin
       case State of
         Appearance:
         begin
              if Alpha = 100 then
              begin
                   if Time>=Wait then State := Disappearance else Time := Time + 0.025;
              end else
                   Alpha := Min(Alpha + 5, 100);
         end;
         Disappearance:
         begin
              Alpha := Max(0, Alpha - 5);
              if Alpha <= 50 then Game.GamePos := gpMainMenu;
         end;
       end;
       Result := S_OK;
  end;

//==============================================================================
// Name: WMKeyDown & WMClick
// Desc: ���� ����� ��� ������� ������� �������� ��������
//==============================================================================
  function WMKeyDown(Key: Word): HRESULT;
  begin
       State := Disappearance;
       Result := S_OK;
  end;

  function WMClick: HRESULT;
  begin
       State := Disappearance;
       Result := S_OK;
  end;
  
end.
