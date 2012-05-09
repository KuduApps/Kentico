<%@ Page Title="" Language="C#" MasterPageFile="~/CMSMasterPages/UI/Dialogs/ModalDialogPage.master"
    AutoEventWireup="True" CodeFile="NewFolder.aspx.cs" Inherits="CMSModules_Content_Controls_Dialogs_FileSystemSelector_NewFolder"
    Theme="Default" %>

<asp:Content ID="cntContent" ContentPlaceHolderID="plcContent" runat="server">
    <asp:Literal runat="server" ID="ltlScript" EnableViewState="false" />
    <asp:Panel runat="server" ID="pnlEdit" CssClass="PageContent" DefaultButton="btnHidden">
        <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" Visible="false" EnableViewState="false" />
        <cms:LocalizedLabel runat="server" ID="lblName" EnableViewState="false" />
        <cms:CMSTextBox runat="server" ID="txtName" CssClass="TextBoxField" MaxLength="100" />
        <asp:Button runat="server" ID="btnHidden" CssClass="HiddenButton" OnClick="btnOK_Click" />
    </asp:Panel>
</asp:Content>
<asp:Content ID="cntFooter" ContentPlaceHolderID="plcFooter" runat="server">
    <div class="FloatRight">
        <cms:LocalizedButton runat="server" ID="btnOK" ResourceString="General.OK" CssClass="SubmitButton"
            OnClick="btnOK_Click" />
        <cms:LocalizedButton runat="server" ID="btnCancel" OnClientClick="window.close(); return false"
            ResourceString="General.Cancel" CssClass="SubmitButton" />
    </div>
</asp:Content>
