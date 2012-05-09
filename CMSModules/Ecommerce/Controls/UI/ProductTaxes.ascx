<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Ecommerce_Controls_UI_ProductTaxes" CodeFile="ProductTaxes.ascx.cs" %>
<%@ Register src="~/CMSAdminControls/UI/UniSelector/UniSelector.ascx" tagname="UniSelector" tagprefix="cms" %>

<asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
    Visible="false" />
<strong>
    <asp:Label runat="server" ID="lblSiteTitle" CssClass="InfoLabel" EnableViewState="false" /></strong>
<cms:UniSelector ID="uniSelector" runat="server" IsLiveSite="false" ObjectType="ecommerce.taxclass"
    SelectionMode="Multiple" ResourcePrefix="taxselect" />
