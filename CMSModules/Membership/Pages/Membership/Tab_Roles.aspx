<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master"
    Title="Membership properties – roles" CodeFile="Tab_Roles.aspx.cs" Theme="Default"
    Inherits="CMSModules_Membership_Pages_Membership_Tab_Roles" %>

<%@ Register Src="~/CMSAdminControls/UI/UniSelector/UniSelector.ascx" TagName="UniSelector"
    TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <cms:CMSUpdatePanel ID="pnlBasic" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <strong>
                <cms:LocalizedLabel runat="server" ID="lblRolesInfo" DisplayColon="true" ResourceString="membership.assignedtoroles"
                    EnableViewState="false" CssClass="InfoLabel" />
            </strong>
            <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
                Visible="false" />
        </ContentTemplate>
    </cms:CMSUpdatePanel>
    <cms:CMSUpdatePanel ID="pnlUpdate" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <cms:UniSelector runat="server" ID="usRoles" IsLiveSite="false" ObjectType="cms.role"
                SelectionMode="Multiple" ResourcePrefix="addroles" />
        </ContentTemplate>
    </cms:CMSUpdatePanel>
</asp:Content>
