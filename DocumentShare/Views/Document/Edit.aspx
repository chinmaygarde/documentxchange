<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<DocumentShare.Models.DocumentViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Edit</h2>

    <%= Html.ValidationSummary("Edit was unsuccessful. Please correct the errors and try again.") %>
    
    <% using (Html.BeginForm()) {%>

        <fieldset>
            <p>Title: <%= Html.TextBox("Title", Model.CurrentDocument.Title.ToString()) %></p>
            <p>Description: <%= Html.TextArea("Description", Model.CurrentDocument.Description.ToString()) %></p>
            <p>
                <input type="submit" value="Save" />
            </p>
        </fieldset>

    <% } %>
    <hr />
    <h3>Categories (Can only add for now)</h3><br />
    <% using (Html.BeginForm("Create", "Category", FormMethod.Post)) {%>
        <%= Html.Hidden("DocumentId", Model.CurrentDocument.Id) %>
        <%= Html.TextBox("Categories") %> <input type="submit" value="Edit" />
    <% } %>
    <hr />
    <div>
        <%=Html.ActionLink("Back to List", "Index") %>
    </div>

</asp:Content>

