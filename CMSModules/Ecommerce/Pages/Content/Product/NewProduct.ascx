<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_Ecommerce_Pages_Content_Product_NewProduct"
    CodeFile="NewProduct.ascx.cs" %>
<%@ Register Src="~/CMSModules/AdminControls/Controls/MetaFiles/File.ascx" TagName="File"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Ecommerce/Controls/UI/PriceSelector.ascx" TagName="PriceSelector"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Ecommerce/FormControls/DepartmentSelector.ascx" TagName="DepartmentSelector"
    TagPrefix="cms" %>
<div style="padding-left: 9px;">
    <asp:Label ID="lblError" runat="server" EnableViewState="false" Visible="false" CssClass="ErrorLabel" />
</div>
<br />
<asp:PlaceHolder ID="plcSKUControls" runat="server">
    <div class="FormPanel">
        <table class="EditingFormTable">
            <asp:PlaceHolder ID="plcSKUName" runat="server">
                <tr>
                    <td class="EditingFormLabelCell">
                        <asp:Label ID="lblSKUName" CssClass="EditingFormLabel" runat="server" EnableViewState="false" />
                    </td>
                    <td>
                        <cms:CMSTextBox ID="txtSKUName" runat="server" MaxLength="440" CssClass="Textboxfield"
                            EnableViewState="false" />
                    </td>
                </tr>
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="plcSKUPrice" runat="server">
                <tr>
                    <td class="EditingFormLabelCell">
                        <asp:Label ID="lblSKUPrice" CssClass="EditingFormLabel" runat="server" EnableViewState="false" />
                    </td>
                    <td>
                        <cms:PriceSelector ID="txtSKUPrice" runat="server" />
                    </td>
                </tr>
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="plcImagePath" runat="server" Visible="false">
                <tr>
                    <td class="EditingFormLabelCell">
                        <asp:Label runat="server" CssClass="EditingFormLabel" ID="lblSKUImagePathSelect" EnableViewState="false" />
                    </td>
                    <td>
                        <cms:ImageSelector ID="imgSelect" runat="server" ImageHeight="50" ShowImagePreview="true"
                            ShowClearButton="true" UseImagePath="true" IsLiveSite="false" />
                    </td>
                </tr>
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="plcMetaFile" runat="server" Visible="false">
                <tr>
                    <td class="EditingFormLabelCell">
                        <asp:Label runat="server" CssClass="EditingFormLabel" ID="lblSKUImagePath" EnableViewState="false" />
                    </td>
                    <td>
                        <cms:File ID="ucMetaFile" runat="server" />
                    </td>
                </tr>
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="plcSKUDescription" runat="server">
                <tr>
                    <td class="EditingFormLabelCell" style="vertical-align: top; padding-top: 5px;">
                        <cms:LocalizedLabel CssClass="EditingFormLabel" ID="lblSKUDescription" runat="server" EnableViewState="false"
                            ResourceString="general.description" DisplayColon="true" />
                    </td>
                    <td>
                        <cms:CMSHtmlEditor ID="htmlSKUDescription" runat="server" Width="400px" Height="300px" />
                    </td>
                </tr>
            </asp:PlaceHolder>
            <tr>
                <td class="EditingFormLabelCell">
                    <asp:Label ID="lblSKUDepartment" CssClass="EditingFormLabel" runat="server" EnableViewState="false" />
                </td>
                <td>
                    <cms:DepartmentSelector runat="server" ID="departmentElem" AddAllItemsRecord="false"
                        AddAllMyRecord="false" AddNoneRecord="true" UseDepartmentNameForSelection="false"
                        IsLiveSite="false" />
                </td>
            </tr>
        </table>
    </div>
</asp:PlaceHolder>
