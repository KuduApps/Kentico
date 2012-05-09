<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_Ecommerce_Controls_Filters_ProductFilter"
    CodeFile="ProductFilter.ascx.cs" %>
<%@ Register Src="~/CMSModules/ECommerce/FormControls/PublicStatusSelector.ascx"
    TagName="PublicStatusSelector" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/ECommerce/FormControls/ManufacturerSelector.ascx"
    TagName="ManufacturerSelector" TagPrefix="cms" %>
<asp:Panel ID="pnlContainer" runat="server">
    <table class="ProductFilter">
        <asp:PlaceHolder ID="plcFirstRow" runat="server">
            <tr>
                <td>
                    <cms:LocalizedLabel ID="lblSearch" runat="server" EnableViewState="false" />
                </td>
                <td>
                    <cms:CMSTextBox ID="txtSearch" runat="server" CssClass="ProductSearch" EnableViewState="false" />
                </td>
                <td>
                    <cms:LocalizedLabel ID="lblStatus" runat="server" EnableViewState="false" />
                </td>
                <td>
                    <cms:PublicStatusSelector ID="statusSelector" runat="server" UseStatusNameForSelection="false"
                        AddAllItemsRecord="true" />
                </td>
                <td>
                    <cms:LocalizedLabel ID="lblManufacturer" runat="server" EnableViewState="false" />
                </td>
                <td>
                    <cms:ManufacturerSelector ID="manufacturerSelector" runat="server" AddAllItemsRecord="true" />
                </td>
                <td>
                    <cms:LocalizedCheckBox ID="chkStock" runat="server" />
                </td>
                <asp:PlaceHolder ID="plcFirstButton" runat="server" Visible="false">
                    <td>
                        <cms:CMSButton ID="btnFirstFilter" runat="server" CssClass="ContentButton" EnableViewState="false"
                            OnClick="btnFilter_Click" UseSubmitBehavior="false" />
                    </td>
                </asp:PlaceHolder>
            </tr>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="plcSecondRow" runat="server">
            <tr>
                <td colspan="2">
                </td>
                <td>
                    <asp:Label ID="lblPaging" AssociatedControlID="drpPaging" EnableViewState="false"
                        runat="server" />
                </td>
                <td>
                    <asp:DropDownList ID="drpPaging" runat="server" CssClass="DropDownList" />
                </td>
                <td>
                    <asp:Label ID="lblSort" AssociatedControlID="drpSort" EnableViewState="false" runat="server" />
                </td>
                <td>
                    <asp:DropDownList ID="drpSort" runat="server" CssClass="DropDownList" />
                </td>
                <asp:PlaceHolder ID="plcSecButton" runat="server">
                    <td>
                        <cms:CMSButton ID="btnSecFilter" runat="server" CssClass="ContentButton" EnableViewState="false"
                            OnClick="btnFilter_Click" UseSubmitBehavior="false" />
                    </td>
                </asp:PlaceHolder>
            </tr>
        </asp:PlaceHolder>
    </table>
</asp:Panel>
