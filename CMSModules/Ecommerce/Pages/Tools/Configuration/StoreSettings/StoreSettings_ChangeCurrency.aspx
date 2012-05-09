<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Ecommerce_Pages_Tools_Configuration_StoreSettings_StoreSettings_ChangeCurrency"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/Dialogs/ModalDialogPage.master"
    CodeFile="StoreSettings_ChangeCurrency.aspx.cs" %>

<%@ Register Src="~/CMSModules/ECommerce/FormControls/CurrencySelector.ascx" TagName="CurrencySelector"
    TagPrefix="cms" %>
<asp:Content ID="Content1" ContentPlaceHolderID="plcContent" runat="Server">
    <div class="PageContent">
        <asp:Label ID="lblInfo" runat="server" CssClass="InfoLabel" EnableViewState="false"
            Visible="false" />
        <asp:Label ID="lblError" runat="server" CssClass="ErrorLabel" EnableViewState="false"
            Visible="false" />
        <table>
            <asp:PlaceHolder runat="server" ID="plcOldCurrency">
                <tr>
                    <td>
                        <asp:Label ID="lblOldMainLabel" runat="server" EnableViewState="false" />&nbsp;
                    </td>
                    <td>
                        <asp:Label ID="lblOldMainCurrency" runat="server" EnableViewState="false" />&nbsp;
                    </td>
                </tr>
            </asp:PlaceHolder>
            <tr>
                <td>
                    <asp:Label ID="lblNewMainCurrency" runat="server" EnableViewState="false" />&nbsp;
                </td>
                <td>
                    <cms:CurrencySelector runat="server" ID="currencyElem" AddSiteDefaultCurrency="false"
                        ExcludeSiteDefaultCurrency="true" IsLiveSite="false" DisplayItems="Enabled" />
                </td>
            </tr>
            <asp:PlaceHolder runat="server" ID="plcRecalculationDetails">
                <tr>
                    <td>
                        <asp:Label ID="lblExchangeRate" runat="server" EnableViewState="false" />&nbsp;
                    </td>
                    <td>
                        <cms:CMSTextBox ID="txtEchangeRate" runat="server" MaxLength="10" EnableViewState="false"
                            CssClass="EditableTextTextBox" />
                        <asp:Image ID="imgHelp" runat="server" EnableViewState="false" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblRound" runat="server" EnableViewState="false" />&nbsp;
                    </td>
                    <td>
                        <cms:CMSTextBox ID="txtRound" runat="server" EnableViewState="false" CssClass="EditableTextTextBox"
                            MaxLength="1" Text="2" />
                        <asp:Image ID="imgRoundHelp" runat="server" EnableViewState="false" />
                    </td>
                </tr>
            </asp:PlaceHolder>
        </table>
        <asp:PlaceHolder runat="server" ID="plcObjectsSelection">
            <br />
            <table>
                <asp:PlaceHolder ID="plcRecalculateFromGlobal" runat="server">
                    <tr>
                        <td colspan="2">
                            <asp:CheckBox ID="chkRecalculateFromGlobal" runat="server" CssClass="ContentCheckBox"
                                Checked="true" EnableViewState="false" />
                        </td>
                    </tr>
                </asp:PlaceHolder>
                <tr>
                    <td colspan="2">
                        <asp:CheckBox ID="chkExchangeRates" runat="server" CssClass="ContentCheckBox" Checked="true"
                            EnableViewState="false" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:CheckBox ID="chkProductPrices" runat="server" CssClass="ContentCheckBox" Checked="true"
                            EnableViewState="false" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:CheckBox ID="chkFlatTaxes" runat="server" CssClass="ContentCheckBox" Checked="true"
                            EnableViewState="false" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:CheckBox ID="chkFlatDiscountsCoupons" runat="server" CssClass="ContentCheckBox"
                            Checked="true" EnableViewState="false" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:CheckBox ID="chkFlatVolumeDiscounts" runat="server" CssClass="ContentCheckBox"
                            Checked="true" EnableViewState="false" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:CheckBox ID="chkCredit" runat="server" CssClass="ContentCheckBox" Checked="true"
                            EnableViewState="false" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:CheckBox ID="chkOrders" runat="server" CssClass="ContentCheckBox" Checked="true"
                            EnableViewState="false" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:CheckBox ID="chkShipping" runat="server" CssClass="ContentCheckBox" Checked="true"
                            EnableViewState="false" />
                    </td>
                </tr>
                <asp:PlaceHolder ID="plcRecountDocuments" runat="server">
                    <tr>
                        <td colspan="2">
                            <asp:CheckBox ID="chkDocuments" runat="server" CssClass="ContentCheckBox" Checked="true"
                                EnableViewState="false" />
                        </td>
                    </tr>
                </asp:PlaceHolder>
                <tr>
                    <td colspan="2">
                        <asp:CheckBox ID="chkFreeShipping" runat="server" CssClass="ContentCheckBox" Checked="true"
                            EnableViewState="false" />
                    </td>
                </tr>
            </table>
        </asp:PlaceHolder>
    </div>
    <asp:Literal runat="server" ID="ltlScript" EnableViewState="false" />
</asp:Content>
<asp:Content ID="cntFooter" runat="server" ContentPlaceHolderID="plcFooter">
    <div class="FloatRight">
        <cms:CMSButton ID="btnOk" runat="server" CssClass="SubmitButton" OnClick="btnOk_Click"
            EnableViewState="false" /><cms:CMSButton ID="btnCancel" runat="server" CssClass="SubmitButton"
                OnClientClick="window.close(); return false;" EnableViewState="false" />
    </div>
</asp:Content>
