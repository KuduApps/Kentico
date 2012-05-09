<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Ecommerce_Pages_Tools_Orders_Order_Edit_Address" Theme="Default" MasterPageFile="~/CMSMasterPages/UI/Dialogs/ModalDialogPage.master" Title="Order - edit adresses" CodeFile="Order_Edit_Address.aspx.cs" %>

<%@ Register Src="~/CMSFormControls/CountrySelector.ascx" TagName="CountrySelector"
    TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <div class="PageContent">
        <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
            Visible="false" />
        <table style="vertical-align: top">
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
                <td class="FieldLabel">
                    <asp:Label runat="server" ID="lblAddressLine1" EnableViewState="false" />
                </td>
                <td>
                    <cms:CMSTextBox ID="txtAddressLine1" runat="server" CssClass="TextBoxField" MaxLength="100"
                        EnableViewState="false" />
                </td>
                <td>
                    <cms:CMSRequiredFieldValidator ID="rqvLine" runat="server" ControlToValidate="txtAddressLine1"
                        ValidationGroup="Address" Display="Dynamic" EnableViewState="false" />
                </td>
            </tr>
            <tr>
                <td class="FieldLabel">
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
                    <cms:CountrySelector ID="ucCountrySelector" runat="server" UseCodeNameForSelection="false"
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
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
    <asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />
</asp:Content>
<asp:Content ID="cntFooter" runat="server" ContentPlaceHolderID="plcFooter">
    <div class="FloatRight">
        <cms:CMSButton ID="btnOk" CssClass="SubmitButton" OnClick="btnOK_Click" ValidationGroup="Address"
            runat="server" EnableViewState="false" /><cms:CMSButton ID="btnCancel" CssClass="SubmitButton"
                runat="server" OnClientClick="window.close(); return false;" EnableViewState="false" />
    </div>
</asp:Content>
