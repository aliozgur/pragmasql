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
  public partial class SslServerTrustDialog : SvnDialogBase
  {
    SslServerCertificateInfo certificateInfo;
    SslFailures failures;

    public SslServerTrustDialog( )
    {
      InitializeComponent();
    }

    public SslServerTrustDialog( SslServerCertificateInfo certificateInfo, SslFailures failures, bool maySave )
      : this()
    {
      this.CertificateInfo = certificateInfo;
      this.Failures = failures;
      this.MaySave = maySave;
    }


    public SslServerCertificateInfo CertificateInfo
    {
      get { return certificateInfo; }
      set
      {
        certificateInfo = value;
        UpdateCertificateInfo();
      }
    }

    public SslFailures Failures
    {
      get { return failures; }
      set
      {
        failures = value;
        UpdateFailures();
      }
    }

    public bool MaySave
    {
      get { return saveCredentialsCheckBox.Checked; }
      set { saveCredentialsCheckBox.Checked = value; }
    }


    public SslServerTrustCredential Credential
    {
      get
      {
        SslServerTrustCredential cred = new SslServerTrustCredential();
        cred.AcceptedFailures = failures;
        cred.MaySave = MaySave;
        return cred;
      }
    }



    void UpdateCertificateInfo( )
    {
      if (certificateInfo != null)
      {
        hostNameLabel.Text = certificateInfo.HostName;
        fingerPrintlabel.Text = certificateInfo.FingerPrint;
        validLabel.Text = "From " + certificateInfo.ValidFrom + " to " + certificateInfo.ValidUntil;
        issuerLabel.Text = certificateInfo.Issuer;
        certificateTextBox.Text = certificateInfo.AsciiCertificate;
      }
      else
      {
        hostNameLabel.Text = String.Empty;
        fingerPrintlabel.Text = String.Empty;
        validLabel.Text = String.Empty;
        issuerLabel.Text = String.Empty;
        certificateTextBox.Text = String.Empty;
      }
    }

    bool HasFailures( SslFailures testFailures )
    {
      return (failures & testFailures) == testFailures;
    }

    void UpdateFailures( )
    {
      if (HasFailures(SslFailures.CertificateAuthorityUnknown))
      {
        certificateAuthorityStatusLabel.Text = "The issuing certificate authority(CA) is not trusted.";
        certificateAuthorityStatusLabel.ForeColor = Color.Red;
      }
      else
      {
        certificateAuthorityStatusLabel.Text = "The issuing certificate authority(CA) is known and trusted.";
        certificateAuthorityStatusLabel.ForeColor = Color.Green;
      }

      if (HasFailures(SslFailures.CertificateNameMismatch))
      {
        certificateNameStatusLabel.Text = "The certificate's hostname does not match the hostname of the server.";
        certificateNameStatusLabel.ForeColor = Color.Red;
      }
      else
      {
        certificateNameStatusLabel.Text = "The certificate's hostname matches the hostname of the server.";
        certificateNameStatusLabel.ForeColor = Color.Green;
      }

      if (HasFailures(SslFailures.Expired))
      {
        certificateDateStatusLabel.Text = "The server certificate has expired.";
        certificateDateStatusLabel.ForeColor = Color.Red;
      }
      else if (HasFailures(SslFailures.NotYetValid))
      {
        certificateDateStatusLabel.Text = "The server certificate is not yet valid.";
        certificateDateStatusLabel.ForeColor = Color.Red;
      }
      else
      {
        certificateDateStatusLabel.Text = "The server certificate date is valid.";
        certificateDateStatusLabel.ForeColor = Color.Green;
      }

    }
  }
}