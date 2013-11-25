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

using ICSharpCode.Core;

using PragmaSQL.Core;
using PragmaSQL.Licencing;


namespace PragmaSQL
{
  [Serializable]
  public class LayoutConfig
  {
    private string _filePath = FileUtility.ApplicationRootPath + "\\layout.dat";
    public List<string> Items = new List<string>();
    public string ItemCount = "0";
    
    private int ItemCntInt
    {
      get
      {
        int itemCnt = 0;
        Int32.TryParse(ItemCount, out itemCnt);
        return itemCnt;
      }
    }

    public void SaveToFile(string fileName)
    {
      FileStream fs = null;
      try
      {
        fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
        byte[] serailizedBytes = SeralizeToByteArray();
        fs.Write(serailizedBytes, 0, serailizedBytes.Length);
        fs.Flush();
      }
      finally
      {
        fs.Close();
      }
    }

    public void SaveToFile()
    {
      SaveToFile(_filePath);
    }

    public void LoadFromFile(string fileName)
    {
      FileStream fs = null;
      try
      {
        fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
        byte[] deserializedBytes = new byte[fs.Length];
        fs.Read(deserializedBytes, 0, deserializedBytes.Length);
        string deserializedValue = System.Text.Encoding.Unicode.GetString(deserializedBytes);
        
        DeserializeFromString(deserializedValue);
      }
      finally
      {
        fs.Close();
      }
    }

    public void LoadFromFile()
    {
      LoadFromFile(_filePath);
    }

    public bool ValidateLayout()
    {
      LicUtils utils = new LicUtils();
      if (this.Items.Count != this.ItemCntInt)
        throw new Exception("File was corrupted!");

      string toDay =  utils.GetSimpleDate(DateTime.Now);
      if (!Items.Contains(toDay))
      {
        Items.Add(toDay);
      }
      
      if (this.Items.Count >= 121)
        return false;
      else
        return true;
    }

    #region Serialization 

    private void DeserializeFromString(string values)
    {
      LicUtils utils = new LicUtils();
      string deserializedValues = utils.Decrypt(values);
      string[] parts = deserializedValues.Split(new char[1] { ';' }, StringSplitOptions.RemoveEmptyEntries);
      bool isItemCntPart = true;
      ItemCount = "0";
      Items.Clear();
      foreach (string part in parts)
      {
        if (isItemCntPart)
        {
          ItemCount = part;
          isItemCntPart = false;
        }
        else
        {
          Items.Add(part);
        }
      }
    }

    private string SerializeToString()
    {
      string seralizedValues = String.Empty;
      int itemCnt = 0;
      string sep = String.Empty;
      foreach (string value in Items)
      {
        seralizedValues += sep + value;
        sep = ";";
        itemCnt++;
      }
      seralizedValues = itemCnt.ToString() + ";" + seralizedValues;
      LicUtils utils = new LicUtils();
      return utils.Encrypt(seralizedValues);
    }

    private byte[] SeralizeToByteArray()
    {
      return System.Text.Encoding.Unicode.GetBytes(SerializeToString());
    }
    #endregion //Serialization
  }
}
