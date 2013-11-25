using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Windows.Forms;
using System.Runtime.CompilerServices;

namespace PragmaSQL.WebBrowserEx
{
  class UnsafeNativeMethods
  {
    private UnsafeNativeMethods()
    { 
    }

    [ComImport, TypeLibType((short)0x1010), InterfaceType((short)2), Guid("34A715A0-6587-11D0-924A-0020AFC7AC4D")]
    public interface DWebBrowserEvents2
    {
      [PreserveSig, MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(0x66)]
			[SecurityPermission(SecurityAction.Assert, Unrestricted = true)] 
			void StatusTextChange([In, MarshalAs(UnmanagedType.BStr)] string Text);

			[PreserveSig, MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(0x6c)]
			[SecurityPermission(SecurityAction.Assert, Unrestricted = true)]
			void ProgressChange([In] int Progress, [In] int ProgressMax);
      
			[PreserveSig, MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(0x69)]
			[SecurityPermission(SecurityAction.Assert, Unrestricted = true)]
			void CommandStateChange([In] int Command, [In] bool Enable);
      
			[PreserveSig, MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(0x6a)]
			[SecurityPermission(SecurityAction.Assert, Unrestricted = true)]
			void DownloadBegin();
      
			[PreserveSig, MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(0x68)]
			[SecurityPermission(SecurityAction.Assert, Unrestricted = true)]
			void DownloadComplete();
      
			[PreserveSig, MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(0x71)]
			[SecurityPermission(SecurityAction.Assert, Unrestricted = true)]
			void TitleChange([In, MarshalAs(UnmanagedType.BStr)] string Text);
      
			[PreserveSig, MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(0x70)]
			[SecurityPermission(SecurityAction.Assert, Unrestricted = true)]
			void PropertyChange([In, MarshalAs(UnmanagedType.BStr)] string szProperty);
      
			[PreserveSig, MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(250)]
			[SecurityPermission(SecurityAction.Assert, Unrestricted = true)]
			void BeforeNavigate2([In, MarshalAs(UnmanagedType.IDispatch)] object pDisp, [In, MarshalAs(UnmanagedType.Struct)] ref object URL, [In, MarshalAs(UnmanagedType.Struct)] ref object Flags, [In, MarshalAs(UnmanagedType.Struct)] ref object TargetFrameName, [In, MarshalAs(UnmanagedType.Struct)] ref object PostData, [In, MarshalAs(UnmanagedType.Struct)] ref object Headers, [In, Out] ref bool Cancel);
      
			[PreserveSig, MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(0xfb)]
			[SecurityPermission(SecurityAction.Assert, Unrestricted = true)]
			void NewWindow2([In, Out, MarshalAs(UnmanagedType.IDispatch)] ref object ppDisp, [In, Out] ref bool Cancel);
      
			[PreserveSig, MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(0xfc)]
			[SecurityPermission(SecurityAction.Assert, Unrestricted = true)]
			void NavigateComplete2([In, MarshalAs(UnmanagedType.IDispatch)] object pDisp, [In, MarshalAs(UnmanagedType.Struct)] ref object URL);
      
			[PreserveSig, MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(0x103)]
			[SecurityPermission(SecurityAction.Assert, Unrestricted = true)]
			void DocumentComplete([In, MarshalAs(UnmanagedType.IDispatch)] object pDisp, [In, MarshalAs(UnmanagedType.Struct)] ref object URL);
      
			[PreserveSig, MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(0xfd)]
			[SecurityPermission(SecurityAction.Assert, Unrestricted = true)]
			void OnQuit();
      
			[PreserveSig, MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(0xfe)]
			[SecurityPermission(SecurityAction.Assert, Unrestricted = true)]
			void OnVisible([In] bool Visible);
      
			[PreserveSig, MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(0xff)]
			[SecurityPermission(SecurityAction.Assert, Unrestricted = true)]
			void OnToolBar([In] bool ToolBar);
      
			[PreserveSig, MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(0x100)]
			[SecurityPermission(SecurityAction.Assert, Unrestricted = true)]
			void OnMenuBar([In] bool MenuBar);
      
