<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<DocumentShare.Models.DocumentViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Details
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%= Model.CurrentDocument.Title %></h2>
    <p><%= Model.CurrentDocument.Description %></p>
    <p>
        <% if(Model.CurrentDocument.User.UserName == User.Identity.Name) %>
        <%=Html.ActionLink("Edit", "Edit", new { id=Model.CurrentDocument.Id }) %> |
        <%=Html.ActionLink("Back to List", "Index") %>
    </p>
    <h4>Categories</h4>
    <p> <% foreach (DocumentShare.Models.CategoryDocument catDoc in Model.CurrentDocument.CategoryDocuments) { %>
            <%= catDoc.Category.Name %> 
          <% } %></p>
    <% if (Model.CurrentDocument.Comments.Count == 0){%>
            <p>There are no comments!</p>
    <% } else { %>
            <h2>Comments</h2>
    <% } %>
    
    <% foreach (DocumentShare.Models.Comment comment in Model.CurrentDocument.Comments) {%>
        <p><strong><%= comment.User.UserName %></strong> says...</p>
        <p><%= comment.Description %></p>
        <hr />
    <% } %>
    
    <% if (User.Identity.IsAuthenticated) {%>
        Comment: <br />
        <% Html.BeginForm("Create", "Comment", FormMethod.Post); %>
            <%= Html.TextArea("Description") %><br />
            <%= Html.Hidden("DocumentId", Model.CurrentDocument.Id) %>
            <input type="submit" value="Comment" />
        <% Html.EndForm(); %>
    <% } %>
</asp:Content>