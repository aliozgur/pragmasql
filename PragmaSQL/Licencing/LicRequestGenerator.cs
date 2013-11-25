using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace PragmaSQL.Licencing
{
	public partial class LicRequestGenerator : UserControl
	{
		public LicRequestGenerator()
		{
			InitializeComponent();
			InitializeCombos();
		}


    private LicUtils _licUtils = new LicUtils();

    private void InitializeCombos()
		{
			cmbProduct.Items.Clear();
			cmbProductCodeName.Items.Clear();
			cmbPurchaseType.Items.Clear();

			string[] enumNames = Enum.GetNames(typeof(Product));
			foreach (string enumName in enumNames)
			{
				cmbProduct.Items.Add(enumName);
			}

			enumNames = Enum.GetNames(typeof(ProductCodeName));
			foreach (string enumName in enumNames)
			{
				cmbProductCodeName.Items.Add(enumName);
			}

			enumNames = Enum.GetNames(typeof(PurchaseType));
			foreach (string enumName in enumNames)
			{
				cmbPurchaseType.Items.Add(enumName);
			}

			enumNames = Enum.GetNames(typeof(LicType));
			foreach (string enumName in enumNames)
			{
				cmbLicType.Items.Add(enumName);
			}
		}

		public PragmaLicense LoadLicenseRequestFromFile(string fileName, bool preview)
		{
			txtOutPut.Text = String.Empty;

			PragmaLicense lic = new LicUtils().FromXmlFile(fileName);

			cmbProduct.FindStringExact(lic.Product.ToString());
			cmbProduct.Text = lic.Product.ToString();

			cmbProductCodeName.FindStringExact(lic.ProductCodeName.ToString());
			cmbProductCodeName.Text = lic.ProductCodeName.ToString();

			txtActivationKey.Text = lic.ActivationKey;
      txtEMail.Text = lic.EMail;
      cmbPurchaseType.FindStringExact(lic.PurchaseType.ToString());
			cmbPurchaseType.Text = lic.PurchaseType.ToString();


			txtMachineKey.Text = lic.MachineKey.Key;
			if (preview)
				txtOutPut.Text = lic.ToXmlString();

			statInfo.Text = "Loaded license request from: " + fileName;
			return lic;
		}

		private PragmaLicense GenerateLicenseRequest(string fileName, bool preview)
		{
			txtOutPut.Text = String.Empty;
			PragmaLicense lic = new PragmaLicense();
			lic.Product = (Product)_licUtils.ParseEnum(typeof(Product), cmbProduct.Text);
			lic.ProductCodeName = (ProductCodeName)_licUtils.ParseEnum(typeof(ProductCodeName), cmbProductCodeName.Text); 
			lic.ActivationKey = txtActivationKey.Text;
      lic.EMail = txtEMail.Text;
			lic.PurchaseType = (PurchaseType)_licUtils.ParseEnum(typeof(PurchaseType), cmbPurchaseType.Text);
			lic.LicType = (LicType)_licUtils.ParseEnum(typeof(LicType), cmbLicType.Text);

			if (lic.PurchaseType == PurchaseType.Demo)
			{
				lic.ValidFrom = DateTime.Now;
				lic.ValidTo = lic.ValidFrom.Value.AddDays(30);
			}
			else if (lic.PurchaseType == PurchaseType.Purchase)
			{
				lic.ValidFrom = null;
				lic.ValidTo = null;
			}

			lic.MachineKey = new MachineID(txtMachineKey.Text);
			if (!String.IsNullOrEmpty(fileName))
			{
				lic.ToFile(fileName);
				statInfo.Text = "Saved license request from: " + fileName;
			}
			else
			{
				statInfo.Text = "Generated license request.";
			}

			if (preview)
				txtOutPut.Text = lic.ToXmlString();
			return lic;
		}

		private void SendLicRequestAsEMail()
		{
			string cmd = "mailto:ali.ozgur@pragmasql.com?subject=PragmaSQL%20License%20Request&body=" + txtOutPut.Text;
			try
			{
				System.Diagnostics.Process.Start(cmd);
			}
			catch (Exception ex)
			{
				MessageBox.Show("Can not generate error e-mail!\nError was: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void toolStripButton2_Click(object sender, EventArgs e)
		{
			if (openFileDialog1.ShowDialog() != DialogResult.OK)
				return;

			LoadLicenseRequestFromFile(openFileDialog1.FileName, true);
		}

		private void GenLicNormal_Click(object sender, EventArgs e)
		{
			DialogResult dlgRes = MessageBox.Show("Do you want to save generated license request to file?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
			string fileName = String.Empty;
			if (dlgRes == DialogResult.Yes)
			{
				if (!String.IsNullOrEmpty(cmbProduct.Text) && !String.IsNullOrEmpty(cmbProductCodeName.Text))
					saveFileDialog1.FileName = cmbProduct.Text + "_" + cmbProductCodeName.Text + "_License";
				if (saveFileDialog1.ShowDialog() != DialogResult.OK)
					return;
				fileName = saveFileDialog1.FileName;
				saveFileDialog1.FileName = String.Empty;
			}

			GenerateLicenseRequest(fileName, true);
		}

		private void button2_Click(object sender, EventArgs e)
		{
			MachineID id = MachineIdProvider.Retrieve();
			txtMachineKey.Text = id.Key;
		}

		private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start("mailto:ali.ozgur@pragmasql.com?Subject=PragmaSQL");
		}

		private void linkLabel12_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start("http://www.pragmasql.com");
		}

		private void toolStripButton4_Click(object sender, EventArgs e)
		{
			SendLicRequestAsEMail();
		}

		private void toolStripButton3_Click(object sender, EventArgs e)
		{
			MessageBox.Show("Not implemented yet!","Info",MessageBoxButtons.OK,MessageBoxIcon.Warning);
		}
	}
}
