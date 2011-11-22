<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.Common.Paging.PagerViewModel>" %>

<hr />

<ul class="pager">
    <% if (Model.PagerInfo.HasPreviousPage) { %>
        <li class="previous"><a href="<%= Url.Action(Model.Action, Model.Controller, new { page = Model.PagerInfo.PageIndex - 1 }) %>">« Zurück</a></li>
    <% } else { %>
        <li class="previous-off">« Zurück</li>
    <% } %>
    <%for (int page = 1; page <= Model.PagerInfo.TotalPages; page++) {
        if (page == Model.PagerInfo.PageIndex) { %>
        <li class="active"><%=page.ToString()%></li>
        <% } else { %>
            <li><a href="<%= Url.Action(Model.Action, Model.Controller, new { page }) %>"><%= page %></a></li>
        <% }
        } 
        if (Model.PagerInfo.HasNextPage) { %>
        <li class="next"><a href="<%= Url.Action(Model.Action, Model.Controller, new { page = Model.PagerInfo.PageIndex + 1 }) %>">Weiter »</a></li>
        <% } else { %>
        <li class="next-off">Weiter »</li>
    <% } %>
</ul>