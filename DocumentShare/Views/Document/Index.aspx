<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<DocumentShare.Models.DocumentViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Index</h2>

    <table>
        <tr>
            <th>Title</th>
            <th>Description</th>
            <th></th>
        </tr>

    <% foreach (DocumentShare.Models.Document document in Model.Documents) { %>
    
        <tr>
            <td><strong><%= Html.ActionLink(document.Title, "Details", new { id = document.Id })%>
            </strong> by <i><%= document.User.UserName %></i></td>
            <td><i><%= document.Description %></i></td>         
            <td>
                <% if(document.User.UserName == User.Identity.Name) %>
                <%= Html.ActionLink("Edit", "Edit", new { id = document.Id }) %>
            </td>
        </tr>
    
    <% } %>

    </table>

    <p>
        <%= Html.ActionLink("Create New", "Create") %>
    </p>

</asp:Content>

