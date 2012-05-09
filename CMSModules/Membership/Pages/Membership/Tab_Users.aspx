<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Tab_Users.aspx.cs" Inherits="CMSModules_Membership_Pages_Membership_Tab_Users"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Theme="Default" %>

<%@ Register Src="~/CMSAdminControls/UI/UniSelector/UniSelector.ascx" TagName="UniSelector"
    TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <cms:CMSUpdatePanel runat="server" ID="pnlBasic" UpdateMode="Conditional">
        <ContentTemplate>
            <cms:LocalizedLabel ID="lblAvialable" runat="server" CssClass="BoldInfoLabel" DisplayColon="true"
                ResourceString="Membership.assignedusers" />
            <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
                Visible="false" />
            <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
                Visible="false" />
        </ContentTemplate>
    </cms:CMSUpdatePanel>
    <div style="display: none">
        <cms:DateTimePicker runat="server" ID="ucCalendar" />
    </div>
    <cms:UniSelector ID="usUsers" runat="server" IsLiveSite="false" ListingObjectType="cms.membershiplist"
        ObjectType="cms.user" SelectionMode="Multiple" ResourcePrefix="addusers" DisplayNameFormat="##USERDISPLAYFORMAT##" />
    <asp:HiddenField runat="server" ID="hdnDate" />
    <asp:HiddenField runat="server" ID="hdnSendNotification" />
</asp:Content>
