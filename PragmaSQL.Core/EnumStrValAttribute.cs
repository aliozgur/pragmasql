using System;
using System.Reflection;

namespace  PragmaSQL.Core
{
    public class EnumStrValAttr: Attribute
    {
        public string _Value = "";

        public string Value
        {
            get
            {
                return _Value;
            }
            set
            {
                _Value = value;
            }
        }
        public EnumStrValAttr(string Value)
        {
            _Value = Value;
        }


        public static string GetStrValue(Enum EnumConst)
        {
            FieldInfo fi = EnumConst.GetType().GetField(EnumConst.ToString());
            EnumStrValAttr[] attr = (EnumStrValAttr[])fi.GetCustomAttributes(typeof(EnumStrValAttr), false);

            if (attr.Length > 0)
            {
                return attr[0].Value;
            }
            else
            {
                return String.Empty;
            }
        }

      public static object GetValue(Type type, string strVal)
      {

        if (!type.IsEnum)
        {
          throw new Exception("Type is not an enumeration!");
        }

        FieldInfo[] fields  = type.GetFields();
        foreach (FieldInfo fi in fields)
        {
          EnumStrValAttr[] attr = (EnumStrValAttr[])fi.GetCustomAttributes(typeof(EnumStrValAttr), false);
          if (attr.Length > 0 && attr[0].Value == strVal )
          {
            return Enum.Parse(type,fi.Name);
          }
        }

        return null;
      }

			public static string GetName(Type type, string strVal)
			{

				if (!type.IsEnum)
				{
					throw new Exception("Type is not an enumeration!");
				}

				FieldInfo[] fields = type.GetFields();
				foreach (FieldInfo fi in fields)
				{
					EnumStrValAttr[] attr = (EnumStrValAttr[])fi.GetCustomAttributes(typeof(EnumStrValAttr), false);
					if (attr.Length > 0 && attr[0].Value == strVal)
					{
						return fi.Name;
					}
				}

				return null;
			}
    }
}

