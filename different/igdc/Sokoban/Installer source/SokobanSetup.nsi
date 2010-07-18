!include "Sections.nsh"

; ����������
SetCompressor /SOLID lzma

; �������� 
Name ".SokobanSetup"
OutFile ".SokobanSetup.exe"
Caption ".SokobanSetup "
UninstallCaption ".SokobanUninstall"

; ������������� � ������� �������
InstallDir $EXEDIR

; �������� XPStyle
XPStyle on

; ���� UserInterface
ChangeUI all "ui.exe"

; ���� ������ ��� ���������
CheckBitmap "checks.bmp"

; ������
Icon "install.ico"
UninstallIcon "uninstall.ico"
   
SilentUnInstall silent

Function .onGuiInit
   InitPluginsDir
   ; ������� �������
   File "/oname=$PLUGINSDIR\background.bmp" "background.bmp"
   SetBrandingImage /IMGID=1100 "$PLUGINSDIR\background.bmp"
   ; ��������� ���� �� d3dx9.dll �� ����������
   IfFileExists $WINDIR\system32\d3dx9_30.dll +8 0
   IfFileExists $WINDIR\system32\d3dx9_29.dll +7 0
   IfFileExists $WINDIR\system32\d3dx9_28.dll +6 0
   IfFileExists $WINDIR\system32\d3dx9_27.dll +5 0
   IfFileExists $WINDIR\system32\d3dx9_26.dll +4 0
   IfFileExists $WINDIR\system32\DirectX.dll  +3 0
   File "/oname=$PLUGINSDIR\warning.bmp" "warning.bmp"
   SetBrandingImage /IMGID=1101 "$PLUGINSDIR\warning.bmp"
FunctionEnd

Function un.onInit
   MessageBox MB_YESNO "Do you really want to uninstall .sokoban?" IDYES NoAbort
   Abort
   NoAbort:
FunctionEnd

; �������� �����������
PageEx components
  Caption " : Select components to install"
PageExEnd

; �������� ����������
PageEx instfiles
  Caption " : Installation..."
  PageCallbacks PreFunc ShowFunc LeaveFunc
PageExEnd

Function PreFunc
FunctionEnd

Function ShowFunc
  ; ���� ������ ��� ���� ����� ��������� ��� �������,
  ; ����������� �� � ������������� ���������
  FindWindow $0 "#32770" "" $HWNDPARENT
  SetCtlColors $0 0x000000 0xE6E7E2
FunctionEnd

Function LeaveFunc
FunctionEnd

!insertmacro INSTALL_DATA