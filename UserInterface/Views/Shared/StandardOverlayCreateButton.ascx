<%@ Control Language="C#" ClassName="StandardOverlayCreateButton" %>
<%@ Import Namespace="UserInterface.Extensions" %>

<a href="/<% = Html.GetControllerName() %>/Create" rel="#overlay" style="text-decoration:none">
    <button type="button">
        Create</button>
</a>