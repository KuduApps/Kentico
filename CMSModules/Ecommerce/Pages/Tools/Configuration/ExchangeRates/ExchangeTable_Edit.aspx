<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Ecommerce_Pages_Tools_Configuration_ExchangeRates_ExchangeTable_Edit"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Exchange table - properties"
    CodeFile="ExchangeTable_Edit.aspx.cs" %>

<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextBox" TagPrefix="cms" %>

<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
        Visible="false" />
    <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
    <table style="vertical-align: top">
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblExchangeTableDisplayName" EnableViewState="false" />
            </td>
            <td>
                <cms:LocalizableTextBox ID="txtExchangeTableDisplayName" runat="server" CssClass="TextBoxField"
                    MaxLength="200" EnableViewState="false" />
                <cms:CMSRequiredFieldValidator ID="rfvDisplayName" runat="server" Display="Dynamic"
                    ValidationGroup="Exchange" ControlToValidate="txtExchangeTableDisplayName:textbox" EnableViewState="false" />
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblExchangeTableValidFrom" EnableViewState="false" />
            </td>
            <td>
                <cms:DateTimePicker ID="dtPickerExchangeTableValidFrom" runat="server" EnableViewState="false" />
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblExchangeTableValidTo" EnableViewState="false" />
            </td>
            <td>
                <cms:DateTimePicker ID="dtPickerExchangeTableValidTo" runat="server" EnableViewState="false" />
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <asp:PlaceHolder ID="plcGrid" runat="server">
            <tr>
                <td colspan="3">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="FieldLabel" style="vertical-align: top; padding-top: 10px; padding-bottom: 10px;">
                    <asp:Label runat="server" ID="lblRates" EnableViewState="false" Font-Bold="true" />
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <asp:PlaceHolder ID="plcNoMainCurrency" runat="server" Visible="false">
                <tr>
                    <td class="FieldLabel" colspan="3" style="vertical-align: top; padding-top: 10px;
                        padding-bottom: 10px;">
                        <asp:Label runat="server" ID="lblNoMainCurrency" EnableViewState="false" CssClass="ErrorLabel" />
                    </td>
                </tr>
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="plcRates" runat="server">
                <asp:PlaceHolder ID="plcRateFromGlobal" runat="server">
                    <tr>
                        <td colspan="3" style="padding-bottom: 6px;">
                            <asp:Label ID="lblFromGlobalToMain" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <table class="UniGridGrid" style="border-collapse: collapse;" border="1" cellspacing="0"
                                rules="rows">
                                <tbody>
                                    <tr class="UniGridHead">
                                        <th scope="col">
                                            <asp:Label ID="lblToCurrency" runat="server" />
                                        </th>
                                        <th scope="col">
                                            <asp:Label ID="lblRateValue" runat="server" />
                                        </th>
                                    </tr>
                                    <tr class="EvenRow">
                                        <td>
                                            <asp:Label ID="lblSiteMainCurrency" runat="server" />
                                        </td>
                                        <td>
                                            <cms:CMSTextBox ID="txtGlobalExchangeRate" runat="server" CssClass="ShortTextBox" MaxLength="10"
                                                EnableViewState="false" />
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                        <td style="vertical-align: top;">
                            <asp:Image ID="imgHelpFromGlobal" runat="server" EnableViewState="false" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="padding: 5px;">
                            &nbsp;
                        </td>
                    </tr>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="plcNoCurrency" runat="server" Visible="false">
                    <tr>
                        <td colspan="3" style="padding-bottom: 6px;">
                            <cms:LocalizedLabel runat="server" ID="lblNoCurrencies" EnableViewState="false" ResourceString="com.currencies.nosuitablefound" />
                        </td>
                    </tr>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="plcSiteRates" runat="server">
                    <tr>
                        <td colspan="3" style="padding-bottom: 6px;">
                            <asp:Label ID="lblMainToSite" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:GridView ID="editGrid" runat="server" AutoGenerateColumns="false" CssClass="UniGridGrid"
                                GridLines="Horizontal" EnableViewState="false">
                                <HeaderStyle CssClass="UniGridHead" />
                                <AlternatingRowStyle CssClass="OddRow" />
                                <RowStyle CssClass="EvenRow" />
                                <Columns>
                                    <asp:BoundField DataField="CurrencyCode">
                                    </asp:BoundField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                        <td style="vertical-align: top;">
                            <asp:Image ID="imgHelp" runat="server" EnableViewState="false" />
                        </td>
                    </tr>
                </asp:PlaceHolder>
            </asp:PlaceHolder>
        </asp:PlaceHolder>
        <tr>
            <td>
            </td>
            <td>
                <cms:CMSButton runat="server" ID="btnOk" OnClick="btnOK_Click" EnableViewState="false"
                    CssClass="SubmitButton" ValidationGroup="Exchange" />
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
