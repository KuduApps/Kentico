<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Membership_Pages_Users_User_ManageRoles" Theme="Default"
    MasterPageFile="~/CMSMasterPages/UI/Dialogs/ModalDialogPage.master" Title="Manage user roles" CodeFile="User_ManageRoles.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/Selectors/ItemSelection.ascx" TagName="ItemSelection"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSFormControls/Sites/SiteSelector.ascx" TagName="SiteSelector" TagPrefix="cms" %>
<asp:Content ID="cntControls" runat="server" ContentPlaceHolderID="plcSiteSelector">
    <cms:LocalizedLabel runat="server" ID="lblSite" ResourceString="general.site" DisplayColon="true" />
    <cms:SiteSelector ID="siteSelector" runat="server" IsLiveSite="false" />
</asp:Content>
<asp:Content ContentPlaceHolderID="plcContent" runat="server">
    <asp:PlaceHolder runat="server" ID="plcTable">
        <div class="PageContent">
            <asp:Panel ID="pnlUsers" runat="server" CssClass="UniSelectorDialogGridPadding">
                <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
                    Visible="false" />
                <cms:CMSUpdatePanel ID="pnlUpdate" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <cms:ItemSelection ID="itemSelectionElem" runat="server" ShowUpDownButtons="false" />
                    </ContentTemplate>
                </cms:CMSUpdatePanel>
            </asp:Panel>
        </div>
    </asp:PlaceHolder>
</asp:Content>
<asp:Content ID="cntFooter" runat="server" ContentPlaceHolderID="plcFooter">
    <div class="FloatRight">
        <cms:CMSButton ID="btnClose" runat="server" CssClass="SubmitButton" OnClientClick="window.close();return false;" />
    </div>
</asp:Content>
