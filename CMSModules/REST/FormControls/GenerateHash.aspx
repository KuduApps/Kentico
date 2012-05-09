<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GenerateHash.aspx.cs"
    Inherits="CMSModules_REST_FormControls_GenerateHash" Theme="Default" EnableEventValidation="true"
    MaintainScrollPositionOnPostback="true" MasterPageFile="~/CMSMasterPages/UI/Dialogs/ModalDialogPage.master"
    Title="REST Service - Generate authetication hash" %>

<asp:Content ID="cntBody" ContentPlaceHolderID="plcContent" runat="server">
    <asp:Panel runat="server" ID="pnlContent" CssClass="PageContent">
        <cms:LocalizedLabel runat="server" ID="lblInfo" ResourceString="rest.generateauthhashinfo"
            EnableViewState="false" /><br /><br />
        <cms:CMSTextBox runat="server" ID="txtUrls" EnableViewState="false" TextMode="MultiLine"
            Width="97%" Height="130" />
    </asp:Panel>
</asp:Content>
<asp:Content ID="cntFooter" ContentPlaceHolderID="plcFooter" runat="server">
    <div class="FloatRight">
        <cms:CMSButton ID="btnAuthenticate" runat="server" CssClass="SubmitButton" EnableViewState="false" />
        <cms:CMSButton ID="btnCancel" runat="server" CssClass="SubmitButton" OnClientClick="window.close(); return false;"
            EnableViewState="false" />
    </div>
</asp:Content>
