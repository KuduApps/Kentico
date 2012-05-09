<%@ Page Language="C#" Inherits="CMSInstall_install" Theme="Default" EnableEventValidation="false"
    ValidateRequest="false" CodeFile="install.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/PageElements/Help.ascx" TagName="Help" TagPrefix="cms" %>
<%@ Register Src="~/CMSInstall/Controls/LicenseDialog.ascx" TagName="LicenseDialog"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSInstall/Controls/SiteCreationDialog.ascx" TagName="SiteCreationDialog"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/System/RequireScript.ascx" TagName="RequireScript"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/AsyncControl.ascx" TagName="AsyncControl" TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/System/ActivityBar.ascx" TagName="ActivityBar"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSInstall/Controls/WagDialog.ascx" TagName="WagDialog" TagPrefix="cms" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>
        <%= ResHelper.GetFileString("General.ProductName") %>
        Database Setup </title>
    <style type="text/css">
        body
        {
            margin: 0px;
            padding: 0px;
            height: 100%;
        }
        .ButtonsPanel
        {
            padding-top: 10px;
        }
    </style>

    <script type="text/javascript">
        //<![CDATA[      
        var installTimerId = 0;

        // Start timer function
        function StartInstallStateTimer(type) {
            var act = document.getElementById('activity');
            if (act != null) {
                act.style.display = 'inline';
            }
            installTimerId = setInterval("GetInstallState('false;" + type + "')", 500);
        }

        // End timer function
        function StopInstallStateTimer() {
            if (installTimerId) {
                clearInterval(installTimerId);
                installTimerId = 0;

                if (window.HideActivity) {
                    window.HideActivity();
                }

                var act = document.getElementById('activity');
                if (act != null) {
                    act.style.display = 'none';
                }
            }
        }

        // Cancel install
        function CancelImport(type) {
            GetInstallState('true;' + type);
            return false;
        }
        //]]>
    </script>

