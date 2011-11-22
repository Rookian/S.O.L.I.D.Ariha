<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Core.Domain.Model.Team>>" %>

<%@ Import Namespace="UserInterface.Extensions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    List of Teams
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        List of Teams</h2>
    <table>
        <tr>
            <th>
            </th>
            <th>
                Name
            </th>
        </tr>
        <% foreach (var item in Model)
           { %>
        <tr>
            <td>
                <%= Html.ActionLink("Edit", "Edit", new { item.Id }, new { rel = "#overlay" }) %>
                |
                <%= Html.ActionLink("Delete", "Delete", new { item.Id })%>
            </td>
            <td>
                <%= Html.Encode(item.Name) %>
            </td>
        </tr>
        <% } %>
    </table>
    <br />
    <%= Html.StandardOverlayCreateButton() %>
    <br class="clear" />
    <div class="apple_overlay" id="overlay">
        <div class="contentWrap">
        </div>
    </div>
</asp:Content>
