<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Bundle.ascx.cs" Inherits="CMSModules_Ecommerce_Controls_UI_ProductTypes_Bundle" %>
<%@ Register TagPrefix="cms" TagName="UniSelector" Src="~/CMSAdminControls/UI/UniSelector/UniSelector.ascx" %>
<asp:Panel ID="pnlBundle" runat="server">
    <table>
        <%-- Bundle inventory type --%>
        <tr>
            <td class="FieldLabel" style="width: 150px;">
                <cms:LocalizedLabel runat="server" ResourceString="com.bundle.removefrominventory"
                    DisplayColon="true" />
            </td>
            <td>
                <cms:LocalizedRadioButton runat="server" ID="radRemoveBundle" GroupName="RemoveFromInventory"
                    Checked="true" ResourceString="com.bundle.removebundle" OnCheckedChanged="RemoveFromInventoryRadioGroup_CheckedChanged"
                    AutoPostBack="true" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <cms:LocalizedRadioButton runat="server" ID="radRemoveProducts" GroupName="RemoveFromInventory"
                    ResourceString="com.bundle.removeproducts" AutoPostBack="true" OnCheckedChanged="RemoveFromInventoryRadioGroup_CheckedChanged" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <cms:LocalizedRadioButton runat="server" ID="radRemoveBundleAndProducts" GroupName="RemoveFromInventory"
                    ResourceString="com.bundle.removebundleandproducts" AutoPostBack="true" OnCheckedChanged="RemoveFromInventoryRadioGroup_CheckedChanged" />
            </td>
        </tr>
        <%-- Products --%>
        <tr>
            <td class="FieldLabel" style="vertical-align: top">
                <cms:LocalizedLabel runat="server" ResourceString="com.bundle.products" DisplayColon="true" />
            </td>
            <td>
                <cms:UniSelector runat="server" ID="productsUniSelector" IsLiveSite="false" ObjectType="ecommerce.sku"
                    SelectionMode="Multiple" ResourcePrefix="productselect" DisplayNameFormat="{%SKUName%}" />
            </td>
        </tr>
    </table>
</asp:Panel>
