using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Xml.Serialization;
using ICSharpCode.Core;
using ComponentFactory.Krypton.Toolkit;

namespace HtmlHelp2.Environment
{
  public partial class HtmlHelp2OptionsDlg : KryptonForm
  {
    string selectedHelp2Collection = string.Empty;


    public HtmlHelp2OptionsDlg( )
    {
      InitializeComponent();
      InitializeF1PadTypes();
      InitializeForm();
    }

    private void InitializeF1PadTypes( )
    {
      cmbPadType.Items.Clear();
      Array values = Enum.GetValues(typeof(F1PadType));
      foreach (F1PadType type in values)
      {
        cmbPadType.Items.Add(type);
      }
    }
    
    private void InitializeForm( )
    {
      selectedHelp2Collection = HtmlHelp2Environment.DefaultNamespaceName;

      help2Collections.SelectedIndexChanged += new EventHandler(OnNamespaceNameChanged);
      tocPictures.Checked = HtmlHelp2Environment.Config.TocPictures;
      cmbPadType.SelectedItem = HtmlHelp2Environment.Config.PadType;
      chkCancelDefault.Checked = HtmlHelp2Environment.Config.CancelDefaultBehaviour;
      chkEnableForKeywordHelp.Checked = HtmlHelp2Environment.Config.UseForKeywordHelp;

      if (!Help2RegistryWalker.BuildNamespacesList(help2Collections, selectedHelp2Collection))
      {
        help2Collections.Enabled = false;
        tocPictures.Enabled = false;
      }
    }

    private void OnNamespaceNameChanged( object sender, EventArgs e )
    {
      if (help2Collections.SelectedItem != null)
      {
        selectedHelp2Collection = Help2RegistryWalker.GetNamespaceName(help2Collections.SelectedItem.ToString());
      }
    }

    private void SaveHelp2Config( )
    {
      bool reload = false;
      reload = HtmlHelp2Environment.Config.SelectedCollection != selectedHelp2Collection;
     
      HtmlHelp2Environment.Config.SelectedCollection = selectedHelp2Collection;
      HtmlHelp2Environment.Config.TocPictures = tocPictures.Checked;
      HtmlHelp2Environment.Config.PadType = (F1PadType)cmbPadType.SelectedItem;
      HtmlHelp2Environment.Config.CancelDefaultBehaviour = chkCancelDefault.Checked;
      HtmlHelp2Environment.Config.UseForKeywordHelp = chkEnableForKeywordHelp.Checked;

      HtmlHelp2Environment.SaveConfiguration();

      if (reload)
      {
        HtmlHelp2Environment.ReloadNamespace();
      }
    }

   

    private void btnOk_Click( object sender, EventArgs e )
    {
      SaveHelp2Config();
    }

    private void btnCancel_Click( object sender, EventArgs e )
    {

    }
  }

  [XmlRoot("help2environment")]
  public class HtmlHelp2Options
  {
    private string selectedCollection = string.Empty;
    private bool tocPictures;
    private bool dynamicHelpDebugInfo;
    private F1PadType padType = F1PadType.Search;
    private bool cancelDefaultBehaviour = true;
    private bool useForKeywordHelp = true;

   
    [XmlElement("collection")]
    public string SelectedCollection
    {
      get { return selectedCollection; }
      set { selectedCollection = value; }
    }

    [XmlElement("padtype")]
    public F1PadType PadType
    {
      get { return padType; }
      set { padType = value; }
    }

    [XmlElement("tocpictures")]
    public bool TocPictures
    {
      get { return tocPictures; }
      set { tocPictures = value; }
    }

    [XmlElement("canceldefaultbehaviour")]
    public bool CancelDefaultBehaviour
    {
      get { return cancelDefaultBehaviour; }
      set { cancelDefaultBehaviour = value; }
    }

    [XmlElement("useforkeywordhelp")]
    public bool UseForKeywordHelp
    {
      get { return useForKeywordHelp; }
      set { useForKeywordHelp = value; }
    }

    [XmlElement("dhdebuginfos")]
    public bool DynamicHelpDebugInfos
    {
      get { return this.dynamicHelpDebugInfo; }
      set { this.dynamicHelpDebugInfo = value; }
    }


  }

  public class ShowHelpOptionsDialog: AbstractMenuCommand
  {
    public override void Run( )
    {
      HtmlHelp2OptionsDlg frm = new HtmlHelp2OptionsDlg();
      frm.ShowDialog();
    }
  }
}