			[PreserveSig, MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(0x101)]
			[SecurityPermission(SecurityAction.Assert, Unrestricted = true)]
			void OnStatusBar([In] bool StatusBar);
      
			[PreserveSig, MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(0x102)]
			[SecurityPermission(SecurityAction.Assert, Unrestricted = true)]
			void OnFullScreen([In] bool FullScreen);
      
			[PreserveSig, MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(260)]
			[SecurityPermission(SecurityAction.Assert, Unrestricted = true)]
			void OnTheaterMode([In] bool TheaterMode);
      
			[PreserveSig, MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(0x106)]
			[SecurityPermission(SecurityAction.Assert, Unrestricted = true)]
			void WindowSetResizable([In] bool Resizable);
      
			[PreserveSig, MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(0x108)]
			[SecurityPermission(SecurityAction.Assert, Unrestricted = true)]
			void WindowSetLeft([In] int Left);
      
			[PreserveSig, MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(0x109)]
			[SecurityPermission(SecurityAction.Assert, Unrestricted = true)]
			void WindowSetTop([In] int Top);
      
			[PreserveSig, MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(0x10a)]
			[SecurityPermission(SecurityAction.Assert, Unrestricted = true)]
			void WindowSetWidth([In] int Width);
      
			[PreserveSig, MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(0x10b)]
			[SecurityPermission(SecurityAction.Assert, Unrestricted = true)]
			void WindowSetHeight([In] int Height);
      
			[PreserveSig, MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(0x107)]
			[SecurityPermission(SecurityAction.Assert, Unrestricted = true)]
			void WindowClosing([In] bool IsChildWindow, [In, Out] ref bool Cancel);

			[SecurityPermission(SecurityAction.Assert, Unrestricted = true)]
			[PreserveSig, MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(0x10c)]
      void ClientToHostWindow([In, Out] ref int CX, [In, Out] ref int CY);
      
			[PreserveSig, MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(0x10d)]
			[SecurityPermission(SecurityAction.Assert, Unrestricted = true)]
			void SetSecureLockIcon([In] int SecureLockIcon);
      
			[PreserveSig, MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(270)]
			[SecurityPermission(SecurityAction.Assert, Unrestricted = true)]
			void FileDownload([In, Out] ref bool Cancel);
      
			[PreserveSig, MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(0x10f)]
			[SecurityPermission(SecurityAction.Assert, Unrestricted = true)]
			void NavigateError([In, MarshalAs(UnmanagedType.IDispatch)] object pDisp, [In, MarshalAs(UnmanagedType.Struct)] ref object URL, [In, MarshalAs(UnmanagedType.Struct)] ref object Frame, [In, MarshalAs(UnmanagedType.Struct)] ref object StatusCode, [In, Out] ref bool Cancel);
      
			[PreserveSig, MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(0xe1)]
			[SecurityPermission(SecurityAction.Assert, Unrestricted = true)]
			void PrintTemplateInstantiation([In, MarshalAs(UnmanagedType.IDispatch)] object pDisp);
      
			[PreserveSig, MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(0xe2)]
			[SecurityPermission(SecurityAction.Assert, Unrestricted = true)]
			void PrintTemplateTeardown([In, MarshalAs(UnmanagedType.IDispatch)] object pDisp);
      
			[PreserveSig, MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(0xe3)]
			[SecurityPermission(SecurityAction.Assert, Unrestricted = true)]
			void UpdatePageStatus([In, MarshalAs(UnmanagedType.IDispatch)] object pDisp, [In, MarshalAs(UnmanagedType.Struct)] ref object nPage, [In, MarshalAs(UnmanagedType.Struct)] ref object fDone);
      
			[PreserveSig, MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(0x110)]
			[SecurityPermission(SecurityAction.Assert, Unrestricted = true)]
			void PrivacyImpactedStateChange([In] bool bImpacted);
      
			[PreserveSig, MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(0x111)]
			[SecurityPermission(SecurityAction.Assert, Unrestricted = true)]
			void NewWindow3([In, Out, MarshalAs(UnmanagedType.IDispatch)] ref object ppDisp, [In, Out] ref bool Cancel, [In] uint dwFlags, [In, MarshalAs(UnmanagedType.BStr)] string bstrUrlContext, [In, MarshalAs(UnmanagedType.BStr)] string bstrUrl);
    }

