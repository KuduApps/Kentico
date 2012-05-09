<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSSiteManager_SplashScreen"
    Title="Welcome to Kentico CMS" Theme="Default" CodeFile="SplashScreen.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server" enableviewstate="false">
    <title>Welcome to Kentico CMS</title>
    <style>
        </style>
</head>
<body class="SplashScreenBody <%=mBodyClass%>">
    <form id="Form1" runat="server">
    <asp:Panel CssClass="SplashScreen" ID="pnlMain" runat="server">
        <table cellpadding="0" cellspacing="0" class="SplashScreenTable">
            <tr>
                <td style="padding: 0px 35px;">
                    <table>
                        <tr>
                            <td style="padding-top: 15px">
                                <asp:Image ID="imgTitle" runat="server" />
                            </td>
                            <td class="SplashScreenTitle">
                                <asp:Literal ID="ltlTitle" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2" class="SplashScreenSubTitle">
                    <asp:Literal ID="lnkKenticoCom" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table width="90%" style="margin: 18px 35px 25px 35px;">
                        <tr>
                            <td colspan="2">
                                <cms:LocalizedLabel ID="lblGettingStarted" runat="server" ResourceString="splashscreen.gettingstarted"
                                    CssClass="Title"></cms:LocalizedLabel>
                            </td>
                        </tr>
                        <tr>
                            <td class="SplashScreenImage">
                                <asp:Image ID="imgGettingStartedQuick" runat="server" />
                            </td>
                            <td style="width: 100%;">
                                <asp:Literal ID="ltlGettingStartedQuick" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="SupportCell SplashScreenImage">
                                <asp:Image ID="imgGettingStartedTutorial" runat="server" />
                            </td>
                            <td>
                                <asp:Literal ID="ltlGettingStartedTutorial" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="padding-bottom: 5px;">
                                <cms:LocalizedLabel ID="lblLicensing" runat="server" ResourceString="splashscreen.licensing.title"
                                    CssClass="Title"></cms:LocalizedLabel>
                            </td>
                        </tr>
                        <tr>
                            <td class="SplashScreenImage">
                                <asp:Image ID="imgLicensing" runat="server" />
                            </td>
                            <td>
                                <asp:Literal ID="ltlLicensingLn1" runat="server" />
                                <div>
                                    <asp:Literal ID="ltlLicensingLn2" runat="server" /><br />
                                    <asp:Literal ID="ltlLicensingLn3" runat="server" />
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="padding-bottom: 5px;">
                                <cms:LocalizedLabel ID="lblSupportAndHelp" runat="server" ResourceString="splashscreen.supportandhelp.title"
                                    CssClass="Title"></cms:LocalizedLabel>
                            </td>
                        </tr>
                        <tr>
                            <td class="SplashScreenImage">
                                <asp:Image ID="imgDocumentation" runat="server" />
                            </td>
                            <td>
                                <asp:Literal ID="ltlDocumentation" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="SupportCell SplashScreenImage">
                                <asp:Image ID="imgOnlineHelp" runat="server" />
                            </td>
                            <td>
                                <asp:Literal ID="ltlOnlineHelp" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="SupportCell SplashScreenImage">
                                <asp:Image ID="imgSupport" runat="server" />
                            </td>
                            <td>
                                <asp:Literal ID="ltlSupport" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="SupportCell SplashScreenImage">
                                <asp:Image ID="imgDevNet" runat="server" />
                            </td>
                            <td>
                                <asp:Literal ID="ltlDevNet" runat="server" />
                            </td>
                        </tr>
                        <asp:PlaceHolder runat="server" ID="plcApiExample" Visible="false">
                            <tr>
                                <td class="SupportCell SplashScreenImage">
                                    <asp:Image ID="imgApiExamples" runat="server" />
                                </td>
                                <td>
                                    <asp:Literal ID="ltlApiExamples" runat="server" />
                                </td>
                            </tr>
                        </asp:PlaceHolder>
                    </table>
                </td>
            </tr>
        </table>
        <div class="SplashScreenTableBottom">
            <table>
                <tr>
                    <td class="SplashScreenBottomCheck">
                        <asp:CheckBox ID="chkDontShowAgain" runat="server" />
                    </td>
                    <td>
                        <cms:CMSButton ID="btnContinue" runat="server" CssClass="LogonButton" OnClick="btnContinue_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
    </form>
</body>
</html>
