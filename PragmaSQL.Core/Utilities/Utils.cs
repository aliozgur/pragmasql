/********************************************************************
  Class Utils
  Created On: $datetime$
  Created by: Ali Özgür
  Contact: ali_ozgur@hotmail.com
  
  Copyright: Ali Özgür
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Drawing;

namespace PragmaSQL.Core
{
  public static class Utils
  {
		private enum ColorFormat
		{
			NamedColor,
			ARGBColor
		}

    public static bool IsDbValueValid( object value )
    {
      return (value != null && value.GetType() != typeof(DBNull));
    }

		public static DialogResult ShowError(string msg,MessageBoxButtons buttons)
		{
			return MessageBox.Show(msg, "Error", buttons, MessageBoxIcon.Error);
		}

		public static DialogResult ShowWarning(string msg, MessageBoxButtons buttons)
		{
			return MessageBox.Show(msg, "Warning", buttons, MessageBoxIcon.Warning);
		}

		public static DialogResult ShowInfo(string msg, MessageBoxButtons buttons)
		{
			return MessageBox.Show(msg, "Info", buttons, MessageBoxIcon.Information);
		}

		public static DialogResult AskYesNoQuestion(string msg,MessageBoxDefaultButton defaultBtn)
		{
			return MessageBox.Show(msg, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, defaultBtn);
		}

		public static string FormatException(Exception ex)
		{
			if (ex == null)
			{
				return "Unknown error.";
			}
			else
			{
				StringBuilder sb = new StringBuilder();
				sb.AppendLine("Type: " + ex.GetType().Name);
				sb.AppendLine(String.Empty);
				sb.AppendLine("Message: " + ex.Message);
				if (ex.InnerException != null)
				{
					sb.AppendLine(String.Empty);
					sb.AppendLine("Inner Exception Type: " + ex.GetType().Name);
					sb.AppendLine(String.Empty);
					sb.AppendLine("Inner Exception Msg:" + ex.InnerException.Message);
				}
				sb.AppendLine(String.Empty);
				sb.AppendLine("Source: " + ex.Source);
				sb.AppendLine(String.Empty);
				sb.AppendLine("Stack Trace:");
				sb.AppendLine(ex.StackTrace);

				return sb.ToString();
			}
		}

		public static string SerializeColor(Color color)
		{
			if (color.IsNamedColor)
				return string.Format("{0}:{1}",
						ColorFormat.NamedColor, color.Name);
			else
				return string.Format("{0}:{1}:{2}:{3}:{4}",
						ColorFormat.ARGBColor,
						color.A, color.R, color.G, color.B);
		}

		public static Color DeserializeColor(string color)
		{
			byte a, r, g, b;

			string[] pieces = color.Split(new char[] { ':' });

			ColorFormat colorType = (ColorFormat)
					Enum.Parse(typeof(ColorFormat), pieces[0], true);

			switch (colorType)
			{
				case ColorFormat.NamedColor:
					return Color.FromName(pieces[1]);
				case ColorFormat.ARGBColor:
					a = byte.Parse(pieces[1]);
					r = byte.Parse(pieces[2]);
					g = byte.Parse(pieces[3]);
					b = byte.Parse(pieces[4]);

					return Color.FromArgb(a, r, g, b);
			}
			return Color.Empty;
		}
	}
}
