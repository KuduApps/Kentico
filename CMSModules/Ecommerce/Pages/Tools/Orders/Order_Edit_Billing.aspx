<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Ecommerce_Pages_Tools_Orders_Order_Edit_Billing"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Order edit - Billing"
    CodeFile="Order_Edit_Billing.aspx.cs" %>

<%@ Register Src="~/CMSModules/ECommerce/FormControls/PaymentSelector.ascx" TagName="PaymentSelector"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/ECommerce/FormControls/CurrencySelector.ascx" TagName="CurrencySelector"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Ecommerce/FormControls/AddressSelector.ascx" TagName="AddressSelector"
    TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">

    <script type="text/javascript">
        //<![CDATA[
        function AddAddress(customerId) {
            if (customerId != 0) {
                modalDialog('Order_Edit_Address.aspx?typeId=1&customerId=' + customerId, 'addAddresss', 600, 450);
            }
        }

        function EditAddress(customerId, addressId) {
            if ((customerId != 0) && (addressId != 0)) {
                modalDialog('Order_Edit_Address.aspx?typeId=1&customerId=' + customerId + "&addressId=" + addressId, 'editAddresss', 600, 450);
            }
        }

        function CheckOrderIsPaid(id, message) {
            var answer = true;
            var checkbox = document.getElementById(id);
            if ((checkbox != null) && (!checkbox.checked) && (checkbox.checked != originalOrderIsPaid)) {
                var answer = confirm(message);
            }
            return answer;
        }
        //]]>
    </script>

    <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
        Visible="false" />
    <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
    <table>
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblPayment" EnableViewState="false" />
            </td>
            <td>
                <cms:PaymentSelector ID="drpPayment" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblCurrency" EnableViewState="false" />
            </td>
            <td>
                <cms:CurrencySelector ID="drpCurrency" runat="server" DisplayOnlyWithExchangeRate="true" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblBillingAddress" EnableViewState="false" />
            </td>
            <td>
                <cms:CMSUpdatePanel runat="server" ID="pnlUpdate" UpdateMode="Always">
                    <ContentTemplate>
                        <cms:AddressSelector runat="server" ID="addressElem" UseStatusNameForSelection="false"
                            AddAllItemsRecord="false" RenderInline="true" />
                        <cms:CMSButton runat="server" ID="btnEdit" EnableViewState="false" CssClass="ContentButton" /><cms:CMSButton
                            runat="server" ID="btnNew" EnableViewState="false" CssClass="ContentButton" />
                        <asp:HiddenField ID="hdnAddress" runat="server" />
                    </ContentTemplate>
                </cms:CMSUpdatePanel>
            </td>
        </tr>
        <tr>
            <td class="FieldLabel" style="vertical-align: top;">
                <asp:Label runat="server" ID="lblPaymentResult" EnableViewState="false" />
            </td>
            <td>
                <asp:Label ID="lblPaymentResultValue" runat="server" EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel runat="server" ResourceString="Com.OrderBilling.OrderIsPaid"
                    DisplayColon="true" />
            </td>
            <td>
                <asp:CheckBox ID="chkOrderIsPaid" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <cms:CMSButton runat="server" ID="btnOk" EnableViewState="false" CssClass="SubmitButton"
                    OnClick="btnOk_Click" />
            </td>
        </tr>
    </table>
    <asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />
    <cms:CMSButton ID="btnChange" runat="server" CssClass="HiddenButton" EnableViewState="false" />
</asp:Content>
