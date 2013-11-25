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
  public partial class ClientCertPassphraseDialog : SvnDialogBase
  {
    public ClientCertPassphraseDialog( )
    {
      InitializeComponent();
    }

    public ClientCertPassphraseDialog( string realm, bool maySave )
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

    public string Passphrase
    {
      get { return passPhraseTextBox.TextBoxText; }
      set { passPhraseTextBox.TextBoxText = value; }
    }

    public bool MaySave
    {
      get { return saveCredentialsCheckBox.Checked; }
      set { saveCredentialsCheckBox.Checked = value; }
    }

    public SslClientCertificatePasswordCredential Credential
    {
      get
      {
        SslClientCertificatePasswordCredential cred = new SslClientCertificatePasswordCredential();
        cred.Password = Passphrase;
        cred.MaySave = MaySave;
        return cred;
      }
    }
  }
}