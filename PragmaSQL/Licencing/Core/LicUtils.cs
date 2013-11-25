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

using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;


namespace PragmaSQL.Licencing
{
	public sealed class LicUtils
	{
    private string _pKey = String.Empty;
    public string PKey
    {
      get
      {
        return new SteganoUtils().ExtractKeyFromBitmaps(Properties.Resources.AppBackground_Stones, Properties.Resources._00_me);
      }
    }

    public string GenerateEncryptedActivationKey(string key, PurchaseType type)
    {
      return Encrypt(key, PKey + "_" + type.ToString());
    }

    public string DecryptActivationKey(string key, PurchaseType type)
    {
      return Decrypt(key, PKey + "_" + type.ToString());
    }

		public string GetXmlContent(XmlDocument doc)
		{
			Stream xmlContent = null;
			StreamReader contentReader = null;
			XmlTextWriter xmlWriter = null;
			try
			{
				xmlContent = new MemoryStream();
				xmlWriter = new XmlTextWriter(xmlContent, Encoding.UTF8);
				xmlWriter.Formatting = Formatting.Indented;
				doc.WriteContentTo(xmlWriter);
				xmlWriter.Flush();

				contentReader = new StreamReader(xmlContent);
				xmlContent.Seek(0, SeekOrigin.Begin);
				return contentReader.ReadToEnd();
			}
			finally
			{
				if (xmlContent != null)
					xmlContent.Close();

				if (xmlWriter != null)
					xmlWriter.Close();
			}
		}

    public string GetXmlContent(XmlNode node)
    {
      Stream xmlContent = null;
      StreamReader contentReader = null;
      XmlTextWriter xmlWriter = null;
      try
      {
        xmlContent = new MemoryStream();
        xmlWriter = new XmlTextWriter(xmlContent, Encoding.UTF8);
        xmlWriter.Formatting = Formatting.Indented;
        node.WriteContentTo(xmlWriter);
        xmlWriter.Flush();

        contentReader = new StreamReader(xmlContent);
        xmlContent.Seek(0, SeekOrigin.Begin);
        return contentReader.ReadToEnd();
      }
      finally
      {
        if (xmlContent != null)
          xmlContent.Close();

        if (xmlWriter != null)
          xmlWriter.Close();
      }
    }

		public XmlElement CreateLicElement(XmlDocument doc, PragmaLicElements type)
		{
			return doc.CreateElement(type.ToString());
		}

		public object ParseEnum(Type enumType,string value)
		{
			string info = String.Empty;
			try
			{
				return Enum.Parse(enumType, value);
			}
			catch
			{
				info = (String.IsNullOrEmpty(value) ? "[Empty]" : value ) + " not defined for " + enumType.Name + " enumeration type!";
				throw new ArgumentException("Undefined enumeration value!" + info);
			}
		}

	  #region License request functionality
    
    public PragmaLicense FromEncryptedXml(string enLicenceXml)
    {
      SteganoUtils su = new SteganoUtils();

      string licXml = Decrypt(enLicenceXml, PKey);
      return this.FromXmlString(licXml);
    }

		public PragmaLicense FromXmlString(string xml)
		{
			PragmaLicense result = new PragmaLicense();

			XmlDocument doc = new XmlDocument();
			doc.LoadXml(xml);
			XmlElement docEl = doc.DocumentElement;

			if (docEl.Name.ToLowerInvariant() != "pragmalicense")
				throw new Exception("XML document is not a PragmaLicense");


			DateTimeFormatInfo fi = new DateTimeFormatInfo();
			fi.SetAllDateTimePatterns(new string[1] { "dd.MM.yyyy" }, 'd');

			foreach (XmlElement e in docEl.ChildNodes)
			{
				
				switch (e.Name.ToLowerInvariant())
				{
					case "product":
						result.Product = (Product)ParseEnum(typeof(Product), e.InnerText);
						break;
					case "productcodename":
						result.ProductCodeName = (ProductCodeName)ParseEnum(typeof(ProductCodeName), e.InnerText);
						break;
					case "activationkey":
						result.ActivationKey = e.InnerText;
						break;
          case "email":
            result.EMail = e.InnerText;
            break;
          case "purchasetype":
						result.PurchaseType = (PurchaseType)ParseEnum(typeof(PurchaseType), e.InnerText);
						break;
					case "lictype":
						result.LicType = (LicType)ParseEnum(typeof(LicType), e.InnerText);
						break;
					case "validfrom":
						result.ValidFrom = String.IsNullOrEmpty(e.InnerText) ? null : (DateTime?)DateTime.Parse(e.InnerText, fi);
						break;
					case "validto":
						result.ValidTo = String.IsNullOrEmpty(e.InnerText) ? null : (DateTime?)DateTime.Parse(e.InnerText, fi);
						break;
					case "machinekey":
						result.MachineKey = new MachineID(e.InnerText);
						break;
					case "machineidtype":
						if (String.IsNullOrEmpty(e.InnerText))
							result.MachineIdType = MachineIdType.Simple;
						else
							result.MachineIdType = (MachineIdType)ParseEnum(typeof(MachineIdType), e.InnerText);
						break;
					default:
						break;
				}
			}

			return result;
		}

