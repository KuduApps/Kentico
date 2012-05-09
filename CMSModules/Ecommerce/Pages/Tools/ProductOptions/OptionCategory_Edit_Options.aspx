<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Ecommerce_Pages_Tools_ProductOptions_OptionCategory_Edit_Options" Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Option category - list of options" CodeFile="OptionCategory_Edit_Options.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Ecommerce/Controls/ProductOptions/ProductOptionSelector.ascx"
    TagName="ProductOptionSelector" TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:Label ID="lblError" runat="server" Visible="false" EnableViewState="false" CssClass="ErrorLabel" />
    <cms:UniGrid runat="server" ID="grid" GridName="OptionCategory_Edit_Options.xml"
        OrderBy="SKUOrder" IsLiveSite="false" Columns="SKUID,SKUName,SKUNumber,SKUPrice,SKUAvailableItems,SKUEnabled,SKUSiteID" />
</asp:Content>
