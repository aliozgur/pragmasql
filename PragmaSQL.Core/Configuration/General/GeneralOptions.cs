/********************************************************************
  Class      : GeneralOptions
  Created by : Ali Özgür
  Contact    : ali_ozgur@hotmail.com
  
  Copyright: Ali Özgür - 2007
*********************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

using WeifenLuo.WinFormsUI.Docking;
using ComponentFactory.Krypton.Toolkit;

namespace PragmaSQL.Core
{
	/// <summary>
	/// General application options. 
	/// <see cref="ConfigurationContent"/>
	/// <remarks>This class is serialized as XML.</remarks>
	/// </summary>
  public class GeneralOptions
  {
    public bool WebBrowser_ShowonStart = false;
    public string WebBrowser_HomeUrl = String.Empty;
    public bool IsSingleInstance = true;
    public bool UseConnectionPooling = false;
    public PopupBlockerFilterLevel WebBrowser_PopupBlockerFilterLevel = PopupBlockerFilterLevel.Medium;
    public bool DoNotNotifyIfNotConnectedToPragmaSQLSystem = false;
    public bool RestoreWorkspace = true;
		//public bool CheckForUpdatesOnStart = true;
		public DocumentStyle HostDocumentStyle = DocumentStyle.DockingMdi;
    public bool WantEmptyScriptEditorOnStart = true;
    public PaletteModeManager PaletteMode = PaletteModeManager.ProfessionalSystem;
    public bool UseCustomPalette = false;
    public bool AutoSaveEnabled = true;
    public int AutoSaveInterval = 5;

		//public DocumentStyle DocumentStyle = DocumentStyle.
  }
} 