﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.42000.
// 
#pragma warning disable 1591

namespace PragmaSQL.LicenceSigning {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2558.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="LicenceSignSvcSoap", Namespace="http://www.pragmasql.com/webservices/")]
    public partial class LicenceSignSvc : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback SignLicenceOperationCompleted;
        
        private System.Threading.SendOrPostCallback SecureSignLicenceOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public LicenceSignSvc() {
            this.Url = global::PragmaSQL.Properties.Settings.Default.PragmaSQL_LicenceSigning_LicenceSignSvc;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event SignLicenceCompletedEventHandler SignLicenceCompleted;
        
        /// <remarks/>
        public event SecureSignLicenceCompletedEventHandler SecureSignLicenceCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.pragmasql.com/webservices/SignLicence", RequestNamespace="http://www.pragmasql.com/webservices/", ResponseNamespace="http://www.pragmasql.com/webservices/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string SignLicence(string licenceXml) {
            object[] results = this.Invoke("SignLicence", new object[] {
                        licenceXml});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void SignLicenceAsync(string licenceXml) {
            this.SignLicenceAsync(licenceXml, null);
        }
        
        /// <remarks/>
        public void SignLicenceAsync(string licenceXml, object userState) {
            if ((this.SignLicenceOperationCompleted == null)) {
                this.SignLicenceOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSignLicenceOperationCompleted);
            }
            this.InvokeAsync("SignLicence", new object[] {
                        licenceXml}, this.SignLicenceOperationCompleted, userState);
        }
        
        private void OnSignLicenceOperationCompleted(object arg) {
            if ((this.SignLicenceCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SignLicenceCompleted(this, new SignLicenceCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.pragmasql.com/webservices/SecureSignLicence", RequestNamespace="http://www.pragmasql.com/webservices/", ResponseNamespace="http://www.pragmasql.com/webservices/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string SecureSignLicence(string encryptedLic) {
            object[] results = this.Invoke("SecureSignLicence", new object[] {
                        encryptedLic});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void SecureSignLicenceAsync(string encryptedLic) {
            this.SecureSignLicenceAsync(encryptedLic, null);
        }
        
        /// <remarks/>
        public void SecureSignLicenceAsync(string encryptedLic, object userState) {
            if ((this.SecureSignLicenceOperationCompleted == null)) {
                this.SecureSignLicenceOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSecureSignLicenceOperationCompleted);
            }
            this.InvokeAsync("SecureSignLicence", new object[] {
                        encryptedLic}, this.SecureSignLicenceOperationCompleted, userState);
        }
        
        private void OnSecureSignLicenceOperationCompleted(object arg) {
            if ((this.SecureSignLicenceCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SecureSignLicenceCompleted(this, new SecureSignLicenceCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2558.0")]
    public delegate void SignLicenceCompletedEventHandler(object sender, SignLicenceCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2558.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SignLicenceCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal SignLicenceCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2558.0")]
    public delegate void SecureSignLicenceCompletedEventHandler(object sender, SecureSignLicenceCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2558.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SecureSignLicenceCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal SecureSignLicenceCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591