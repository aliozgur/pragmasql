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

namespace PragmaSQL.Licencing
{
  public class ServiceCallError
  {
    public string ErrorType = null;
    public string ErrorMessage = String.Empty;
    public string ErrorSource = String.Empty;

    public string InnerErrorType = null;
    public string InnerErrorMessage = String.Empty;

    public override string ToString()
    {
      return "ErrorType: " + ErrorType
        + "\r\n" + "ErrorMessage: " + ErrorMessage
        + "\r\n" + "ErrorSource: " + ErrorSource
        + "\r\n" + "InnerErrorType: " + InnerErrorType
        + "\r\n" + "InnerErrorMessage: " + InnerErrorMessage;
    }
  }

  public static class ErrorXml
  {
    public static ServiceCallError CreateServiceCallError(string  errorXml)
    {
      XmlDocument doc = new XmlDocument();
      doc.LoadXml(errorXml);
      return CreateServiceCallError(doc);
    }

    public static ServiceCallError CreateServiceCallError(XmlDocument doc)
    {
      ServiceCallError result = new ServiceCallError();
      XmlElement docEl = doc.DocumentElement;
      if (docEl.Name.ToLowerInvariant() != "pragmaserviceerror")
        throw new Exception("XML document is not a PragmaLicense");


      foreach (XmlElement e in docEl.ChildNodes)
      {

        switch (e.Name.ToLowerInvariant())
        {
          case "type":
            result.ErrorType = e.InnerText;
            break;
          case "source":
            result.ErrorSource = e.InnerText;
            break;
          case "message":
            result.ErrorMessage = e.InnerText;
            break;
          case "innertype":
            result.InnerErrorType = e.InnerText;
            break;
          case "innermessage":
            result.InnerErrorMessage = e.InnerText;
            break;
          default:
            break;
        }
      }
      return result;
    }

    public static XmlDocument Create(Exception ex)
    {
			XmlDocument doc = new XmlDocument();
      XmlElement docEl = doc.CreateElement("PragmaServiceError");
			doc.AppendChild(docEl);

      XmlElement e = doc.CreateElement("Type");
			e.InnerText = ex.GetType().Name.ToString();
			docEl.AppendChild(e);

      e = doc.CreateElement("Source");
      e.InnerText = ex.Source.ToString();
      docEl.AppendChild(e);

      e = doc.CreateElement("Message");
			e.InnerText = ex.Message;
			docEl.AppendChild(e);

      if (ex.InnerException != null)
      {
        e = doc.CreateElement("InnerType");
        e.InnerText = ex.InnerException.GetType().Name;
        docEl.AppendChild(e);

        e = doc.CreateElement("InnerMessage");
        e.InnerText = ex.InnerException.Message;
        docEl.AppendChild(e);
      }
			return doc;
    }

    public static bool IsError(string errorXml)
    {
      XmlDocument doc = new XmlDocument();
      doc.LoadXml(errorXml);
      return IsError(doc);
    }

    public static bool IsError(XmlDocument doc)
    {
      if (doc == null)
        return false;

      XmlElement docEl = doc.DocumentElement;
      return docEl.Name.ToLowerInvariant() == "pragmaserviceerror";
    }


  }
}
