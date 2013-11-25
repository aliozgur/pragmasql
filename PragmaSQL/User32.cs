using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PragmaSQL
{
  using System;
    using System.Runtime.InteropServices;

   public static class User32
	{
		[DllImport("User32.dll", CharSet=CharSet.Auto)]
		public static extern IntPtr SetClipboardViewer(IntPtr hWnd);
		[DllImport("User32.dll", CharSet=CharSet.Auto)]
		public static extern bool ChangeClipboardChain(IntPtr hWndRemove, IntPtr hWndNewNext);
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		public static extern IntPtr SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll")]
        public static extern int RegisterWindowMessage(string msgName);
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        private static Dictionary<JumpListOperationsEnum,int> _jumpListMessages = new Dictionary<JumpListOperationsEnum,int>(); 
        static User32()
        {
            Array values = Enum.GetValues(typeof(JumpListOperationsEnum));
            foreach(JumpListOperationsEnum val in values)
            {
              int msg = RegisterWindowMessage(String.Format("PragmaSQL.{0}",val));
              _jumpListMessages.Add(val,msg);
            }
        }

        public static int GetJumpListMsg(string jumpListEnumValue)
        {
          int enumValInt = -1;
          if (!int.TryParse(jumpListEnumValue, out enumValInt))
            return -1;

          JumpListOperationsEnum jlEnum = (JumpListOperationsEnum)int.Parse(jumpListEnumValue);
          return GetJumpListMsg(jlEnum);
        }

        public static int GetJumpListMsg(JumpListOperationsEnum jlEnum)
        {
          if (!_jumpListMessages.ContainsKey(jlEnum))
            return -1;
          return _jumpListMessages[jlEnum];
        }

        public static bool SendMessage(string windowTitle, int msgId, IntPtr wParam, IntPtr lParam)
        {
            var window = FindWindow(null, windowTitle);
            if (window == IntPtr.Zero) return false;

            var result = SendMessage(window, msgId, wParam, lParam);

            return result == IntPtr.Zero;
        }
	}
}

