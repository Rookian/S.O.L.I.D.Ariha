<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Overlay.master" Inherits="System.Web.Mvc.ViewPage<Core.Domain.Model.ConsumerProtection.SalesmanArticle>" %>

<%@ Import Namespace="UserInterface.Constants" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Create
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Create</h2>
    <% using (Html.BeginForm())
       {%>
    <%= Html.ValidationSummary(true) %>
    <fieldset>
        <legend>Fields</legend>
        <div class="editor-label">
            <%= Html.LabelFor(model => model.Amount) %>
        </div>
        <div class="editor-field">
            <%= Html.TextBoxFor(model => model.Amount) %>
            <%= Html.ValidationMessageFor(model => model.Amount) %>
        </div>
        <div class="editor-label">
            <%= Html.LabelFor(model => model.Date) %>
        </div>
        <div class="editor-field">
            <%= Html.TextBoxFor(model => model.Date) %>
            <%= Html.ValidationMessageFor(model => model.Date) %>
        </div>
        <div class="editor-label">
            <%= Html.LabelFor(model => model.Cost) %>
        </div>
        <div class="editor-field">
            <%= Html.TextBoxFor(model => model.Cost) %>
            <%= Html.ValidationMessageFor(model => model.Cost) %>
        </div>
        <div class="editor-label">
            <label for="salesmanId">
                Salesman</label>
        </div>
        <div class="editor-field">
            <%= Html.DropDownList("salesmanId", ViewData[Keys.SalesmanDropDownList] as SelectList) %>
        </div>
        <div class="editor-label">
            <label for="articleId">
                Article</label>
        </div>
        <div class="editor-field">
            <%= Html.DropDownList("articleId", ViewData[Keys.ArticleDropDownList] as SelectList) %>
        </div>
        <p>
            <input type="submit" value="Create" />
        </p>
    </fieldset>
    <% } %>
</asp:Content>
