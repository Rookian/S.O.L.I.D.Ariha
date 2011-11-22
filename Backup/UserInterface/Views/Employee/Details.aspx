<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Overlay.master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Core.Domain.Model.LoanedItem>>" %>
<%@ Import Namespace="UserInterface.Constants" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Loaned items
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Loaned items for <%= ViewData[Keys.EmployeeName]%></h2>
    <table>
        <tr>
            <th>
                Name
            </th>
            <th>
                DateOfIssue
            </th>
            <th>
                IncludesCDDVD
            </th>
        </tr>
        <% foreach (var item in Model)
           { %>
        <tr>
            <td>
                <%= Html.Encode(item.Name) %>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:g}", item.DateOfIssue)) %>
            </td>
            <td>
                <%= Html.Encode(item.IncludesCDDVD) %>
            </td>
        </tr>
        <% } %>
    </table>
</asp:Content>
