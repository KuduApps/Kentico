<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_Ecommerce_Controls_MyDetails_MyOrders" CodeFile="MyOrders.ascx.cs" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<div class="MyOrders">
    <cms:UniGrid runat="server" ID="gridOrders" GridName="~/CMSModules/Ecommerce/Controls/MyDetails/MyOrders.xml"
        OrderBy="OrderDate DESC" Columns="OrderID,OrderDate,CurrencyFormatString,OrderTotalPrice,StatusDisplayName,OrderTrackingNumber" />
</div>
