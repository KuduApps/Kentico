<%@ Page Language="C#" AutoEventWireup="true" CodeFile="~/CMSModules/Reporting/Dialogs/ReportParametersSelector.aspx.cs"
    MasterPageFile="~/CMSMasterPages/UI/Dialogs/ModalDialogPage.master" Inherits="CMSModules_Reporting_Dialogs_ReportParametersSelector"
    Theme="Default" %>

<asp:Content ID="cntBody" ContentPlaceHolderID="plcContent" runat="Server">
    <asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />
    <asp:HiddenField ID="hdnGuid" runat="server" />
    <asp:Panel runat="server" ID="pnlInfo" Visible="false" CssClass="SelectorNoResults">
        <br />
        <asp:Label ID="lblInfo" runat="server" CssClass="InfoLabel" />
    </asp:Panel>
    <br />
    <cms:BasicForm runat="server" ID="bfParameters" />
</asp:Content>
<asp:Content ID="cntFooter" ContentPlaceHolderID="plcFooter" runat="Server">
    <div class="FloatRight">
        <cms:LocalizedButton ID="btnOK" runat="server" CssClass="SubmitButton" EnableViewState="False"
            OnClick="btnOK_Click" />
        <cms:LocalizedButton ID="btnCancel" runat="server" CssClass="SubmitButton" EnableViewState="False"
            OnClientClick="window.close()" />
    </div>
</asp:Content>
