using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using PragmaSQL.Core;

namespace PragmaSQL
{
	public static class MultiExecSpec
	{

    public static readonly  string TemplatesDirectory = HostServicesSingleton.HostServices.HostOptions.UserAppDataFolder + "MultiDbTemplates\\";
    public static readonly string TemplateFileExt = ".$tmp$";
    public static readonly string DefaultTemplateFileName = "Default.$$$";
    public static readonly string DefaultTemplate= TemplatesDirectory + DefaultTemplateFileName;

    static MultiExecSpec()
    {
      if (!Directory.Exists(TemplatesDirectory))
        Directory.CreateDirectory(TemplatesDirectory);
    }

    public static SerializableDictionary<string, ConnectionParams> Load(string path)
    {
      SerializableDictionary<string, ConnectionParams> result = null;
      if (!File.Exists(path))
      {
        result = new SerializableDictionary<string, ConnectionParams>();
      }
      else
      {
        result = ObjectXMLSerializer<SerializableDictionary<string, ConnectionParams>>.Load(path);
        if (result == null)
          result = new SerializableDictionary<string, ConnectionParams>();
      }

      result = PrepareConnectionParams(result,false);
      return result;
    }
    		
    public static void Save(SerializableDictionary<string, ConnectionParams> spec, string path)
    {
      SerializableDictionary<string, ConnectionParams> tmp = PrepareConnectionParams(spec,true);
      ObjectXMLSerializer<SerializableDictionary<string, ConnectionParams>>.Save(tmp, path);
    }
    

		private static SerializableDictionary<string, ConnectionParams> PrepareConnectionParams(SerializableDictionary<string, ConnectionParams> spec,bool encrypt)
		{
			SerializableDictionary<string, ConnectionParams> result = new SerializableDictionary<string, ConnectionParams>();
			foreach (string key  in spec.Keys)
			{
        if (spec[key] == null)
					continue;

        ConnectionParams cp = spec[key].CreateCopy();
				string tmpKey = key;
				if (String.IsNullOrEmpty(cp.IntegratedSecurity) && cp.SaveEncrypted)
          cp.Password = encrypt ? EncryiptionHelper.Encrypt(cp.Password) : EncryiptionHelper.Decrypt(cp.Password);
				else if (!String.IsNullOrEmpty(cp.IntegratedSecurity))
					cp.Password = String.Empty;

        tmpKey =  encrypt ? key.Replace((Char)29, (Char)(168)) : key.Replace((Char)(168), (Char)29);
				result.Add(tmpKey, cp);
			}
			return result;
		}
	
	}
}
