<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Overlay.master" Inherits="System.Web.Mvc.ViewPage<Core.Domain.Model.LoanedItem>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Edit</h2>

    <% using (Html.BeginForm()) {%>
        <%= Html.ValidationSummary(true) %>
        
        <fieldset>
            <legend>Fields</legend>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.DateOfIssue) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.DateOfIssue, String.Format("{0:g}", Model.DateOfIssue)) %>
                <%= Html.ValidationMessageFor(model => model.DateOfIssue) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.IsLoaned) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.IsLoaned) %>
                <%= Html.ValidationMessageFor(model => model.IsLoaned) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.Name) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.Name) %>
                <%= Html.ValidationMessageFor(model => model.Name) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.IncludesCDDVD) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.IncludesCDDVD) %>
                <%= Html.ValidationMessageFor(model => model.IncludesCDDVD) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.Id) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.Id) %>
                <%= Html.ValidationMessageFor(model => model.Id) %>
            </div>
            
            <p>
                <input type="submit" value="Save" />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%= Html.ActionLink("Back to List", "Index") %>
    </div>

</asp:Content>

