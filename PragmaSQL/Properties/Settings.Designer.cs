﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PragmaSQL.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "15.8.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("http://www.pragmasql.com/webservices/CheckUpdates.asmx")]
        public string PragmaSQL_ProducUpdateCheck_CheckUpdates {
            get {
                return ((string)(this["PragmaSQL_ProducUpdateCheck_CheckUpdates"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("http://www.pragmasql.com/home/downloads/pragmasql.aspx")]
        public string PragmaSQLDownloadsUrl {
            get {
                return ((string)(this["PragmaSQLDownloadsUrl"]));
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("http://msdn.microsoft.com/tr-tr/library/system.data.datacolumn.expression(en-us,V" +
            "S.80).aspx")]
        public string Help_QueryResultFiltering {
            get {
                return ((string)(this["Help_QueryResultFiltering"]));
            }
            set {
                this["Help_QueryResultFiltering"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("http://msdn.microsoft.com/en-us/library/system.data.dataview.sort(VS.80).aspx")]
        public string Help_QueryResultSorting {
            get {
                return ((string)(this["Help_QueryResultSorting"]));
            }
            set {
                this["Help_QueryResultSorting"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("http://www.pragmasql.com/home/downloads/pragmasql.aspx")]
        public string DownloadUrl {
            get {
                return ((string)(this["DownloadUrl"]));
            }
            set {
                this["DownloadUrl"] = value;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("http://localhost/PragmaWeb/WebServices/LicenceSignSvc.asmx")]
        public string PragmaSQL_LicenceSigning_LicenceSignSvc {
            get {
                return ((string)(this["PragmaSQL_LicenceSigning_LicenceSignSvc"]));
            }
        }
    }
}
