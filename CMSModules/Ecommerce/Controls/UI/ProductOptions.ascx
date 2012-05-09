<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_Ecommerce_Controls_UI_ProductOptions"
    CodeFile="ProductOptions.ascx.cs" %>
<%@ Register Src="~/CMSAdminControls/UI/UniSelector/UniSelector.ascx" TagName="UniSelector"
    TagPrefix="cms" %>
<asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
    Visible="false" />
<strong>
    <asp:Label runat="server" ID="lblAvailable" CssClass="InfoLabel" EnableViewState="false" /></strong>
<cms:UniSelector ID="uniSelector" runat="server" IsLiveSite="false" ObjectType="ecommerce.optioncategory"
    SelectionMode="Multiple" ResourcePrefix="optioncategoryselect" />
