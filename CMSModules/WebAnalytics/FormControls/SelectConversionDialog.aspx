<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectConversionDialog.aspx.cs"
    Inherits="CMSModules_WebAnalytics_FormControls_SelectConversionDialog" Theme="default"
    MasterPageFile="~/CMSMasterPages/UI/Dialogs/ModalDialogPage.master" %>

<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagPrefix="cms" TagName="UniGrid" %>
<asp:Content ID="cntContent" ContentPlaceHolderID="plcContent" runat="Server">
    <div class="PageHeaderLine">
        <cms:LocalizedLabel ID="lblNameFilter" runat="server" ResourceString="om.conversionnameoritspart" DisplayColon="true" />
        <cms:CMSTextBox ID="txtNameFilter" runat="server" CssClass="TextBoxField" />
        <cms:LocalizedButton runat="server" ID="btnSelect" ResourceString="general.search"
            CssClass="ContentButton" />
    </div>
    <div class="UniSelectorDialogGridArea">
        <cms:UniGrid runat="server" ID="ucConversions" />
    </div>
</asp:Content>
<asp:Content ID="cntFooter" ContentPlaceHolderID="plcFooter" runat="server">
    <div class="FloatRight">
        <cms:LocalizedButton ID="btnCancel" runat="server" CssClass="SubmitButton" ResourceString="general.cancel"
            EnableViewState="False" />
    </div>
</asp:Content>
