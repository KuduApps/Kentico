<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_InlineControls_Pages_Development_Sites" Theme="Default"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Document Type Edit - Sites" CodeFile="Sites.aspx.cs" %>
<%@ Register src="~/CMSAdminControls/UI/UniSelector/UniSelector.ascx" tagname="UniSelector" tagprefix="cms" %>

<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <cms:LocalizedLabel ID="lblAvailable" runat="server" CssClass="BoldInfoLabel" EnableViewState="false" ResourceString="InlineControl_Sites.Info" />
    <cms:UniSelector ID="usSites" runat="server" IsLiveSite="false" ObjectType="cms.site"
        SelectionMode="Multiple" ResourcePrefix="sitesselect" />
</asp:Content>
