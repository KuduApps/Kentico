<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_Ecommerce_Controls_UI_ProductEdit"
    CodeFile="ProductEdit.ascx.cs" %>
<%@ Register TagPrefix="cms" TagName="File" Src="~/CMSModules/AdminControls/Controls/MetaFiles/File.ascx" %>
<%@ Register TagPrefix="cms" TagName="PriceSelector" Src="~/CMSModules/Ecommerce/Controls/UI/PriceSelector.ascx" %>
<%@ Register TagPrefix="cms" TagName="DepartmentSelector" Src="~/CMSModules/Ecommerce/FormControls/DepartmentSelector.ascx" %>
<%@ Register TagPrefix="cms" TagName="ManufacturerSelector" Src="~/CMSModules/Ecommerce/FormControls/ManufacturerSelector.ascx" %>
<%@ Register TagPrefix="cms" TagName="SupplierSelector" Src="~/CMSModules/Ecommerce/FormControls/SupplierSelector.ascx" %>
<%@ Register TagPrefix="cms" TagName="PublicStatusSelector" Src="~/CMSModules/Ecommerce/FormControls/PublicStatusSelector.ascx" %>
<%@ Register TagPrefix="cms" TagName="InternalStatusSelector" Src="~/CMSModules/Ecommerce/FormControls/InternalStatusSelector.ascx" %>
<%@ Register TagPrefix="cms" TagName="SelectProductType" Src="~/CMSModules/Ecommerce/FormControls/SelectProductType.ascx" %>
<%@ Register TagPrefix="cms" TagName="Bundle" Src="~/CMSModules/Ecommerce/Controls/UI/ProductTypes/Bundle.ascx" %>
<%@ Register TagPrefix="cms" TagName="Donation" Src="~/CMSModules/Ecommerce/Controls/UI/ProductTypes/Donation.ascx" %>
<%@ Register TagPrefix="cms" TagName="EProduct" Src="~/CMSModules/Ecommerce/Controls/UI/ProductTypes/EProduct.ascx" %>
<%@ Register TagPrefix="cms" TagName="Membership" Src="~/CMSModules/Ecommerce/Controls/UI/ProductTypes/Membership.ascx" %>
<%@ Register TagPrefix="cms" TagName="Conversion" Src="~/CMSModules/WebAnalytics/FormControls/SelectConversion.ascx" %>
<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextBox"
    TagPrefix="cms" %>
<%-- Messages --%>
<cms:CMSUpdatePanel ID="pnlUpdateInfo" runat="server">
    <ContentTemplate>
        <asp:Label ID="lblInfo" runat="server" CssClass="InfoLabel" EnableViewState="false"
            Visible="false" />
    </ContentTemplate>
</cms:CMSUpdatePanel>
<asp:Label runat="server" ID="lblNewProductError" CssClass="ErrorLabel" EnableViewState="false"
    Visible="false" />
<asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
    Visible="false" />
<cms:LocalizedLabel ID="lblUsedProductWarning" runat="server" EnableViewState="false"
    ResourceString="com.usedproductwarning" CssClass="WarningLabel" Visible="false" />
