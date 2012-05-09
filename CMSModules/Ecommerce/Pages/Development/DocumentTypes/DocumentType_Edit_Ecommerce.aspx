<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Ecommerce_Pages_Development_DocumentTypes_DocumentType_Edit_Ecommerce"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Document Type Edit - Ecommerce"
    CodeFile="DocumentType_Edit_Ecommerce.aspx.cs" %>

<%@ Register Src="~/CMSModules/Ecommerce/FormControls/DepartmentSelector.ascx" TagName="DepartmentSelector"
    TagPrefix="cms" %>
<%@ Register TagPrefix="cms" TagName="SelectProductType" Src="~/CMSModules/Ecommerce/FormControls/SelectProductType.ascx" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
        Visible="false" />
    <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
    <asp:Label runat="server" ID="lblTitle" CssClass="InfoLabel" EnableViewState="false" />
    <table>
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblName" EnableViewState="false" />
            </td>
            <td>
                <asp:DropDownList ID="drpName" runat="server" CssClass="DropDownField" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblImage" EnableViewState="false" />
            </td>
            <td>
                <asp:DropDownList ID="drpImage" runat="server" CssClass="DropDownField" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblWeight" EnableViewState="false" />
            </td>
            <td>
                <asp:DropDownList ID="drpWeight" runat="server" CssClass="DropDownField" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblHeight" EnableViewState="false" />
            </td>
            <td>
                <asp:DropDownList ID="drpHeight" runat="server" CssClass="DropDownField" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblWidth" EnableViewState="false" />
            </td>
            <td>
                <asp:DropDownList ID="drpWidth" runat="server" CssClass="DropDownField" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblDepth" EnableViewState="false" />
            </td>
            <td>
                <asp:DropDownList ID="drpDepth" runat="server" CssClass="DropDownField" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblPrice" EnableViewState="false" />
            </td>
            <td>
                <asp:DropDownList ID="drpPrice" runat="server" CssClass="DropDownField" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblDescription" EnableViewState="false" />
            </td>
            <td>
                <asp:DropDownList ID="drpDescription" runat="server" CssClass="DropDownField" />
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="FieldLabel" colspan="2">
                <asp:CheckBox ID="chkGenerateSKU" runat="server" CssClass="CheckBoxMovedLeft" EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblDepartments" EnableViewState="false" />
            </td>
            <td>
                <cms:DepartmentSelector runat="server" ID="departmentElem" DropDownListMode="false"
                    UseDepartmentNameForSelection="false" ShowAllSites="true" AddNoneRecord="true" IsLiveSite="false" />
            </td>
        </tr>
        <%-- Default product type --%>
        <asp:PlaceHolder runat="server" ID="plcDefaultProductType" Visible="false">
            <tr>
                <td class="FieldLabel">
                    <cms:LocalizedLabel runat="server" ResourceString="doctype.ecommerce.defaultproducttype"
                        EnableViewState="false" />
                </td>
                <td>
                    <cms:SelectProductType runat="server" ID="defaultProductType" />
                </td>
            </tr>
        </asp:PlaceHolder>
        <tr>
            <td>
            </td>
            <td>
                <br />
                <cms:CMSButton runat="server" ID="btnOk" OnClick="btnOK_Click" CssClass="SubmitButton"
                    EnableViewState="false" />
            </td>
        </tr>
    </table>
    <asp:Literal ID="ltlScrpt" runat="server" EnableViewState="false" />
</asp:Content>
