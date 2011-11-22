<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<UserInterface.Models.TeamEmployeeInput>" %>

<%@ Import Namespace="UserInterface.Extensions" %>
<div>
    <h2>
        Create</h2>
    <% using (Html.BeginForm("Create", "Employee", FormMethod.Post, new { id = "employee_form" }))
       {
    %>
    <%= Html.ValidationSummary(true) %>
    <fieldset>
        <legend>Fields</legend>
        <div class="editor-label">
            <%= Html.LabelFor(x=>x.EmployeeLastName) %>
        </div>
        <div class="editor-field">
            <%= Html.TextBoxFor(model => model.EmployeeLastName)%>
            <%= Html.ValidationMessageFor(model => model.EmployeeLastName) %>
        </div>
        <div class="editor-label">
            <%= Html.LabelFor(model => model.EmployeeFirstName) %>
        </div>
        <div class="editor-field">
            <%= Html.TextBoxFor(model => model.EmployeeFirstName) %>
            <%= Html.ValidationMessageFor(model => model.EmployeeFirstName) %>
        </div>
        <div class="editor-label">
            <%= Html.LabelFor(model => model.EmployeeEMail) %>
        </div>
        <div class="editor-field">
            <%= Html.TextBoxFor(model => model.EmployeeEMail) %>
            <%= Html.ValidationMessageFor(model => model.EmployeeEMail) %>
        </div>
        <div class="editor-label">
            <%= Html.LabelFor(model => model.Teams) %>
        </div>
        <div class="editor-field">
            <%= Html.DropDownListFor(x => x.SelectedTeam, Model.Teams, Model.SelectedTeam) %>
        </div>
    </fieldset>
    <p>
        <input type="submit" value="Create" />
    </p>
    <% } %>
</div>
<script type="text/javascript">
    $("#employee_form").submit(function (event) {
        event.preventDefault();
        ajaxFormRequest(this, update_grid, "html");
    });
</script>
