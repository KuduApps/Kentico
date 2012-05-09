<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_PortalEngine_UI_PageTemplates_PageTemplate_Sites" Theme="Default"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Page Template Edit - Sites" CodeFile="PageTemplate_Sites.aspx.cs" %>
<%@ Register src="~/CMSAdminControls/UI/UniSelector/UniSelector.ascx" tagname="UniSelector" tagprefix="cms" %>


<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">

    <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false" />
    
    <asp:Label ID="lblAvialable" runat="server" CssClass="BoldInfoLabel" EnableViewState="false" />
    <cms:UniSelector ID="usSites" runat="server" IsLiveSite="false" ObjectType="cms.site"
        SelectionMode="Multiple" ResourcePrefix="sitesselect" />
</asp:Content>
