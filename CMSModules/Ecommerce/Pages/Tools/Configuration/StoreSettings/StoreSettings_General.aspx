<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Ecommerce_Pages_Tools_Configuration_StoreSettings_StoreSettings_General"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" CodeFile="StoreSettings_General.aspx.cs" %>

<%@ Register TagPrefix="cms" TagName="SettingsGroupViewer" Src="~/CMSModules/Settings/Controls/SettingsGroupViewer.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="plcContent" runat="Server">
    <asp:Label ID="lblInfo" runat="server" CssClass="InfoLabel" EnableViewState="false"
        Visible="false" />
    <div class="WebPartForm">
        <table class="EditingFormCategoryTableHeader" cellpadding="0" cellspacing="0">
            <tbody>
                <tr class="EditingFormCategoryRow">
                    <td class="EditingFormLeftBorder">
                        &nbsp;
                    </td>
                    <td colspan="2" class="EditingFormCategory" id="Accounts">
                        <cms:LocalizedLabel ID="lblCurrency" runat="server" EnableViewState="false" ResourceString="com.storesettings.currencies"
                            DisplayColon="false" />
                    </td>
                    <td class="EditingFormRightBorder">
                        &nbsp;
                    </td>
                </tr>
            </tbody>
        </table>
        <div>
            <table class="EditingFormCategoryTableContent" border="0" cellpadding="0" cellspacing="0">
                <tbody>
                    <tr class="EditingFormRow">
                        <td class="EditingFormLeftBorder">
                            &nbsp;
                        </td>
                        <td class="EditingFormLabelCell" style="width: 250px;">
                            <cms:LocalizedLabel ID="lblCurrentMainCurrency" runat="server" EnableViewState="false"
                                ResourceString="Configuration_StoreSettings.lblMainCurrency" DisplayColon="false" />
                        </td>
                        <td style="width: 25px;">
                        </td>
                        <td class="EditingFormValueCell" style="width: 400px;">
                            <asp:Label ID="lblMainCurrency" runat="server" EnableViewState="false" />&nbsp;
                        </td>
                        <td>
                            <cms:LocalizedButton ID="btnChangeCurrency" runat="server" EnableViewState="false"
                                CssClass="ContentButton" ResourceString="general.change" />
                            <asp:Label ID="lblHdnChangeCurrency" runat="server" CssClass="" AssociatedControlID="btnChangeCurrency"></asp:Label>
                        </td>
                        <td>
                        </td>
                        <td class="EditingFormRightBorder">
                            &nbsp;
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <table cellpadding="0" cellspacing="0">
            <tbody>
                <tr class="EditingFormFooterRow">
                    <td class="EditingFormLeftBorder">
                        &nbsp;
                    </td>
                    <td class="EditingFormLabelCell">
                        &nbsp;
                    </td>
                    <td class="EditingFormValueCell">
                        &nbsp;
                    </td>
                    <td class="EditingFormRightBorder">
                        &nbsp;
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <cms:SettingsGroupViewer ID="SettingsGroupViewer" runat="server" />
</asp:Content>
