<%@ Page Title="Edit metadata" Language="C#" MasterPageFile="~/CMSMasterPages/LiveSite/Dialogs/ModalDialogPage.master"
    AutoEventWireup="true" CodeFile="MetaDataEditor.aspx.cs" Inherits="CMSModules_MediaLibrary_CMSPages_MetaDataEditor"
    Theme="Default" %>

<%@ Register Src="~/CMSModules/MediaLibrary/Controls/MediaLibrary/MediaFileMetaDataEditor.ascx"
    TagName="MediaFileMetaDataEditor" TagPrefix="cms" %>
<asp:Content ID="cntContent" ContentPlaceHolderID="plcContent" runat="server">
    <div class="PageContent">
        <cms:mediafilemetadataeditor id="metaDataEditor" runat="server" />
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
