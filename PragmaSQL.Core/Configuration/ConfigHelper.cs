/********************************************************************
  Class      : ConfigurationLoader
  Created by : Ali Özgür
  Contact    : ali_ozgur@hotmail.com
  
  Copyright: Ali Özgür - 2007
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

using PragmaSQL.Core;

namespace PragmaSQL.Core
{
  public static class ConfigHelper
  {
    private static ConfigurationContent _current = null;
    public static ConfigurationContent Current
    {
      get { return ConfigHelper._current; }
    }

		private static string defaultFileName=String.Empty;

    private static string _userDataDirectory = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\PragmaSQL";
    public static string UserDataDirectory
    {
      get { return _userDataDirectory;}
    }

    public static string CustomPaletteFileName
    {
      get { return Path.Combine(UserDataDirectory, "CustomPalette.xml"); }
    }


    static ConfigHelper()
    {
      if(!Directory.Exists(_userDataDirectory))
      {
        Directory.CreateDirectory(_userDataDirectory);
      }
      defaultFileName = _userDataDirectory + "\\PragmaSQL.options";
    }
    
    public static ConfigurationContent LoadFromDefault()
    {
      return LoadConfigurationFrom(defaultFileName);
    }

    public static ConfigurationContent LoadConfigurationFrom(string fileName)
    {
      ConfigurationContent result = null;

      if(!File.Exists(fileName))
      {
        _current = new ConfigurationContent();
        return _current;
      }

      result = ObjectXMLSerializer<ConfigurationContent>.Load(fileName);
      if(result == null)
      {
        result = new ConfigurationContent();
      }
      _current = result;
      return result;
    }

    public static void SaveAsDefault(ConfigurationContent configContent)
    {
      SaveConfigurationAs(configContent,defaultFileName);
    }

    public static void SaveConfigurationAs(ConfigurationContent configContent,string fileName)
    {
      ObjectXMLSerializer<ConfigurationContent>.Save(configContent,fileName);
    }
  }
}
