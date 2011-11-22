<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Overlay.master" Inherits="System.Web.Mvc.ViewPage<Core.Domain.Model.LoanedItem>" %>

<%@ Import Namespace="UserInterface.Constants" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Create
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Create</h2>
    <% using (Html.BeginForm("Create", "LoanedItem", FormMethod.Post))
       {%>
    <%= Html.ValidationSummary(true) %>
    <fieldset>
        <legend>Fields</legend>
        <div class="editor-label">
            <%= Html.LabelFor(model => model.Name) %>
        </div>
        <div class="editor-field">
            <%= Html.TextBoxFor(model => model.Name) %>
            <%= Html.ValidationMessageFor(model => model.Name) %>
        </div>
        <div class="editor-label">
            <%= Html.LabelFor(model => model.DateOfIssue) %>
        </div>
        <div class="editor-field">
            <%= Html.TextBoxFor(model => model.DateOfIssue) %>
            <%= Html.ValidationMessageFor(model => model.DateOfIssue) %>
        </div>
        <div class="editor-label">
            <%= Html.LabelFor(model => model.IncludesCDDVD) %>
        </div>
        <div class="editor-field">
            <%= Html.TextBoxFor(model => model.IncludesCDDVD) %>
            <%= Html.ValidationMessageFor(model => model.IncludesCDDVD) %>
        </div>
        <p>
            <input type="submit" value="Create" />
        </p>
    </fieldset>
    <% } %>
</asp:Content>