    [ComImport, SuppressUnmanagedCodeSecurity, TypeLibType(TypeLibTypeFlags.FOleAutomation | (TypeLibTypeFlags.FDual | TypeLibTypeFlags.FHidden)), Guid("D30C1661-CDAF-11d0-8A3E-00C04FC9E26E")]
    public interface IWebBrowser2
    {
      [DispId(100)]
      void GoBack();
      [DispId(0x65)]
      void GoForward();
      [DispId(0x66)]
      void GoHome();
      [DispId(0x67)]
      void GoSearch();
      [DispId(0x68)]
      void Navigate([In] string Url, [In] ref object flags, [In] ref object targetFrameName, [In] ref object postData, [In] ref object headers);
      [DispId(-550)]
      void Refresh();
      [DispId(0x69)]
      void Refresh2([In] ref object level);
      [DispId(0x6a)]
      void Stop();
      [DispId(200)]
      object Application { [return: MarshalAs(UnmanagedType.IDispatch)] get; }
      [DispId(0xc9)]
      object Parent { [return: MarshalAs(UnmanagedType.IDispatch)] get; }
      [DispId(0xca)]
      object Container { [return: MarshalAs(UnmanagedType.IDispatch)] get; }
      [DispId(0xcb)]
      object Document { [return: MarshalAs(UnmanagedType.IDispatch)] get; }
      [DispId(0xcc)]
      bool TopLevelContainer { get; }
      [DispId(0xcd)]
      string Type { get; }
      [DispId(0xce)]
      int Left { get; set; }
      [DispId(0xcf)]
      int Top { get; set; }
      [DispId(0xd0)]
      int Width { get; set; }
      [DispId(0xd1)]
      int Height { get; set; }
      [DispId(210)]
      string LocationName { get; }
      [DispId(0xd3)]
      string LocationURL { get; }
      [DispId(0xd4)]
      bool Busy { get; }
      [DispId(300)]
      void Quit();
      [DispId(0x12d)]
      void ClientToWindow(out int pcx, out int pcy);
      [DispId(0x12e)]
      void PutProperty([In] string property, [In] object vtValue);
      [DispId(0x12f)]
      object GetProperty([In] string property);
      [DispId(0)]
      string Name { get; }
      [DispId(-515)]
      int HWND { get; }
      [DispId(400)]
      string FullName { get; }
      [DispId(0x191)]
      string Path { get; }
      [DispId(0x192)]
      bool Visible { get; set; }
      [DispId(0x193)]
      bool StatusBar { get; set; }
      [DispId(0x194)]
      string StatusText { get; set; }
      [DispId(0x195)]
      int ToolBar { get; set; }
      [DispId(0x196)]
      bool MenuBar { get; set; }
      [DispId(0x197)]
      bool FullScreen { get; set; }
      [DispId(500)]
      void Navigate2([In] ref object URL, [In] ref object flags, [In] ref object targetFrameName, [In] ref object postData, [In] ref object headers);
      [DispId(0x1f5)]
      NativeMethods.OLECMDF QueryStatusWB([In] NativeMethods.OLECMDID cmdID);
      [DispId(0x1f6)]
      void ExecWB([In] NativeMethods.OLECMDID cmdID, [In] NativeMethods.OLECMDEXECOPT cmdexecopt, ref object pvaIn, IntPtr pvaOut);
      [DispId(0x1f7)]
      void ShowBrowserBar([In] ref object pvaClsid, [In] ref object pvarShow, [In] ref object pvarSize);
      [DispId(-525)]
      WebBrowserReadyState ReadyState { get; }
      [DispId(550)]
      bool Offline { get; set; }
      [DispId(0x227)]
      bool Silent { get; set; }
      [DispId(0x228)]
      bool RegisterAsBrowser { get; set; }
      [DispId(0x229)]
      bool RegisterAsDropTarget { get; set; }
      [DispId(0x22a)]
      bool TheaterMode { get; set; }
      [DispId(0x22b)]
      bool AddressBar { get; set; }
      [DispId(0x22c)]
      bool Resizable { get; set; }
    }

  }
}
