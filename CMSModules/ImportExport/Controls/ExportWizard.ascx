<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_ImportExport_Controls_ExportWizard"
    CodeFile="ExportWizard.ascx.cs" %>
<%@ Register Src="~/CMSAdminControls/Wizard/Header.ascx" TagName="WizardHeader" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/ImportExport/Controls/ExportPanel.ascx" TagName="ExportPanel"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/ImportExport/Controls/ExportConfiguration.ascx" TagName="ExportConfiguration"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/System/ActivityBar.ascx" TagName="ActivityBar"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/AsyncControl.ascx" TagName="AsyncControl" TagPrefix="cms" %>

<script type="text/javascript">
    //<![CDATA[      
    var timerId = 0;
    var timerSelectionId = 0;

    // start timer function
    function StartExportStateTimer() {
        timerId = setInterval("GetExportState(false)", 500);
    }

    // end timer function
    function StopExportStateTimer() {
        if (timerId) {
            clearInterval(timerId);
            timerId = 0;
        }
    }

    // End timer function
    function StopSelectionTimer() {
        if (timerSelectionId) {
            clearInterval(timerSelectionId);
            timerSelectionId = 0;

            if (window.HideActivity) {
                window.HideActivity();
            }
        }
    }

    // Start timer function
    function StartSelectionTimer() {
        if (window.Activity) {
            timerSelectionId = setInterval("window.Activity()", 500);
        }
    }

    // Cancel export
    function CancelExport() {
        GetExportState(true);
        return false;
    }
    //]]>
</script>

<asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />
<asp:Panel ID="pnlWrapper" runat="Server">
    <cms:LocalizedLabel ID="lblBeta" runat="server" EnableViewState="false" Visible="false"
        CssClass="ErrorLabel" />
    <table class="GlobalWizard" border="0" cellpadding="0" cellspacing="0">
        <tr class="Top">
            <td class="Left">
                &nbsp;
            </td>
            <td class="Center">
                <div style="width: 750px;">
                    <cms:WizardHeader ID="ucHeader" runat="server" EnableViewState="false" />
                </div>
            </td>
            <td class="Right">
                &nbsp;
            </td>
        </tr>
        <tr class="Middle">
            <td class="Center" colspan="3">
                <div id="wzdBody">
                    <cms:CMSWizard ID="wzdExport" ShortID="w" runat="server" DisplaySideBar="False" NavigationButtonStyle-Width="100"
                        NavigationStyle-HorizontalAlign="Right" CssClass="Wizard">
                        <StartNavigationTemplate>
                            <div class="WizardProgress">
                                <div id="actDiv" style="display: none;">
                                    <div class="WizardProgressLabel">
                                        <cms:LocalizedLabel ID="lblActivityInfo" runat="server" Text="{$Export.SelectionInfo$}"
                                            EnableViewState="false" />
                                    </div>
                                    <cms:ActivityBar runat="server" ID="barActivity" Visible="true" />
                                </div>
                            </div>
                            <div id="buttonsDiv" class="WizardButtons">
                                <cms:LocalizedButton UseSubmitBehavior="True" ID="StepNextButton" runat="server"
                                    CommandName="MoveNext" Text="{$general.next$} >" CssClass="SubmitButton" OnClientClick="return exNextStepAction();"
                                    EnableViewState="false" RenderScript="true" />
                            </div>
                        </StartNavigationTemplate>
                        <StepNavigationTemplate>
                            <div id="buttonsDiv" class="WizardButtons">
                                <cms:LocalizedButton UseSubmitBehavior="True" ID="StepPreviousButton" runat="server"
                                    CommandName="MovePrevious" Text="{$ExportSiteSettings.PreviousStep$}" CssClass="SubmitButton"
                                    CausesValidation="false" EnableViewState="false" RenderScript="true" /><cms:LocalizedButton
                                        UseSubmitBehavior="True" ID="StepNextButton" runat="server" CommandName="MoveNext"
                                        Text="{$general.next$} >" CssClass="SubmitButton" OnClientClick="return exNextStepAction();"
                                        EnableViewState="false" RenderScript="true" />
                            </div>
                        </StepNavigationTemplate>
                        <FinishNavigationTemplate>
                            <div class="WizardProgress">
                                <div id="actDiv">
                                    <div class="WizardProgressLabel">
                                        <cms:LocalizedLabel ID="lblActivityInfo" runat="server" Text="{$Export.Progress$}"
                                            EnableViewState="false" />
                                    </div>
                                    <cms:ActivityBar runat="server" ID="barActivity" Visible="true" />
                                </div>
                            </div>
                            <div id="buttonsDiv" class="WizardButtons">
                                <cms:LocalizedButton UseSubmitBehavior="False" ID="StepCancelButton" runat="server"
                                    Text="{$general.cancel$}" CssClass="SubmitButton" EnableViewState="false" RenderScript="true" /><cms:LocalizedButton
                                        UseSubmitBehavior="True" ID="StepFinishButton" runat="server" Enabled="false"
                                        CommandName="MoveComplete" ResourceString="general.finish" CssClass="SubmitButton"
                                        EnableViewState="false" RenderScript="true" />
                            </div>
                        </FinishNavigationTemplate>
                        <WizardSteps>
                            <asp:WizardStep ID="wzdStepStart" runat="server" AllowReturn="False" StepType="Start"
                                EnableViewState="true">
                                <div class="GlobalWizardStep" style="height: <%= PanelHeight %>px">
                                    <cms:ExportConfiguration ID="configExport" ShortID="c" runat="server" />
                                </div>
                            </asp:WizardStep>
                            <asp:WizardStep ID="wzdStepSelectionPanel" runat="server" AllowReturn="False" StepType="Step"
                                EnableViewState="true">
                                <div class="GlobalWizardStep" style="height: <%= PanelHeight %>px; padding: 0px;">
                                    <div class="WizardBorder">
                                        <cms:ExportPanel ID="pnlExport" ShortID="p" runat="server" />
                                    </div>
                                </div>
                            </asp:WizardStep>
                            <asp:WizardStep ID="wzdStepProgress" runat="server" AllowReturn="False" StepType="Finish"
                                EnableViewState="true">
                                <div class="GlobalWizardStep" style="height: <%= PanelHeight %>px">
                                    <asp:Label ID="lblProgress" runat="Server" CssClass="WizardLog" EnableViewState="false" />
                                </div>
                            </asp:WizardStep>
                        </WizardSteps>
                    </cms:CMSWizard>
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
<br />
<asp:Label ID="lblError" runat="server" CssClass="ErrorLabel" EnableViewState="false" />
<asp:Label ID="lblWarning" runat="server" CssClass="ErrorLabel" EnableViewState="false" />
<asp:Panel ID="pnlPermissions" runat="server" Visible="false" EnableViewState="false">
    <br />
    <asp:HyperLink ID="lnkPermissions" runat="server" EnableViewState="false" />
</asp:Panel>
<asp:HiddenField ID="hdnState" runat="server" />
<asp:Literal ID="ltlScriptAfter" runat="server" EnableViewState="false" />
<cms:AsyncControl ID="ctrlAsync" runat="server" />
