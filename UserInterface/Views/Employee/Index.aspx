<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="ViewPage<Core.Common.Paging.PagedList<UserInterface.Models.TeamEmployeeForm>>" %>

<%@ Import Namespace="UserInterface.Extensions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    List of Employees
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        List of Employees</h2>
    <br />
    <div id="gridcontainer">
        <% Html.RenderPartial("Grid"); %>
        <%Html.RenderPager();%>
    </div>
    <br />
    <br />
    <%= Html.StandardOverlayCreateButton() %>
    <div class="apple_overlay" id="overlay">
        <div class="contentWrap"></div>
    </div>
</asp:Content>
