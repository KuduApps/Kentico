<%@ Page Language="C#" AutoEventWireup="true" CodeFile="User_Edit_Membership.aspx.cs"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Inherits="CMSModules_Membership_Pages_Users_User_Edit_Membership" %>

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
                    <cms:LocalizedLabel runat="server" ID="lblMembershipInfo" DisplayColon="true" ResourceString="edit_user.membershipInfo"
                        EnableViewState="false" CssClass="InfoLabel" />
                </strong>
                <cms:LocalizedLabel runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
                    ResourceString="General.ChangesSaved" Visible="false" />
            </ContentTemplate>
        </cms:CMSUpdatePanel>
        <cms:CMSUpdatePanel ID="pnlUpdate" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:HiddenField runat="server" ID="hdnDate" />
                <div style="display: none">
                    <cms:DateTimePicker runat="server" ID="ucCalendar" />
                </div>
                <cms:UniSelector runat="server" ID="usMemberships" IsLiveSite="false" ObjectType="cms.membership"
                    ListingObjectType="cms.membershiplist" SelectionMode="Multiple" ResourcePrefix="membershipselector" />
            </ContentTemplate>
        </cms:CMSUpdatePanel>
    </asp:PlaceHolder>
</asp:Content>
