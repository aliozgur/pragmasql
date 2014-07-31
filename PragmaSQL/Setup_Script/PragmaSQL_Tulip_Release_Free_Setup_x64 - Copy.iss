; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

[Setup]
AppName=PragmaSQL
AppVerName= PragmaSQL x64 (3.1.0.9678)
AppVersion=3.1.0.9678
AppId=PragmaSQL
AppPublisher=PragmaSQL (Ali �zg�r)
AppPublisherURL=http://www.pragmasql.com/
AppSupportURL=http://www.pragmasql.com/pragmasql/support.aspx
AppUpdatesURL=http://www.pragmasql.com/pragmasql/download.aspx
AppContact=aliozgur79@gmail.com
DefaultDirName={pf}\PragmaSQL
DefaultGroupName= PragmaSQL
DisableProgramGroupPage=yes
OutputBaseFilename=PragmaSQL_Tulip_x64
VersionInfoCopyright=Copyright (C) 2007-2011 PragmaTouch and Ali �zg�r
SetupIconFile=InstallResources\install.ico
;WizardImageFile=InstallResources\PragmaSQL_Logo.bmp
WizardImageBackColor=clWhite
WizardSmallImageFile=InstallResources\PragmaSQL_Logo_Small.bmp
WizardImageStretch=no
LicenseFile=InstallResources\license.rtf
UsePreviousAppDir=yes
AppCopyright=Copyright (C) 2007-2011 PragmaTouch and Ali �zg�r
[Files]
Source: "..\bin\x64\Release\*.*"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\x64\Release\AddIns\*.*"; DestDir: "{app}\AddIns"; Flags: ignoreversion
Source: "..\bin\x64\Release\AddIns\AddInManager\*.*"; DestDir: "{app}\AddIns\AddInManager"; Flags: ignoreversion
Source: "..\bin\x64\Release\AddIns\HtmlHelp2\*.*"; DestDir: "{app}\AddIns\HtmlHelp2"; Flags: ignoreversion
Source: "..\bin\x64\Release\AddIns\SQLManagement\*.*"; DestDir: "{app}\AddIns\SQLManagement"; Flags: ignoreversion
Source: "..\bin\x64\Release\AddIns\PragmaSQL.ExternalTools\*.*"; DestDir: "{app}\AddIns\ExternalTools"; Flags: ignoreversion
Source: "..\bin\x64\Release\AddIns\PragmaSQL.VirtualResultRenderers\*.*"; DestDir: "{app}\AddIns\PragmaSQL.VirtualResultRenderers"; Flags: ignoreversion
Source: "InstallResources\ReadMe.txt"; DestDir: "{app}"; Flags: ignoreversion
Source: "InstallResources\PragmaSQL Online.url"; DestDir: "{app}"; Flags: ignoreversion
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{group}\PragmaSQL"; Filename: "{app}\PragmaSQL.exe"
Name: "{group}\ReadMe"; Filename:"{app}\ReadMe.txt"
Name: "{group}\PragmaSQLOnline"; Filename: "{app}\PragmaSQL Online.url"
Name: "{group}\Uninstall PragmaSQL"; Filename: "{uninstallexe}"

[Run]
Filename: "{app}\PragmaSQL.exe"; Description: "Run PragmaSQL"; Flags: postinstall shellexec skipifsilent skipifdoesntexist
Filename: "{app}\ReadMe.txt"; Description: "View readme"; Flags: postinstall shellexec skipifsilent skipifdoesntexist




