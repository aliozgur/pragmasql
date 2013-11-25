using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using WeifenLuo.WinFormsUI.Docking;
using ComponentFactory.Krypton.Toolkit;

namespace PragmaSQL
{
  public partial class frmTextDiff : KryptonForm//DockContent
  {
    private static frmTextDiff _activeTextDiff = null;
    public static frmTextDiff ActiveTextDiff
    {
      get { return frmTextDiff._activeTextDiff; }
    }

    internal frmTextDiff( )
    {
      InitializeComponent();
    }

    

    private void diffControl_OnAfterOpenFile( object sender, TargetEditorType targetType, string fileName )
    {
      switch (targetType)
      {
        case TargetEditorType.Source:
          diffControl.SourceHeaderText = fileName;
          break;
        case TargetEditorType.Dest:
          diffControl.DestHeaderText = fileName;
          break;
        default:
          break;
      }
    }

    private void diffControl_OnAfterPasteFromClipboard( object sender, TargetEditorType targetType )
    {
      switch (targetType)
      {
        case TargetEditorType.Source:
          diffControl.SourceHeaderText = "Source [From Clipboard]";
          break;
        case TargetEditorType.Dest:
          diffControl.DestHeaderText = "Destination [From Clipboard]";
          break;
        default:
          break;
      }
    }

    private void frmTextDiff_Activated( object sender, EventArgs e )
    {
      frmTextDiff._activeTextDiff = this;
    }

    private void frmTextDiff_FormClosed( object sender, FormClosedEventArgs e )
    {
      if (frmTextDiff._activeTextDiff == this)
      {
        frmTextDiff._activeTextDiff = null;
      }
    }

    private void diffControl_OnAfterTextDragDrop( object sender, TargetEditorType targetType )
    {
      switch (targetType)
      {
        case TargetEditorType.Source:
          diffControl.SourceHeaderText = "Source [Dropped Text]";
          break;
        case TargetEditorType.Dest:
          diffControl.DestHeaderText = "Destination [Dropped Text]";
          break;
        default:
          break;
      }
    }


  }
}