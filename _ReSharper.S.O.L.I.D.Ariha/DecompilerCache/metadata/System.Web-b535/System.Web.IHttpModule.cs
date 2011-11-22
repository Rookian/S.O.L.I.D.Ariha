// Type: System.Web.IHttpModule
// Assembly: System.Web, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// Assembly location: C:\Windows\Microsoft.NET\Framework\v2.0.50727\System.Web.dll

namespace System.Web
{
    public interface IHttpModule
    {
        void Init(HttpApplication context);
        void Dispose();
    }
}
