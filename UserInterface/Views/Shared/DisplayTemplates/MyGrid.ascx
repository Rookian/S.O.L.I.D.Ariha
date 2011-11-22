<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IList>" %>
<%@ Import Namespace="UserInterface.Extensions" %>
<%@ Import Namespace="UserInterface.Models" %>
<script runat="server">
    public static bool ShouldShow(ModelMetadata metadata,
        ViewDataDictionary viewData)
    {
        return metadata.ShowForDisplay
          && !metadata.IsComplexType
          && !viewData.TemplateInfo.Visited(metadata);
    }
</script>

<%
    var properties = ModelMetadata.FromLambdaExpression(m => m[0], ViewData)
      .Properties
      .Where(pm => ShouldShow(pm, ViewData));
%>
<table>
    <tr>
        <th>
        </th>
        <% foreach (var property in properties)
           { %>
        <th>
            <%= property.GetDisplayName() %>
        </th>
        <% } %>
    </tr>
    <% for (int i = 0; i < Model.Count; i++)
       {
           var itemMD = ModelMetadata.FromLambdaExpression(m => m[i], ViewData); %>
    <tr>
        <td>
            <% = Html.EditImageButton(((IGridViewModel)Model[i]).EditAndDeleteId) %>
            <% = Html.DeleteImageButton(((IGridViewModel)Model[i]).EditAndDeleteId) %>
        </td>
        <% foreach (var property in properties)
           { %>
        <td>
            <% var propertyMetadata = itemMD.Properties
              .Single(m => m.PropertyName == property.PropertyName); %>
            <%= Html.DisplayFor(m => propertyMetadata.Model) %>
        </td>
        <% } %>
    </tr>
    <% } %>
</table>
