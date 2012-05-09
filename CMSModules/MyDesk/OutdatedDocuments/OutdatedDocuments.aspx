<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_MyDesk_OutdatedDocuments_OutdatedDocuments"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="MyDesk - Outdated documents"
    CodeFile="OutdatedDocuments.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/AdminControls/Controls/Documents/Documents.ascx" TagName="OutdatedDocuments"
    TagPrefix="cms" %>
<asp:Content ID="Content1" ContentPlaceHolderID="plcContent" runat="server">
    <cms:OutdatedDocuments runat="server" ID="ucOutdatedDocuments" ListingType="OutdatedDocuments" IsLiveSite="false" />
</asp:Content>
