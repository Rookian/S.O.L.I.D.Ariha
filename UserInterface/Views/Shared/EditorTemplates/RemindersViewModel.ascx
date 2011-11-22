<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<UserInterface.Controllers.RemindersViewModel>" %>

<% =Html.DropDownListFor(x => x.SelectedReminder, new SelectList(Model.Reminder, "Value", "Text")) %>

