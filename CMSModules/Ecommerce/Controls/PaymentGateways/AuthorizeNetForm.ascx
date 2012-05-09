<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Ecommerce_Controls_PaymentGateways_AuthorizeNetForm" CodeFile="AuthorizeNetForm.ascx.cs" %>
<asp:Label ID="lblTitle" runat="server" EnableViewState="false" CssClass="BlockTitle" />
<asp:Label ID="lblError" runat="server" EnableViewState="false" CssClass="ErrorLabel"
    Visible="false" />
<table cellpadding="3">
    <tr>
        <td>
            <asp:Label ID="lblCardNumber" runat="server" EnableViewState="false" />
        </td>
        <td>
            <cms:CMSTextBox ID="txtCardNumber" runat="server" CssClass="TextBoxField" MaxLength="100"
                EnableViewState="false" /><asp:Label ID="lblMark1" runat="server" EnableViewState="false" />
            <cms:CMSRequiredFieldValidator ID="rfvCardNumber" runat="server" ControlToValidate="txtCardNumber"
                Display="Dynamic" ValidationGroup="ButtonNext" EnableViewState="false" />
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblCCV" runat="server" EnableViewState="false" />
        </td>
        <td>
            <cms:CMSTextBox ID="txtCCV" runat="server" CssClass="TextBoxField" MaxLength="100" EnableViewState="false" /><asp:Label
                ID="lblMark2" runat="server" EnableViewState="false" />
            <cms:CMSRequiredFieldValidator ID="rfvCCV" runat="server" ControlToValidate="txtCCV"
                Display="Dynamic" ValidationGroup="ButtonNext" EnableViewState="false" />
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblExpiration" runat="server" EnableViewState="false" />
        </td>
        <td>
            <asp:DropDownList ID="drpMonths" runat="server" />
            <asp:DropDownList ID="drpYears" runat="server" />
            <asp:Label ID="lblMark3" runat="server" EnableViewState="false" />
            <asp:Label ID="lblErrorDate" runat="server" EnableViewState="false" ForeColor="Red" />
        </td>
    </tr>
</table>
