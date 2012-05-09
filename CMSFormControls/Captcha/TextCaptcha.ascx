<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TextCaptcha.ascx.cs" Inherits="CMSFormControls_Captcha_TextCaptcha" %>
<div>
    <cms:LocalizedLabel ID="lblSecurityCode" runat="server" EnableViewState="false" Visible="false" />
</div>
<table class="CaptchaTable">
    <tr>
        <td>
            &nbsp;<asp:Image ID="imgSecurityCode" AlternateText="Security code" runat="server"
                EnableViewState="false" />
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Panel ID="pnlAnswer" runat="server" EnableViewState="false">
            </asp:Panel>
        </td>
        <asp:PlaceHolder runat="server" ID="plcAfterText" Visible="false">
            <td class="CaptchaAfterText">
                <asp:Label ID="lblAfterText" runat="server" EnableViewState="false" />
            </td>
        </asp:PlaceHolder>
    </tr>
</table>
