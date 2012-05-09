<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Newsletter_Issue_New_Send.aspx.cs"
    Inherits="CMSModules_Newsletters_Tools_Newsletters_Newsletter_Issue_New_Send"
    Theme="default" MasterPageFile="~/CMSMasterPages/UI/Dialogs/ModalDialogPage.master"
    Title="Tools - Send new newsletter issue" %>

<%@ Register Src="~/CMSAdminControls/Wizard/Header.ascx" TagName="WizardHeader" TagPrefix="cms" %>

<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">

    <div style="padding: 10px;">
        <table class="GlobalWizard NewsletterWizard" cellspacing="0">
            <tr class="Top">
                <td class="Left">
                    &nbsp;
                </td>
                <td class="Center">
                    <cms:WizardHeader ID="ucHeader" runat="server" />
                </td>
                <td class="Right">
                    &nbsp;
                </td>
            </tr>
            <tr class="Middle">
                <td class="Center" colspan="3">
                    <div id="wzdBody">
                        <table class="Wizard" cellspacing="0" cellpadding="0" border="0" style="border-collapse: collapse;">
                            <tbody>
                                <tr style="height: 100%;">
                                    <td>
                                        <div class="NewsletterWizardStep">
                                            <table style="width: 100%">
                                                <tr>
                                                    <td>
                                                        <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
                                                            Visible="false" />
                                                        <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
                                                            Visible="false" />
                                                        <table>
                                                            <tr>
                                                                <td style="padding-bottom: 25px;">
                                                                    <cms:LocalizedRadioButton ID="radSendNow" runat="server" GroupName="Send" 
                                                                        AutoPostBack="true" OnCheckedChanged="radGroupSend_CheckedChanged"
                                                                        ResourceString="newsletter_issue_new_send.sendnow" 
                                                                        Checked="true" />
                                                                </td>                                                                
                                                            </tr>
                                                            <tr>
                                                                <td style="padding-bottom: 25px;">
                                                                    <cms:LocalizedRadioButton ID="radSchedule" runat="server" GroupName="Send"
                                                                        AutoPostBack="true" OnCheckedChanged="radGroupSend_CheckedChanged"
                                                                        ResourceString="newsletter_issue_new_send.schedule" />                                                                        
                                                                    <div class="UnderRadioContent">
                                                                        <cms:LocalizedLabel ID="lblDateTime" runat="server"                                                                             
                                                                            ResourceString="newsletter_issue_new_send.datetime" DisplayColon="true"
                                                                            AssociatedControlID="calendarControl" />
                                                                        <cms:DateTimePicker ID="calendarControl" runat="server" Enabled="false"
                                                                            SupportFolder="~/CMSAdminControls/Calendar" />
                                                                    </div>
                                                                </td>
                                                            </tr>                                                          
                                                            <tr>
                                                                <td style="padding-bottom: 25px;">
                                                                    <cms:LocalizedRadioButton ID="radSendDraft" runat="server" GroupName="Send"
                                                                        AutoPostBack="true" OnCheckedChanged="radGroupSend_CheckedChanged"
                                                                        ResourceString="newsletter_issue_new_send.senddraft" />                                                                        
                                                                    <div class="UnderRadioContent">
                                                                        <cms:LocalizedLabel ID="lblDraftEmails" runat="server"                                                                             
                                                                            ResourceString="newsletter_issue_new_send.emails" DisplayColon="true"
                                                                            AssociatedControlID="txtSendDraft" />
                                                                        <cms:CMSTextBox ID="txtSendDraft" runat="server" 
                                                                            MaxLength="450" Enabled="false" CssClass="TextBoxField" />
                                                                    </div>
                                                                </td>                                                                
                                                            </tr>                                                            
                                                            <tr>
                                                                <td>
                                                                    <cms:LocalizedRadioButton ID="radSendLater" runat="server" GroupName="Send"
                                                                        AutoPostBack="true" OnCheckedChanged="radGroupSend_CheckedChanged"
                                                                        ResourceString="newsletter_issue_new_send.sendlater" />
                                                                </td>                                                                
                                                            </tr>
                                                        </table>                                                        
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="ButtonRow">
                                        <div id="buttonsDiv">
                                            <cms:LocalizedButton ID="btnFinish" runat="server" CssClass="SubmitButton" 
                                                ResourceString="general.finish" OnClick="btnFinish_Click" Enabled="false"
                                                EnableViewState="false" />
                                        </div>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
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
    </div>
</asp:Content>
<asp:Content ID="cntFooter" runat="server" ContentPlaceHolderID="plcFooter">
    <div class="FloatRight">
        <cms:LocalizedButton ID="btnClose" runat="server" CssClass="SubmitButton"
            ResourceString="general.close" OnClientClick="window.close();RefreshPage();return false;" />
    </div>
</asp:Content>
