/********************************************************************
  Class      : frmOpenSharedSnippet
  Created by : Ali Özgür
  Contact    : ali_ozgur@hotmail.com

  Copyright  : Ali Özgür - 2007
*********************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using PragmaSQL.Core;
using ComponentFactory.Krypton.Toolkit;

namespace PragmaSQL
{
  public enum SharedSnippetSelectDialogMode
  {
    Open,
    Save
  }

  public partial class frmSharedSnippetSelectDialog : KryptonForm
  {
    public frmSharedSnippetSelectDialog( )
    {
#if PERSONAL_EDITION
      throw new PersonalEditionLimitation();
#endif
      InitializeComponent();
      ucSharedSnippets1.Mode = SharedSnippetMode.Open;
      ucSharedSnippets1.InitializeAddInSupport();
    }

    public SharedSnippetItemData SelectedItem
    {
      get
      {
        return ucSharedSnippets1.SelectedNodeData;
      }
    }

    public ucSharedSnippets SharedSnippetsControl
    {
      get
      {
        return ucSharedSnippets1;
      }
    }
  }
}