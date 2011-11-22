<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Core.Domain.Model.ConsumerProtection.SalesmanArticle>>" %>

<%@ Import Namespace="Core.Domain.Model.ConsumerProtection" %>
<%@ Import Namespace="UserInterface.Extensions" %>
<%@ Import Namespace="UserInterface.Constants" %>
<%@ Import Namespace="System.Globalization" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        <script type="text/javascript">
            $(function () {
                $('#section').load('<%= Url.Action("Data") %>');
            });
        </script>
        Article grouped by month and description</h2>
    <div id="section">
    </div>
    <table>
        <tr>
            <th>
                Goodsgroup
            </th>
            <th>
                Month
            </th>
            <th>
                Amount
            </th>
            <th>
                Cost
            </th>
        </tr>
        <% foreach (var item in ((List<SalesmanArticleGroupedByMonthAndDescription>)ViewData[Keys.SalesmanArticleGroupedByMonthAndDescription]))
           { %>
        <tr>
            <td>
                <%=Html.Encode(item.Description)%>
            </td>
            <td>
                <%= Html.Encode(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(item.Month)) %>
            </td>
            <td align="right">
                <%=Html.Encode(item.Amount)%>
            </td>
            <td align="right">
                <%= Html.Encode(String.Format("{0:c}",item.Cost)) %>
            </td>
        </tr>
        <% } %>
    </table>
    <br />
    <h2>
        SalesmanArticle</h2>
    <table>
        <tr>
            <th>
            </th>
            <th>
                Article
            </th>
            <th>
                Salesman
            </th>
            <th>
                Goodsgroup
            </th>
            <th>
                Date
            </th>
            <th>
                Amount
            </th>
            <th>
                Cost
            </th>
            <th>
                Sum
            </th>
        </tr>
        <% foreach (var item in Model)
           { %>
        <tr>
            <td>
                <%= Html.ActionLink("Edit", "Edit", new { id=item.Id }, new {rel = "#overlay"}) %>
                |
                <%= Html.ActionLink("Delete", "Delete", new { id=item.Id})%>
            </td>
            <td>
                <% if (item.Article != null)
                   {%>
                <%=Html.Encode(item.Article.Description)%>
                <%}%>
            </td>
            <td>
                <% if (item.Salesman != null)
                   {%>
                <%=Html.Encode(item.Salesman.Name)%>
                <%}%>
            </td>
            <td>
                <% if (item.Article != null && item.Article.GoodsGroup != null)
                   {%>
                <% =Html.Encode(item.Article.GoodsGroup.Description)%>
                <%
                    }%>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:d}", item.Date))%>
            </td>
            <td>
                <%= Html.Encode(item.Amount) %>
            </td>
            <td align="right">
                <%= Html.Encode(String.Format("{0:c}", item.Cost)) %>
            </td>
            <td align="right">
                <%= Html.Encode(String.Format("{0:c}",item.Sum)) %>
            </td>
        </tr>
        <% } %>
    </table>
    <%= Html.StandardOverlayCreateButton() %>
    
    <div class="apple_overlay" id="overlay">
        <div class="contentWrap">
        </div>
    </div>
</asp:Content>
