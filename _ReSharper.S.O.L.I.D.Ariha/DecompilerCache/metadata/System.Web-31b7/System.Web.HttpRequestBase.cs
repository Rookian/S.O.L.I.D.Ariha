// Type: System.Web.HttpRequestBase
// Assembly: System.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// Assembly location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.0\System.Web.dll

using System;
using System.Collections.Specialized;
using System.IO;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Security.Authentication.ExtendedProtection;
using System.Security.Principal;
using System.Text;
using System.Web.Routing;

namespace System.Web
{
    [TypeForwardedFrom("System.Web.Abstractions, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
    public abstract class HttpRequestBase
    {
        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        protected HttpRequestBase();

        public virtual string[] AcceptTypes { get; }
        public virtual string ApplicationPath { get; }
        public virtual string AnonymousID { get; }
        public virtual string AppRelativeCurrentExecutionFilePath { get; }
        public virtual HttpBrowserCapabilitiesBase Browser { get; }
        public virtual ChannelBinding HttpChannelBinding { get; }
        public virtual HttpClientCertificate ClientCertificate { get; }
        public virtual Encoding ContentEncoding { get; set; }
        public virtual int ContentLength { get; }
        public virtual string ContentType { get; set; }
        public virtual HttpCookieCollection Cookies { get; }
        public virtual string CurrentExecutionFilePath { get; }
        public virtual string FilePath { get; }
        public virtual HttpFileCollectionBase Files { get; }
        public virtual Stream Filter { get; set; }
        public virtual NameValueCollection Form { get; }
        public virtual string HttpMethod { get; }
        public virtual Stream InputStream { get; }
        public virtual bool IsAuthenticated { get; }
        public virtual bool IsLocal { get; }
        public virtual bool IsSecureConnection { get; }
        public virtual WindowsIdentity LogonUserIdentity { get; }
        public virtual NameValueCollection Params { get; }
        public virtual string Path { get; }
        public virtual string PathInfo { get; }
        public virtual string PhysicalApplicationPath { get; }
        public virtual string PhysicalPath { get; }
        public virtual string RawUrl { get; }
        public virtual RequestContext RequestContext { get; internal set; }
        public virtual string RequestType { get; set; }
        public virtual NameValueCollection ServerVariables { get; }
        public virtual int TotalBytes { get; }
        public virtual Uri Url { get; }
        public virtual Uri UrlReferrer { get; }
        public virtual string UserAgent { get; }
        public virtual string[] UserLanguages { get; }
        public virtual string UserHostAddress { get; }
        public virtual string UserHostName { get; }
        public virtual NameValueCollection Headers { get; }
        public virtual NameValueCollection QueryString { get; }
        public virtual string this[string key] { get; }
        public virtual byte[] BinaryRead(int count);
        public virtual int[] MapImageCoordinates(string imageFieldName);
        public virtual string MapPath(string virtualPath);
        public virtual string MapPath(string virtualPath, string baseVirtualDir, bool allowCrossAppMapping);
        public virtual void ValidateInput();
        public virtual void SaveAs(string filename, bool includeHeaders);
    }
}
