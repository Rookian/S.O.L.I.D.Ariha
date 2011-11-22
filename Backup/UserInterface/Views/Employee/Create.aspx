<%@ Page Title="" MasterPageFile="~/Views/Shared/Overlay.master" Language="C#" Inherits="System.Web.Mvc.ViewPage<Core.Domain.Model.Employee>" %>
<%@ Import Namespace="UserInterface.Constants" %>

<asp:Content ID="TitleContent" runat="server" ContentPlaceHolderID="TitleContent">
Create Employee
</asp:Content>
<asp:Content ID="ContentId" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Create</h2>
    <% using (Html.BeginForm("Create", "Employee", FormMethod.Post))
       {
    %>
    <%= Html.ValidationSummary(true) %>
    <fieldset>
        <legend>Fields</legend>
        <div class="editor-label">
            <%= Html.LabelFor(model => model.LastName) %>
        </div>
        <div class="editor-field">
            <%= Html.TextBoxFor(model => model.LastName) %>
            <%= Html.ValidationMessageFor(model => model.LastName) %>
        </div>
        <div class="editor-label">
            <%= Html.LabelFor(model => model.FirstName) %>
        </div>
        <div class="editor-field">
            <%= Html.TextBoxFor(model => model.FirstName) %>
            <%= Html.ValidationMessageFor(model => model.FirstName) %>
        </div>
        <div class="editor-label">
            <%= Html.LabelFor(model => model.EMail) %>
        </div>
        <div class="editor-field">
            <%= Html.TextBoxFor(model => model.EMail) %>
            <%= Html.ValidationMessageFor(model => model.EMail) %>
        </div>
        <div class="editor-field">
            <%= Html.DropDownList("teamId", ViewData[Keys.Teams] as SelectList) %>
        </div>
    </fieldset>
    <p>
        <input type="submit" value="Create" />
    </p>
    <% } %>
</asp:Content>
