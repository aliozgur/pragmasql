/********************************************************************
  Class ApplicationMessage
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

namespace PragmaSQL.Core
{
  public class ApplicationMessage
  {
    public DateTime MsgDateTime;
    public string Message = String.Empty;
    public MessageType MsgType = MessageType.None;
    public string MethodName = String.Empty;
    public string MethodDeclaringTypeAssemblyName = String.Empty;
    public string MethodDeclaringTypeName = String.Empty;

    public override string ToString( )
    {
      StringBuilder sb = new StringBuilder();
      sb.AppendLine(MsgDateTime.ToString() + " : " + Message);
      if( !String.IsNullOrEmpty(MethodName)) 
        sb.AppendLine("MethodName: " + MethodName);

      if (!String.IsNullOrEmpty(MethodDeclaringTypeName))
        sb.AppendLine("Declaring Type:" + MethodDeclaringTypeName);

      if (!String.IsNullOrEmpty(MethodDeclaringTypeAssemblyName))
        sb.AppendLine("Declaring Type Assembly:" + MethodDeclaringTypeAssemblyName);
      
      return sb.ToString();
    }

    public ApplicationMessage( )
    {
    
    }

		public ApplicationMessage(MessageType type, string msg, MethodBase methodInfo)
    {
      this.MsgType = type;
      this.Message = msg;

      if (methodInfo != null)
      {
        MethodName = methodInfo.Name;
        MethodDeclaringTypeName = methodInfo.DeclaringType.FullName;
        MethodDeclaringTypeAssemblyName = methodInfo.DeclaringType.Assembly.FullName;
      }
    }

    
    public static void PublishWithLoggerServices( ApplicationMessage msg )
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
  }


}
