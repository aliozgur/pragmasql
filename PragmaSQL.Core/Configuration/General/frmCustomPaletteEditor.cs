using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using ComponentFactory.Krypton.Toolkit;

namespace PragmaSQL.Core
{
  public partial class frmCustomPaletteEditor : ComponentFactory.Krypton.Toolkit.KryptonForm
  {
    public frmCustomPaletteEditor()
    {
      InitializeComponent();
      InitializeCustomPalette();
    }

    public static DialogResult ShowCustomPaletteEditor()
    {
      frmCustomPaletteEditor frm = new frmCustomPaletteEditor();
      return frm.ShowDialog();
    }
    
    private void InitializeCustomPalette()
    {
      if (File.Exists(ConfigHelper.CustomPaletteFileName))
        kryptonPaletteCustom.Import(ConfigHelper.CustomPaletteFileName);

      //Attribute kryptonPersist = new KryptonPersistAttribute(true);
      //propertyGrid1.BrowsableAttributes = new AttributeCollection(new Attribute[] { kryptonPersist });
      propertyGrid1.SelectedObject = kryptonPaletteCustom;
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
      try
      {
        kryptonPaletteCustom.Export(ConfigHelper.CustomPaletteFileName, true, true);
        DialogResult = DialogResult.OK;
      }
      catch(Exception ex)
      {
        GenericErrorDialog.ShowError("Palette Save Error", "Can not save custom palette.", ex);
        DialogResult = DialogResult.None;
      }
    }
  }
}