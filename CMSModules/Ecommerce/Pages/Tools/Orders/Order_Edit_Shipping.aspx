<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Ecommerce_Pages_Tools_Orders_Order_Edit_Shipping" Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Order edit - Shipping" CodeFile="Order_Edit_Shipping.aspx.cs" %>

<%@ Register Src="~/CMSModules/ECommerce/FormControls/ShippingSelector.ascx" TagName="ShippingSelector"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Ecommerce/FormControls/AddressSelector.ascx" TagName="AddressSelector"
    TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">

    <script type="text/javascript">
        //<![CDATA[        
        function AddAddress(customerId) {
            if (customerId != 0) {
                modalDialog('Order_Edit_Address.aspx?typeId=0&customerId=' + customerId, 'addAddresss', 600, 450);
            }
        }

        function EditAddress(customerId, addressId) {
            if ((customerId != 0) && (addressId != 0)) {
                modalDialog('Order_Edit_Address.aspx?typeId=0&customerId=' + customerId + "&addressId=" + addressId, 'editAddresss', 600, 450);
            }
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
                <asp:Label runat="server" ID="lblOption" EnableViewState="false" />
            </td>
            <td>
                <cms:ShippingSelector ID="drpShippingOption" runat="server" AddNoneRecord="true" IsLiveSite="false" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblAddress" EnableViewState="false" />
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
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblTrackingNumber" EnableViewState="false" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtTrackingNumber" runat="server" CssClass="TextBoxField" MaxLength="100"
                    EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td colspan="2">
                <cms:CMSButton runat="server" ID="btnOk" EnableViewState="false" CssClass="SubmitButton" />
            </td>
        </tr>
    </table>
    <cms:CMSButton ID="btnChange" runat="server" CssClass="HiddenButton" EnableViewState="false" />
</asp:Content>
