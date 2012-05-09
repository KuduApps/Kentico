<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_Ecommerce_Controls_ShoppingCart_ShoppingCartPreview"
    CodeFile="ShoppingCartPreview.ascx.cs" %>
<asp:Label ID="lblTitle" runat="server" CssClass="BlockTitle" EnableViewState="false" />
<div class="BlockContent">
    <asp:Label ID="lblError" runat="server" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
    <table width="100%">
        <tr style="vertical-align: top;" runat="server" id="tblAddressPreview">
            <td>
                <asp:Panel ID="pnlBillingAddress" runat="server" EnableViewState="false">
                    <div class="AddressPreview">
                        <asp:Literal ID="lblBill" runat="server" EnableViewState="false" />
                        <asp:PlaceHolder ID="plcIDs" runat="server" Visible="false">
                            <table border="0">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblOrganizationID" runat="server" EnableViewState="false" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblOrganizationIDVal" runat="server" EnableViewState="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblTaxRegistrationID" runat="server" EnableViewState="false" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblTaxRegistrationIDVal" runat="server" EnableViewState="false" />
                                    </td>
                                </tr>
                            </table>
                        </asp:PlaceHolder>
                    </div>
                </asp:Panel>
            </td>
            <td runat="server" id="tdShippingAddress">
                <asp:Panel ID="pnlShippingAddress" runat="server" EnableViewState="false">
                    <div class="AddressPreview">
                        <asp:Literal ID="lblShip" runat="server" EnableViewState="false" />
                    </div>
                </asp:Panel>
            </td>
            <td runat="server" id="tdCompanyAddress">
                <asp:Panel ID="pnlCompanyAddress" runat="server" Height="50%" EnableViewState="false">
                    <div class="AddressPreview">
                        <asp:Literal ID="lblCompany" runat="server" EnableViewState="false" />
                    </div>
                </asp:Panel>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td colspan="2">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="width: 50%;">
                <table>
                    <tr>
                        <td>
                            <strong>
                                <asp:Label ID="lblPaymentMethod" runat="server" EnableViewState="false" /></strong>
                        </td>
                        <td>
                            <cms:LocalizedLabel ID="lblPaymentMethodValue" runat="server" EnableViewState="false" />
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                <asp:PlaceHolder ID="plcShippingOption" runat="server" EnableViewState="false">
                    <table>
                        <tr>
                            <td>
                                <strong>
                                    <asp:Label ID="lblShippingOption" runat="server" EnableViewState="false" /></strong>
                            </td>
                            <td>
                                <cms:LocalizedLabel ID="lblShippingOptionValue" runat="server" EnableViewState="false" />
                            </td>
                        </tr>
                    </table>
                </asp:PlaceHolder>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:GridView ID="gridData" runat="server" AutoGenerateColumns="false" CellPadding="3"
                    CssClass="UniGridGrid CartContentTable" Width="100%" GridLines="Horizontal" EnableViewState="false">
                    <HeaderStyle CssClass="UniGridHead TextLeft" />
                    <RowStyle CssClass="OddRow" />
                    <AlternatingRowStyle CssClass="EvenRow" />
                    <Columns>
                        <asp:BoundField DataField="CartItemId" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Label ID="lblGuid" runat="server" Text='<%# Eval("CartItemGuid")%>' EnableViewState="false" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="CartItemParentGuid" />
                        <asp:BoundField DataField="SKUID" />
                        <asp:TemplateField>
                            <HeaderStyle Width="100%" />
                            <ItemTemplate>
                                <%# GetSKUName(Eval("SKUName"), Eval("IsProductOption"), Eval("IsBundleItem"), Eval("CartItemText"))%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Units">
                            <HeaderStyle Wrap="false" CssClass="TextRight" />
                            <ItemStyle Wrap="false" CssClass="TextRight" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderStyle-Wrap="false">
                            <ItemStyle Wrap="false" CssClass="TextRight" />
                            <HeaderStyle Wrap="false" CssClass="TextRight" />
                            <ItemTemplate>
                                <asp:Label ID="lblSKUPrice" runat="server" Text='<%# GetFormattedValue(Eval("UnitPrice")) %>'
                                    EnableViewState="false" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-Wrap="false">
                            <ItemStyle Wrap="false" CssClass="TextRight" />
                            <HeaderStyle Wrap="false" CssClass="TextRight" />
                            <ItemTemplate>
                                <asp:Label ID="lblUnitDiscount" runat="server" Text='<%# GetFormattedValue(Eval("UnitTotalDiscount")) %>'
                                    EnableViewState="false" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-Wrap="false">
                            <ItemStyle Wrap="false" CssClass="TextRight" />
                            <HeaderStyle Wrap="false" CssClass="TextRight" />
                            <ItemTemplate>
                                <asp:Label ID="lblTax" runat="server" Text='<%# GetFormattedValue(Eval("TotalTax")) %>'
                                    EnableViewState="false" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-Wrap="false">
                            <ItemStyle Wrap="false" CssClass="TextRight" />
                            <HeaderStyle Wrap="false" CssClass="TextRight" />
                            <ItemTemplate>
                                <asp:Label ID="lblSubtotal" runat="server" Text='<%# GetFormattedValue(Eval("TotalPrice")) %>'
                                    EnableViewState="false" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td colspan="3" class="TextRight">
                <br />
                <table cellpadding="2" cellspacing="0" border="0" width="100%">
                    <asp:PlaceHolder ID="plcShipping" runat="server" EnableViewState="false">
                        <tr class="TotalShipping">
                            <td style="width: 100%">
                                &nbsp;
                            </td>
                            <td style="padding-right: 10px;">
                                <asp:Label ID="lblShipping" runat="server" EnableViewState="false" />
                            </td>
                            <td class="TextRight" style="white-space: nowrap;">
                                <asp:Label ID="lblShippingValue" runat="server" EnableViewState="false" />
                            </td>
                        </tr>
                    </asp:PlaceHolder>
                    <tr class="TotalPrice">
                        <td style="width: 100%">
                            &nbsp;
                        </td>
                        <td style="padding-right: 10px; white-space: nowrap;">
                            <strong>
                                <asp:Label ID="lblTotalPrice" runat="server" EnableViewState="false" /></strong>
                        </td>
                        <td class="TextRight" style="white-space: nowrap;">
                            <strong>
                                <asp:Label ID="lblTotalPriceValue" runat="server" EnableViewState="false" /></strong>
                        </td>
                    </tr>
                </table>
                <br />
                <asp:GridView ID="gridTaxSummary" runat="server" AutoGenerateColumns="false" CellPadding="3"
                    CssClass="UniGridGrid CartContentTable" GridLines="Horizontal" Width="100%" EnableViewState="false">
                    <HeaderStyle CssClass="UniGridHead" />
                    <RowStyle CssClass="OddRow" />
                    <AlternatingRowStyle CssClass="EvenRow" />
                    <Columns>
                        <asp:TemplateField>
                            <HeaderStyle CssClass="TextLeft" />
                            <ItemStyle CssClass="TextLeft" Width="90%" />
                            <ItemTemplate>
                                <cms:LocalizedLabel ID="txtTaxName" runat="server" Text='<%# HTMLHelper.HTMLEncode(Eval("TaxClassDisplayName").ToString()) %>'
                                    EnableViewState="false" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderStyle CssClass="TextRight" Width="10%" Wrap="false" />
                            <ItemStyle CssClass="TextRight" />
                            <ItemTemplate>
                                <asp:Label ID="lblTaxSummary" runat="server" Text='<%# GetFormattedValue(Eval("TaxSummary")) %>'
                                    EnableViewState="false" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <br />
                <asp:GridView ID="gridShippingTaxSummary" runat="server" AutoGenerateColumns="false"
                    CellPadding="3" CssClass="UniGridGrid CartContentTable" GridLines="Horizontal"
                    Width="100%" EnableViewState="false">
                    <HeaderStyle CssClass="UniGridHead" />
                    <RowStyle CssClass="OddRow" />
                    <AlternatingRowStyle CssClass="EvenRow" />
                    <Columns>
                        <asp:TemplateField>
                            <HeaderStyle CssClass="TextLeft" />
                            <ItemStyle CssClass="TextLeft" Width="90%" />
                            <ItemTemplate>
                                <cms:LocalizedLabel ID="txtTaxName" runat="server" Text='<%# HTMLHelper.HTMLEncode(Eval("TaxClassDisplayName").ToString()) %>'
                                    EnableViewState="false" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderStyle CssClass="TextRight" Width="10%" Wrap="false" />
                            <ItemStyle CssClass="TextRight" />
                            <ItemTemplate>
                                <asp:Label ID="lblTaxSummary" runat="server" Text='<%# GetFormattedValue(Eval("TaxSummary")) %>'
                                    EnableViewState="false" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <br />
                <strong>
                    <asp:Label ID="lblNote" runat="server" AssociatedControlID="txtNote" EnableViewState="false" /></strong><br />
                <cms:ExtendedTextBox ID="txtNote" runat="server" TextMode="MultiLine" CssClass="TextAreaField"
                    Width="100%" MaxLength="500" />
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:CheckBox ID="chkSendEmail" runat="server" Visible="false" />
            </td>
        </tr>
    </table>
</div>
