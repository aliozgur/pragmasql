using System;
using System.Collections.Generic;
using System.Text;

namespace ICSharpCode.Core
{
	internal static class RevisionClass
	{
		public const string Major = "2";
		public const string Minor = "1";
		public const string Build = "0";
		public const string Revision = "2017";

		public const string MainVersion = Major + "." + Minor;
		public const string FullVersion = Major + "." + Minor + "." + Build + "." + Revision;
	}
}
