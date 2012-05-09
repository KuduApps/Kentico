<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master"
    Inherits="CMSModules_Ecommerce_Pages_Tools_Customers_Customer_New" Theme="Default"
    Title="Customer - New" CodeFile="Customer_New.aspx.cs" %>

<%@ Register Src="~/CMSFormControls/CountrySelector.ascx" TagName="CountrySelector"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Membership/FormControls/Passwords/PasswordStrength.ascx" TagName="PasswordStrength"
    TagPrefix="cms" %>
<asp:Content ContentPlaceHolderID="plcContent" ID="content" runat="server">
    <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
        Visible="false" />
    <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
    <div style="width: 700px;">
        <%-- General information --%>
        <asp:Panel ID="pnlGeneral" runat="server">
            <table style="vertical-align: top">
                <tr>
                    <td class="FieldLabel" style="width: 175px;">
                        <asp:Label runat="server" ID="lblCustomerFirstName" EnableViewState="false" />
                    </td>
                    <td>
                        <cms:CMSTextBox ID="txtCustomerFirstName" runat="server" CssClass="TextBoxField" MaxLength="200"
                            EnableViewState="false" />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="FieldLabel">
                        <asp:Label runat="server" ID="lblCustomerLastName" EnableViewState="false" />
                    </td>
                    <td>
                        <cms:CMSTextBox ID="txtCustomerLastName" runat="server" CssClass="TextBoxField" MaxLength="200"
                            EnableViewState="false" />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="FieldLabel">
                        <asp:Label runat="server" ID="lblCustomerCompany" EnableViewState="false" />
                    </td>
                    <td>
                        <cms:CMSTextBox ID="txtCustomerCompany" runat="server" CssClass="TextBoxField" MaxLength="200"
                            EnableViewState="false" />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="FieldLabel" style="vertical-align: top; padding-top: 6px">
                        <asp:Label runat="server" ID="lblCountry" EnableViewState="false" />
                    </td>
                    <td>
                        <cms:CountrySelector ID="drpCountry" runat="server" UseCodeNameForSelection="false"
                            AddSelectCountryRecord="false" AddNoneRecord="true" IsLiveSite="false" />
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <br />
        <%-- User info--%>
        <asp:Panel ID="pnlUserInfo" runat="server">
            <table style="vertical-align: top">
                <tr>
                    <td colspan="3">
                        <asp:CheckBox ID="chkCreateLogin" runat="server" CssClass="CheckBoxMovedLeft" EnableViewState="false" />
                    </td>
                </tr>
                <tr id="loginLine1">
                    <td class="FieldLabel" style="width: 175px;">
                        <asp:Label runat="server" ID="lblUserName" EnableViewState="false" />
                    </td>
                    <td>
                        <cms:CMSTextBox ID="txtUserName" runat="server" CssClass="TextBoxField" MaxLength="100"
                            EnableViewState="false" />
                    </td>
                    <td>
                        <cms:CMSRequiredFieldValidator ID="rqvUserName" runat="server" ControlToValidate="txtUserName"
                            ValidationGroup="Login" EnableViewState="false" />
                    </td>
                </tr>
                <tr id="loginLine2">
                    <td class="FieldLabel FieldLabelTop">
                        <asp:Label runat="server" ID="lblPassword1" EnableViewState="false" />
                    </td>
                    <td>
                        <cms:PasswordStrength runat="server" ID="passStrength" AllowEmpty="true" />
                    </td>
                    <td>                        
                    </td>
                </tr>
                <tr id="loginLine3">
                    <td class="FieldLabel">
                        <asp:Label runat="server" ID="lblPassword2" EnableViewState="false" />
                    </td>
                    <td>
                        <cms:CMSTextBox ID="txtPassword2" runat="server" CssClass="TextBoxField" TextMode="Password"
                            MaxLength="100" EnableViewState="false" />
                    </td>
                    <td>
                        <cms:CMSRequiredFieldValidator ID="rqvPassword2" runat="server" ControlToValidate="txtPassword2"
                            ValidationGroup="Login" EnableViewState="false" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <table style="vertical-align: top; margin-top: 10px;">
            <tr>
                <td>
                    <cms:CMSButton runat="server" ID="btnOk" OnClick="btnOK_Click" EnableViewState="false"
                        CssClass="SubmitButton" />
                </td>
                <td>
                </td>
            </tr>
        </table>
    </div>
    <asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />
</asp:Content>
