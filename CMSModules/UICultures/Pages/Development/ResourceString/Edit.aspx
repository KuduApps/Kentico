<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_UICultures_Pages_Development_ResourceString_Edit"
    ValidateRequest="false" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master"
    Theme="Default" CodeFile="Edit.aspx.cs" %>

<asp:Content ContentPlaceHolderID="plcContent" ID="content" runat="server">
    <cms:LocalizedLabel ID="lblInfo" runat="server" CssClass="InfoLabel" Visible="False"
        ResourceString="General.ChangesSaved" />
    <cms:LocalizedLabel ID="lblError" runat="server" CssClass="ErrorLabel" Visible="false" />
    <table>
        <tr>
            <td>
                <cms:LocalizedLabel ID="lblKey" runat="server" CssClass="ContentLabel" EnableViewState="false"
                    ResourceString="Administration-UICulture_String_New.Key" />
            </td>
            <td>
                <cms:LocalizedLabel ID="lblKeyEng" runat="server" CssClass="ContentLabel" EnableViewState="false" />
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
                <td>
                    <cms:LocalizedLabel ID="lblEnglishText" runat="server" CssClass="ContentLabel" EnableViewState="false" />
                </td>
                <td>
                    <cms:LocalizedLabel ID="lblEnglishValue" runat="server" CssClass="ContentLabel" EnableViewState="false" />
                </td>
            </tr>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="plcCustom" runat="server" EnableViewState="false">
            <tr>
                <td>
                    <cms:LocalizedLabel ID="lblCustomString" runat="server" CssClass="ContentLabel" EnableViewState="false"
                        ResourceString="Administration-UICulture_String_New.CustomString" />
                </td>
                <td>
                    <asp:CheckBox ID="chkCustomString" runat="server" CssClass="ContentCheckBox" />
                </td>
            </tr>
        </asp:PlaceHolder>
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
