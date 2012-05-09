<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master"
    Title="Banned IP Properties" Inherits="CMSModules_BannedIP_Tools_BannedIP_Edit"
    Theme="Default" CodeFile="BannedIP_Edit.aspx.cs" %>

<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <table style="vertical-align: top">
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblIPAddress" EnableViewState="false" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtIPAddress" runat="server" CssClass="TextBoxField" MaxLength="100" />
                <cms:CMSRequiredFieldValidator ID="rfvIPAddress" runat="server" ControlToValidate="txtIPAddress"
                    Display="Dynamic" EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblIPAddressBanType" EnableViewState="false" />
            </td>
            <td>
                <asp:DropDownList ID="drpIPAddressBanType" runat="server" CssClass="DropDownField" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblIPAddressBanEnabled" EnableViewState="false" />
            </td>
            <td>
                <asp:CheckBox ID="chkIPAddressBanEnabled" runat="server" Checked="true" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblIPAddressBanReason" EnableViewState="false" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtIPAddressBanReason" runat="server" TextMode="MultiLine" CssClass="TextAreaLarge"
                    MaxLength="450" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
            </td>
            <td>
                <asp:RadioButton ID="radBanIP" runat="server" GroupName="IPAllowed" Checked="true" /><br />
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <asp:RadioButton ID="radAllowIP" runat="server" GroupName="IPAllowed" />
            </td>
        </tr>
        <asp:PlaceHolder ID="plcIPOveride" runat="server" Visible="false">
            <tr>
                <td class="FieldLabel">
                    <br />
                    <asp:Label runat="server" ID="lblIPAddressAllowOverride" EnableViewState="false" />
                </td>
                <td>
                    <br />
                    <asp:CheckBox ID="chkIPAddressAllowOverride" runat="server" CssClass="CheckBoxMovedLeft" />
                </td>
            </tr>
        </asp:PlaceHolder>
        <tr>
            <td>
            </td>
            <td>
                <cms:CMSButton runat="server" ID="btnOk" OnClick="btnOK_Click" EnableViewState="false"
                    CssClass="SubmitButton" />
            </td>
        </tr>
    </table>
</asp:Content>
