<%@ Page Title="" Language="C#" MasterPageFile="~/CMSMasterPages/UI/Dialogs/ModalDialogPage.master"
    AutoEventWireup="True" CodeFile="EditTextFile.aspx.cs" Inherits="CMSModules_Content_Controls_Dialogs_FileSystemSelector_EditTextFile"
    Theme="Default" %>

<asp:Content ID="cntContent" ContentPlaceHolderID="plcContent" runat="server">
    <asp:Literal runat="server" ID="ltlScript" EnableViewState="false" />
    <div class="PageContent">
        <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" Visible="false" EnableViewState="false" />
        <cms:LocalizedLabel runat="server" ID="lblName" EnableViewState="false" DisplayColon="true" />
        <cms:CMSTextBox runat="server" ID="txtName" CssClass="TextBoxField" MaxLength="100" />
        <asp:Label runat="server" ID="lblExt" EnableViewState="false" />
        <br />
        <br />
        <cms:ExtendedTextArea runat="server" ID="txtContent" Width="97%" Height="480" EditorMode="Advanced" />
    </div>
</asp:Content>
<asp:Content ID="cntFooter" ContentPlaceHolderID="plcFooter" runat="server">
    <div class="FloatRight">
        <cms:LocalizedButton runat="server" ID="btnOK" ResourceString="General.OK" CssClass="SubmitButton"
            OnClick="btnOK_Click" />
        <cms:LocalizedButton runat="server" ID="btnCancel" OnClientClick="window.close(); return false"
            ResourceString="General.Cancel" CssClass="SubmitButton" />
    </div>
</asp:Content>
