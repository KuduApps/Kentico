<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Membership_Pages_Roles_Role_Edit_Users"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Role Edit - Users"
    CodeFile="Role_Edit_Users.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/UniSelector/UniSelector.ascx" TagName="UniSelector"
    TagPrefix="cms" %>
<asp:Content ContentPlaceHolderID="plcContent" ID="content" runat="server">
    <cms:CMSUpdatePanel ID="pnlBasic" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Label ID="lblAvialable" runat="server" CssClass="BoldInfoLabel" />
            <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
                Visible="false" />
            <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
                Visible="false" />
        </ContentTemplate>
    </cms:CMSUpdatePanel>
    <div style="display: none">
        <cms:DateTimePicker runat="server" ID="ucCalendar" />
    </div>
    <cms:UniSelector ID="usUsers" runat="server" IsLiveSite="false" ListingObjectType="cms.userrolelist"
        ObjectType="cms.user" SelectionMode="Multiple" ResourcePrefix="addusers" DisplayNameFormat="##USERDISPLAYFORMAT##" />
    <asp:HiddenField runat="server" ID="hdnDate" />
</asp:Content>
