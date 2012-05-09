<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSAdminControls_UI_Dialogs_ChangeGroup"
    Title="Untitled Page" ValidateRequest="false" Theme="default" MasterPageFile="~/CMSMasterPages/UI/Dialogs/ModalDialogPage.master" CodeFile="ChangeGroup.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/Selectors/SelectDocumentGroup.ascx" TagName="SelectDocumentGroup"
    TagPrefix="cms" %>
<asp:Content ID="cntContent" ContentPlaceHolderID="plcContent" runat="Server">
    <div class="PageContent">
        <cms:SelectDocumentGroup ID="selectDocumentGroupElem" runat="server" DisaplayButtons="false" />
    </div>
</asp:Content>
<asp:Content ID="cntFooter" runat="server" ContentPlaceHolderID="plcFooter">
    <div class="FloatRight">
        <cms:LocalizedButton ID="btnOk" runat="server" CssClass="SubmitButton" OnClick="btnOk_Click"
            ResourceString="general.ok" /><cms:LocalizedButton ID="btnCancel" runat="server"
                CssClass="SubmitButton" OnClientClick="window.close(); return false;" ResourceString="general.cancel" />
    </div>
</asp:Content>
