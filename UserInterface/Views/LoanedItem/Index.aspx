<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<UserInterface.Models.LoanedItemForm>>" %>

<%@ Import Namespace="UserInterface.Extensions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    List of loaned items
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        List of loaned items</h2>
    <table>
        <tr>
            <th>
            </th>
            <th>
                Name
            </th>
            <th>
                DateOfIssue
            </th>
            <th>
                IsLoaned
            </th>
            <th>
                IncludesCDDVD
            </th>
            <th>
                Loaned by
            </th>
        </tr>
        <% foreach (var item in Model)
           { %>
        <tr>
            <td>
                <%= Html.ActionLink("Edit", "Edit", new { id=item.Id}, new { rel = "#overlay" }) %>
                |
                <%= Html.ActionLink("Delete", "Delete", new { id = item.Id })%>
            </td>
            <td>
                <%= Html.Encode(item.Name) %>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:g}", item.DateOfIssue)) %>
            </td>
            <td>
                <%= Html.Encode(item.IsLoaned) %>
            </td>
            <td>
                <%= Html.Encode(item.IncludesCDDVD) %>
            </td>
            <td>
                <%= Html.Encode(item.EmployeeName) %>
            </td>
        </tr>
        <% } %>
    </table>
    <br />
    <%= Html.StandardOverlayCreateButton()%>
    <br class="clear" />
    <div class="apple_overlay" id="overlay">
        <div class="contentWrap">
        </div>
    </div>
</asp:Content>
