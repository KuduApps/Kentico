<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Culture_Edit_Sites.aspx.cs"
    Inherits="CMSSiteManager_Development_Cultures_Culture_Edit_Sites" Theme="Default"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Culture Edit - Sites" %>
<%@ Register src="~/CMSAdminControls/UI/UniSelector/UniSelector.ascx" tagname="UniSelector" tagprefix="cms" %>

<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <cms:LocalizedLabel ID="lblAvailable" runat="server" CssClass="BoldInfoLabel" 
        ResourceString="Culture_Edit_Sites.SiteTitle" EnableViewState="false" />
    <cms:UniSelector ID="usSites" runat="server" IsLiveSite="false" ObjectType="cms.site"
        SelectionMode="Multiple" ResourcePrefix="sitesselect" />
</asp:Content>