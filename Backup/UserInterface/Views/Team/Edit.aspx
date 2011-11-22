<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Overlay.master" Inherits="System.Web.Mvc.ViewPage<Core.Domain.Model.Team>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Edit</h2>

    <% using (Html.BeginForm("Edit", "Team", FormMethod.Post)) {%>
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

            <p>
                <input type="submit" value="Save" />
            </p>
        </fieldset>
    <% } %>
</asp:Content>

