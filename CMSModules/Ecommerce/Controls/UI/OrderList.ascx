<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_Ecommerce_Controls_UI_OrderList"
    CodeFile="OrderList.ascx.cs" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<%@ Register Src="~/CMSFormControls/Sites/SiteSelector.ascx" TagName="SiteSelector"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Ecommerce/FormControls/OrderStatusSelector.ascx" TagName="OrderStatusSelector"
    TagPrefix="cms" %>
<table>
    <%-- Order ID --%>
    <tr>
        <td class="FieldLabel">
            <asp:Label ID="lblOrderID" runat="server" EnableViewState="false" />
        </td>
        <td>
            <cms:CMSTextBox ID="txtOrderId" runat="server" CssClass="TextBoxField" MaxLength="9"
                EnableViewState="false" />
            <asp:Label ID="lblErrorOrderId" runat="server" ForeColor="red" EnableViewState="false" />
        </td>
    </tr>
    <%-- Customer name --%>
    <asp:PlaceHolder ID="plcCustomerFilter" runat="server">
        <tr>
            <td class="FieldLabel">
                <asp:Label ID="lblCustomerLastName" runat="server" EnableViewState="false" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtCustomerLastName" runat="server" CssClass="TextBoxField" EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <asp:Label ID="lblCustomerFirstName" runat="server" EnableViewState="false" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtCustomerFirstName" runat="server" CssClass="TextBoxField" EnableViewState="false" />
            </td>
        </tr>
    </asp:PlaceHolder>
    <%-- Status --%>
    <tr>
        <td class="FieldLabel">
            <asp:Label ID="lblStatus" runat="server" EnableViewState="false" />
        </td>
        <td>
            <cms:OrderStatusSelector runat="server" AddAllItemsRecord="true" ID="statusElem"
                UseStatusNameForSelection="false" IsLiveSite="false" DisplayOnlyEnabled="false" />
        </td>
    </tr>
    <%-- Order is paid --%>
    <tr>
        <td class="FieldLabel">
            <cms:LocalizedLabel runat="server" ResourceString="com.orderlist.orderispaid" DisplayColon="true"
                EnableViewState="false" />
        </td>
        <td>
            <cms:LocalizedDropDownList runat="server" ID="drpOrderIsPaid" UseResourceStrings="true"
                CssClass="DropDownField" />
        </td>
    </tr>
    <%-- Site --%>
    <asp:PlaceHolder ID="plcSiteFilter" runat="server">
        <tr>
            <td class="FieldLabel">
                <asp:Label ID="lblSites" runat="server" EnableViewState="false" />
            </td>
            <td>
                <cms:SiteSelector ID="siteSelector" runat="server" IsLiveSite="false" />
            </td>
        </tr>
    </asp:PlaceHolder>
    <tr>
        <td>
        </td>
        <td>
            <cms:CMSButton ID="btnFilter" runat="server" CssClass="ContentButton" EnableViewState="false" />
        </td>
    </tr>
</table>
<br />
<cms:CMSUpdatePanel ID="pnlUpdate" runat="server" UpdateMode="Always">
    <ContentTemplate>
        <cms:UniGrid runat="server" ID="gridElem" GridName="Order_List.xml" OrderBy="OrderDate DESC"
            Columns="OrderID,OrderDate,CustomerFirstName,CustomerLastName,CustomerCompany,CustomerEmail,CurrencyFormatString,OrderTotalPrice,StatusDisplayName,StatusColor,OrderIsPaid,OrderSiteID" />
    </ContentTemplate>
</cms:CMSUpdatePanel>
