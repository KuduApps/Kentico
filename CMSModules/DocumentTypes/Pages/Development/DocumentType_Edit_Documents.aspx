<%@ Page Language="C#" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" AutoEventWireup="true"
    Inherits="CMSModules_DocumentTypes_Pages_Development_DocumentType_Edit_Documents"
    Title="Document Type Edit - Documents" Theme="Default" CodeFile="DocumentType_Edit_Documents.aspx.cs" %>

<%@ Register Src="~/CMSModules/AdminControls/Controls/Documents/Documents.ascx" TagName="Documents"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSFormControls/Filters/DocumentFilter.ascx" TagName="DocumentFilter"
    TagPrefix="cms" %>
<asp:Content ID="Content" ContentPlaceHolderID="plcContent" runat="server">
    <cms:CMSUpdatePanel ID="pnlUpdate" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <asp:PlaceHolder ID="plcFilter" runat="server">
                <cms:DocumentFilter ID="filterDocuments" runat="server" LoadSites="true" />
                <br />
                <br />
            </asp:PlaceHolder>
            <cms:LocalizedLabel ID="lblTitle" runat="server" ResourceString="document_type_edit.documents.used"
                DisplayColon="true" EnableViewState="false" />
            <br />
            <br />
            <cms:Documents ID="docElem" runat="server" ListingType="DocTypeDocuments" IsLiveSite="false" />
        </ContentTemplate>
    </cms:CMSUpdatePanel>
</asp:Content>
