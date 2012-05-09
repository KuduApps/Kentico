<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_DocumentTypes_Pages_Development_DocumentType_List"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Document Types - Document Type List"
    CodeFile="DocumentType_List.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:Label runat="server" ID="lblError" ForeColor="red" EnableViewState="false" Visible="false" />
    <cms:UniGrid runat="server" ID="uniGrid" ShortID="g" GridName="DocumentType_List.xml"
        OrderBy="ClassDisplayName" IsLiveSite="false" />
</asp:Content>
