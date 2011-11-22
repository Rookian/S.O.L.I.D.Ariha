// Type: System.Web.Mvc.RedirectResult
// Assembly: System.Web.Mvc, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// Assembly location: c:\Program Files (x86)\Microsoft ASP.NET\ASP.NET MVC 3\Assemblies\System.Web.Mvc.dll

namespace System.Web.Mvc
{
    public class RedirectResult : ActionResult
    {
        public RedirectResult(string url);
        public RedirectResult(string url, bool permanent);
        public bool Permanent { get; }
        public string Url { get; }
        public override void ExecuteResult(ControllerContext context);
    }
}
