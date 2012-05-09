<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSWebParts_Membership_Logon_LogonForm"
    CodeFile="~/CMSWebParts/Membership/Logon/LogonForm.ascx.cs" %>
<asp:Panel ID="pnlBody" runat="server" CssClass="LogonPageBackground">
    <table class="DialogPosition">
        <tr style="vertical-align: middle;">
            <td>
                <asp:Login ID="Login1" runat="server" DestinationPageUrl="~/Default.aspx">
                    <LayoutTemplate>
                        <asp:Panel runat="server" ID="pnlLogin" DefaultButton="LoginButton">
                            <table style="border: none;">
                                <tr>
                                    <td class="TopLeftCorner">
                                    </td>
                                    <td class="TopMiddleBorder">
                                    </td>
                                    <td class="TopRightCorner">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" class="LogonDialog">
                                        <table style="border: none; width: 100%; border-collapse: separate;">
                                            <tr>
                                                <td nowrap="nowrap">
                                                    <cms:LocalizedLabel ID="lblUserName" runat="server" AssociatedControlID="UserName"
                                                        EnableViewState="false" />
                                                </td>
                                                <td nowrap="nowrap">
                                                    <cms:CMSTextBox ID="UserName" runat="server" MaxLength="100" CssClass="LogonTextBox" />
                                                    <cms:CMSRequiredFieldValidator ID="rfvUserNameRequired" runat="server" ControlToValidate="UserName"
                                                         EnableViewState="false">*</cms:CMSRequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td nowrap="nowrap">
                                                    <cms:LocalizedLabel ID="lblPassword" runat="server" AssociatedControlID="Password"
                                                        EnableViewState="false" />
                                                </td>
                                                <td>
                                                    <cms:CMSTextBox ID="Password" runat="server" TextMode="Password" MaxLength="110" CssClass="LogonTextBox" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td style="text-align: left;" nowrap="nowrap">
                                                    <cms:LocalizedCheckBox ID="chkRememberMe" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" style="color: red">
                                                    <cms:LocalizedLiteral ID="FailureText" runat="server" EnableViewState="False" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td style="text-align: left;">
                                                    <cms:LocalizedButton ID="LoginButton" runat="server" CommandName="Login" EnableViewState="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </LayoutTemplate>
                </asp:Login>
            </td>
        </tr>
        <tr>
            <td>
                <asp:LinkButton ID="lnkPasswdRetrieval" runat="server" EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="pnlPasswdRetrieval" runat="server" CssClass="LoginPanelPasswordRetrieval"
                    DefaultButton="btnPasswdRetrieval">
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="lblPasswdRetrieval" runat="server" EnableViewState="false" AssociatedControlID="txtPasswordRetrieval" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <cms:CMSTextBox ID="txtPasswordRetrieval" runat="server" />
                                <cms:CMSButton ID="btnPasswdRetrieval" runat="server" EnableViewState="false" /><br />
                                <cms:CMSRequiredFieldValidator ID="rqValue" runat="server" ControlToValidate="txtPasswordRetrieval"
                                    EnableViewState="false" />
                            </td>
                        </tr>
                    </table>
                    <asp:Label ID="lblResult" runat="server" Visible="false" EnableViewState="false" />
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />
<asp:HiddenField runat="server" ID="hdnPasswDisplayed" />
