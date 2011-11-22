<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IList>" %>
<%@ Import Namespace="UserInterface.Extensions" %>
<%@ Import Namespace="UserInterface.Models" %>


<%= Html.ListBox(ViewData.ModelMetadata.PropertyName, ((IEnumerable<IDropdownList>)ViewData.Model).ToSelectList())%>