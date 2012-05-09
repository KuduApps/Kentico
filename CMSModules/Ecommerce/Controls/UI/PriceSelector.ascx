<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_Ecommerce_Controls_UI_PriceSelector"
    CodeFile="PriceSelector.ascx.cs" %>
<%-- Price input --%>
<cms:CMSTextBox ID="txtPrice" runat="server" CssClass="TextBoxField" MaxLength="20" />
<asp:Label ID="lblCurrencyCode" runat="server" EnableViewState="false" />
<%-- Validators --%>
<asp:Panel ID="pnlNewLineWrapper" runat="server" Visible="false" />
<asp:PlaceHolder ID="plcValidators" runat="server">
    <cms:CMSRangeValidator ID="rvPrice" runat="server" ControlToValidate="txtPrice" MaximumValue="9999999999"
        MinimumValue="-9999999999" Type="Double" Display="Dynamic" EnableViewState="false" />
    <cms:CMSRequiredFieldValidator ID="rfvPrice" runat="server" ControlToValidate="txtPrice"
        Display="Dynamic" EnableViewState="false" />
</asp:PlaceHolder>
