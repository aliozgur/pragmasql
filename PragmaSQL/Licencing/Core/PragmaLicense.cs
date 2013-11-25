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
using System.Text;
using System.Xml;
using System.IO;
using System.Globalization;

namespace PragmaSQL.Licencing
{
	public sealed class PragmaLicense
	{
   
    private Product _product = Product.None;
		public Product Product
		{
			get { return _product; }
			set { _product = value; }
		}

		private ProductCodeName _productCodeName = ProductCodeName.None;
		public ProductCodeName ProductCodeName
		{
			get { return _productCodeName; }
			set { _productCodeName = value; }
		}

		private string _activationKey;
		public string ActivationKey
		{
			get { return _activationKey; }
			set { _activationKey = value; }
		}

		private PurchaseType _purchaseType = PurchaseType.None;
		public PurchaseType PurchaseType
		{
			get { return _purchaseType; }
			set { _purchaseType = value; }
		}

		private LicType _licType = LicType.None;
		public LicType LicType
		{
			get { return _licType; }
			set { _licType = value; }
		}
	
		private DateTime? _validFrom;
		public DateTime? ValidFrom
		{
			get { return _validFrom; }
			set { _validFrom = value; }
		}

		private DateTime? _validTo;
		public DateTime? ValidTo
		{
			get { return _validTo; }
			set { _validTo = value; }
		}

		
		private MachineID _machineKey;
		public MachineID MachineKey
		{
			get { return _machineKey; }
			set { _machineKey = value; }
		}

		private MachineIdType _machineIdType = MachineIdType.Simple;
		public MachineIdType MachineIdType
		{
			get { return _machineIdType; }
			set { _machineIdType = value; }
		}
		
		private string _email;
    public string EMail
    {
      get { return _email; }
      set { _email = value; }
    }

    private LicUtils _licUtils = new LicUtils();


    private string _pKey = String.Empty;
    public string PKey
    {
      get 
      {
        return new SteganoUtils().ExtractKeyFromBitmaps(Properties.Resources.AppBackground_Stones, Properties.Resources._00_me);
      }
    }

		#region XML Generation

		public XmlDocument ToXmlDoc()
		{
			XmlDocument doc = new XmlDocument();
			XmlElement docEl = _licUtils.CreateLicElement(doc,PragmaLicElements.PragmaLicense);
			doc.AppendChild(docEl);

			XmlElement e = _licUtils.CreateLicElement(doc, PragmaLicElements.Product);
			e.InnerText = _product.ToString();
			docEl.AppendChild(e);

			e = _licUtils.CreateLicElement(doc, PragmaLicElements.ProductCodeName);
			e.InnerText = _productCodeName.ToString();
			docEl.AppendChild(e);

			e = _licUtils.CreateLicElement(doc, PragmaLicElements.ActivationKey);
			e.InnerText = _activationKey;
			docEl.AppendChild(e);

			e = _licUtils.CreateLicElement(doc, PragmaLicElements.LicType);
			e.InnerText = _licType.ToString();
			docEl.AppendChild(e);

			e = _licUtils.CreateLicElement(doc, PragmaLicElements.PurchaseType);
			e.InnerText = _purchaseType.ToString();
			docEl.AppendChild(e);

			e = _licUtils.CreateLicElement(doc, PragmaLicElements.ValidFrom);
			e.InnerText = !_validFrom.HasValue ? String.Empty : GetSimpleDate(_validFrom.Value);
			docEl.AppendChild(e);

			e = _licUtils.CreateLicElement(doc, PragmaLicElements.ValidTo);
			e.InnerText = !_validTo.HasValue ? String.Empty : GetSimpleDate(_validTo.Value);
			docEl.AppendChild(e);



			e = _licUtils.CreateLicElement(doc, PragmaLicElements.MachineKey);
			e.InnerText = _machineKey.Key;
			docEl.AppendChild(e);

			e = _licUtils.CreateLicElement(doc, PragmaLicElements.MachineIdType);
			e.InnerText = _machineIdType.ToString();
			docEl.AppendChild(e);

			
			e = _licUtils.CreateLicElement(doc, PragmaLicElements.EMail);
      e.InnerText = _email;
      docEl.AppendChild(e);
      
      return doc;
		}

		public string ToXmlString()
		{
			XmlDocument doc = this.ToXmlDoc();
			return _licUtils.GetXmlContent(doc);
		}

		public void ToFile(string fileName)
		{
			XmlDocument doc = this.ToXmlDoc();
			doc.Save(fileName);
		}

    public string ToEncryptedXmlString()
    {
      return new LicUtils().Encrypt(this.ToXmlString()); 
    }

		#endregion //XML Generation

		private string GetSimpleDate(DateTime val)
		{
			return val.Day.ToString("00") + "." + val.Month.ToString("00") + "." + val.Year.ToString();
		}


		public override string ToString()
		{
			return
				"Product: " + _product
				+ "\r\n" + "ProductCodeName: " + _productCodeName
				+ "\r\n" + "ActivationKey: " + _activationKey
        + "\r\n" + "EMail: " + _email
        + "\r\n" + "Type: " + _purchaseType
				+ "\r\n" + "Purchase Type: " + _licType
        + (!_validFrom.HasValue ? String.Empty : "\r\n" + "Valid From: " + _validFrom.Value.ToString())
				+ (!_validTo.HasValue ? String.Empty : "\r\n" + "Valid To: " + _validTo.Value.ToString())
				+ "\r\n" + "MachineKey: " + _machineKey.Key
				+ "\r\n" + "MachineIdType: " + _machineIdType.ToString();

		}	
	}
}
