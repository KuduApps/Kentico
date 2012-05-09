<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_RelationshipNames_RelationshipName_Sites"
    Theme="default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Relationship name - Sites" CodeFile="RelationshipName_Sites.aspx.cs" %>
<%@ Register src="~/CMSAdminControls/UI/UniSelector/UniSelector.ascx" tagname="UniSelector" tagprefix="cms" %>


<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
        Visible="false" />
    <cms:LocalizedLabel ID="lblAvialable" runat="server" CssClass="BoldInfoLabel" ResourceString="relationshipnames.available" />
    <cms:UniSelector ID="usRelNames" runat="server" IsLiveSite="false" ObjectType="cms.site"
        SelectionMode="Multiple" ResourcePrefix="siteselect" />
</asp:Content>
