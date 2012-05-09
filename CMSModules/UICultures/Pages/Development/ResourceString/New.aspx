<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_UICultures_Pages_Development_ResourceString_New"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" ValidateRequest="false"
    CodeFile="New.aspx.cs" %>

<asp:Content ContentPlaceHolderID="plcContent" ID="content" runat="server">
    <cms:LocalizedLabel ID="lblError" runat="server" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
    <table>
        <tr>
            <td>
                <cms:LocalizedLabel ID="lblKey" runat="server" CssClass="ContentLabel" EnableViewState="false"
                    ResourceString="Administration-UICulture_String_New.Key" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtKey" runat="server" CssClass="TextBoxField" /><br />
                <cms:CMSRequiredFieldValidator ID="rfvKey" runat="server" ControlToValidate="txtKey"
                    Display="Dynamic" EnableViewState="false"></cms:CMSRequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top;">
                <cms:LocalizedLabel ID="lblText" runat="server" CssClass="ContentLabel" EnableViewState="false"
                    ResourceString="Administration-UICulture_String_New.Text" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtText" runat="server" CssClass="TextAreaField" TextMode="multiline" />
            </td>
        </tr>
        <asp:PlaceHolder ID="plcDefaultText" runat="server" EnableViewState="false">
            <tr>
                <td style="vertical-align: top;">
                    <cms:LocalizedLabel ID="lblEnglishText" runat="server" CssClass="ContentLabel" EnableViewState="false" />
                </td>
                <td>
                    <cms:CMSTextBox ID="txtEnglishText" runat="server" CssClass="TextAreaField" TextMode="multiline" />
                </td>
            </tr>
        </asp:PlaceHolder>
        <tr>
            <td>
                <cms:LocalizedLabel ID="lblCustomString" runat="server" CssClass="ContentLabel" EnableViewState="false"
                    ResourceString="Administration-UICulture_String_New.CustomString" />
            </td>
            <td>
                <asp:CheckBox ID="chkCustomString" runat="server" CssClass="ContentCheckBox" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <cms:LocalizedButton ID="btnOk" runat="server" OnClick="btnOK_Click" CssClass="SubmitButton"
                    EnableViewState="false" ResourceString="general.ok" />
            </td>
        </tr>
    </table>
</asp:Content>
