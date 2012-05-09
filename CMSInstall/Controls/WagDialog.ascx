<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WagDialog.ascx.cs" Inherits="CMSInstall_Controls_WagDialog" %>
<asp:Label ID="lblError" runat="server" CssClass="ErrorLabel" EnableViewState="false"
    Visible="false" />
<style type="text/css">
.wwagLicense
{
	position: relative;
	top: -15px;
}
.IE8 .wwagLicense
{
	top: 0;
}
.IE9 .wwagLicense
{
	top: 10px;
}
.wwagLicense tr
{
	height: 30px;
}
.IE8 .wwagLicense tr, .IE9 .wwagLicense tr
{
	height: 38px;
}
.wwagLicense .InstallFormTextBox {
    margin-left: 2px;
}
</style>
<table class="InstallWizard wwagLicense" border="0" cellpadding="0" cellspacing="10">
    <tr>
        <td>
            <cms:LocalizedLabel ID="lblFreeKeyForDomain" runat="server" ResourceString="Install.wag.lblFreeKeyForDomain"
                EnableViewState="false" />
        </td>
    </tr>
    <tr>
        <td>
            <table border="0" cellpadding="0" cellspacing="5">
                <tr>
                    <td nowrap="nowrap" align="right" style="width:80px;">
                        <cms:LocalizedLabel ID="lblUserDomain" runat="server" ResourceString="Install.wag.lblUserDomain"
                            EnableViewState="false" DisplayColon="true" />
                    </td>
                    <td>
                        <asp:TextBox ID="txtUserDomain" CssClass="InstallFormTextBox" runat="server" /><br />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td nowrap="nowrap" align="right">
                    </td>
                    <td>
                        <cms:LocalizedLabel ID="lblDomainFormat" runat="server" ResourceString="Install.wag.lblDomainFormat"
                            EnableViewState="false" />
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <cms:LocalizedLabel ID="lblOptionalForm" runat="server" ResourceString="Install.wag.lblOptionalForm"
                EnableViewState="false" DisplayColon="true" />
        </td>
    </tr>
    <tr>
        <td>
            <table border="0" cellpadding="0" cellspacing="5">
                <tr>
                    <td nowrap="nowrap" align="right" style="width:80px;">
                        <cms:LocalizedLabel ID="lblUserFirstName" runat="server" ResourceString="Install.wag.lblFirstName"
                            EnableViewState="false" DisplayColon="true" />
                    </td>
                    <td>
                        <asp:TextBox ID="txtUserFirstName" CssClass="InstallFormTextBox" runat="server" />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td nowrap="nowrap" align="right">
                        <cms:LocalizedLabel ID="lblUserLastName" runat="server" ResourceString="Install.wag.lblLastName"
                            EnableViewState="false" DisplayColon="true" />
                    </td>
                    <td>
                        <asp:TextBox ID="txtUserLastName" CssClass="InstallFormTextBox" runat="server" />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td nowrap="nowrap" align="right">
                        <cms:LocalizedLabel ID="lblUserEmail" runat="server" ResourceString="Install.wag.lblUserEmail"
                            EnableViewState="false" DisplayColon="true" />
                    </td>
                    <td>
                        <asp:TextBox ID="txtUserEmail" CssClass="InstallFormTextBox" runat="server" />
                    </td>
                    <td>
                    </td>
                </tr>
                <asp:PlaceHolder ID="plcPass" runat="server" Visible="false">
                    <tr>
                    <td nowrap="nowrap" align="right">
                            <cms:LocalizedLabel ID="LocalizedLabel1" runat="server" ResourceString="Install.wag.lblPassword"
                                EnableViewState="false" DisplayColon="true" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtPassword" CssClass="InstallFormTextBox" runat="server" TextMode="Password" />
                        </td>
                        <td>
                        </td>
                    </tr>
                </asp:PlaceHolder>
            </table>
        </td>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="2">
            &nbsp;
        </td>
    </tr>
</table>
<asp:Image ID="imgTrack" runat="server" EnableViewState="false" />
