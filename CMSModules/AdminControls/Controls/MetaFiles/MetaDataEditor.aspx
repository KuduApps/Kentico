<%@ Page Title="Edit metadata" Language="C#" MasterPageFile="~/CMSMasterPages/UI/Dialogs/ModalDialogPage.master"
    AutoEventWireup="true" CodeFile="MetaDataEditor.aspx.cs" Inherits="CMSModules_AdminControls_Controls_MetaFiles_MetaDataEditor"
    Theme="Default" %>

<%@ Register Src="~/CMSAdminControls/ImageEditor/MetaDataEditor.ascx" TagName="MetaDataEditor"
    TagPrefix="cms" %>
<asp:Content ID="cntContent" ContentPlaceHolderID="plcContent" runat="server">
    <div class="PageContent">
        <cms:metadataeditor id="metaDataEditor" runat="server" />
    </div>
</asp:Content>
<asp:Content ID="cntFooter" ContentPlaceHolderID="plcFooter" runat="server">
    <div class="FloatRight">
        <cms:localizedbutton id="btnSave" runat="server" cssclass="LongSubmitButton" resourcestring="general.saveandclose"
            enableviewstate="false" />
        <cms:localizedbutton id="btnClose" runat="server" cssclass="SubmitButton" resourcestring="general.close"
            enableviewstate="false" onclientclick="window.close();" />
    </div>
    <asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />
</asp:Content>
