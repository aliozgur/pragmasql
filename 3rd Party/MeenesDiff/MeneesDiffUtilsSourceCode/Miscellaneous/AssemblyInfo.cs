#region Copyright And Revision History

/*---------------------------------------------------------------------------

	AssemblyInfo.cs
	Copyright © 2002 Bill Menees.  All rights reserved.
	Bill@Menees.com

	Who		When		What
	-------	----------	-----------------------------------------------------
	BMenees	10.20.2002	Created.

-----------------------------------------------------------------------------*/

#endregion

using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

[assembly: ComVisible(false)]
[assembly: CLSCompliant(true)]

[assembly: AssemblyTitle("Menees DiffUtils")]
[assembly: AssemblyDescription("Differencing Utilities for .NET")]
#if DEBUG
[assembly: AssemblyConfiguration("Debug")]
#else
[assembly: AssemblyConfiguration("Release")]
#endif
[assembly: AssemblyCompany("Bill Menees")]
[assembly: AssemblyProduct("Menees Difference Utilities")]
[assembly: AssemblyCopyright("Copyright © 2002-2006 Bill Menees")]

//TODO: Update Diff.NET's version when you update this.
[assembly: AssemblyVersion("2.0.2.0")]

[assembly: AssemblyDelaySign(false)]
[assembly: AssemblyKeyFile("")]
[assembly: AssemblyKeyName("")]