		public PragmaLicense FromXmlFile(string fileName)
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(fileName);
			return FromXmlDocument(doc);
		}

		public PragmaLicense FromXmlDocument(XmlDocument doc)
		{
			Stream xmlContent = null;
			StreamReader contentReader = null;
			XmlTextWriter xmlWriter = null;
			try
			{
				xmlContent = new MemoryStream();
				xmlWriter = new XmlTextWriter(xmlContent, Encoding.UTF8);
				doc.WriteContentTo(xmlWriter);
				xmlWriter.Flush();

				contentReader = new StreamReader(xmlContent);
				xmlContent.Seek(0, SeekOrigin.Begin);
				return FromXmlString(contentReader.ReadToEnd());
			}
			finally
			{
				if (xmlContent != null)
					xmlContent.Close();

				if (xmlWriter != null)
					xmlWriter.Close();
			}
		}

		#endregion //License request functionality

		#region License signing functionality

		public XmlDocument SignRequest(PragmaLicense lic)
		{
			XmlDocument requestDoc = lic.ToXmlDoc();

			// Get the key pair from the key store.
			CspParameters parms = new CspParameters(1); // PROV_RSA_FULL
			parms.Flags = CspProviderFlags.UseMachineKeyStore;// Use Machine store
			parms.KeyContainerName = "PragmaSQL";
			parms.KeyNumber = 2; // AT_SIGNATURE
			RSACryptoServiceProvider csp = new RSACryptoServiceProvider(parms);

			// Creating the XML signing object.
			SignedXml sxml = new SignedXml(requestDoc);
			sxml.SigningKey = csp;

			// Set the canonicalization method for the document.
			sxml.SignedInfo.CanonicalizationMethod =
				SignedXml.XmlDsigCanonicalizationUrl; // No comments.

			// Create an empty reference (not enveloped) for the XPath
			// transformation.
			Reference r = new Reference("");

			// Create the XPath transform and add it to the reference list.
			r.AddTransform(new XmlDsigEnvelopedSignatureTransform(false));

			// Add the reference to the SignedXml object.
			sxml.AddReference(r);

			// Compute the signature.
			sxml.ComputeSignature();

			// Get the signature XML and add it to the document element.
			XmlElement sig = sxml.GetXml();
			requestDoc.DocumentElement.AppendChild(sig);

			return requestDoc;
		}
		public XmlDocument SignRequest(string fileName)
		{
			PragmaLicense lic = FromXmlFile(fileName);
			return SignRequest(lic);
		}

		#endregion //License signing functionality


		#region License verification functionality

		public bool VerifyLicense(string publicKey, XmlDocument xmlLic)
		{
			RSACryptoServiceProvider csp = new RSACryptoServiceProvider();
			csp.FromXmlString(publicKey);

			// Create the signed XML object.
			SignedXml sxml = new SignedXml(xmlLic);

			try
			{
				// Get the XML Signature node and load it into the signed XML object.
				XmlNode dsig = xmlLic.GetElementsByTagName("Signature",
					SignedXml.XmlDsigNamespaceUrl)[0];
				sxml.LoadXml((XmlElement)dsig);
			}
			catch
			{
				throw new Exception("Error: no signature found.");
			}

			// Verify the signature.
			if (sxml.CheckSignature(csp))
				return true;
			else
				return false;
		}
		
