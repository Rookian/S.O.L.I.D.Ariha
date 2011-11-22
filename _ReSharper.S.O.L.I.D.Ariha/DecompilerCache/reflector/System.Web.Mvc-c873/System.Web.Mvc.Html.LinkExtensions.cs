namespace System.Web.Mvc.Html
{
  using System;
  using System.Collections.Generic;
  using System.Runtime.CompilerServices;
  using System.Web.Mvc;
  using System.Web.Mvc.Resources;
  using System.Web.Routing;

  /// <summary>Represents support for HTML links in an application.</summary>
  public static class LinkExtensions
  {
    /// <summary>Returns an anchor element (a element) that contains the virtual path of the specified action.</summary>
    /// <returns>An anchor element (a element).</returns>
    /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
    /// <param name="linkText">The inner text of the anchor element.</param>
    /// <param name="actionName">The name of the action.</param>
    /// <exception cref="T:System.ArgumentException">The <paramref name="linkText" /> parameter is null or empty.</exception>
    public static MvcHtmlString ActionLink(this HtmlHelper htmlHelper, string linkText, string actionName)
    {
      return htmlHelper.ActionLink(linkText, actionName, null, new RouteValueDictionary(), new RouteValueDictionary());
    }

    /// <summary>Returns an anchor element (a element) that contains the virtual path of the specified action.</summary>
    /// <returns>An anchor element (a element).</returns>
    /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
    /// <param name="linkText">The inner text of the anchor element.</param>
    /// <param name="actionName">The name of the action.</param>
    /// <param name="routeValues">An object that contains the parameters for a route. The parameters are retrieved through reflection by examining the properties of the object. The object is typically created by using object initializer syntax.</param>
    /// <exception cref="T:System.ArgumentException">The <paramref name="linkText" /> parameter is null or empty.</exception>
    public static MvcHtmlString ActionLink(this HtmlHelper htmlHelper, string linkText, string actionName, object routeValues)
    {
      return htmlHelper.ActionLink(linkText, actionName, null, new RouteValueDictionary(routeValues), new RouteValueDictionary());
    }

    /// <summary>Returns an anchor element (a element) that contains the virtual path of the specified action.</summary>
    /// <returns>An anchor element (a element).</returns>
    /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
    /// <param name="linkText">The inner text of the anchor element.</param>
    /// <param name="actionName">The name of the action.</param>
    /// <param name="controllerName">The name of the controller.</param>
    /// <exception cref="T:System.ArgumentException">The <paramref name="linkText" /> parameter is null or empty.</exception>
    public static MvcHtmlString ActionLink(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName)
    {
      return htmlHelper.ActionLink(linkText, actionName, controllerName, new RouteValueDictionary(), new RouteValueDictionary());
    }

    /// <summary>Returns an anchor element (a element) that contains the virtual path of the specified action.</summary>
    /// <returns>An anchor element (a element).</returns>
    /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
    /// <param name="linkText">The inner text of the anchor element.</param>
    /// <param name="actionName">The name of the action.</param>
    /// <param name="routeValues">An object that contains the parameters for a route.</param>
    /// <exception cref="T:System.ArgumentException">The <paramref name="linkText" /> parameter is null or empty.</exception>
    public static MvcHtmlString ActionLink(this HtmlHelper htmlHelper, string linkText, string actionName, RouteValueDictionary routeValues)
    {
      return htmlHelper.ActionLink(linkText, actionName, null, routeValues, new RouteValueDictionary());
    }

    /// <summary>Returns an anchor element (a element) that contains the virtual path of the specified action.</summary>
    /// <returns>An anchor element (a element).</returns>
    /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
    /// <param name="linkText">The inner text of the anchor element.</param>
    /// <param name="actionName">The name of the action.</param>
    /// <param name="routeValues">An object that contains the parameters for a route. The parameters are retrieved through reflection by examining the properties of the object. The object is typically created by using object initializer syntax.</param>
    /// <param name="htmlAttributes">An object that contains the HTML attributes for the element. The attributes are retrieved through reflection by examining the properties of the object. The object is typically created by using object initializer syntax.</param>
    /// <exception cref="T:System.ArgumentException">The <paramref name="linkText" /> parameter is null or empty.</exception>
    public static MvcHtmlString ActionLink(this HtmlHelper htmlHelper, string linkText, string actionName, object routeValues, object htmlAttributes)
    {
      return htmlHelper.ActionLink(linkText, actionName, null, new RouteValueDictionary(routeValues), new RouteValueDictionary(htmlAttributes));
    }

    /// <summary>Returns an anchor element (a element) that contains the virtual path of the specified action.</summary>
    /// <returns>An anchor element (a element).</returns>
    /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
    /// <param name="linkText">The inner text of the anchor element.</param>
    /// <param name="actionName">The name of the action.</param>
    /// <param name="routeValues">An object that contains the parameters for a route.</param>
    /// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element.</param>
    /// <exception cref="T:System.ArgumentException">The <paramref name="linkText" /> parameter is null or empty.</exception>
    public static MvcHtmlString ActionLink(this HtmlHelper htmlHelper, string linkText, string actionName, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes)
    {
      return htmlHelper.ActionLink(linkText, actionName, null, routeValues, htmlAttributes);
    }

    /// <summary>Returns an anchor element (a element) that contains the virtual path of the specified action.</summary>
    /// <returns>An anchor element (a element).</returns>
    /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
    /// <param name="linkText">The inner text of the anchor element.</param>
    /// <param name="actionName">The name of the action.</param>
    /// <param name="controllerName">The name of the controller.</param>
    /// <param name="routeValues">An object that contains the parameters for a route. The parameters are retrieved through reflection by examining the properties of the object. The object is typically created by using object initializer syntax.</param>
    /// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element.</param>
    /// <exception cref="T:System.ArgumentException">The <paramref name="linkText" /> parameter is null or empty.</exception>
    public static MvcHtmlString ActionLink(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, object routeValues, object htmlAttributes)
    {
      return htmlHelper.ActionLink(linkText, actionName, controllerName, new RouteValueDictionary(routeValues), new RouteValueDictionary(htmlAttributes));
    }

    /// <summary>Returns an anchor element (a element) that contains the virtual path of the specified action.</summary>
    /// <returns>An anchor element (a element).</returns>
    /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
    /// <param name="linkText">The inner text of the anchor element.</param>
    /// <param name="actionName">The name of the action.</param>
    /// <param name="controllerName">The name of the controller.</param>
    /// <param name="routeValues">An object that contains the parameters for a route.</param>
    /// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element.</param>
    /// <exception cref="T:System.ArgumentException">The <paramref name="linkText" /> parameter is null or empty.</exception>
    public static MvcHtmlString ActionLink(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes)
    {
      if (string.IsNullOrEmpty(linkText))
      {
        throw new ArgumentException(MvcResources.Common_NullOrEmpty, "linkText");
      }
      return MvcHtmlString.Create(HtmlHelper.GenerateLink(htmlHelper.ViewContext.RequestContext, htmlHelper.RouteCollection, linkText, null, actionName, controllerName, routeValues, htmlAttributes));
    }

    /// <summary>Returns an anchor element (a element) that contains the virtual path of the specified action.</summary>
    /// <returns>An anchor element (a element).</returns>
    /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
    /// <param name="linkText">The inner text of the anchor element.</param>
    /// <param name="actionName">The name of the action.</param>
    /// <param name="controllerName">The name of the controller.</param>
    /// <param name="protocol">The protocol for the URL, such as "http" or "https".</param>
    /// <param name="hostName">The host name for the URL.</param>
    /// <param name="fragment">The URL fragment name (the anchor name).</param>
    /// <param name="routeValues">An object that contains the parameters for a route. The parameters are retrieved through reflection by examining the properties of the object. The object is typically created by using object initializer syntax.</param>
    /// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element.</param>
    /// <exception cref="T:System.ArgumentException">The <paramref name="linkText" /> parameter is null or empty.</exception>
    public static MvcHtmlString ActionLink(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, string protocol, string hostName, string fragment, object routeValues, object htmlAttributes)
    {
      return htmlHelper.ActionLink(linkText, actionName, controllerName, protocol, hostName, fragment, new RouteValueDictionary(routeValues), new RouteValueDictionary(htmlAttributes));
    }

    /// <summary>Returns an anchor element (a element) that contains the virtual path of the specified action.</summary>
    /// <returns>An anchor element (a element).</returns>
    /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
    /// <param name="linkText">The inner text of the anchor element.</param>
    /// <param name="actionName">The name of the action.</param>
    /// <param name="controllerName">The name of the controller.</param>
    /// <param name="protocol">The protocol for the URL, such as "http" or "https".</param>
    /// <param name="hostName">The host name for the URL.</param>
    /// <param name="fragment">The URL fragment name (the anchor name).</param>
    /// <param name="routeValues">An object that contains the parameters for a route.</param>
    /// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element.</param>
    /// <exception cref="T:System.ArgumentException">The <paramref name="linkText" /> parameter is null or empty.</exception>
    public static MvcHtmlString ActionLink(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, string protocol, string hostName, string fragment, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes)
    {
      if (string.IsNullOrEmpty(linkText))
      {
        throw new ArgumentException(MvcResources.Common_NullOrEmpty, "linkText");
      }
      return MvcHtmlString.Create(HtmlHelper.GenerateLink(htmlHelper.ViewContext.RequestContext, htmlHelper.RouteCollection, linkText, null, actionName, controllerName, protocol, hostName, fragment, routeValues, htmlAttributes));
    }

    /// <summary>Returns an anchor element (a element) that contains the virtual path of the specified action.</summary>
    /// <returns>An anchor element (a element).</returns>
    /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
    /// <param name="linkText">The inner text of the anchor element.</param>
    /// <param name="routeValues">An object that contains the parameters for a route. The parameters are retrieved through reflection by examining the properties of the object. The object is typically created by using object initializer syntax.</param>
    /// <exception cref="T:System.ArgumentException">The <paramref name="linkText" /> parameter is null or empty.</exception>
    public static MvcHtmlString RouteLink(this HtmlHelper htmlHelper, string linkText, object routeValues)
    {
      return htmlHelper.RouteLink(linkText, new RouteValueDictionary(routeValues));
    }

    /// <summary>Returns an anchor element (a element) that contains the virtual path of the specified action.</summary>
    /// <returns>An anchor element (a element).</returns>
    /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
    /// <param name="linkText">The inner text of the anchor element.</param>
    /// <param name="routeName">The name of the route that is used to return a virtual path.</param>
    /// <exception cref="T:System.ArgumentException">The <paramref name="linkText" /> parameter is null or empty.</exception>
    public static MvcHtmlString RouteLink(this HtmlHelper htmlHelper, string linkText, string routeName)
    {
      return htmlHelper.RouteLink(linkText, routeName, null);
    }

    /// <summary>Returns an anchor element (a element) that contains the virtual path of the specified action.</summary>
    /// <returns>An anchor element (a element).</returns>
    /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
    /// <param name="linkText">The inner text of the anchor element.</param>
    /// <param name="routeValues">An object that contains the parameters for a route.</param>
    /// <exception cref="T:System.ArgumentException">The <paramref name="linkText" /> parameter is null or empty.</exception>
    public static MvcHtmlString RouteLink(this HtmlHelper htmlHelper, string linkText, RouteValueDictionary routeValues)
    {
      return htmlHelper.RouteLink(linkText, routeValues, new RouteValueDictionary());
    }

    /// <summary>Returns an anchor element (a element) that contains the virtual path of the specified action.</summary>
    /// <returns>An anchor element (a element).</returns>
    /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
    /// <param name="linkText">The inner text of the anchor element.</param>
    /// <param name="routeValues">An object that contains the parameters for a route. The parameters are retrieved through reflection by examining the properties of the object. The object is typically created by using object initializer syntax.</param>
    /// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element.</param>
    /// <exception cref="T:System.ArgumentException">The <paramref name="linkText" /> parameter is null or empty.</exception>
    public static MvcHtmlString RouteLink(this HtmlHelper htmlHelper, string linkText, object routeValues, object htmlAttributes)
    {
      return htmlHelper.RouteLink(linkText, new RouteValueDictionary(routeValues), new RouteValueDictionary(htmlAttributes));
    }

    /// <summary>Returns an anchor element (a element) that contains the virtual path of the specified action.</summary>
    /// <returns>An anchor element (a element).</returns>
    /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
    /// <param name="linkText">The inner text of the anchor element.</param>
    /// <param name="routeName">The name of the route that is used to return a virtual path.</param>
    /// <param name="routeValues">An object that contains the parameters for a route. The parameters are retrieved through reflection by examining the properties of the object. The object is typically created by using object initializer syntax.</param>
    /// <exception cref="T:System.ArgumentException">The <paramref name="linkText" /> parameter is null or empty.</exception>
    public static MvcHtmlString RouteLink(this HtmlHelper htmlHelper, string linkText, string routeName, object routeValues)
    {
      return htmlHelper.RouteLink(linkText, routeName, new RouteValueDictionary(routeValues));
    }

    /// <summary>Returns an anchor element (a element) that contains the virtual path of the specified action.</summary>
    /// <returns>An anchor element (a element).</returns>
    /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
    /// <param name="linkText">The inner text of the anchor element.</param>
    /// <param name="routeName">The name of the route that is used to return a virtual path.</param>
    /// <param name="routeValues">An object that contains the parameters for a route.</param>
    /// <exception cref="T:System.ArgumentException">The <paramref name="linkText" /> parameter is null or empty.</exception>
    public static MvcHtmlString RouteLink(this HtmlHelper htmlHelper, string linkText, string routeName, RouteValueDictionary routeValues)
    {
      return htmlHelper.RouteLink(linkText, routeName, routeValues, new RouteValueDictionary());
    }

    /// <summary>Returns an anchor element (a element) that contains the virtual path of the specified action.</summary>
    /// <returns>An anchor element (a element).</returns>
    /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
    /// <param name="linkText">The inner text of the anchor element.</param>
    /// <param name="routeValues">An object that contains the parameters for a route.</param>
    /// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element.</param>
    /// <exception cref="T:System.ArgumentException">The <paramref name="linkText" /> parameter is null or empty.</exception>
    public static MvcHtmlString RouteLink(this HtmlHelper htmlHelper, string linkText, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes)
    {
      return htmlHelper.RouteLink(linkText, null, routeValues, htmlAttributes);
    }

    /// <summary>Returns an anchor element (a element) that contains the virtual path of the specified action.</summary>
    /// <returns>An anchor element (a element).</returns>
    /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
    /// <param name="linkText">The inner text of the anchor element.</param>
    /// <param name="routeName">The name of the route that is used to return a virtual path.</param>
    /// <param name="routeValues">An object that contains the parameters for a route. The parameters are retrieved through reflection by examining the properties of the object. The object is typically created by using object initializer syntax.</param>
    /// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element.</param>
    /// <exception cref="T:System.ArgumentException">The <paramref name="linkText" /> parameter is null or empty.</exception>
    public static MvcHtmlString RouteLink(this HtmlHelper htmlHelper, string linkText, string routeName, object routeValues, object htmlAttributes)
    {
      return htmlHelper.RouteLink(linkText, routeName, new RouteValueDictionary(routeValues), new RouteValueDictionary(htmlAttributes));
    }

    /// <summary>Returns an anchor element (a element) that contains the virtual path of the specified action.</summary>
    /// <returns>An anchor element (a element).</returns>
    /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
    /// <param name="linkText">The inner text of the anchor element.</param>
    /// <param name="routeName">The name of the route that is used to return a virtual path.</param>
    /// <param name="routeValues">An object that contains the parameters for a route. </param>
    /// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element.</param>
    /// <exception cref="T:System.ArgumentException">The <paramref name="linkText" /> parameter is null or empty.</exception>
    public static MvcHtmlString RouteLink(this HtmlHelper htmlHelper, string linkText, string routeName, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes)
    {
      if (string.IsNullOrEmpty(linkText))
      {
        throw new ArgumentException(MvcResources.Common_NullOrEmpty, "linkText");
      }
      return MvcHtmlString.Create(HtmlHelper.GenerateRouteLink(htmlHelper.ViewContext.RequestContext, htmlHelper.RouteCollection, linkText, routeName, routeValues, htmlAttributes));
    }

    /// <summary>Returns an anchor element (a element) that contains the virtual path of the specified action.</summary>
    /// <returns>An anchor element (a element).</returns>
    /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
    /// <param name="linkText">The inner text of the anchor element.</param>
    /// <param name="routeName">The name of the route that is used to return a virtual path.</param>
    /// <param name="protocol">The protocol for the URL, such as "http" or "https".</param>
    /// <param name="hostName">The host name for the URL.</param>
    /// <param name="fragment">The URL fragment name (the anchor name).</param>
    /// <param name="routeValues">An object that contains the parameters for a route. The parameters are retrieved through reflection by examining the properties of the object. The object is typically created by using object initializer syntax.</param>
    /// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element.</param>
    /// <exception cref="T:System.ArgumentException">The <paramref name="linkText" /> parameter is null or empty.</exception>
    public static MvcHtmlString RouteLink(this HtmlHelper htmlHelper, string linkText, string routeName, string protocol, string hostName, string fragment, object routeValues, object htmlAttributes)
    {
      return htmlHelper.RouteLink(linkText, routeName, protocol, hostName, fragment, new RouteValueDictionary(routeValues), new RouteValueDictionary(htmlAttributes));
    }

    /// <summary>Returns an anchor element (a element) that contains the virtual path of the specified action.</summary>
    /// <returns>An anchor element (a element).</returns>
    /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
    /// <param name="linkText">The inner text of the anchor element.</param>
    /// <param name="routeName">The name of the route that is used to return a virtual path.</param>
    /// <param name="protocol">The protocol for the URL, such as "http" or "https".</param>
    /// <param name="hostName">The host name for the URL.</param>
    /// <param name="fragment">The URL fragment name (the anchor name).</param>
    /// <param name="routeValues">An object that contains the parameters for a route.</param>
    /// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element.</param>
    /// <exception cref="T:System.ArgumentException">The <paramref name="linkText" /> parameter is null or empty.</exception>
    public static MvcHtmlString RouteLink(this HtmlHelper htmlHelper, string linkText, string routeName, string protocol, string hostName, string fragment, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes)
    {
      if (string.IsNullOrEmpty(linkText))
      {
        throw new ArgumentException(MvcResources.Common_NullOrEmpty, "linkText");
      }
      return MvcHtmlString.Create(HtmlHelper.GenerateRouteLink(htmlHelper.ViewContext.RequestContext, htmlHelper.RouteCollection, linkText, routeName, protocol, hostName, fragment, routeValues, htmlAttributes));
    }
  }
}
