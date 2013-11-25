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
using System.Collections;
using System.Collections.Generic;
using System.Text;

using System.Management;

namespace PragmaSQL.Licencing
{
	public struct MachineID
	{
		public string Key;

		public MachineID(string key)
		{
			Key = key;
		}

		public override string ToString()
		{
			return Key;
		}

		public override bool Equals(object obj)
		{
			MachineID mObj = (MachineID)obj;
			return this.Key == mObj.Key;
		}

    public override int GetHashCode()
    {
      return this.Key.GetHashCode();
    }

	}

	public class MachineIDComparer:IComparer
	{
		public int Compare(object x, object y)
		{
			MachineID mx = (MachineID)x ;
			MachineID my = (MachineID)y ;
			return mx.Key.CompareTo(my.Key);
		}
	}



	public static class MachineIdProvider
	{
		private static string Api_Processor = "Win32_Processor";
		private static string Api_BaseBoard = "Win32_BaseBoard";
    private static string Api_Bios = "Win32_BIOS";

		//private static string Api_NetworkAdapterConfiguration = "Win32_NetworkAdapterConfiguration";

		private static string Retreive_ProcessorInfo()
		{
			ArrayList cpuIds = new ArrayList();
			string mId;
			string result = String.Empty;

			ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM " + Api_Processor);
			ManagementObjectCollection objCol = searcher.Get();
			if (objCol != null && objCol.Count > 0)
			{
				foreach (ManagementObject mo in objCol)
				{
					if (mo != null)
					{
						object value = mo.GetPropertyValue("ProcessorId");
						if (value != null && !String.IsNullOrEmpty(value.ToString()))
						{
							mId = value.ToString().Trim().ToLowerInvariant();
							if (!cpuIds.Contains(mId))
								cpuIds.Add(mId);
						}
					}
				}
			}

			cpuIds.Sort();

			foreach (string id in cpuIds)
			{
				result += id;
			}

			return result;
		}

		private static string Retreive_BaseBoardInfo()
		{
			ArrayList boardIs = new ArrayList();
			string mId;
			string result = String.Empty;

			ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM " + Api_BaseBoard);
			ManagementObjectCollection objCol = searcher.Get();
			if (objCol != null && objCol.Count > 0)
			{
				foreach (ManagementObject mo in objCol)
				{
					if (mo != null)
					{
						object value = mo.GetPropertyValue("SerialNumber");
						if (value != null && !String.IsNullOrEmpty(value.ToString()))
						{
							mId = value.ToString().Trim().ToLowerInvariant();
							if (!boardIs.Contains(mId))
								boardIs.Add(mId);
						}
					}
				}
			}

			boardIs.Sort();

			foreach (string id in boardIs)
			{
				result += id;
			}

			return result;
		}

    private static string Retreive_BiosInfo()
    {
      ArrayList boardIs = new ArrayList();
      string mId;
      string result = String.Empty;

      ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM " + Api_Bios);
      ManagementObjectCollection objCol = searcher.Get();
      if (objCol != null && objCol.Count > 0)
      {
        foreach (ManagementObject mo in objCol)
        {
          if (mo != null)
          {
            object value = mo.GetPropertyValue("SerialNumber");
            if (value != null && !String.IsNullOrEmpty(value.ToString()))
            {
              mId = value.ToString().Trim().ToLowerInvariant();
              if (!boardIs.Contains(mId))
                boardIs.Add(mId);
            }
          }
        }
      }

      boardIs.Sort();

      foreach (string id in boardIs)
      {
        result += id;
      }

      return result;
    }

		public static MachineID Retrieve(MachineIdType idType)
		{
			MachineID result = new MachineID();
      switch (idType)
      {
        case MachineIdType.Composite:
          result.Key = String.Format("{0}{1}",Retreive_ProcessorInfo(),Retreive_BaseBoardInfo());
          break;
        case MachineIdType.Composite2:
          result.Key = String.Format("{0}_{1}_{2}",Retreive_ProcessorInfo(),Retreive_BaseBoardInfo(),Retreive_BiosInfo());
          break;
        case MachineIdType.Simple:
        default:
          result.Key = Retreive_ProcessorInfo();
          break;
      }
			
			return result;
		}

  }
}
