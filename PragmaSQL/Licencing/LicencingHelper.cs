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
using System.IO;
using System.Windows.Forms;

using ICSharpCode.Core;
using PragmaSQL.Licencing;

namespace PragmaSQL
{
  internal class LicencingHelper
  {
    internal readonly string _licFile = FileUtility.ApplicationRootPath + "\\licence.lic";
    private readonly string _demoExpireFile = FileUtility.ApplicationRootPath + "\\layout.dat";

    internal PragmaLicense LoadLicence()
    {
      if (!File.Exists(_licFile))
        return null;

      LicUtils utils = new LicUtils();
      return utils.FromXmlFile(_licFile);
    }

    public bool VerifyLicence(out string message)
    {
      // 1- Try to load licence from licence file.
      LicUtils utils = new LicUtils();
      PragmaLicense lic = LoadLicence();
      message = String.Empty;


      // 2- No licence file. 
      if (lic == null)
      {
        message = "No license found.\r\n";
        //message = _licFile;
        return false;
      }

      // 2- Verify the licence.
      try
      {
        if (!utils.VerifyLicense(_licFile))
        {
          message = "You are trying to use an invalid license.\r\nPlease visit www.PragmaSQL.com to purchase a valid license.";
          return false;
        }
      }
      catch (Exception ex)
      {
        message = "Error:" + ex.Message;
        return false;
      }

      MachineID current = MachineIdProvider.Retrieve(lic.MachineIdType);
      if (current.Key != lic.MachineKey.Key)
      {
        message = "You are trying to use a license which was not issued to you.\r\nPlease visit www.PragmaSQL.com to purchase a valid license.";
        return false;
      }

      if (new ProductInfo().CurrentCodeName != lic.ProductCodeName)
      {
        message = "You are trying to use a license which was not issued for this version.\r\nPlease visit www.PragmaSQL.com to purchase a valid license.";
        return false;
      }

      // 3- If licence is a demo licence check existance of the expire file
      if (lic.PurchaseType == PurchaseType.Demo)
      {
        try
        {
          if (!File.Exists(_demoExpireFile))
          {
            message = "Demo expire date can not be determined!\r\nPlease visit www.PragmaSQL.com to purchase a license.";
            return false;          
          }

          if (!ExpireDemoLicence(lic))
          {
            message = "Your demo period expired!\r\nPlease visit www.PragmaSQL.com to purchase a license.";
            return false;
          }
        }
        catch (Exception ex)
        {
          message = ex.Message;
          return false;
        }
      }
      
      return true;
    }

    private bool ExpireDemoLicence(PragmaLicense lic)
    {
      if (lic != null && lic.PurchaseType == PurchaseType.Demo)
      {
        DateTime now = DateTime.Now;
        if (now > lic.ValidTo || now < lic.ValidFrom)
          return false;
      }


      LayoutConfig dex = new LayoutConfig();
      dex.LoadFromFile();
      bool result = dex.ValidateLayout();
      dex.SaveToFile();

      return true;
    }
  }
}
