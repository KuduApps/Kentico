<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Ecommerce_Pages_Tools_Configuration_ExchangeRates_ExchangeTable_List"
    Theme="default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Exchangle table - List"
    CodeFile="ExchangeTable_List.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:Label ID="lblGlobalInfo" runat="server" Visible="false" EnableViewState="false"
        Font-Bold="true" CssClass="InfoLabel" />
    <cms:UniGrid runat="server" ID="gridElem" GridName="ExchangeTable_List.xml" OrderBy="ExchangeTableDisplayName"
        IsLiveSite="false" Columns="ExchangeTableID,ExchangeTableDisplayName,ExchangeTableValidFrom,ExchangeTableValidTo" />
</asp:Content>
