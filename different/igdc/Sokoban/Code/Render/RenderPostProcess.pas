// PART OF ORBITAL ENGINE 2.0 SOURCE CODE
unit RenderPostProcess;
//==============================================================================
// Unit: Render2D.pas
// Desc: ���������� �������
//       �2006 .gear
//==============================================================================
interface
uses
  Windows, D3D9, D3DX9Def, D3DX9Link;

  function Initialize: HRESULT;
  function SetRenderTexture(Name: String): HRESULT;
  function Reset: HRESULT;  
  procedure Release;
  
var
  DisplaySurface: IDirect3DSurface9;

implementation
uses
  RenderMain, Render2D, RenderTextures;
var
  hr: HRESULT;
  
//==============================================================================
// Name: Reset
// Desc: ...
//==============================================================================
  function Reset: HRESULT;
  begin
       Result := Device.GetRenderTarget(0, DisplaySurface);
  end;
  
//==============================================================================
// Name: _Present
// Desc: PostProcess ��������� ������ ����������� �� �����
//==============================================================================
  function _Present: HRESULT;
  begin
       Device.SetRenderTarget(0, DisplaySurface);
       Device.BeginScene;
       DrawSpriteEx('RenderTexture', 0, 0, 1024, 768, 0, 0, 1, 1, $FFFFFFFF, 'Normal');
       Device.EndScene;
       Device.Present(nil, nil, 0, nil);
       SetRenderTexture('RenderTexture');
       Device.Clear(0, nil, D3DCLEAR_TARGET, $FF0000FF, 1, 0);

       Result := D3D_OK;
  end;

//==============================================================================
// Name: SetRenderTexture
// Desc: ...
//==============================================================================
  function SetRenderTexture(Name: String): HRESULT;
  var Surface: IDirect3DSurface9;
  begin
       hr := RenderTextures.FindTexture(Name).GetSurfaceLevel(0,Surface);
       if FAILED(hr)then begin Result := hr; Exit; end;

       Device.SetRenderTarget(0,Surface);
       if FAILED(hr)then begin Result := hr; Exit; end;

       Result := D3D_OK;
  end;

//==============================================================================
// Name: Initialize
// Desc: �������� ���������� ���������
//==============================================================================
  function Initialize: HRESULT;
  begin
       hr := Device.GetRenderTarget(0, DisplaySurface);
       if FAILED(hr)then begin Result := hr; Exit; end;

       hr := CreateRenderTargetTexture('RenderTexture',1025,769);
       if FAILED(hr)then begin Result := hr; Exit; end;

       SetRenderTexture('RenderTexture');
       if FAILED(hr)then begin Result := hr; Exit; end;

       // �������� ��������� ������
       RenderMain.Present := _Present;

       Result := D3D_OK;
  end;

//==============================================================================
// Name: Release
// Desc: �������� �����������
//==============================================================================
  procedure Release;
  begin
       if DisplaySurface <> nil then
       begin
            DisplaySurface._Release;
            DisplaySurface := nil;
       end;
  end;

end.
