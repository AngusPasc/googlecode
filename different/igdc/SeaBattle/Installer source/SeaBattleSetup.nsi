!include "Sections.nsh"

; ����������
SetCompressor /SOLID lzma

; �������� 
Name ".SeaBattleSetup"
OutFile ".SeaBattleSetup.exe"
Caption ".SeaBattleSetup "
UninstallCaption ".SeaBattleUninstall"

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
   File "/oname=$PLUGINSDIR\sfxlogo.bmp" "background.bmp"
   SetBrandingImage /IMGID=1100 "$PLUGINSDIR\sfxlogo.bmp"   
FunctionEnd

Function un.onInit
   MessageBox MB_YESNO "Do you really want to uninstall .SeaBattle?" IDYES NoAbort
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