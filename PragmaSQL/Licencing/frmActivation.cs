/************************************************************************************************************
 * Ali Özgür
 * ali_ozgur@hotmail.com
 * www.pragmasql.com 
 * 
 * Source code included in this file can not be used without written
 * permissions of the owner mentioned above. 
 * All rigths reserver
 * Copyright PragmaSQL 2007 
 ************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Net;

using ICSharpCode.Core;
using PragmaSQL.Licencing;
using PragmaSQL.LicenceSigning;

namespace PragmaSQL
{
  public partial class frmActivation : Form
  {
    private readonly string _licFile = FileUtility.ApplicationRootPath + "\\licence.lic";
    private readonly string _demoExpireFile = FileUtility.ApplicationRootPath + "\\layout.dat";


    #region CTOR
    
    public frmActivation()
    {
			frmSplashScreen.HideSplash();
			InitializeComponent();
      InitializeValues();
    }
    
    #endregion //CTOR

    #region Initialization
    private void InitializeValues()
    {
      cmbCodeName.Items.Clear();
      cmbPurchaseType.Items.Clear();
      txtMachineKey.Text = String.Empty;

      string[] enumNames = Enum.GetNames(typeof(ProductCodeName));
      foreach (string enumName in enumNames)
      {
        cmbCodeName.Items.Add(enumName);
      }

      enumNames = Enum.GetNames(typeof(PurchaseType));
      foreach (string enumName in enumNames)
      {
        cmbPurchaseType.Items.Add(enumName);
      }
      txtMachineKey.Text = MachineIdProvider.Retrieve(MachineIdType.Composite2).Key;
    }
    #endregion Initialization


    private bool ActivatePragmaSql()
    {
      string error = String.Empty;
      /*
      if (String.IsNullOrEmpty(cmbCodeName.Text.Trim()))
      {
        error += "\r\n" + " - Product code name";
      }
      */

      if (String.IsNullOrEmpty(cmbPurchaseType.Text.Trim()))
      {
        error += "\r\n" + " - Purchase type";
      }

      if (String.IsNullOrEmpty(txtActivationKey.Text.Trim()))
      {
        error += "\r\n" + " - Activation key";
      }

      if (String.IsNullOrEmpty(txtMachineKey.Text.Trim()))
      {
        error += "\r\n" + " - Machine key";
      }

			if (!IsDemo && String.IsNullOrEmpty(txtEMail.Text.Trim()))
			{
				error += "\r\n" + " - EMail";
			}

      if (!String.IsNullOrEmpty(error))
      {
        MessageService.ShowError("Required fields listed below are empty!\r\n" + error);
        return false;
      }


      // Create request licence object 
      LicUtils licUtils = new LicUtils();
      PragmaLicense lic = new PragmaLicense();
      lic.Product = Product.PragmaSQL;
      lic.ProductCodeName = new ProductInfo().CurrentCodeName;
      
      //lic.ProductCodeName = (ProductCodeName)licUtils.ParseEnum(typeof(ProductCodeName), cmbCodeName.Text);
      lic.PurchaseType = (PurchaseType)licUtils.ParseEnum(typeof(PurchaseType), cmbPurchaseType.Text);
      lic.ActivationKey = txtActivationKey.Text;
			lic.MachineKey = new MachineID(txtMachineKey.Text);
			lic.MachineIdType = MachineIdType.Composite2;
			lic.LicType = LicType.Machine;
			lic.EMail = txtEMail.Text;


      if (lic.PurchaseType == PurchaseType.Demo)
      {
        lic.ValidFrom = DateTime.Now;
        lic.ValidTo = DateTime.Now.AddDays(121);
      }

      // Sign the licence request via web service
      string licXml =  lic.ToXmlString();
      LicenceSignSvc svc = new LicenceSignSvc();
			try
			{
				WebProxy prx = WebProxy.GetDefaultProxy();
				if (prx != null)
				{
					prx.Credentials = CredentialCache.DefaultCredentials;
					svc.Proxy = prx;
				}
			}
			catch (Exception ex)
			{
				frmException.ShowAppError("Default static proxy settings can not be retreived!", ex);			
			}

      string signedLicXml = svc.SignLicence(licXml);
      
      XmlDocument signedXml = new XmlDocument();
      signedXml.LoadXml(signedLicXml);

      // Check if resulting string is an error xml
      if (ErrorXml.IsError(signedLicXml))
      {
        ServiceCallError er = ErrorXml.CreateServiceCallError(signedXml);
        string msg = "Product can not be activated!\r\n";
        msg += "Error:" + er.ErrorMessage;
        msg += !String.IsNullOrEmpty(er.InnerErrorMessage) ? "Detail:" + er.InnerErrorMessage : String.Empty;
        MessageService.ShowError(msg);
        return false;
      }

      if (lic.PurchaseType == PurchaseType.Demo)
      {
        LayoutConfig dex = new LayoutConfig();
        dex.Items.Add(licUtils.GetSimpleDate(DateTime.Now));
        dex.SaveToFile();
      }

      signedXml.Save(_licFile);
      return true;
    }

    private void btnActivate_Click(object sender, EventArgs e)
    {
      bool result = ActivatePragmaSql();
      if (result)
      {
        MessageService.ShowMessage("PragmaSQL activated sucessfully.");
        DialogResult = DialogResult.OK;
				frmSplashScreen.ShowSplash();
				Close();
				Application.DoEvents();
      }
      else
        DialogResult = DialogResult.None;
    }

		private bool IsDemo
		{
			get { return cmbPurchaseType.SelectedItem.ToString().ToLowerInvariant() == PurchaseType.Demo.ToString().ToLowerInvariant(); }
		}
		private string prevEMail = String.Empty;
		private void cmbPurchaseType_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (IsDemo)
				prevEMail = txtEMail.Text;

			txtEMail.Text = IsDemo ? String.Empty : prevEMail;
			txtEMail.ReadOnly = IsDemo; 
		}
  }
}