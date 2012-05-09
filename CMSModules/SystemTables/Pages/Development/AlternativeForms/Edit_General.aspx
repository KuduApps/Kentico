<%@ Page Language="C#" AutoEventWireup="true"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Alternative forms - General properties"
    Inherits="CMSModules_SystemTables_Pages_Development_AlternativeForms_Edit_General"
    Theme="Default" CodeFile="Edit_General.aspx.cs" %>

<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextBox" TagPrefix="cms" %>

<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:Label ID="lblInfo" runat="server" CssClass="InfoLabel" Visible="false" EnableViewState="false" />
    <asp:Label ID="lblError" runat="server" CssClass="ErrorLabel" Visible="false" EnableViewState="false" />
    <asp:Panel ID="pnlGeneral" runat="server">
        <table>
            <tr>
                <td class="FieldLabel">
                    <cms:LocalizedLabel ID="lblDisplayName" runat="server" EnableViewState="false" DisplayColon="true"
                        ResourceString="general.displayname" />
                </td>
                <td>
                    <cms:LocalizableTextBox ID="txtDisplayName" runat="server" CssClass="TextBoxField" MaxLength="100" />
                    <cms:CMSRequiredFieldValidator ID="rfvDisplayName" runat="server" ErrorMessage="" ControlToValidate="txtDisplayName:textbox"
                        EnableViewState="false" />
                </td>
            </tr>
            <tr>
                <td class="FieldLabel">
                    <cms:LocalizedLabel ID="lblCodeName" runat="server" EnableViewState="false" DisplayColon="true"
                        ResourceString="general.codename" />
                </td>
                <td>
                    <cms:CMSTextBox ID="txtCodeName" runat="server" CssClass="TextBoxField" MaxLength="100" />
                    <cms:CMSRequiredFieldValidator ID="rfvCodeName" runat="server" ErrorMessage="" ControlToValidate="txtCodeName"
                        EnableViewState="false" />
                </td>
            </tr>
            <asp:Panel ID="pnlCombineUserSettings" runat="server" Visible="false">
                <tr>
                    <td>
                        <cms:LocalizedLabel ID="lblCombineUserSettings" runat="server" EnableViewState="false"
                            DisplayColon="true" ResourceString="altform.combineusersettings"></cms:LocalizedLabel>
                    </td>
                    <td>
                        <asp:CheckBox ID="chkCombineUserSettings" runat="server" Enabled="false" />
                    </td>
                </tr>
            </asp:Panel>
            <tr>
                <td>
                </td>
                <td>
                    <cms:CMSButton ID="btnOk" runat="server" CssClass="SubmitButton" />
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
