<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_CssStylesheets_Pages_CssStylesheet_List" Theme="Default"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Css stylesheets - List" CodeFile="CssStylesheet_List.aspx.cs" %>
<%@ Register src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" tagname="UniGrid" tagprefix="cms" %>

<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <cms:UniGrid ID="UniGridCssStyleSheets" runat="server" GridName="CssStylesheet_List.xml"
        OrderBy="StylesheetDisplayName" IsLiveSite="false" ObjectType="cms.cssstylesheet" />
</asp:Content>
