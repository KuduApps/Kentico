<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewCode.aspx.cs" Inherits="CMSModules_Content_CMSDesk_Validation_ViewCode"
    MasterPageFile="~/CMSMasterPages/UI/Dialogs/ModalDialogPage.master" Theme="Default" %>

<asp:Content ContentPlaceHolderID="plcContent" runat="server" ID="cntContent">
    <cms:ExtendedTextArea runat="server" ID="txtCodeText" EnableViewState="false" EditorMode="Advanced" 
        Language="HTML" Height="521" ShowLineNumbers="true" Width="100%" ReadOnly="true" Wrap="true" />
    <asp:HiddenField ID="hdnHTML" runat="server" EnableViewState="false" />
</asp:Content>
