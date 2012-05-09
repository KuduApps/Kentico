<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Ecommerce_Pages_Tools_Orders_Order_Edit_General"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Order edit - General"
    CodeFile="Order_Edit_General.aspx.cs" %>

<%@ Register Src="~/CMSModules/Ecommerce/FormControls/OrderStatusSelector.ascx" TagName="OrderStatusSelector"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Ecommerce/FormControls/AddressSelector.ascx" TagName="AddressSelector"
    TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">

    <script type="text/javascript">
        //<![CDATA[
        function EditCustomer(customerid) {
            if (customerid != 0) {
                modalDialog('Order_Edit_CustomerFrameset.aspx?customerid=' + customerid, 'editcustomer', 742, 600);
            }
        }

        function AddAddress(customerId) {
            if (customerId != 0) {
                modalDialog('Order_Edit_Address.aspx?typeId=2&customerId=' + customerId, 'addAddresss', 600, 450);
            }
        }

        function EditAddress(customerId, addressId) {
            if ((customerId != 0) && (addressId != 0)) {
                modalDialog('Order_Edit_Address.aspx?typeId=2&customerId=' + customerId + "&addressId=" + addressId, 'editAddresss', 600, 450);
            }
        }
        //]]>
    </script>

    <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
        Visible="false" />
    <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
    <table style="vertical-align: top">
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblOrderId" EnableViewState="false" />
            </td>
            <td>
                <asp:Label runat="server" ID="lblOrderIdValue" />
            </td>
            <td>
                &nbsp;
            </td>
            <td style="width: 100%;">
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblOrderDate" EnableViewState="false" />
            </td>
            <td>
                <cms:DateTimePicker ID="orderDate" runat="server" SupportFolder="~/CMSAdminControls/Calendar" />
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblInvoiceNumber" EnableViewState="false" />
            </td>
            <td>
                <asp:Label runat="server" ID="lblInvoiceNumberValue" />
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblStatus" EnableViewState="false" />
            </td>
            <td>
                <cms:OrderStatusSelector runat="server" ID="statusElem" UseStatusNameForSelection="false"
                    AddAllItemsRecord="false" IsLiveSite="false" />
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblCustomer" EnableViewState="false" />
            </td>
            <td>
                <asp:Label runat="server" ID="lblCustomerName" />
            </td>
            <td style="padding-left: 3px;">
                <cms:CMSButton ID="btnEditCustomer" runat="server" CssClass="ContentButton" EnableViewState="false"
                    Style="margin-left: 0px;" />
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblCompanyAddress" EnableViewState="false" />
            </td>
            <td>
                <cms:CMSUpdatePanel runat="server" ID="pnlUpdate">
                    <ContentTemplate>
                        <cms:AddressSelector runat="server" ID="addressElem" UseStatusNameForSelection="false"
                            AddAllItemsRecord="false" RenderInline="true" IsLiveSite="false" />
                    </ContentTemplate>
                </cms:CMSUpdatePanel>
            </td>
            <td style="white-space: nowrap;">
                <cms:CMSUpdatePanel runat="server" ID="CMSUpdatePanel1" RenderMode="Inline">
                    <ContentTemplate>
                        <cms:CMSButton runat="server" ID="btnEditAddress" EnableViewState="false" CssClass="ContentButton" /><cms:CMSButton
                            runat="server" ID="btnNewAddress" EnableViewState="false" CssClass="ContentButton" />
                        <asp:HiddenField ID="hdnAddress" runat="server" />
                    </ContentTemplate>
                </cms:CMSUpdatePanel>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblNotes" EnableViewState="false" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtNotes" runat="server" TextMode="MultiLine" CssClass="TextAreaField"
                    EnableViewState="false" />
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <cms:CMSButton runat="server" ID="btnOk" OnClick="btnOK_Click" EnableViewState="false"
                    CssClass="SubmitButton" />
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    <asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />
    <cms:CMSButton ID="btnChange" runat="server" CssClass="HiddenButton" EnableViewState="false" />
</asp:Content>
