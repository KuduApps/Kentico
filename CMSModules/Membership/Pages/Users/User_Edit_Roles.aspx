<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Membership_Pages_Users_User_Edit_Roles"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="User Edit - Roles"
    CodeFile="User_Edit_Roles.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/UniSelector/UniSelector.ascx" TagName="UniSelector"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSFormControls/Sites/SiteSelector.ascx" TagName="SiteSelector"
    TagPrefix="cms" %>
<asp:Content ID="cntSiteSelect" runat="server" ContentPlaceHolderID="plcSiteSelector">
    <asp:PlaceHolder ID="plcSites" runat="server">
        <div style="padding-bottom: 0px;">
            <cms:LocalizedLabel ID="lblSelectSite" runat="server" ResourceString="general.site"
                DisplayColon="true" />&nbsp;
            <cms:SiteSelector ID="siteSelector" runat="server" IsLiveSite="false" />
        </div>
    </asp:PlaceHolder>
</asp:Content>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:Label runat="server" ID="lblErrorDeskAdmin" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
    <asp:PlaceHolder ID="plcTable" runat="server">
        <cms:CMSUpdatePanel ID="pnlBasic" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <strong>
                    <cms:LocalizedLabel runat="server" ID="lblRolesInfo" DisplayColon="true" ResourceString="edituserroles.userroles"
                        EnableViewState="false" CssClass="InfoLabel" />
                </strong>            
                <cms:LocalizedLabel runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
                    Visible="false" ResourceString="General.ChangesSaved" />
            </ContentTemplate>
        </cms:CMSUpdatePanel>
        <cms:CMSUpdatePanel ID="pnlUpdate" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div style="display: none">
                    <cms:DateTimePicker runat="server" ID="ucCalendar" />
                </div>
                <asp:Label runat="server" ID="lblError" Visible="false" CssClass="ErrorLabel" EnableViewState="false" />
                <asp:HiddenField runat="server" ID="hdnDate" />
                <cms:UniSelector ID="usRoles" runat="server" IsLiveSite="false" ListingObjectType="cms.userrolelist"
                    ObjectType="cms.role" SelectionMode="Multiple" ResourcePrefix="addroles" />
            </ContentTemplate>
        </cms:CMSUpdatePanel>
    </asp:PlaceHolder>
</asp:Content>
