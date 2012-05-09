<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Ecommerce_Pages_Tools_Customers_Customer_Edit_Address_Edit" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Theme="Default" Title="Address properties" CodeFile="Customer_Edit_Address_Edit.aspx.cs" %>

<%@ Register Src="~/CMSFormControls/CountrySelector.ascx" TagName="CountrySelector"
    TagPrefix="cms" %>
<asp:Content ContentPlaceHolderID="plcContent" ID="content" runat="server">
    <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
        Visible="false" />
    <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
    <table style="vertical-align: top">
        <tr>
            <td>
                <asp:Label runat="server" ID="lblPersonalName" EnableViewState="false" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtPersonalName" runat="server" CssClass="TextBoxField" MaxLength="200"
                    EnableViewState="false" />
            </td>
            <td>
                <cms:CMSRequiredFieldValidator ID="rqvPersonalName" runat="server" ControlToValidate="txtPersonalName"
                    ValidationGroup="Address" EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel" style="vertical-align: top;">
                <asp:Label runat="server" ID="lblAddressLine1" EnableViewState="false" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtAddressLine1" runat="server" CssClass="TextBoxField" MaxLength="100"
                    EnableViewState="false" />
            </td>
            <td style="vertical-align: top;">
                <cms:CMSRequiredFieldValidator ID="rqvLine" runat="server" ControlToValidate="txtAddressLine1"
                    ValidationGroup="Address" EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                <cms:CMSTextBox ID="txtAddressLine2" runat="server" CssClass="TextBoxField" MaxLength="100"
                    EnableViewState="false" />
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblAddressCity" EnableViewState="false" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtAddressCity" runat="server" CssClass="TextBoxField" MaxLength="100"
                    EnableViewState="false" />
            </td>
            <td>
                <cms:CMSRequiredFieldValidator ID="rqvCity" runat="server" ControlToValidate="txtAddressCity"
                    ValidationGroup="Address" EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblAddressZip" EnableViewState="false" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtAddressZip" runat="server" CssClass="TextBoxField" MaxLength="20"
                    EnableViewState="false" />
            </td>
            <td>
                <cms:CMSRequiredFieldValidator ID="rqvZipCode" runat="server" ValidationGroup="Address"
                    ControlToValidate="txtAddressZip" Display="Dynamic" EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblAddressCountry" EnableViewState="false" />
            </td>
            <td>
                <cms:CountrySelector ID="ucCountrySelector" runat="server" UseCodeNameForSelection="false" AddNoneRecord="true"
                    AddSelectCountryRecord="false" IsLiveSite="false" />
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblAddressDeliveryPhone" EnableViewState="false" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtAddressDeliveryPhone" runat="server" CssClass="TextBoxField"
                    MaxLength="100" EnableViewState="false" />
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel runat="server" ID="lblAddressEnabled" EnableViewState="false"
                    ResourceString="general.enabled" DisplayColon="true" />
            </td>
            <td>
                <asp:CheckBox ID="chkAddressEnabled" runat="server" CssClass="CheckBoxMovedLeft"
                    Checked="true" EnableViewState="false" />
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblAddressIsShipping" EnableViewState="false" />
            </td>
            <td>
                <asp:CheckBox ID="chkAddressIsShipping" runat="server" CssClass="CheckBoxMovedLeft"
                    Checked="true" EnableViewState="false" />
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblAddressIsBilling" EnableViewState="false" />
            </td>
            <td>
                <asp:CheckBox ID="chkAddressIsBilling" runat="server" CssClass="CheckBoxMovedLeft"
                    Checked="true" EnableViewState="false" />
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblAddressIsCompany" EnableViewState="false" />
            </td>
            <td>
                <asp:CheckBox ID="chkAddressIsCompany" runat="server" CssClass="CheckBoxMovedLeft"
                    Checked="true" EnableViewState="false" />
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
                    CssClass="SubmitButton" ValidationGroup="Address" />
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
