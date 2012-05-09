<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSPages_logon" Theme="Default"
    CodeFile="logon.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/PageElements/Help.ascx" TagName="Help" TagPrefix="cms" %>
<%@ Register Src="../CMSAdminControls/UI/System/RequireScript.ascx" TagName="RequireScript"
    TagPrefix="cms" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>CMS Login</title>
    <style type="text/css">
        body
        {
            margin: 0px;
            padding: 0px;
            height: 98% !important;
        }
        form
        {
            padding: 0px;
        }
        .TablePadding
        {
            padding-top: 15px;
        }
        .Red
        {
            color: Red;
        }
        .ForgottenLabel
        {
            width: 104px;
        }
        .LoginLabel
        {
            width: 84px;
        }
        .ForgottenTextBox
        {
            width: 240px;
        }
        .LoginTextBox
        {
            width: 260px;
        }
    </style>
    <base target="_self" />
</head>
<body class="<%=mBodyClass%> LogonPageBody">
    <form id="form1" runat="server">
    <cms:RequireScript ID="reqScript" runat="server" />
    <asp:Panel ID="pnlMainContainer" CssClass="OverBox" runat="server">
        <div class="loginMargin">
            <div class="loginContainer">
                <div class="loginBox">
                    <div class="LogonFormLogo">
                    </div>
                    <asp:Panel ID="pnlBody" runat="server" CssClass="LogonPageBackground">
                        <asp:Login ID="Login1" CssClass="LogonTable" runat="server" DestinationPageUrl="~/Default.aspx">
                            <LayoutTemplate>
                                <asp:Panel ID="pnlContainer" runat="server" DefaultButton="LoginButton">
                                    <div class="LogonData">
                                        <table cellpadding="1" cellspacing="0" style="width: 328px;" class="<%=tablePadding%>">
                                            <tr>
                                                <td colspan="2" class="LogonTitle">
                                                    <cms:LocalizedLabel ID="lblLogOn" runat="server" ResourceString="LogonForm.LogOn" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="<%= labelClass %>">
                                                    <cms:LocalizedLabel ID="lblUserName" runat="server" AssociatedControlID="UserName" />
                                                </td>
                                                <td class="<%= textboxClass %>">
                                                    <cms:CMSTextBox ID="UserName" runat="server" MaxLength="100" CssClass="LogonTextBox" />
                                                    <cms:CMSRequiredFieldValidator ID="rfvUserNameRequired" runat="server" ControlToValidate="UserName"
                                                        ValidationGroup="Login1">*</cms:CMSRequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="<%= labelClass %>">
                                                    <cms:LocalizedLabel ID="lblPassword" runat="server" AssociatedControlID="Password" />
                                                </td>
                                                <td class="<%= textboxClass %>">
                                                    <cms:CMSTextBox ID="Password" runat="server" TextMode="Password" MaxLength="100"
                                                        CssClass="LogonTextBox" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <cms:LocalizedCheckBox ID="chkRememberMe" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" class="RedLabels" align="center">
                                                    <cms:LocalizedLabel ID="FailureText" runat="server" EnableViewState="False" CssClass="Red" />
                                                    <cms:LocalizedLabel ID="lblForgottenResult" runat="server" EnableViewState="false"
                                                        Visible="false" />
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div class="LogonButtons">
                                        <table width="100%" cellpadding="1" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <cms:Help ID="ucHelp" runat="Server" />
                                                    <asp:ImageButton ID="lnkPassword" runat="server" Visible="false" />
                                                    <asp:ImageButton ID="lnkLanguage" runat="server" Visible="false" />
                                                </td>
                                                <td class="LanguageSelectorTd">
                                                    <div id="pnlLanguage" runat="server" class="LogonDialog" style="display: none;">
                                                        <cms:LocalizedLabel ID="lblCulture" EnableViewState="false" runat="server" />
                                                        <asp:DropDownList ID="drpCulture" runat="server" CssClass="LogonDropDownList" />
                                                    </div>
                                                </td>
                                                <td class="LogonButtonTd">
                                                    <cms:LocalizedButton ID="LoginButton" runat="server" CommandName="Login" ValidationGroup="Login1"
                                                        CssClass="LogonButton" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </asp:Panel>
                            </LayoutTemplate>
                        </asp:Login>
                    </asp:Panel>
                    <asp:Literal ID="ltlScript" EnableViewState="false" runat="server" />
                    <div class="loginLine">
                        <asp:Label ID="lblVersion" runat="server" />
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
    </form>
</body>
</html>
