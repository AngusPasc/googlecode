object MainForm: TMainForm
  Left = 3
  Top = 71
  BorderStyle = bsNone
  Caption = 'MainForm'
  ClientHeight = 704
  ClientWidth = 1009
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'MS Sans Serif'
  Font.Style = []
  OldCreateOrder = False
  OnClose = FormClose
  OnCreate = FormCreate
  OnKeyDown = FormKeyDown
  OnKeyUp = FormKeyUp
  PixelsPerInch = 96
  TextHeight = 13
  object DXDraw: TDXDraw
    Left = 0
    Top = 0
    Width = 1009
    Height = 704
    AutoInitialize = True
    AutoSize = True
    Color = clBtnFace
    Display.FixedBitCount = True
    Display.FixedRatio = True
    Display.FixedSize = False
    Options = [doAllowReboot, doWaitVBlank, doCenter, doDirectX7Mode, doHardware, doSelectDriver]
    SurfaceHeight = 704
    SurfaceWidth = 1009
    Align = alClient
    TabOrder = 0
    OnClick = DXDrawClick
    OnMouseDown = DXDrawMouseDown
    OnMouseMove = DXDrawMouseMove
    OnMouseUp = DXDrawMouseUp
  end
  object DXTimer: TDXTimer
    ActiveOnly = False
    Enabled = True
    Interval = 0
    OnTimer = DXTimerTimer
    Left = 8
    Top = 8
  end
  object DXWaveList: TDXWaveList
    DXSound = DXSound
    Items = <>
    Left = 40
    Top = 40
  end
  object DXImageList: TDXImageList
    DXDraw = DXDraw
    Items.ColorTable = {
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000}
    Items = <>
    Left = 40
    Top = 72
  end
  object DXSpriteEngine: TDXSpriteEngine
    DXDraw = DXDraw
    Left = 8
    Top = 72
  end
  object DXInput: TDXInput
    ActiveOnly = True
    Joystick.BindInputStates = True
    Joystick.Effects.Effects = {
      FF0A0044454C50484958464F524345464545444241434B454646454354003010
      7F000000545046301D54466F726365466565646261636B456666656374436F6D
      706F6E656E74025F3107456666656374730E01044E616D650607456666656374
      730A45666665637454797065070665744E6F6E6506506572696F64023205506F
      7765720310270454696D6503E8030E537461727444656C617954696D65020000
      000000}
    Joystick.Enabled = True
    Joystick.ForceFeedback = False
    Joystick.AutoCenter = True
    Joystick.DeadZoneX = 50
    Joystick.DeadZoneY = 50
    Joystick.DeadZoneZ = 50
    Joystick.ID = 0
    Joystick.RangeX = 1000
    Joystick.RangeY = 1000
    Joystick.RangeZ = 1000
    Keyboard.BindInputStates = True
    Keyboard.Effects.Effects = {
      FF0A0044454C50484958464F524345464545444241434B454646454354003010
      7F000000545046301D54466F726365466565646261636B456666656374436F6D
      706F6E656E74025F3107456666656374730E01044E616D650607456666656374
      730A45666665637454797065070665744E6F6E6506506572696F64023205506F
      7765720310270454696D6503E8030E537461727444656C617954696D65020000
      000000}
    Keyboard.Enabled = True
    Keyboard.ForceFeedback = False
    Keyboard.Assigns = {
      4B00000026000000680000002000000000000000000000004800000025000000
      640000004C00000027000000660000005A000000200000000000000058000000
      0D00000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000071000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000000000000000}
    Mouse.BindInputStates = False
    Mouse.Effects.Effects = {
      FF0A0044454C50484958464F524345464545444241434B454646454354003010
      7F000000545046301D54466F726365466565646261636B456666656374436F6D
      706F6E656E74025F3107456666656374730E01044E616D650607456666656374
      730A45666665637454797065070665744E6F6E6506506572696F64023205506F
      7765720310270454696D6503E8030E537461727444656C617954696D65020000
      000000}
    Mouse.Enabled = False
    Mouse.ForceFeedback = False
    UseDirectInput = True
    Left = 40
    Top = 8
  end
  object DXSound: TDXSound
    AutoInitialize = True
    Options = []
    Left = 8
    Top = 40
  end
end
