<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Edit.ascx.cs" Inherits="CMSModules_Scoring_Controls_UI_Score_Edit" %>
<%@ Register Src="~/CMSFormControls/Basic/TextBoxControl.ascx" TagName="TextBoxControl"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/Selectors/ScheduleInterval.ascx" TagPrefix="cms"
    TagName="ScheduleInterval" %>
<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextBox"
    TagPrefix="cms" %>
    
<table style="width: 100%">
    <tr>
        <td style="vertical-align: top; width: 100%;">
            <cms:UIForm runat="server" ID="EditForm" ObjectType="om.score" IsLiveSite="false"
                RedirectUrlAfterCreate="Frameset.aspx?scoreid={%EditedObject.ID%}&saved=1">
                <SecurityCheck Resource="CMS.Scoring" Permission="modify" />
                <LayoutTemplate>
                    <cms:FormField runat="server" ID="fDispName" Field="ScoreDisplayName" FormControl="LocalizableTextBox"
                        ResourceString="general.displayname" DisplayColon="true" />
                    <cms:FormField runat="server" ID="fName" Field="ScoreName" FormControl="TextBoxControl"
                        ResourceString="general.codename" DisplayColon="true" />
                    <cms:FormField runat="server" Layout="Inline" ID="fDesc" Field="ScoreDescription">
                        <tr>
                            <td>
                                <cms:FormLabel runat="server" ID="lDescription" ResourceString="general.description"
                                    DisplayColon="true" />
                            </td>
                            <td>
                                <cms:LocalizableTextBox ID="tDescription" runat="server" TextMode="MultiLine" CssClass="TextAreaField" />
                            </td>
                            <td>
                                <cms:FormErrorLabel runat="server" ID="eDescription" />
                            </td>
                        </tr>
                    </cms:FormField>
                    <cms:FormField runat="server" ID="fEnabled" Field="ScoreEnabled" FormControl="CheckBoxControl"
                        ResourceString="general.enabled" DisplayColon="true" Value="true" />
                    <asp:PlaceHolder runat="server" ID="plcUpdate">
                        <cms:FormField runat="server" ID="fSendAtScore" Field="ScoreEmailAtScore" FormControl="TextBoxControl"
                            ResourceString="om.score.emailatscore" DisplayColon="true" />
                        <cms:FormField runat="server" ID="fEmail" Field="ScoreNotificationEmail" FormControl="EmailInput"
                            ResourceString="om.score.email" DisplayColon="true" />
                        <tr>
                            <td style="vertical-align: top;">
                                <cms:LocalizedLabel runat="server" ID="lblScheduler" EnableViewState="false" ResourceString="om.contactgroup.schedule"
                                    DisplayColon="true" AssociatedControlID="chkSchedule" />
                            </td>
                            <td>
                                <asp:CheckBox ID="chkSchedule" runat="server" Checked="true" AutoPostBack="true"
                                    OnCheckedChanged="chkSchedule_CheckedChanged" /><br />
                                <cms:ScheduleInterval ID="schedulerInterval" runat="server" DefaultPeriod="day" />
                            </td>
                            <td></td>
                        </tr>
                    </asp:PlaceHolder>
                </LayoutTemplate>
            </cms:UIForm>
        </td>
        <td style="vertical-align: top">
            <div style="padding-right: 30px;">
                <asp:Panel runat="server" ID="pnlInfo" Visible="false">
                    <div style="padding: 10px">
                        <cms:CMSUpdatePanel runat="server" ID="pnlUpdate" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:Timer ID="timRefresh" runat="server" Interval="3000" EnableViewState="false" />
                                <table cellspacing="2" cellpadding="3">
                                    <tr>
                                        <td class="FieldLabel">
                                            <cms:LocalizedLabel runat="server" ID="lblStatus" EnableViewState="false" ResourceString="general.status"
                                                DisplayColon="true" />
                                        </td>
                                        <td class="FieldLabel">
                                            <cms:LocalizedLabel runat="server" ID="lblStatusValue" EnableViewState="false" />
                                            <asp:Literal runat="server" ID="ltrProgress" EnableViewState="false" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="FieldLabel">
                                            <cms:LocalizedLabel runat="server" ID="lblLastEval" EnableViewState="false" ResourceString="om.score.lastrecalculation"
                                                DisplayColon="true" />
                                        </td>
                                        <td class="FieldLabel">
                                            <cms:LocalizedLabel runat="server" ID="lblLastEvalValue" EnableViewState="false" />
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </cms:CMSUpdatePanel>
                    </div>
                </asp:Panel>
            </div>
        </td>
    </tr>
</table>
    
<asp:HiddenField ID="hdnConfirmDelete" runat="server" />