</head>
<body class="InstallBody <%=BodyClass%>">
    <form id="Form1" method="post" runat="server">
    <asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />
    <cms:RequireScript ID="rqScript" runat="server" UseFileStrings="true" />
    <asp:Panel runat="server" ID="pnlBody" CssClass="InstallerBody">
        <cms:LayoutPanel ID="layPanel" runat="server" LayoutCssClass="InstallPanel">
            <asp:Label ID="lblHeader" CssClass="InstallHeader" runat="server" />
            <asp:Image ID="imgHeader" runat="server" CssClass="InstalHeader" />
            <asp:Panel runat="server" ID="pnlWizard" CssClass="InstallerContent">
                <asp:Button ID="btnHiddenNext" runat="server" CssClass="HiddenButton" OnClick="btnHiddenNext_onClick" />
                <asp:Button ID="btnHiddenBack" runat="server" CssClass="HiddenButton" OnClick="btnHiddenBack_onClick" />
                <asp:Wizard ID="wzdInstaller" runat="server" DisplaySideBar="False" OnPreviousButtonClick="wzdInstaller_PreviousButtonClick"
                    ActiveStepIndex="1" Width="100%">
                    <StepNavigationTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td style="text-align: right;">
                                    <div class="InstallFooter">
                                        <div id="buttonsDiv" class="ButtonsDiv">
                                            <cms:LocalizedButton ID="StepPrevButton" Source="file" CssClass="StepButton" runat="server"
                                                CommandName="MovePrevious" OnClientClick="PrevStep(this,document.getElementById('buttonsDiv')); return false;"
                                                Text="{$Install.BackStep$}" Width="100" RenderScript="true" />
                                            <cms:LocalizedButton UseSubmitBehavior="True" Source="file" CssClass="StepButton"
                                                ID="StepNextButton" runat="server" CommandName="MoveNext" Width="100" Text="{$Install.NextStep$}"
                                                OnClientClick="NextStep(this,document.getElementById('buttonsDiv')); return false;"
                                                RenderScript="true" />
                                        </div>
                                        <span style="padding: 0px 10px 0px 0px; float: left;">
                                            <cms:Help ID="hlpContext" runat="server" />
                                        </span><span id="activity" style="display: none; float: left; padding: 9px 30px 0px 0px;">
                                            <cms:ActivityBar runat="server" ID="barActivity" Visible="true" />
                                        </span>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </StepNavigationTemplate>
                    <StartNavigationTemplate>
                        <div class="InstallFooter">
                            <div id="buttonsDiv" class="ButtonsDiv">
                                <cms:LocalizedButton UseSubmitBehavior="True" Source="file" CssClass="StepButton"
                                    ID="StepNextButton" runat="server" CommandName="MoveNext" Width="100" Text="{$Install.NextStep$}"
                                    OnClientClick="NextStep(this,document.getElementById('buttonsDiv')); return false;"
                                    RenderScript="true" />
                            </div>
                            <span class="HelpButton">
                                <cms:Help ID="hlpContext" runat="server" />
                            </span>
                        </div>
                    </StartNavigationTemplate>
                    <WizardSteps>
                        <asp:WizardStep ID="stpUserServer" runat="server" StepType="Start">
                            <div class="InstallContent">
                                <table class="InstallWizard" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td colspan="2">
                                            <asp:Label ID="lblSQLServer" runat="server" CssClass="InstallGroupTitle UserServer" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td nowrap="nowrap" align="right" style="padding-right: 8px">
                                            <asp:Label ID="lblServerName" AssociatedControlID="txtServerName" runat="server" />
                                        </td>
                                        <td width="100%">
                                            <cms:CMSTextBox ID="txtServerName" CssClass="InstallFormTextBox" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="left">
                                            <asp:RadioButton ID="radSQLAuthentication" runat="server" AutoPostBack="True" GroupName="AuthenticationType"
                                                Checked="True"></asp:RadioButton>
                                        </td>
                                    </tr>
                                    <tr class="InstallSQLName">
                                        <td nowrap="nowrap" align="right" style="padding-right: 8px">
                                            <asp:Label ID="lblDBUsername" AssociatedControlID="txtDBUsername" runat="server" />
                                        </td>
                                        <td>
                                            <cms:CMSTextBox ID="txtDBUsername" CssClass="InstallFormTextBox" runat="server" />
                                        </td>
                                    </tr>
                                    <tr class="InstallSQLPwd">
                                        <td nowrap="nowrap" align="right" style="padding-right: 8px">
                                            <asp:Label ID="lblDBPassword" AssociatedControlID="txtDBPassword" runat="server" />
                                        </td>
                                        <td>
                                            <cms:CMSTextBox ID="txtDBPassword" CssClass="InstallFormTextBox" runat="server" TextMode="Password" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="left">
                                            <asp:RadioButton ID="radWindowsAuthentication" runat="server" AutoPostBack="True"
                                                GroupName="AuthenticationType"></asp:RadioButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </asp:WizardStep>
                        <asp:WizardStep ID="stpDatabase" runat="server" StepType="Step">
                            <div class="InstallContent">
                                <table class="InstallWizard" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td colspan="3">
                                            <asp:Label ID="lblDatabase" AssociatedControlID="radCreateNew" runat="server" CssClass="InstallGroupTitle UserServer" />
                                        </td>
                                    </tr>
                                    <asp:PlaceHolder runat="server" ID="plcNewDB">
                                        <tr>
                                            <td colspan="3">
                                                <asp:RadioButton ID="radCreateNew" runat="server" AutoPostBack="True" GroupName="DatabaseType"
                                                    Checked="True"></asp:RadioButton>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 25px;">
                                                &nbsp;
                                            </td>
                                            <td nowrap="nowrap" align="left" style="width: 140px; padding-right: 8px;">
                                                <asp:Label ID="lblNewDatabaseName" AssociatedControlID="txtNewDatabaseName" runat="server" />
                                            </td>
                                            <td>
                                                <cms:CMSTextBox ID="txtNewDatabaseName" CssClass="InstallFormTextBox" runat="server"
                                                    Enabled="False" MaxLength="90" />
                                            </td>
                                        </tr>
                                    </asp:PlaceHolder>
                                    <tr>
                                        <td colspan="3">
                                            <asp:RadioButton ID="radUseExisting" runat="server" AutoPostBack="True" GroupName="DatabaseType" />
                                        </td>
                                    </tr>
                                    <asp:PlaceHolder runat="server" ID="plcExistingDBName">
                                        <tr>
                                            <td style="width: 25px;">
                                                &nbsp;
                                            </td>
                                            <td nowrap="nowrap" align="left" style="width: 140px; padding-right: 8px;">
                                                <asp:Label ID="lblExistingDatabaseName" AssociatedControlID="txtExistingDatabaseName"
                                                    runat="server" />
                                            </td>
                                            <td>
                                                <cms:CMSTextBox ID="txtExistingDatabaseName" CssClass="InstallFormTextBox" runat="server"
                                                    MaxLength="90" />
                                            </td>
                                        </tr>
                                    </asp:PlaceHolder>
                                    <tr>
                                        <td>
                                        </td>
                                        <td colspan="2">
                                            <asp:CheckBox ID="chkCreateDatabaseObjects" runat="server" CssClass="InstallCreateDBObjects"
                                                Checked="True"></asp:CheckBox>
                                        </td>
                                    </tr>
                                    <asp:PlaceHolder ID="plcAdvanced" runat="server" Visible="false">
                                        <tr>
                                            <td style="width: 25px;">
                                                &nbsp;
                                            </td>
                                            <td nowrap="nowrap" align="left" style="width: 140px;" colspan="2">
                                                <asp:HyperLink ID="lblAdvanced" AssociatedControlID="txtExistingDatabaseName" runat="server"
                                                    Style="cursor: pointer;" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 25px;">
                                                &nbsp;
                                            </td>
                                            <td nowrap="nowrap" align="left" style="width: 140px;">
                                                <asp:Label ID="lblSchema" AssociatedControlID="txtSchema" runat="server" Style="display: none;" />
                                            </td>
                                            <td>
                                                <cms:CMSTextBox ID="txtSchema" CssClass="InstallFormTextBox" runat="server" Style="display: none;"
                                                    Text="dbo" ReadOnly="true" Enabled="false" />
                                            </td>
                                        </tr>
                                    </asp:PlaceHolder>
                                </table>
                            </div>
                        </asp:WizardStep>
                        <asp:WizardStep ID="stpConnectionString" runat="server" AllowReturn="false" StepType="Start">
                            <asp:Panel ID="pnlConnectionString" runat="server">
                                <div class="InstallContent">
                                    <asp:Label ID="lblConnectionString" runat="server" CssClass="InstallGroupTitle" Visible="False" />
                                    <table class="InstallWizard" border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="lblErrorConnMessage" runat="server" CssClass="connMessageError" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </asp:Panel>
                        </asp:WizardStep>
                        <asp:WizardStep ID="stpDBProgress" runat="server" AllowReturn="false" StepType="Step">
                            <div class="InstallDBProgressLabel">
                                <asp:Label ID="lblDBProgress" runat="server" EnableViewState="false" />
                            </div>
                            <asp:Panel ID="pnlDBProgress" runat="server">
                                <div class="InstallDBProgress">
                                    <table class="InstallWizard" border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td align="left" style="vertical-align: top">
                                                <div class="InstallProgress">
                                                    <div style="margin: 5px 0px 5px 5px;">
                                                        <asp:Literal ID="ltlDBProgress" runat="server" />
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </asp:Panel>
                        </asp:WizardStep>
                        <asp:WizardStep ID="stpLicenseSetting" runat="server" AllowReturn="false" StepType="Start">
                            <div class="InstallContent">
                                <table class="InstallWizard" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="left">
                                            <cms:LicenseDialog ID="ucLicenseDialog" runat="server" />
                                            <cms:WagDialog ID="ucWagDialog" runat="server" Visible="false" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </asp:WizardStep>
                        <asp:WizardStep ID="stpSiteCreation" runat="server" AllowReturn="false" StepType="Start">
                            <asp:Panel ID="pnlSiteCreation" runat="server">
                                <asp:Label ID="lblSiteCreation" runat="server" CssClass="InstallGroupTitle" Visible="False" />
                                <div class="InstallContentNewSite">
                                    <table class="InstallWizard" border="0" cellpadding="0" cellspacing="0">
                                        <tr style="vertical-align: top;">
                                            <td align="left">
                                                <cms:SiteCreationDialog ID="ucSiteCreationDialog" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </asp:Panel>
                        </asp:WizardStep>
                        <asp:WizardStep ID="stpProgress" runat="server" AllowReturn="false" StepType="Step">
                            <asp:Panel ID="pnlProgress" runat="server">
                                <div class="InstallSiteProgress">
                                    <table class="InstallWizard" border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td align="left" style="vertical-align: top">
                                                <div class="SiteProgress">
                                                    <div style="margin: 5px 0px 0px 5px;">
                                                        <asp:Literal ID="ltlProgress" runat="server" />
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </asp:Panel>
                        </asp:WizardStep>
                        <asp:WizardStep ID="stpFinish" runat="server" StepType="Complete">
                            <asp:Panel ID="pnlFinished" runat="server">
                                <table class="InstallWizard" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="InstallCompleted" style="text-align: center">
                                            <asp:Label ID="lblCompleted" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: center">
                                            <asp:Label ID="lblMediumTrustInfo" runat="server" Visible="false" /><br />
                                            <asp:LinkButton ID="btnWebSite" runat="server" OnClick="btnWebSite_onClick" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </asp:WizardStep>
                        <asp:WizardStep ID="stpCollation" runat="server" StepType="Step">
                            <asp:Panel ID="pnlCollation" runat="server">
                                <div class="InstallContent">
                                    <table class="InstallWizard" border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td align="left">
                                                <div style="height: 305px;">
                                                    <asp:Label ID="lblCollation" runat="server" /><br />
                                                    <br />
                                                    <br />
                                                    <asp:RadioButton ID="rbChangeCollationCI" Checked="true" runat="server" GroupName="Collation" />
                                                    <br />
                                                    <br />
                                                    <asp:RadioButton ID="rbLeaveCollation" runat="server" GroupName="Collation" />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </asp:Panel>
                        </asp:WizardStep>
                    </WizardSteps>
                </asp:Wizard>
            </asp:Panel>
            <asp:Panel ID="pnlPermission" runat="server" CssClass="InstallerContent" Visible="false">
                <div class="InstallContentPermission">
                    <div style="text-align: left; padding: 0px 20px 10px 20px;">
                        <asp:Label ID="lblPermission" runat="server" />
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel CssClass="ButtonsPanel" ID="pnlButtons" runat="server" Visible="false">
                <div class="InstallContent">
                    <asp:Button ID="btnPermissionTest" runat="server" CssClass="XLongButton" />&nbsp;<asp:Button
                        ID="btnPermissionSkip" runat="server" CssClass="ContentButton" />
                </div>
            </asp:Panel>
            <asp:Panel ID="pnlPermissionSuccess" runat="server" Visible="false">
                <div class="InstallContent" style="padding: 20px 20px 10px;">
                    <asp:Label ID="lblPermissionSuccess" runat="server" /><br />
                    <br />
                    <asp:Button ID="btnPermissionContinue" runat="server" CssClass="ContentButton" />
                </div>
            </asp:Panel>
        </cms:LayoutPanel>
        <asp:Panel ID="pnlVersion" runat="server" CssClass="InstallerFooter">
            <div class="AppSupport">
                <asp:Label ID="lblSupport" runat="server" />
            </div>
            <div class="AppVersion">
                <asp:Label ID="lblVersion" runat="server" />
            </div>
            <div class="ClearBoth">
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlError" runat="server" CssClass="InstallerError">
            <div style="text-align: left; padding: 0px 20px 10px 20px;">
                <div style="padding-bottom: 8px">
                    <cms:Help ID="hlpTroubleshoot" runat="Server" Visible="false" />
                </div>
                <asp:Label ID="lblError" runat="server" CssClass="ErrorLabel" />
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlLog" runat="server" CssClass="InstallerContent" Visible="False">
            <div style="text-align: left; padding: 0px 25px 10px 25px;">
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lblLog" AssociatedControlID="txtLog" runat="server" CssClass="InstallGroupTitle" />
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Panel runat="server" ID="pnlGroupLog">
                                <cms:CMSTextBox ID="txtLog" runat="server" CssClass="InstallLog" TextMode="MultiLine"
                                    ReadOnly="True" />
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlWarning" runat="server" CssClass="InstallerContent">
            <div style="text-align: left; padding: 0px 0px 10px 20px;">
                <asp:Label ID="lblWarning" runat="server" CssClass="ErrorLabel" />
            </div>
        </asp:Panel>
    </asp:Panel>
    <asp:HiddenField ID="hdnState" runat="server" />
    <asp:HiddenField ID="hdnAdvanced" runat="server" />
    <asp:Literal ID="ltlInstallScript" runat="server" EnableViewState="false" />
    <asp:Literal ID="ltlAdvanced" runat="server" EnableViewState="false" />
    <cms:AsyncControl ID="ucAsyncControl" runat="server" PostbackOnError="false" />
    <cms:AsyncControl ID="ucDBAsyncControl" runat="server" PostbackOnError="false" UseFileStrings="True" />
    </form>
</body>
</html>
