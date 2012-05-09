<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_Membership_Controls_Roles_Roles"
    CodeFile="Roles.ascx.cs" %>
<%@ Register Src="Role.ascx" TagName="Role" TagPrefix="cms" %>
<%@ Register Src="RoleList.ascx" TagName="RoleList" TagPrefix="cms" %>
<%@ Register Src="RoleEdit.ascx" TagName="RoleEdit" TagPrefix="cms" %>
<asp:Panel runat="server" ID="pnlBody" CssClass="Roles">
    <asp:Panel runat="server" ID="headerLinks" CssClass="PageHeaderLinks">
        <div class="Actions">
            <asp:Image ID="imgNewRole" runat="server" CssClass="NewItemImage" />
            <cms:LocalizedLinkButton ID="btnNewRole" runat="server" CssClass="NewItemLink" ResourceString="Administration-Role_List.NewRole" />
        </div>
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlRolesBreadcrumbs">
        <div class="Actions">
            <cms:LocalizedLinkButton ID="btnBreadCrumbs" ResourceString="general.roles" runat="server"
                CssClass="TitleBreadCrumb" />
            <span class="TitleBreadCrumbSeparator">&nbsp;</span>
            <cms:LocalizedLabel ID="lblRole" runat="server" CssClass="TitleBreadCrumbLast" ResourceString="Administration-Role_New.NewRole" />
        </div>
    </asp:Panel>
    <div class="RolesContent">
        <cms:Role ID="Role" runat="server" />
        <cms:RoleList ID="RoleList" runat="server" />
        <cms:RoleEdit ID="RoleEdit" runat="server" />
    </div>
    <asp:Literal ID="ltlScript" runat="server" />
</asp:Panel>
