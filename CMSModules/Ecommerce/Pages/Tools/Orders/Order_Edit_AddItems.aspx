<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Ecommerce_Pages_Tools_Orders_Order_Edit_AddItems"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/Dialogs/ModalSimplePage.master"
    Title="Ecommerce - Add order item" CodeFile="Order_Edit_AddItems.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/PageElements/PageTitle.ascx" TagName="PageTitle"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Ecommerce/Controls/ProductOptions/ShoppingCartItemSelector.ascx"
    TagName="ShoppingCartItemSelector" TagPrefix="cms" %>
<%@ Register Src="~/CMSFormControls/Sites/SiteSelector.ascx" TagName="SiteSelector"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Ecommerce/FormControls/DepartmentSelector.ascx" TagName="DepartmentSelector"
    TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">

    <script type="text/javascript">
        //<![CDATA[
        function AddProducts(productIDs, quantities, options, price, isPrivate) {
            window.close();
            wopener.AddProduct(productIDs, quantities, options, price, isPrivate);
            return false;
        }
        function RefreshCart() {
            window.close();
            wopener.RefreshCart();
            return false;
        }
        //]]>
    </script>

    <ajaxToolkit:ToolkitScriptManager ID="Scriptmanager1" runat="server" />
    <asp:Panel ID="PanelTitle" runat="server" CssClass="PageHeader" EnableViewState="false">
        <cms:PageTitle ID="PageTitleAddItems" runat="server" EnableViewState="false" />
    </asp:Panel>
    <cms:CMSUpdatePanel ID="pnlUpdate" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <%-- Products --%>
            <asp:PlaceHolder ID="plcProducts" runat="server">
                <asp:Panel ID="PanelNewItem" runat="server" CssClass="CartAddItem">
                    <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
                        Visible="false" />
                    <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
                        Visible="false" />
                    <table>
                        <tr>
                            <td class="FieldLabel">
                                <asp:Label ID="lblProductName" runat="server" EnableViewState="false" />
                            </td>
                            <td>
                                <cms:CMSTextBox ID="txtProductName" runat="server" CssClass="TextBoxField" MaxLength="450"
                                    EnableViewState="false" />
                            </td>
                        </tr>
                        <tr>
                            <td class="FieldLabel">
                                <asp:Label ID="lblProductCode" runat="server" EnableViewState="false" />
                            </td>
                            <td>
                                <cms:CMSTextBox ID="txtProductCode" runat="server" CssClass="TextBoxField" MaxLength="200"
                                    EnableViewState="false" />
                            </td>
                        </tr>
                        <tr>
                            <td class="FieldLabel" style="padding-top: 5px; vertical-align: top;">
                                <asp:Label ID="lblDepartment" runat="server" EnableViewState="false" />
                            </td>
                            <td>
                                <cms:DepartmentSelector runat="server" ID="departmentElem" AddAllItemsRecord="true"
                                    AddNoneRecord="false" UseDepartmentNameForSelection="false" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <cms:CMSButton ID="btnShow" runat="server" OnClick="BtnShow_Click" CssClass="ContentButton"
                                    EnableViewState="false" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <div style="overflow: auto; height: 333px; width: 100%; text-align: center;">
                    <asp:GridView ID="GridViewProducts" runat="server" AutoGenerateColumns="false" OnDataBound="GridViewProducts_DataBound"
                        CellPadding="3" Width="100%" CssClass="UniGridGrid" EnableViewState="false">
                        <HeaderStyle HorizontalAlign="Left" CssClass="UniGridHead" Wrap="false" />
                        <RowStyle HorizontalAlign="Left" CssClass="EvenRow" />
                        <AlternatingRowStyle CssClass="OddRow" />
                        <Columns>
                            <asp:TemplateField>
                                <ItemStyle Wrap="false" />
                                <HeaderStyle Wrap="false" Width="100%" />
                                <ItemTemplate>
                                    <span style="padding-left: 5px;">&nbsp;</span>
                                    <asp:LinkButton ID="btnAddOneUnit" runat="server" CommandArgument='<%# Eval("SKUID") %>'
                                        OnClick="btnAddOneUnit_Click" Text='<%# HTMLHelper.HTMLEncode(ResHelper.LocalizeString(Convert.ToString(Eval("SKUName")))) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="SKUNumber">
                                <ItemStyle Wrap="false" />
                                <HeaderStyle Wrap="false" />
                            </asp:BoundField>
                            <asp:TemplateField>
                                <ItemStyle Wrap="false" HorizontalAlign="Right" />
                                <HeaderStyle Wrap="false" HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <%# CMS.Ecommerce.SKUInfoProvider.GetSKUFormattedPrice(new CMS.Ecommerce.SKUInfo(((System.Data.DataRowView)Container.DataItem).Row), ShoppingCartObj, true, false)%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <cms:CMSTextBox ID="txtUnits" runat="server" Width="50" MaxLength="9" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="SKUID">
                                <ItemStyle />
                            </asp:BoundField>
                            <asp:TemplateField></asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <asp:Panel runat="server" CssClass="PageFooterLine" ID="pnlFooter">
                    <div class="FloatRight">
                        <cms:CMSButton ID="btnAdd" runat="server" OnClick="BtnAdd_Click" CssClass="SubmitButton"
                            EnableViewState="false" Enabled="false" /><cms:LocalizedButton ID="btnCancel" runat="server"
                                ResourceString="general.cancel" CssClass="SubmitButton" OnClientClick="window.close(); return false;"
                                EnableViewState="False" />
                    </div>
                </asp:Panel>
            </asp:PlaceHolder>
            <%-- Shopping cart item selector --%>
            <asp:PlaceHolder ID="plcSelector" runat="server">
                <div style="padding: 10px;">
                    <asp:Label runat="server" ID="lblSKUName" EnableViewState="false" CssClass="BoldInfoLabel" />
                    <asp:Label runat="server" ID="lblPrice" EnableViewState="false" />&nbsp;&nbsp;
                    <asp:Label runat="server" ID="lblPriceValue" EnableViewState="false" />
                    <br />
                    <br />
                    <cms:LocalizedLabel runat="server" ID="lblTitle" CssClass="InfoLabel" />
                    <cms:ShoppingCartItemSelector ID="cartItemSelector" runat="server" ShowProductOptions="true"
                        ShowDonationProperties="true" ShowUnitsTextBox="true" ShowTotalPrice="true" DialogMode="True"
                        IsLiveSite="false" />
                </div>
            </asp:PlaceHolder>
        </ContentTemplate>
    </cms:CMSUpdatePanel>
</asp:Content>
