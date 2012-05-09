<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Avatars_Avatar_Edit"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Theme="default" Title="Avatars - Edit" CodeFile="Avatar_Edit.aspx.cs" %>

<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextBox" TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
        Visible="false" />
    <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
    <cms:LocalizedLabel runat="server" ID="lblSharedInfo" Visible="false" EnableViewState="false"  />
    <table>
        <tr>
            <td>
                <cms:LocalizedLabel runat="server" DisplayColon="true" ID="lblAvatarName" ResourceString="avat.avatarname" EnableViewState="false" />
            </td>
            <td>
                <cms:LocalizableTextBox runat="server" ID="txtAvatarName" CssClass="TextBoxField" MaxLength="200" />
                <cms:CMSRequiredFieldValidator runat="server" ID="valAvatarName" ControlToValidate="txtAvatarName:textbox" EnableViewState="false" /></td>
        </tr>
        <tr>
            <td>
                <cms:LocalizedLabel DisplayColon="true" runat="server" ID="lblAvatarType" ResourceString="avat.avatartype" EnableViewState="false" /></td>
            <td>
                <asp:DropDownList runat="server" ID="drpAvatarType" CssClass="DropDownField" AutoPostBack="true" /></td>
        </tr>
        <asp:PlaceHolder runat="server" ID="plcImage" Visible="false">
            <tr>
                <td>
                    <cms:LocalizedLabel DisplayColon="true" runat="server" ID="lblImage" ResourceString="avat.image" EnableViewState="false" /></td>
                <td>
                    <asp:Image runat="server" ID="imgAvatar" EnableViewState="false" />
                </td>
            </tr>
        </asp:PlaceHolder>
        <tr>
            <td>
                <cms:LocalizedLabel runat="server" ID="lblUpload" ResourceString="uploader.upload" EnableViewState="false" />
            </td>
            <td>
                <cms:CMSFileUpload runat="server" ID="uploadAvatar" />
            </td>
        </tr>
        <asp:PlaceHolder ID="plcDefaultUserAvatar" runat="server" EnableViewState="false"
            Visible="false">
            <tr>
                <td>
                    <cms:LocalizedLabel runat="server" ID="lblDefaultUserAvatar" DisplayColon="true" ResourceString="avat.DefaultUserAvatar" EnableViewState="false" /></td>
                <td>
                    <asp:CheckBox runat="server" ID="chkDefaultUserAvatar" CssClass="ContentCheckBox" />
                </td>
            </tr>
            <tr>
                <td>
                    <cms:LocalizedLabel runat="server" ID="lblDefaultMaleUserAvatar" DisplayColon="true" ResourceString="avat.DefaultMaleUserAvatar" EnableViewState="false" /></td>
                <td>
                    <asp:CheckBox runat="server" ID="chkDefaultMaleUserAvatar" CssClass="ContentCheckBox" />
                </td>
            </tr>
            <tr>
                <td>
                    <cms:LocalizedLabel runat="server" ID="lblDefaultFemaleUserAvatar" DisplayColon="true" ResourceString="avat.DefaultFemaleUserAvatar" EnableViewState="false" /></td>
                <td>
                    <asp:CheckBox runat="server" ID="chkDefaultFemaleUserAvatar" CssClass="ContentCheckBox" />
                </td>
            </tr>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="plcDefaultGroupAvatar" runat="server" EnableViewState="false"
            Visible="false">
            <tr>
                <td>
                    <cms:LocalizedLabel runat="server" ID="lblDefaultGroupAvatar" DisplayColon="true" ResourceString="avat.DefaultGroupAvatar" EnableViewState="false" /></td>
                <td>
                    <asp:CheckBox runat="server" ID="chkDefaultGroupAvatar" CssClass="ContentCheckBox" />
                </td>
            </tr>
        </asp:PlaceHolder>
        <tr>
            <td>
            </td>
            <td>
                <cms:CMSButton runat="server" ID="btnOk" Style="text-align: center;" CssClass="SubmitButton"
                    OnClick="btnOK_Click" EnableViewState="false" />
            </td>
        </tr>
    </table>
</asp:Content>
