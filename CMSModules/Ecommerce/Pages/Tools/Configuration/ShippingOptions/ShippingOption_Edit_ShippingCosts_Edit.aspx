<%@ Page Title="" Language="C#" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master"
    AutoEventWireup="true" CodeFile="ShippingOption_Edit_ShippingCosts_Edit.aspx.cs"
    Inherits="CMSModules_Ecommerce_Pages_Tools_Configuration_ShippingOptions_ShippingOption_Edit_ShippingCosts_Edit"
    Theme="Default" %>

<%@ Register TagPrefix="cms" Namespace="CMS.UIControls" Assembly="CMS.UIControls" %>
<%@ Register Src="~/CMSModules/Ecommerce/Controls/UI/PriceSelector.ascx" TagName="PriceSelector"
    TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <div style="position: relative; top: -10px; left: -7px;">
        <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
            Visible="false" />
        <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
            Visible="false" />
        <table style="vertical-align: top">
            <tr>
                <td class="FieldLabel" style="height: 32px">
                    <asp:Label runat="server" ID="lblShippingCostMinWeight" EnableViewState="false" />
                </td>
                <td style="height: 32px">
                    <cms:CMSTextBox ID="txtShippingCostMinWeight" runat="server" CssClass="TextBoxField"
                        MaxLength="9" />&nbsp;&nbsp;
                    <div>
                        <cms:CMSRequiredFieldValidator ID="rfvMinWeight" runat="server" ControlToValidate="txtShippingCostMinWeight"
                            Display="Dynamic" EnableViewState="false" /><cms:CMSRangeValidator ID="rvMinWeight"
                                runat="server" ControlToValidate="txtShippingCostMinWeight" MinimumValue="0"
                                Type="Double" Display="Dynamic" MaximumValue="999999999" EnableViewState="false"></cms:CMSRangeValidator>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="FieldLabel">
                    <asp:Label runat="server" ID="lblShippingCostValue" EnableViewState="false" />
                </td>
                <td>
                    <cms:PriceSelector ID="txtShippingCostCharge" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <cms:CMSButton runat="server" ID="btnOk" OnClick="btnOK_Click" EnableViewState="false"
                        CssClass="SubmitButton" />
                </td>
            </tr>
        </table>
    </div>
    <asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />
</asp:Content>
