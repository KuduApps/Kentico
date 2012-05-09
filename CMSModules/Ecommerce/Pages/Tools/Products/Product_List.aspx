<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master"
    Inherits="CMSModules_Ecommerce_Pages_Tools_Products_Product_List" Theme="Default"
    Title="Product list" CodeFile="Product_List.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<%@ Register Src="~/CMSFormControls/Sites/SiteSelector.ascx" TagName="SiteSelector"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSFormControls/Sites/SiteOrGlobalSelector.ascx" TagName="SiteOrGlobalSelector"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/PageElements/HeaderActions.ascx" TagName="HeaderActions"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Ecommerce/FormControls/PublicStatusSelector.ascx"
    TagName="PublicStatusSelector" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Ecommerce/FormControls/InternalStatusSelector.ascx"
    TagName="InternalStatusSelector" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Ecommerce/FormControls/DepartmentSelector.ascx" TagName="DepartmentSelector"
    TagPrefix="cms" %>
<%@ Register TagPrefix="cms" TagName="SelectProductType" Src="~/CMSModules/Ecommerce/FormControls/SelectProductType.ascx" %>
<asp:Content ID="cntControls" runat="server" ContentPlaceHolderID="plcSiteSelector">
    <cms:LocalizedLabel runat="server" ID="lblSite" EnableViewState="false" DisplayColon="true"
        ResourceString="General.Site" />
    <cms:SiteOrGlobalSelector ID="SelectSite" runat="server" IsLiveSite="false" />
</asp:Content>
<asp:Content ID="cntActions" runat="server" ContentPlaceHolderID="plcActions">
    <cms:CMSUpdatePanel ID="pnlActons" runat="server">
        <ContentTemplate>
            <div class="LeftAlign">
                <cms:HeaderActions ID="hdrActions" runat="server" />
            </div>
            <cms:LocalizedLabel ID="lblWarnNew" runat="server" ResourceString="com.chooseglobalorsite"
                EnableViewState="false" Visible="false" CssClass="ActionsInfoLabel" />
            <div class="ClearBoth">
            </div>
        </ContentTemplate>
    </cms:CMSUpdatePanel>
</asp:Content>
<asp:Content ContentPlaceHolderID="plcContent" ID="content" runat="server">
    <cms:LocalizedLabel ID="lblMissingRate" runat="server" CssClass="ErrorLabel" EnableViewState="false"
        ResourceString="com.NeedExchangeRateFromGlobal" Visible="false" />
    <cms:CMSUpdatePanel ID="pnlUpdate" runat="server">
        <ContentTemplate>
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lblName" runat="server" CssClass="ContentLabel" EnableViewState="false" />
                    </td>
                    <td>
                        <cms:CMSTextBox ID="txtName" runat="server" CssClass="TextBoxField" MaxLength="450"
                            EnableViewState="false" />
                    </td>
                </tr>
                <asp:PlaceHolder ID="plcAdvancedFilterNumber" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblNumber" runat="server" CssClass="ContentLabel" EnableViewState="false" />
                        </td>
                        <td>
                            <cms:CMSTextBox ID="txtNumber" runat="server" CssClass="TextBoxField" MaxLength="200"
                                EnableViewState="false" />
                        </td>
                    </tr>
                </asp:PlaceHolder>
                <%-- Product type filter --%>
                <tr>
                    <td>
                        <cms:LocalizedLabel runat="server" ResourceString="product_list.producttype" DisplayColon="true" />
                    </td>
                    <td>
                        <cms:SelectProductType runat="server" ID="selectProductTypeElem" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblDepartment" runat="server" CssClass="ContentLabel" EnableViewState="false" />
                    </td>
                    <td>
                        <cms:DepartmentSelector runat="server" ID="departmentElem" AddAllMyRecord="true"
                            AddAllItemsRecord="false" IsLiveSite="false" AddNoneRecord="false" AddWithoutDepartmentRecord="true" UseDepartmentNameForSelection="false" />
                    </td>
                </tr>
                <asp:PlaceHolder ID="plcAdvancedFilterStatuses" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblStoreStatus" runat="server" CssClass="ContentLabel" EnableViewState="false" />
                        </td>
                        <td>
                            <cms:PublicStatusSelector runat="server" ID="publicStatusElem" AddAllItemsRecord="true"
                                AddNoneRecord="false" IsLiveSite="false" UseStatusNameForSelection="false" DisplayOnlyEnabled="false" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblInternalStatus" runat="server" CssClass="ContentLabel" EnableViewState="false" />
                        </td>
                        <td>
                            <cms:InternalStatusSelector runat="server" ID="internalStatusElem" AddAllItemsRecord="true"
                                AddNoneRecord="false" UseStatusNameForSelection="false" DisplayOnlyEnabled="false" />
                        </td>
                    </tr>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="plcAdvancedFilterAssignedToDocument" runat="server">
                    <tr>
                        <td>
                            <cms:LocalizedLabel runat="server" CssClass="ContentLabel" EnableViewState="false"
                                ResourceString="product_list.assignedtodocument" DisplayColon="true" />
                        </td>
                        <td>
                            <asp:DropDownList runat="server" ID="drpAssignedToDocument" CssClass="DropDownField" />
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
            <asp:PlaceHolder ID="plcAdvancedFilter" runat="server">
                <div>
                    <asp:Image runat="server" ID="imgShowSimpleFilter" CssClass="NewItemImage" />
                    <asp:LinkButton ID="lnkShowSimpleFilter" runat="server" OnClick="lnkShowSimpleFilter_Click" />
                </div>
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="plcSimpleFilter" runat="server">
                <div>
                    <asp:Image runat="server" ID="imgShowAdvancedFilter" CssClass="NewItemImage" />
                    <asp:LinkButton ID="lnkShowAdvancedFilter" runat="server" OnClick="lnkShowAdvancedFilter_Click" />
                </div>
            </asp:PlaceHolder>
            <br />
            <asp:Label ID="lblError" runat="server" CssClass="ErrorLabel" Visible="false" EnableViewState="false" />
            <asp:Label ID="lblInfo" runat="server" CssClass="InfoLabel" Visible="false" EnableViewState="false" />
            <cms:UniGrid ID="gridData" runat="server" GridName="Product_List.xml" OrderBy="SKUName"
                IsLiveSite="false" Columns="SKUID, SKUName, SKUNumber, SKUPrice, SKUAvailableItems, SKUEnabled, SKUSiteID" />
        </ContentTemplate>
    </cms:CMSUpdatePanel>
</asp:Content>
