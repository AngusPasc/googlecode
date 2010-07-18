unit GuiUtils;
//==============================================================================
// Unit: GuiUtils.pas
// Desc: ���� ���� �� ����������� min � max ������� � ���������� ������.
//       Math ���� �� �������� ������������ ������ ��-�� ���� ���� �������.
//==============================================================================
interface

  function Max(a,b: Single): Single;
  function Min(a,b: Single): Single;

implementation

  function Max(a,b: Single): Single;
  begin
       if a>b then Result := a else Result := b;
  end;

  function Min(a,b: Single): Single;
  begin
       if a<b then Result := a else Result := b;
  end;

end.
