<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Overlay.master" Inherits="System.Web.Mvc.ViewPage<Core.Domain.Model.Employee>" %>
<%@ Import Namespace="UserInterface.Constants" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Edit
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Edit</h2>
    <% using (Html.BeginForm("Edit", "Employee", FormMethod.Post))
       {%>
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
        <%= Html.DropDownList("teamId", ViewData[Keys.Teams] as SelectList)%>
        <p>
            <input type="submit" value="Save" />
        </p>
    </fieldset>
    <% } %>
</asp:Content>
