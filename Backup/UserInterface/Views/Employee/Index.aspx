<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<UserInterface.Models.EmployeeForm>>" %>

<%@ Import Namespace="UserInterface.Extensions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    List of Employees
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        List of Employees</h2>
    <table>
        <tr>
            <th>
            </th>
            <th>
                Last name
            </th>
            <th>
                First name
            </th>
            <th>
                E-Mail
            </th>
            <th>
                Team
            </th>
        </tr>
        <% foreach (var item in Model)
           { %>
        <tr>
            <td>
                <%= Html.ActionLink("Edit", "Edit", new { id = item.Id }, new { rel = "#overlay" })%> |
                <%= Html.ActionLink("Details", "Details", new { id = item.Id }, new { rel = "#overlay" })%> |
                <%= Html.ActionLink("Delete", "Delete", new { id = item.Id })%>
            </td>
            <td>
                <%= Html.Encode(item.LastName) %>
            </td>
            <td>
                <%= Html.Encode(item.FirstName) %>
            </td>
            <td>
                <%= Html.Encode(item.EMail) %>
            </td>
            <td>
                <%= Html.Encode(item.TeamName)%>
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
