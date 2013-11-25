/********************************************************************
  Class AppMsgService
  Created On: $datetime$
  Created by: Ali Özgür
  Contact: ali_ozgur@hotmail.com
  
  Copyright: Ali Özgür - 2007
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Reflection;
using ICSharpCode.Core;

using PragmaSQL.Core;

namespace PragmaSQL
{
  public class AppMsgService : IAppMessageService
  {
    private ListView _lv = null;
    public ListView ListView
    {
      get { return _lv; }
    }

    private EventHandler _showMessagesCallback;
    public event EventHandler ShowMessagesCallback
    {
      add { _showMessagesCallback += value; }
      remove { _showMessagesCallback -= value; }
    }
    
    public AppMsgService( ListView lv )
    {
      _lv = lv;
    }

		private void PublishMessageInListView(ApplicationMessage msg)
		{
			if (!_lv.InvokeRequired)
			{
				PublishMessageInListView_Internal(msg);
			}
			else
			{
				object[] param = new object[1]{msg};
				_lv.Invoke(new OnPublishMessageInListViewDelegate(PublishMessageInListView_Internal),param);
			}
		}

		private void PublishMessageInListView_Internal(ApplicationMessage msg)
    {
      if (msg == null || ListView == null)
      {
        return;
      }

      ListViewItem item = _lv.Items.Add(String.Empty, (int)msg.MsgType);
      item.UseItemStyleForSubItems = false;

      Color foreColor = Color.Navy;
      Color bgColor = Color.White;
      switch (msg.MsgType)
      {
        case MessageType.Info:
          foreColor = Color.Navy;
          break;
        case MessageType.Warning:
          foreColor = Color.Maroon;
          break;
        case MessageType.Error:
          foreColor = Color.Red;
          break;
        case MessageType.None:
        default:
          foreColor = Color.Black;
          break;

      }

      item.SubItems.Add(msg.MsgDateTime.ToString(), foreColor, bgColor, _lv.Font);
      item.SubItems.Add(msg.Message, foreColor, bgColor, _lv.Font);
      item.SubItems.Add(msg.MethodName, foreColor, bgColor, _lv.Font);
      item.SubItems.Add(msg.MethodDeclaringTypeName, foreColor, bgColor, _lv.Font);
      item.SubItems.Add(msg.MethodDeclaringTypeAssemblyName, foreColor, bgColor, _lv.Font);
    }

    private void PublishWithLoggerServices( ApplicationMessage msg )
    {
      switch (msg.MsgType)
      {
        case MessageType.None:
          LoggingService.Info(msg.ToString());
          break;
        case MessageType.Info:
          LoggingService.Info(msg.ToString());
          break;
        case MessageType.Warning:
          LoggingService.Warn(msg.ToString());
          break;
        case MessageType.Error:
          LoggingService.Error(msg.ToString());
          break;
        case MessageType.Debug:
          LoggingService.Debug(msg.ToString());
          break;
        default:
          break;
      }
    }

    public ApplicationMessage Create( MessageType msgType, string msgText, string methodName, string methodDeclaringTypeName, string methodAssemblyName )
    {
      ApplicationMessage result = new ApplicationMessage();

      string tmpMessage = msgText.Replace("\n", String.Empty);
      tmpMessage = tmpMessage.Replace("\r", String.Empty);
      tmpMessage = tmpMessage.Replace("\t", " ");

      result.MsgDateTime = DateTime.Now;
      result.Message = tmpMessage;
      result.MsgType = msgType;
      result.MethodName = methodName;
      result.MethodDeclaringTypeName = methodDeclaringTypeName;
      result.MethodDeclaringTypeAssemblyName = methodAssemblyName;

      PublishMessageInListView(result);
      ApplicationMessage.PublishWithLoggerServices(result);
      return result;
    }

		public void ClearMessages()
		{
			if (!_lv.InvokeRequired)
			{
				ClearMessages_Internal();
			}
			else
			{
				_lv.Invoke(new ActionF(ClearMessages_Internal));
			}
		}

		public void ClearMessages_Internal()
		{
			_lv.Items.Clear();
		}

    public ApplicationMessage DebugMsg( string msgText, string methodName, string methodDeclaringTypeName, string methodAssemblyName )
    {
      return Create(MessageType.Debug, msgText, methodName, methodDeclaringTypeName, methodAssemblyName);
    }
    public ApplicationMessage DebugMsg( string msgText )
    {
      return DebugMsg(msgText, String.Empty, String.Empty, String.Empty);
    }

    public ApplicationMessage ErrorMsg( string msgText, string methodName, string methodDeclaringTypeName, string methodAssemblyName )
    {
      return Create(MessageType.Error, msgText, methodName, methodDeclaringTypeName, methodAssemblyName);
    }
    public ApplicationMessage ErrorMsg( string msgText )
    {
      return ErrorMsg(msgText, String.Empty, String.Empty, String.Empty);
    }

    public ApplicationMessage WarningMsg( string msgText, string methodName, string methodDeclaringTypeName, string methodAssemblyName )
    {
      return Create(MessageType.Warning, msgText, methodName, methodDeclaringTypeName, methodAssemblyName);
    }

    public ApplicationMessage WarningMsg( string msgText )
    {
      return WarningMsg(msgText, String.Empty, String.Empty, String.Empty);
    }

    public ApplicationMessage InfoMsg( string msgText, string methodName, string methodDeclaringTypeName, string methodAssemblyName )
    {
      return Create(MessageType.Info, msgText, methodName, methodDeclaringTypeName, methodAssemblyName);
    }
    
    public ApplicationMessage InfoMsg( string msgText )
    {
      return InfoMsg(msgText, String.Empty, String.Empty, String.Empty);
    }

    public void AddMessage( ApplicationMessage msg )
    {
      PublishMessageInListView(msg);
    }

    public void ShowMessages( )
    {
      if (_showMessagesCallback != null)
      {
        _showMessagesCallback(this, EventArgs.Empty);
      }
    }

		public ApplicationMessage AddMessage(MessageType type, string msg, MethodBase methodInfo)
    {
      if (methodInfo == null)
      {
        return Create(type, msg, String.Empty, String.Empty, String.Empty);
      }
      else
      {
        return Create(type, msg, methodInfo.Name, methodInfo.DeclaringType.FullName, methodInfo.DeclaringType.Assembly.FullName);
      }
    }

		public ApplicationMessage DebugMsg(string msg, MethodBase methodInfo)
    {
      return AddMessage(MessageType.Debug, msg, methodInfo);
    }

		public ApplicationMessage DebugMsg(Exception ex, MethodBase methodInfo)
    {
      string exMessage = String.Empty;
      if (ex != null)
      {
        exMessage = ex.GetType().FullName + ":" + ex.Message;
      }
      return AddMessage(MessageType.Debug, exMessage, methodInfo);
    }

		public ApplicationMessage ErrorMsg(string msg, MethodBase methodInfo)
    {
      return AddMessage(MessageType.Error, msg, methodInfo);
    }

		public ApplicationMessage ErrorMsg(Exception ex, MethodBase methodInfo)
    {
      string exMessage = String.Empty;
      if (ex != null)
      {
        exMessage = ex.GetType().FullName + ":" + ex.Message;
      }
      return AddMessage(MessageType.Error, exMessage, methodInfo);
    }

		public ApplicationMessage WarningMsg(string msg, MethodBase methodInfo)
    {
      return AddMessage(MessageType.Warning, msg, methodInfo);
    }

		public ApplicationMessage InfoMsg(string msg, MethodBase methodInfo)
    {
      return AddMessage(MessageType.Info, msg, methodInfo);
    }

		public void LogMessage(MessageType type, string msg, MethodBase methodInfo)
    {
      ApplicationMessage appMsg = new ApplicationMessage(type, msg, methodInfo);
      ApplicationMessage.PublishWithLoggerServices(appMsg);
    }

    public void LogMessage( MessageType type, string msg )
    {
      LogMessage(type, msg, null);
    }

		public void LogDebug(string msg, MethodBase methodInfo)
    {
      LogMessage(MessageType.Debug, msg, methodInfo);
    }

    public void LogDebug( string msg )
    {
      LogDebug(msg,null);
    }

		public void LogError(string msg, MethodBase methodInfo)
    {
      LogMessage(MessageType.Error, msg, methodInfo);
    }

    public void LogError( string msg )
    {
      LogError(msg, null);
    }

		public void LogInfo(string msg, MethodBase methodInfo)
    {
      LogMessage(MessageType.Info, msg, methodInfo);
    }

    public void LogInfo( string msg )
    {
      LogInfo(msg, null);
    }

		public void LogWarning(string msg, MethodBase methodInfo)
    {
      LogMessage(MessageType.Warning, msg, methodInfo);
    }

    public void LogWarning( string msg )
    {
      LogWarning(msg, null);
    }

  }

	internal delegate void OnPublishMessageInListViewDelegate(ApplicationMessage msg);
}
