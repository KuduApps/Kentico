<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CMSMasterPages/UI/Dialogs/ModalDialogPage.master"
    EnableEventValidation="false" Theme="Default" CodeFile="Add_Contact_Dialog.aspx.cs"
    Inherits="CMSModules_ContactManagement_Pages_Tools_Account_Add_Contact_Dialog" %>

<%@ Register Src="~/CMSModules/ContactManagement/FormControls/ContactRoleSelector.ascx"
    TagName="ContactRoleSelector" TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/UniSelector/Controls/SelectionDialog.ascx"
    TagName="SelectionDialog" TagPrefix="cms" %>
<asp:Content ID="cntContent" ContentPlaceHolderID="plcContent" runat="Server">
    <cms:SelectionDialog runat="server" ID="selectionDialog" IsLiveSite="false" />
    <asp:Panel runat="server" ID="pnlRole" CssClass="UniSelectorDialogGridArea">
        <cms:LocalizedLabel ID="lblAddAccounts" runat="server" ResourceString="om.contact.addwithrole"
            EnableViewState="false" CssClass="LeftAlign AddContactLabel" DisplayColon="true" /><cms:ContactRoleSelector
                ID="contactRoleSelector" ShortID="rs" runat="server" CssClass="LeftAlign" IsLiveSite="false" />
    </asp:Panel>
</asp:Content>
<asp:Content ID="cntFooter" ContentPlaceHolderID="plcFooter" runat="server">
    <div class="FloatRight">
        <cms:LocalizedButton ID="btnOk" runat="server" CssClass="SubmitButton" EnableViewState="False" /><cms:LocalizedButton
            ID="btnCancel" runat="server" CssClass="SubmitButton" EnableViewState="False" />
    </div>
</asp:Content>
