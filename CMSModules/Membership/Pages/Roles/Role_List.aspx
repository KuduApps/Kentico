<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Membership_Pages_Roles_Role_List"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Roles - Role List"
    CodeFile="Role_List.aspx.cs" %>

<%@ Register Src="~/CMSModules/Membership/Controls/Roles/RoleList.ascx" TagName="RoleList"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSFormControls/Sites/SiteSelector.ascx" TagName="SiteSelector"
    TagPrefix="cms" %>
<asp:Content ContentPlaceHolderID="plcContent" runat="server">
    <asp:Panel ID="pnlSites" runat="server" CssClass="PageHeaderLine SiteHeaderLine">
        <cms:LocalizedLabel runat="server" ID="lblSites" EnableViewState="false" ResourceString="general.site"
            DisplayColon="true" />
        <cms:SiteSelector ID="siteSelector" runat="server" IsLiveSite="false" AllowGlobal="true" />
    </asp:Panel>
    <asp:Panel ID="pnlNewRole" runat="server" CssClass="PageHeaderLine" EnableViewState="false">
        <asp:Image ID="imgNewRole" runat="server" CssClass="NewItemImage" EnableViewState="false" />
        <asp:HyperLink ID="lnkNewRole" runat="server" CssClass="NewItemLink" EnableViewState="false" />
    </asp:Panel>
    <asp:Panel ID="pnlUsers" runat="server" CssClass="PageContent">
        <cms:CMSUpdatePanel ID="pnlUpdate" runat="server" UpdateMode="Conditional" ShowProgress="true">
            <ContentTemplate>
                <cms:RoleList ID="roleListElem" runat="server" IsLiveSite="false" />
            </ContentTemplate>
        </cms:CMSUpdatePanel>
    </asp:Panel>
    <asp:Literal ID="ltlScript" runat="server" />
</asp:Content>
