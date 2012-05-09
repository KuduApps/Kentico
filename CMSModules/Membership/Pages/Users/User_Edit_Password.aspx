<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Membership_Pages_Users_User_Edit_Password" Theme="Default"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="User Edit - Password" CodeFile="User_Edit_Password.aspx.cs" %>
<%@ Register Src="~/CMSModules/Membership/FormControls/Passwords/PasswordStrength.ascx" TagName="PasswordStrength"
    TagPrefix="cms" %>

<asp:Content ID="cntActions" runat="server" ContentPlaceHolderID="plcActions">
<table cellpadding="0" cellspacing="0">
            <tbody>
                <tr>
                    <td>
                        <asp:Image ID="imgGenPassword" runat="server" CssClass="NewItemImage" EnableViewState="false" />
                        <asp:LinkButton CssClass="NewItemLink" ID="btnGenerateNew" runat="server"
                        OnClick="btnGenerateNew_Click">LinkButton</asp:LinkButton>
                    </td>
                    <td style="width: 20px;" />
                    <td>
                    <asp:Image ID="imgSendPassword" runat="server" CssClass="NewItemImage" EnableViewState="false" />
                        <asp:LinkButton CssClass="NewItemLink" ID="btnSendPswd" runat="server"
                        OnClick="btnSendPswd_Click">LinkButton</asp:LinkButton>
                    </td>
                </tr>
            </tbody>
        </table>    
</asp:Content>

<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
  <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
        Visible="false" />
    <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
    <asp:PlaceHolder ID="plcTable" runat="server">
        <table>
            <tr>
                <td class="FieldLabel FieldLabelTop" style="width:150px;">
                    <asp:Label ID="LabelPassword" runat="server" Text="Label" />
                </td>
                <td>
                  <div style="width: 292px">
                    <cms:PasswordStrength runat="server" ID="passStrength" AllowEmpty="true" />
                  </div>
                </td>
            </tr>
            <tr>
                <td class="FieldLabel">
                    <asp:Label ID="LabelConfirmPassword" runat="server" Text="Label" /></td>
                <td>
                    <cms:CMSTextBox ID="TextBoxConfirmPassword" runat="server" TextMode="Password" CssClass="TextBoxField"
                        MaxLength="100"></cms:CMSTextBox ></td>
            </tr>
            <tr>
                <td colspan="2">
                    <br />
                    <asp:CheckBox ID="chkSendEmail" runat="server" Visible="true" CssClass="CheckBoxMovedLeft"
                        Checked="true" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <br />
                    <cms:CMSButton ID="ButtonSetPassword" runat="server" Text="" OnClick="ButtonSetPassword_Click"
                                    CssClass="LongSubmitButton" EnableViewState="false" />
               </td>
            </tr>
        </table>
    </asp:PlaceHolder>
</asp:Content>
