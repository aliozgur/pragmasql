using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using NSvn.Core;

namespace PragmaSQL.Svn.Gui
{
  public partial class ClientCertDialog : SvnDialogBase
  {
    public ClientCertDialog( )
    {
      InitializeComponent();
    }

    public ClientCertDialog( string realm, bool maySave )
      : this()
    {
      realmLabel.Text = realm;
      saveCredentialsCheckBox.Checked = maySave;
    }

    public string Realm
    {
      get { return realmLabel.Text; }
      set { realmLabel.Text = value; }
    }

    public string FileName
    {
      get { return pathTextBox.Path; }
      set { pathTextBox.Path = value; }
    }

    public bool MaySave
    {
      get { return saveCredentialsCheckBox.Checked; }
      set { saveCredentialsCheckBox.Checked = value; }
    }

    public SslClientCertificateCredential Credential
    {
      get
      {
        SslClientCertificateCredential cred = new SslClientCertificateCredential();
        cred.CertificateFile = FileName;
        cred.MaySave = MaySave;
        return cred;
      }
    }
  }
}