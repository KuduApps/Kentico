<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSWebParts_Membership_Registration_CustomRegistrationForm" CodeFile="~/CMSWebParts/Membership/Registration/CustomRegistrationForm.ascx.cs" %>
<%@ Register Src="~/CMSFormControls/Inputs/SecurityCode.ascx" TagName="SecurityCode" TagPrefix="uc1" %>
<asp:Label ID="lblError" runat="server" EnableViewState="false" Visible="false" />
<asp:Label ID="lblInfo" runat="server" EnableViewState="false" Visible="false" />
<asp:Panel ID="pnlRegForm" runat="server" DefaultButton="btnRegister">
    <cms:DataForm ID="formUser" runat="server" IsLiveSite="true" />
    <asp:PlaceHolder runat="server" ID="plcCaptcha">
        <table>
            <tr>
                <td>
                    <cms:LocalizedLabel runat="server" ID="lblCaptcha" ResourceString="webparts_membership_registrationform.captcha" /></td>
                <td>
                    <uc1:SecurityCode ID="captchaElem" runat="server" />
                </td>
            </tr>
        </table>
    </asp:PlaceHolder>
    <cms:CMSButton ID="btnRegister" runat="server" CssClass="RegisterButton" />
</asp:Panel>
