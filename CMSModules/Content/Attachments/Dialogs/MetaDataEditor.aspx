<%@ Page Title="Edit metadata" Language="C#" MasterPageFile="~/CMSMasterPages/UI/Dialogs/ModalDialogPage.master"
    AutoEventWireup="true" CodeFile="MetaDataEditor.aspx.cs" Inherits="CMSModules_Content_Attachments_Dialogs_MetaDataEditor"
    Theme="Default" %>

<%@ Register Src="~/CMSAdminControls/ImageEditor/MetaDataEditor.ascx" TagName="MetaDataEditor"
    TagPrefix="cms" %>
<asp:Content ID="cntContent" ContentPlaceHolderID="plcContent" runat="server">
    <div class="PageContent">
        <cms:MetaDataEditor ID="metaDataEditor" runat="server" />
    </div>
</asp:Content>
<asp:Content ID="cntFooter" ContentPlaceHolderID="plcFooter" runat="server">
    <div class="FloatRight">
        <cms:LocalizedButton ID="btnSave" runat="server" CssClass="LongSubmitButton" ResourceString="general.saveandclose"
            EnableViewState="false" />
        <cms:LocalizedButton ID="btnClose" runat="server" CssClass="SubmitButton" ResourceString="general.close"
            EnableViewState="false" OnClientClick="window.close();" />
    </div>
    <asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />
</asp:Content>
