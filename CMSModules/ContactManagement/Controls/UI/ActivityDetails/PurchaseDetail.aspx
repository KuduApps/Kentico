<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CMSMasterPages/UI/Dialogs/ModalDialogPage.master"
    Inherits="CMSModules_ContactManagement_Controls_UI_ActivityDetails_PurchaseDetail"
    Theme="Default" CodeFile="PurchaseDetail.aspx.cs" %>

<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:Panel runat="server" ID="p">
        <asp:Literal runat="server" ID="ltl" />
    </asp:Panel>
</asp:Content>
<asp:Content ID="cntFooter" ContentPlaceHolderID="plcFooter" runat="server">
    <div class="FloatRight">
        <cms:LocalizedButton ID="btnClose" runat="server" CssClass="SubmitButton" ResourceString="general.close"
            OnClientClick="window.close();return false;" EnableViewState="false" />
    </div>
</asp:Content>
