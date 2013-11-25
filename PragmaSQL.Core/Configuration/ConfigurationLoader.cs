/********************************************************************
  Class      : ConfigurationLoader
  Created by : Ali Özgür
  Contact    : ali_ozgur@hotmail.com
  
  Copyright: Istanbul Bilgi University
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

using PragmaSQL.Common;

namespace PragmaSQL
{
  public static class ConfigurationLoader
  {
    private static ConfigurationContent _currentConfig = null;
    public static ConfigurationContent CurrentConfig
    {
      get { return ConfigurationLoader._currentConfig; }
    }

    private static string defaultFileName = String.Empty;
    
    static ConfigurationLoader()
    {
      
      string appDataDir = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\PragmaSQL";
      if(!Directory.Exists(appDataDir))
      {
        Directory.CreateDirectory(appDataDir);
      }

      if(!Directory.Exists(appDataDir) )
      {
        FileInfo fi = new FileInfo(Application.ExecutablePath);
        defaultFileName  = fi.Directory + "\\PragmaSQL.options";
      }
      else
      {
        defaultFileName  = appDataDir + "\\PragmaSQL.options";
      }
    }
    
    public static ConfigurationContent LoadCurrentConfiguration()
    {
      return LoadCurrentConfiguration(defaultFileName);
    }

    public static ConfigurationContent LoadCurrentConfiguration(string fileName)
    {
      ConfigurationContent result = null;

      if(!File.Exists(fileName))
      {
        _currentConfig = new ConfigurationContent();
        return _currentConfig;
      }

      result = ObjectXMLSerializer<ConfigurationContent>.Load(fileName);
      if(result == null)
      {
        result = new ConfigurationContent();
      }
      _currentConfig = result;
      return result;
    }

    public static void SaveCurrentConfiguration(ConfigurationContent configContent)
    {
      SaveCurrentConfiguration(configContent,defaultFileName);
    }

    public static void SaveCurrentConfiguration(ConfigurationContent configContent,string fileName)
    {
      ObjectXMLSerializer<ConfigurationContent>.Save(configContent,fileName);
    }
  }
}