		public bool VerifyLicense(string publicKey, string xmlLicFile)
		{
			XmlDocument xmlLic = new XmlDocument();
			xmlLic.Load(xmlLicFile);
			return VerifyLicense(publicKey, xmlLic);
		}

    public bool VerifyLicense(string xmlLicFile)
    {
      string publicKey = new SteganoUtils().ExtractKeyFromBitmaps();
      XmlDocument xmlLic = new XmlDocument();
      xmlLic.Load(xmlLicFile);
      return VerifyLicense(publicKey, xmlLic);
    }

		public bool VerifyAndAcceptLicense(Product product, ProductCodeName codeName, string publicKey, XmlDocument doc)
		{
			if (!VerifyLicense(publicKey, doc))
			{
				return false;
			}

			PragmaLicense lic = FromXmlDocument(doc);
			MachineID mId = MachineIdProvider.Retrieve(lic.MachineIdType);
			return ( mId.Key.ToLowerInvariant() == lic.MachineKey.Key.ToLowerInvariant()) && ( lic.ProductCodeName == codeName )
				&& (lic.Product == product);
		}

		public bool VerifyAndAcceptLicense(Product product, ProductCodeName codeName, string publicKey, string fileName)
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(fileName);
			return VerifyAndAcceptLicense(product,codeName,publicKey, doc);
		}

 

		#endregion //License verification functionality

    #region Encryption Helpers
    private const string PASSWORD = "{A1830210-2F11-481c-8243-5722CC0C910C}";

    public string Encrypt(string clearText, string pwd)
    {
      string Password = pwd;
      byte[] clearBytes = System.Text.Encoding.Unicode.GetBytes(clearText);
      PasswordDeriveBytes pdb = new PasswordDeriveBytes(Password,
        new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });

      MemoryStream ms = new MemoryStream();
      Rijndael alg = Rijndael.Create();

      alg.Key = pdb.GetBytes(32);
      alg.IV = pdb.GetBytes(16);

      CryptoStream cs = new CryptoStream(ms, alg.CreateEncryptor(), CryptoStreamMode.Write);

      cs.Write(clearBytes, 0, clearBytes.Length);

      cs.Close();

      byte[] encryptedData = ms.ToArray();

      return Convert.ToBase64String(encryptedData);
    }

    public string Encrypt(string clearText)
    {
      return Encrypt(clearText, PASSWORD);
    }

    public string Decrypt(string cipherText, string pwd)
    {
      string Password = pwd;
      byte[] cipherBytes = Convert.FromBase64String(cipherText);

      PasswordDeriveBytes pdb = new PasswordDeriveBytes(Password,
        new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });

      MemoryStream ms = new MemoryStream();

      Rijndael alg = Rijndael.Create();
      alg.Key = pdb.GetBytes(32);
      alg.IV = pdb.GetBytes(16);

      CryptoStream cs = new CryptoStream(ms, alg.CreateDecryptor(), CryptoStreamMode.Write);

      cs.Write(cipherBytes, 0, cipherBytes.Length);
      cs.Close();
      byte[] decryptedData = ms.ToArray();

      return System.Text.Encoding.Unicode.GetString(decryptedData);
    }

    public string Decrypt(string cipherText)
    {
      return Decrypt(cipherText, PASSWORD);
    }

    public string SteganoEncrypt(string clearText)
    {
      string pwd = new SteganoUtils().ExtractKeyFromBitmaps();
      return Encrypt(clearText, pwd);
    }

    public string SteganoDecrypt(string cipherText)
    {
      string pwd = new SteganoUtils().ExtractKeyFromBitmaps();
      return Decrypt(cipherText, pwd);
    }

    #endregion //Encryption Helpers

    #region General Utils
    
    public string GetSimpleDate(DateTime val)
    {
      return val.Day.ToString("00") + "." + val.Month.ToString("00") + "." + val.Year.ToString();
    }

    #endregion //General Utils
  }
}
