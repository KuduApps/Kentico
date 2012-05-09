<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Inherits="CMSModules_BadWords_BadWords_Edit_General"
    Theme="Default" CodeFile="BadWords_Edit_General.aspx.cs" %>

<%@ Register Assembly="CMS.ExtendedControls" Namespace="CMS.ExtendedControls" TagPrefix="cc1" %>
<%@ Register Src="~/CMSModules/BadWords/FormControls/SelectBadWordAction.ascx" TagPrefix="cms"
    TagName="SelectBadWordAction" %>
<asp:Content ContentPlaceHolderID="plcContent" ID="content" runat="server">
    <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
        Visible="false" />
    <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
    <table style="vertical-align: top;">
        <col width="200" />
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel runat="server" ID="lblWordExpression" EnableViewState="false"
                    DisplayColon="true" ResourceString="BadWords_Edit.WordExpressionLabel" />
            </td>
            <td colspan="2">
                <cms:CMSTextBox ID="txtWordExpression" runat="server" CssClass="TextBoxField" MaxLength="200" />
                <cms:CMSRequiredFieldValidator ID="rqfWordExpression" runat="server" Display="static"
                    ControlToValidate="txtWordExpression" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel ID="lblIsRegular" runat="server" DisplayColon="true" EnableViewState="false"
                    ResourceString="badwords_edit.wordisregularexpressionlabel" />
            </td>
            <td colspan="2">
                <asp:CheckBox ID="chkIsRegular" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel ID="lblMatchWholeWord" runat="server" DisplayColon="true" EnableViewState="false"
                    ResourceString="badwords_edit.matchwholewordlabel" />
            </td>
            <td colspan="2">
                <asp:CheckBox ID="chkMatchWholeWord" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel runat="server" ID="lblWordAction" EnableViewState="false" DisplayColon="true"
                    ResourceString="BadWords_Edit.WordActionLabel" />
            </td>
            <td>
                <cms:SelectBadWordAction ID="SelectBadWordActionControl" runat="server" AllowAutoPostBack="true"
                    ReloadDataOnPostback="false" />
            </td>
            <td>
                <cms:LocalizedCheckBox ID="chkInheritAction" runat="server" ResourceString="BadWords_Edit.ActionInherit"
                    AutoPostBack="true" Checked="true" />
            </td>
        </tr>
        <asp:PlaceHolder ID="plcReplacement" runat="server">
            <tr>
                <td class="FieldLabel">
                    <cms:LocalizedLabel runat="server" ID="lblWordReplacement" EnableViewState="false"
                        ResourceString="BadWords_Edit.WordReplacementLabel" DisplayColon="true" />
                </td>
                <td>
                    <cms:CMSTextBox ID="txtWordReplacement" runat="server" CssClass="TextBoxField" MaxLength="200" />
                </td>
                <td>
                    <cms:LocalizedCheckBox ID="chkInheritReplacement" runat="server" ResourceString="BadWords_Edit.ActionInherit"
                        AutoPostBack="true" Checked="true" />
                </td>
            </tr>
        </asp:PlaceHolder>
        <tr>
            <td>
            </td>
            <td colspan="2">
                <cms:LocalizedButton runat="server" ID="btnOk" OnClick="btnOK_Click" EnableViewState="false"
                    CssClass="SubmitButton" ResourceString="General.OK" />
            </td>
        </tr>
    </table>
</asp:Content>