<div style="width: 700px;">
    <%-- General information --%>
    <asp:Panel ID="pnlGeneral" runat="server">
        <table>
            <%-- Name --%>
            <tr>
                <td class="FieldLabel" style="width: 150px; vertical-align: top; padding-top: 5px;">
                    <cms:LocalizedLabel runat="server" EnableViewState="false" ResourceString="com_sku_edit_general.skunamelabel"
                        DisplayColon="true" />
                </td>
                <td>
                    <cms:LocalizableTextBox ID="txtSKUName" runat="server" CssClass="TextBoxField" MaxLength="440"
                        EnableViewState="false" />
                    <div>
                        <cms:CMSRequiredFieldValidator ID="rfvSKUName" runat="server" ControlToValidate="txtSKUName:textbox"
                            Display="Dynamic" EnableViewState="false" />
                    </div>
                </td>
            </tr>
            <%-- Number --%>
            <tr>
                <td class="FieldLabel">
                    <cms:LocalizedLabel runat="server" EnableViewState="false" ResourceString="com_sku_edit_general.skunumberlabel"
                        DisplayColon="true" />
                </td>
                <td>
                    <cms:CMSTextBox ID="txtSKUNumber" runat="server" CssClass="TextBoxField" MaxLength="200"
                        EnableViewState="false" />
                </td>
            </tr>
            <%-- Price --%>
            <tr>
                <td class="FieldLabel">
                    <cms:LocalizedLabel ID="LocalizedLabel4" runat="server" EnableViewState="false" ResourceString="com_sku_edit_general.skupricelabel"
                        DisplayColon="true" />
                </td>
                <td>
                    <cms:PriceSelector ID="txtSKUPrice" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <%-- Department --%>
            <tr>
                <td class="FieldLabel">
                    <cms:LocalizedLabel ID="LocalizedLabel10" runat="server" EnableViewState="false"
                        ResourceString="com_sku_edit_general.skudepartmentidlabel" DisplayColon="true" />
                </td>
                <td>
                    <cms:DepartmentSelector runat="server" ID="departmentElem" AddAllItemsRecord="false"
                        AddAllMyRecord="false" AddNoneRecord="true" UseDepartmentNameForSelection="false"
                        IsLiveSite="false" />
                </td>
            </tr>
            <%-- Manufacturer --%>
            <tr>
                <td class="FieldLabel">
                    <cms:LocalizedLabel ID="LocalizedLabel11" runat="server" EnableViewState="false"
                        ResourceString="com_sku_edit_general.skumanufactureridlabel" DisplayColon="true" />
                </td>
                <td>
                    <cms:ManufacturerSelector runat="server" ID="manufacturerElem" AddAllItemsRecord="false"
                        AddNoneRecord="true" IsLiveSite="false" />
                </td>
            </tr>
            <%-- Supplier --%>
            <tr>
                <td class="FieldLabel">
                    <cms:LocalizedLabel ID="LocalizedLabel12" runat="server" EnableViewState="false"
                        ResourceString="com_sku_edit_general.skusupplieridlabel" DisplayColon="true" />
                </td>
                <td>
                    <cms:SupplierSelector runat="server" ID="supplierElem" AddNoneRecord="true" IsLiveSite="false" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <%-- Image - new product --%>
            <asp:PlaceHolder ID="plcNewProductImage" runat="server" Visible="false">
                <tr>
                    <td class="FieldLabel">
                        <cms:LocalizedLabel runat="server" ResourceString="com.newproduct.lblskuimage" DisplayColon="true"
                            EnableViewState="false" />
                    </td>
                    <td>
                        <cms:ImageSelector ID="imgSelect" runat="server" ImageHeight="50" ShowImagePreview="true"
                            ShowClearButton="true" UseImagePath="true" IsLiveSite="false" Enabled="false" />
                    </td>
                </tr>
            </asp:PlaceHolder>
            <%-- Image - existing product --%>
            <asp:PlaceHolder ID="plcExistingProductImage" runat="server" Visible="false">
                <tr>
                    <td class="FieldLabel">
                        <cms:LocalizedLabel runat="server" ResourceString="com.newproduct.skuimagepath" DisplayColon="true"
                            EnableViewState="false" />
                    </td>
                    <td>
                        <cms:File ID="ucMetaFile" runat="server" Enabled="false" />
                    </td>
                </tr>
            </asp:PlaceHolder>
            <%-- Description --%>
            <tr>
                <td class="FieldLabel" style="vertical-align: top; padding-top: 5px;">
                    <cms:LocalizedLabel runat="server" EnableViewState="false" ResourceString="general.description"
                        DisplayColon="true" />
                </td>
                <td>
                    <cms:CMSHtmlEditor ID="htmlTemplateBody" runat="server" Width="450px" Height="200px" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <%-- Type --%>
            <tr>
                <td class="FieldLabel">
                    <cms:LocalizedLabel ID="LocalizedLabel5" runat="server" EnableViewState="false" ResourceString="com.productedit.producttype"
                        DisplayColon="true" />
                </td>
                <td>
                    <cms:SelectProductType runat="server" ID="selectProductTypeElem" OnOnSelectionChanged="selectProductTypeElem_OnSelectionChanged"
                        AutoPostBack="true" AllowAll="false" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <br />
    <%-- Product type information --%>
    <cms:CMSUpdatePanel ID="pnlUpdateTypeSpecificOptions" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="selectProductTypeElem" EventName="OnSelectionChanged" />
        </Triggers>
        <ContentTemplate>
            <asp:MultiView ID="typeSpecificOptionsElem" runat="server" ActiveViewIndex="-1">
                <%-- Membership --%>
                <asp:View ID="membershipViewElem" runat="server">
                    <asp:Panel ID="pnlMembership" runat="server">
                        <cms:Membership ID="membershipElem" runat="server" OnOnValidityChanged="membershipElem_OnValidityChanged" />
                    </asp:Panel>
                    <br />
                </asp:View>
                <%-- E-product --%>
                <asp:View ID="eProductViewElem" runat="server">
                    <asp:Panel ID="pnlEProduct" runat="server">
                        <cms:EProduct ID="eProductElem" runat="server" OnOnValidityChanged="eProductElem_OnValidityChanged"
                            OnOnBeforeUpload="eProductElem_OnBeforeUpload" OnOnAfterUpload="eProductElem_OnAfterUpload" />
                    </asp:Panel>
                    <br />
                </asp:View>
                <%-- Donation --%>
                <asp:View ID="donationViewElem" runat="server">
                    <asp:Panel ID="pnlDonation" runat="server">
                        <cms:Donation ID="donationElem" runat="server" />
                    </asp:Panel>
                    <br />
                </asp:View>
                <%-- Bundle --%>
                <asp:View ID="bundleViewElem" runat="server">
                    <asp:Panel ID="pnlBundle" runat="server">
                        <cms:Bundle ID="bundleElem" runat="server" OnOnProductsSelectionChangesSaved="bundleElem_OnProductsSelectionChangesSaved" />
                    </asp:Panel>
                    <br />
                </asp:View>
            </asp:MultiView>
        </ContentTemplate>
    </cms:CMSUpdatePanel>
    <%-- Status information --%>
    <asp:Panel ID="pnlStatus" runat="server">
        <table>
            <%-- Enabled --%>
            <tr>
                <td class="FieldLabel" style="width: 150px;">
                    <cms:LocalizedLabel runat="server" EnableViewState="false" ResourceString="general.enabled"
                        DisplayColon="true" />
                </td>
                <td>
                    <asp:CheckBox ID="chkSKUEnabled" runat="server" CssClass="CheckBoxMovedLeft" Checked="true"
                        EnableViewState="false" />
                </td>
            </tr>
            <%-- Public status --%>
            <tr>
                <td class="FieldLabel">
                    <cms:LocalizedLabel runat="server" EnableViewState="false" ResourceString="com_sku_edit_general.skupublicstatusidlabel"
                        DisplayColon="true" />
                </td>
                <td>
                    <cms:PublicStatusSelector runat="server" ID="publicStatusElem" AddAllItemsRecord="false"
                        AddNoneRecord="true" UseStatusNameForSelection="false" IsLiveSite="false" />
                </td>
            </tr>
            <%-- Internal status --%>
            <tr>
                <td class="FieldLabel">
                    <cms:LocalizedLabel runat="server" EnableViewState="false" ResourceString="com_sku_edit_general.skuinternalstatusidlabel"
                        DisplayColon="true" />
                </td>
                <td>
                    <cms:InternalStatusSelector runat="server" ID="internalStatusElem" AddAllItemsRecord="false"
                        AddNoneRecord="true" UseStatusNameForSelection="false" IsLiveSite="false" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <br />
    <%-- Inventory information --%>
    <cms:CMSUpdatePanel ID="pnlUpdateInventory" runat="server" ChildrenAsTriggers="false"
        UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="selectProductTypeElem" EventName="OnSelectionChanged" />
            <asp:AsyncPostBackTrigger ControlID="membershipElem" EventName="OnValidityChanged" />
            <asp:AsyncPostBackTrigger ControlID="eProductElem" EventName="OnValidityChanged" />
        </Triggers>
        <ContentTemplate>
            <asp:Panel ID="pnlInventory" runat="server">
                <table>
                    <%-- Sell only available --%>
                    <tr>
                        <td class="FieldLabel" style="width: 150px;">
                            <cms:LocalizedLabel ID="LocalizedLabel6" runat="server" EnableViewState="false" ResourceString="com.productedit.sellonlyavailable"
                                DisplayColon="true" />
                        </td>
                        <td>
                            <asp:CheckBox ID="chkSKUSellOnlyAvailable" runat="server" CssClass="CheckBoxMovedLeft"
                                Checked="false" EnableViewState="false" />
                        </td>
                    </tr>
                    <%-- Available items --%>
                    <tr>
                        <td class="FieldLabel" style="vertical-align: top; padding-top: 5px;">
                            <cms:LocalizedLabel ID="LocalizedLabel7" runat="server" EnableViewState="false" ResourceString="com.productedit.availableitems"
                                DisplayColon="true" />
                        </td>
                        <td>
                            <cms:CMSTextBox ID="txtSKUAvailableItems" runat="server" CssClass="TextBoxField"
                                EnableViewState="false" MaxLength="9" />
                            <div>
                                <cms:CMSRangeValidator ID="rvSKUAvailableItems" runat="server" ControlToValidate="txtSKUAvailableItems"
                                    MaximumValue="999999999" MinimumValue="-99999999" Type="Integer" EnableViewState="false"
                                    Display="Dynamic" />
                            </div>
                        </td>
                    </tr>
                    <%-- Availability --%>
                    <tr>
                        <td class="FieldLabel" style="vertical-align: top; padding-top: 5px;">
                            <cms:LocalizedLabel ID="LocalizedLabel8" runat="server" EnableViewState="false" ResourceString="com.productedit.availability"
                                DisplayColon="true" />
                        </td>
                        <td>
                            <cms:CMSTextBox ID="txtSKUAvailableInDays" runat="server" CssClass="TextBoxField"
                                MaxLength="5" EnableViewState="false" />
                            <div>
                                <cms:CMSRangeValidator ID="rvSKUAvailableInDays" runat="server" ControlToValidate="txtSKUAvailableInDays"
                                    MaximumValue="99999" MinimumValue="-9999" Type="Integer" Display="Dynamic" EnableViewState="false" />
                            </div>
                        </td>
                    </tr>
                    <%-- Max order items --%>
                    <tr>
                        <td class="FieldLabel" style="vertical-align: top; padding-top: 5px;">
                            <cms:LocalizedLabel ID="LocalizedLabel9" runat="server" EnableViewState="false" ResourceString="com.productedit.maxorderitems"
                                DisplayColon="true" />
                        </td>
                        <td>
                            <cms:CMSTextBox ID="txtMaxOrderItems" runat="server" CssClass="TextBoxField" MaxLength="9" />
                            <div>
                                <cms:CMSRangeValidator ID="rvMaxOrderItems" runat="server" ControlToValidate="txtMaxOrderItems"
                                    MaximumValue="999999999" MinimumValue="0" Type="Integer" EnableViewState="false"
                                    Display="Dynamic" />
                            </div>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
    </cms:CMSUpdatePanel>
    <br />
    <%-- Shipping information --%>
    <cms:CMSUpdatePanel ID="pnlUpdateShipping" runat="server" ChildrenAsTriggers="false"
        UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="selectProductTypeElem" EventName="OnSelectionChanged" />
            <asp:AsyncPostBackTrigger ControlID="bundleElem" EventName="OnRemoveFromInventoryChanged" />
        </Triggers>
        <ContentTemplate>
            <asp:Panel ID="pnlShipping" runat="server">
                <table>
                    <%-- Needs shipping --%>
                    <tr id="needsShippingTableRowElem" runat="server">
                        <td class="FieldLabel" style="width: 150px;">
                            <cms:LocalizedLabel ID="LocalizedLabel1" runat="server" EnableViewState="false" ResourceString="com.productedit.needsshipping"
                                DisplayColon="true" />
                        </td>
                        <td>
                            <asp:CheckBox ID="chkNeedsShipping" runat="server" Checked="true" />
                        </td>
                    </tr>
                    <%-- Package weight --%>
                    <tr>
                        <td class="FieldLabel" style="vertical-align: top; padding-top: 5px; width: 150px;">
                            <cms:LocalizedLabel runat="server" EnableViewState="false" ResourceString="com.productedit.packageweight"
                                DisplayColon="true" />
                        </td>
                        <td>
                            <cms:CMSTextBox ID="txtSKUWeight" runat="server" CssClass="TextBoxField" EnableViewState="false"
                                MaxLength="10" />
                            <div>
                                <cms:CMSRangeValidator ID="rvSKUWeight" runat="server" Type="Double" ControlToValidate="txtSKUWeight"
                                    MaximumValue="9999999999" MinimumValue="0" EnableViewState="false" Display="Dynamic" />
                            </div>
                        </td>
                    </tr>
                    <%-- Package height --%>
                    <tr>
                        <td class="FieldLabel" style="vertical-align: top; padding-top: 5px; width: 150px;">
                            <cms:LocalizedLabel runat="server" EnableViewState="false" ResourceString="com.productedit.packageheight"
                                DisplayColon="true" />
                        </td>
                        <td>
                            <cms:CMSTextBox ID="txtSKUHeight" runat="server" CssClass="TextBoxField" EnableViewState="false"
                                MaxLength="10" />
                            <div>
                                <cms:CMSRangeValidator ID="rvSKUHeight" runat="server" ControlToValidate="txtSKUHeight"
                                    MaximumValue="9999999999" MinimumValue="0" Type="Double" EnableViewState="false"
                                    Display="Dynamic" />
                            </div>
                        </td>
                    </tr>
                    <%-- Package width --%>
                    <tr>
                        <td class="FieldLabel" style="vertical-align: top; padding-top: 5px; width: 150px;">
                            <cms:LocalizedLabel runat="server" EnableViewState="false" ResourceString="com.productedit.packagewidth"
                                DisplayColon="true" />
                        </td>
                        <td>
                            <cms:CMSTextBox ID="txtSKUWidth" runat="server" CssClass="TextBoxField" EnableViewState="false"
                                MaxLength="10" />
                            <div>
                                <cms:CMSRangeValidator ID="rvSKUWidth" runat="server" ControlToValidate="txtSKUWidth"
                                    MaximumValue="9999999999" MinimumValue="0" Type="Double" EnableViewState="false"
                                    Display="Dynamic" />
                            </div>
                        </td>
                    </tr>
                    <%-- Package depth --%>
                    <tr>
                        <td class="FieldLabel" style="vertical-align: top; padding-top: 5px; width: 150px;">
                            <cms:LocalizedLabel runat="server" EnableViewState="false" ResourceString="com.productedit.packagedepth"
                                DisplayColon="true" />
                        </td>
                        <td>
                            <cms:CMSTextBox ID="txtSKUDepth" runat="server" CssClass="TextBoxField" EnableViewState="false"
                                MaxLength="10" />
                            <div>
                                <cms:CMSRangeValidator ID="rvSKUDepth" runat="server" ControlToValidate="txtSKUDepth"
                                    MaximumValue="9999999999" MinimumValue="0" Type="Double" EnableViewState="false"
                                    Display="Dynamic" />
                            </div>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
    </cms:CMSUpdatePanel>
    <br />
    <%-- Conversion --%>
    <asp:Panel ID="pnlConversion" runat="server">
        <table>
            <tr>
                <td class="FieldLabel" style="width: 150px;">
                    <cms:LocalizedLabel ID="lblConversionName" runat="server" EnableViewState="false"
                        ResourceString="conversion.name" DisplayColon="true" />
                </td>
                <td>
                    <cms:Conversion runat="server" ID="ucConversion" IsLiveSite="false" />
                </td>
            </tr>
            <tr>
                <td class="FieldLabel" style="width: 150px;">
                    <cms:LocalizedLabel ID="lblConversionValue" runat="server" EnableViewState="false"
                        ResourceString="om.trackconversionvalue" DisplayColon="true" />
                </td>
                <td>
                    <cms:CMSTextBox ID="txtConversionValue" runat="server" CssClass="TextBoxField" EnableViewState="false"
                        MaxLength="200" />
                </td>
            </tr>
        </table>
    </asp:Panel>
</div>
