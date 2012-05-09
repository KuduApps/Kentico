<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_CssStylesheets_Pages_CssStylesheet_Sites" Theme="Default"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Css stylesheets - Sites" CodeFile="CssStylesheet_Sites.aspx.cs" %>
<%@ Register src="~/CMSAdminControls/UI/UniSelector/UniSelector.ascx" tagname="UniSelector" tagprefix="cms" %>


<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
        Visible="false" />
    <asp:Label ID="lblAvialable" runat="server" CssClass="BoldInfoLabel" />
    <cms:UniSelector ID="usSites" runat="server" IsLiveSite="false" ObjectType="cms.site" SelectionMode="Multiple" ResourcePrefix="sitesselect" />
</asp:Content>
