<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_AdminControls_Controls_Class_ClassSites" CodeFile="ClassSites.ascx.cs" %>
<%@ Register src="~/CMSAdminControls/UI/UniSelector/UniSelector.ascx" tagname="UniSelector" tagprefix="cms" %>



<asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
    Visible="false" />
<asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
    Visible="false" />

<asp:Label ID="lblAvialable" runat="server" CssClass="BoldInfoLabel" EnableViewState="false" />
<cms:UniSelector ID="usSites" runat="server" IsLiveSite="false" ObjectType="cms.site"
    SelectionMode="Multiple" ResourcePrefix="sitesselect" />
