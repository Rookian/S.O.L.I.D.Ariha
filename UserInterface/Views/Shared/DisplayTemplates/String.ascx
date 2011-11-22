<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="Core.Common" %>
<%= Html.Encode(ViewData.TemplateInfo.FormattedModelValue.ToNullSafeString()) %>