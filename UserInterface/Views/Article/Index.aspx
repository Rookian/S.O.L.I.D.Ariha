﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Core.Domain.Model.ConsumerProtection.Article>>" %>

<%@ Import Namespace="UserInterface.Extensions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Index</h2>
    <table>
        <tr>
            <th>
            </th>
            <th>
                Description
            </th>
            <th>
                GoodsGroup
            </th>
        </tr>
        <% foreach (var item in Model)
           { %>
        <tr>
            <td>
                <%= Html.ActionLink("Edit", "Edit", new { id=item.Id  }, new { rel = "#overlay"}) %>
                |
                <%= Html.ActionLink("Delete", "Delete", new { id = item.Id })%>
            </td>
            <td>
                <%= Html.Encode(item.Description)%>
            </td>
            <td>
                <% if (item.GoodsGroup != null)
                   {%>
                <%=Html.Encode(item.GoodsGroup.Description) %>
                <%}%>
            </td>
        </tr>
        <% } %>
    </table>
    <%= Html.StandardOverlayCreateButton() %>
    <br class="clear" />
    <div class="apple_overlay" id="overlay">
        <div class="contentWrap">
        </div>
    </div>
</asp:Content>