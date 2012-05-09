<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Licenses_Pages_License_Import_Licenses" Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master"
    Title="Licenses - Import licenses" CodeFile="License_Import_Licenses.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/Wizard/Header.ascx" TagName="WizardHeader" TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/System/ActivityBar.ascx" TagName="ActivityBar" TagPrefix="cms" %>
<asp:Content ContentPlaceHolderID="plcContent" runat="server">

    <script type="text/javascript">
    <!--
        var timerId = 0;
        var activityId = 0;
        var i = 0;

        function GetStateAction() {
            if (i++ >= 10) {
                i = 0;
                try {
                    GetState(false);
                }
                catch (ex) {
                }
            }
        }

        // End timer function
        function StopStateTimer() {
            if (timerId) {
                clearInterval(timerId);
                timerId = 0;
            }

            if (activityId) {
                clearInterval(activityId);
                activityId = 0;
            }
        }

        // Start timer function
        function StartStateTimer() {
            activityId = setInterval("window.Activity()", 500);
            timerId = setInterval("GetStateAction()", 100);
        }

        // Cancel import
        function CancelImport() {
            GetState(true);
            return false;
        }
        
    -->
    </script>

    <asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />
    <asp:Panel ID="pnlWrapper" runat="Server">
        <table class="GlobalWizard" border="0" cellpadding="0" cellspacing="0">
            <tr class="Top">
                <td class="Left">
                    &nbsp;
                </td>
                <td class="Center">
                    <div style="width: 750px;">
                        <cms:WizardHeader ID="ucHeader" runat="server" />
                    </div>
                </td>
                <td class="Right">
                    &nbsp;
                </td>
            </tr>
            <tr class="Middle">
                <td class="Center" colspan="3">
                    <div id="wzdBody">
                        <asp:Wizard ID="wzdImport" runat="server" DisplaySideBar="False" NavigationButtonStyle-Width="100"
                            NavigationStyle-HorizontalAlign="Right" CssClass="Wizard">
                            <StartNavigationTemplate>
                                <div id="buttonsDiv" class="WizardButtons">
                                    <cms:LocalizedButton UseSubmitBehavior="True" ID="StepNextButton" runat="server"
                                        CommandName="MoveNext" Text="{$general.next$} >" CssClass="SubmitButton" RenderScript="true" />
                                </div>
                            </StartNavigationTemplate>
                            <FinishNavigationTemplate>
                                <div class="WizardProgress">
                                    <div id="actDiv">
                                        <div class="WizardProgressLabel">
                                            <cms:LocalizedLabel ID="lblActivityInfo" runat="server" ResourceString="license.import.inprogress" />
                                        </div>
                                        <cms:ActivityBar runat="server" ID="barActivity" Visible="true" />
                                    </div>
                                </div>
                                <div id="buttonsDiv" class="WizardButtons">
                                    <cms:LocalizedButton UseSubmitBehavior="False" ID="StepCancelButton" runat="server"
                                        ResourceString="general.cancel" CssClass="SubmitButton" RenderScript="true" />
                                    <cms:LocalizedButton UseSubmitBehavior="True" ID="StepFinishButton" runat="server"
                                        Enabled="false" CommandName="MoveComplete" ResourceString="general.finish" CssClass="SubmitButton"
                                        RenderScript="true" />
                                </div>
                            </FinishNavigationTemplate>
                            <WizardSteps>
                                <asp:WizardStep ID="wzdStepStart" runat="server" AllowReturn="False" StepType="Start"
                                    EnableViewState="true">
                                    <div class="GlobalWizardStep" style="height: <%= PanelHeight %>px">
                                        <cms:LocalizedLabel ID="lblErrorFirstStep" runat="server" EnableViewState="false" CssClass="ErrorLabel"
                                            ResourceString="license.import.selectfile" Visible="false"></cms:LocalizedLabel>
                                        <cms:LocalizedLabel ID="lblAvailable" runat="server" EnableViewState="false" CssClass="InfoLabel"
                                            ResourceString="license.import.available" DisplayColon="true"></cms:LocalizedLabel>
                                        <table>
                                            <tr>
                                                <td>
                                                    <cms:CMSUpdatePanel runat="server" ID="pnlUpdate" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <asp:ListBox runat="server" ID="lstImports" CssClass="ContentListBoxLow" Width="450"
                                                                Enabled="false" />
                                                            <div class="ClearBoth">
                                                                &nbsp;</div>
                                                            <asp:Panel runat="server" ID="pnlRefresh" CssClass="PageHeaderItemRight" Style="padding: 8px 5px">
                                                                <asp:Image runat="server" ID="imgRefresh" CssClass="NewItemImage" /><cms:LocalizedLinkButton
                                                                    runat="server" ID="btnRefresh" CssClass="NewItemLink" ResourceString="general.refresh"
                                                                    OnClick="btnRefresh_Click" />
                                                            </asp:Panel>
                                                        </ContentTemplate>
                                                    </cms:CMSUpdatePanel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:CheckBox runat="server" ID="chkOverrideExisting" />
                                                    <cms:LocalizedLabel runat="server" ID="lblOverrideExisting" EnableViewState="false"
                                                        ResourceString="license.import.override"></cms:LocalizedLabel>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </asp:WizardStep>
                                <asp:WizardStep ID="wzdStepProgress" runat="server" AllowReturn="False" StepType="Finish"
                                    EnableViewState="true">
                                    <div class="GlobalWizardStep" style="height: <%= PanelHeight %>px">
                                        <asp:Label runat="server" ID="lblLog" CssClass="InfoLabel" EnableViewState="true" />
                                    </div>
                                </asp:WizardStep>
                            </WizardSteps>
                        </asp:Wizard>
                    </div>
                </td>
            </tr>
            <tr class="Bottom">
                <td class="Left">
                    &nbsp;
                </td>
                <td class="Center">
                    &nbsp;
                </td>
                <td class="Right">
                    &nbsp;
                </td>
            </tr>
        </table>
    </asp:Panel>
    <cms:LocalizedLabel runat="server" EnableViewState="false"  ID="lblError" CssClass="ErrorLabel" />
    <asp:HiddenField ID="hdnLog" runat="server" EnableViewState="false" />
</asp:Content>
