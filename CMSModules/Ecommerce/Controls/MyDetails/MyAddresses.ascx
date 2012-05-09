<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_Ecommerce_Controls_MyDetails_MyAddresses" CodeFile="MyAddresses.ascx.cs" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<%@ Register Src="~/CMSFormControls/CountrySelector.ascx" TagName="CountrySelector"
    TagPrefix="cms" %>
<div class="MyAddresses">
    <asp:Label ID="lblError" runat="server" Visible="false" EnableViewState="false" Style="color: Red" />
    <%--Address list--%>
    <asp:PlaceHolder runat="server" ID="plhList">
        <div style="margin-bottom: 15px">
            <asp:LinkButton ID="btnNew" runat="server" OnClick="btnNew_OnClick" /></div>
        <cms:UniGrid runat="server" ID="gridAddresses" GridName="~/CMSModules/Ecommerce/Controls/MyDetails/MyAddresses.xml"
            Columns="AddressID,AddressName" OrderBy="AddressName" />
    </asp:PlaceHolder>
    <%--Address edit--%>
    <asp:PlaceHolder runat="server" ID="plhEdit" Visible="false">
        <asp:Panel ID="pnlAddressEdit" runat="server" DefaultButton="btnOK">
            <div style="margin-bottom: 15px">
                <asp:LinkButton ID="btnList" runat="server" OnClick="btnList_OnClick" />&nbsp;
                <asp:Label ID="lblAddress" runat="server" />
            </div>
            <asp:Label runat="server" ID="lblInfo" EnableViewState="false" Visible="false" />
            <table style="vertical-align: top">
                <tr>
                    <td class="FieldLabel">
                        <asp:Label runat="server" ID="lblPersonalName" EnableViewState="false" />
                    </td>
                    <td>
                        <cms:ExtendedTextBox ID="txtPersonalName" runat="server" MaxLength="200" CssClass="TextBoxField"
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
                        <table border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <cms:ExtendedTextBox ID="txtAddressLine1" runat="server" MaxLength="100" CssClass="TextBoxField"
                                        EnableViewState="false" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <cms:ExtendedTextBox ID="txtAddressLine2" runat="server" MaxLength="100" CssClass="TextBoxField"
                                        EnableViewState="false" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="vertical-align: top;">
                        <cms:CMSRequiredFieldValidator ID="rqvLine" runat="server" ControlToValidate="txtAddressLine1"
                            ValidationGroup="Address" EnableViewState="false" />
                    </td>
                </tr>
                <tr>
                    <td class="FieldLabel">
                        <asp:Label runat="server" ID="lblAddressCity" EnableViewState="false" />
                    </td>
                    <td>
                        <cms:ExtendedTextBox ID="txtAddressCity" runat="server" MaxLength="100" CssClass="TextBoxField"
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
                        <cms:ExtendedTextBox ID="txtAddressZip" runat="server" MaxLength="20" CssClass="TextBoxField"
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
                            AddSelectCountryRecord="false" AddNoneRecord="true" />
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
                        <cms:ExtendedTextBox ID="txtAddressDeliveryPhone" runat="server" MaxLength="100"
                            CssClass="TextBoxField" EnableViewState="false" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <br />
                        <cms:CMSButton runat="server" ID="btnOk" OnClick="btnOK_OnClick" EnableViewState="false"
                            ValidationGroup="Address" CssClass="ContentButton" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </asp:PlaceHolder>
    <asp:Button ID="btnHiddenEdit" runat="server" CssClass="HiddenButton" EnableViewState="false" />
    <asp:Button ID="btnHiddenDelete" runat="server" CssClass="HiddenButton" EnableViewState="false" />
    <asp:HiddenField ID="hdnID" runat="server" EnableViewState="false" />
</div>
