<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Ecommerce_Pages_Tools_Configuration_OrderStatus_OrderStatus_List"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="E-commerce Configuration - Order status"
    CodeFile="OrderStatus_List.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:Label ID="lblError" runat="server" Visible="false" EnableViewState="false" CssClass="ErrorLabel" />
    <asp:Label ID="lblGlobalInfo" runat="server" Visible="false" EnableViewState="false"
        Font-Bold="true" CssClass="InfoLabel" />
    <cms:unigrid runat="server" id="gridElem" gridname="OrderStatus_List.xml" orderby="StatusOrder"
        islivesite="false" columns="StatusID,StatusDisplayName,StatusEnabled,StatusColor,StatusSendNotification,StatusOrderIsPaid" />
</asp:Content>
