<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Newsletters_Tools_Newsletters_Newsletter_Configuration"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Tools - Newsletter configuration"
    CodeFile="Newsletter_Configuration.aspx.cs" %>

<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextBox"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Newsletters/FormControls/NewsletterTemplateSelector.ascx"
    TagPrefix="cms" TagName="NewsletterTemplateSelector" %>
<%@ Register Src="~/CMSAdminControls/UI/Selectors/ScheduleInterval.ascx" TagPrefix="cms"
    TagName="ScheduleInterval" %>
<%@ Register Src="~/CMSAdminControls/UI/System/ServerChecker.ascx" TagPrefix="cms"
    TagName="ServerChecker" %>
<asp:Content ContentPlaceHolderID="plcContent" ID="content" runat="server">
    <table style="vertical-align: top">
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel runat="server" ID="lblNewsletterDisplayName" EnableViewState="false"
                    ResourceString="Newsletter_Edit.NewsletterDisplayNameLabel" DisplayColon="true"
                    AssociatedControlID="txtNewsletterDisplayName" />
            </td>
            <td>
                <cms:LocalizableTextBox ID="txtNewsletterDisplayName" runat="server" CssClass="TextBoxField"
                    MaxLength="250" />
                <cms:CMSRequiredFieldValidator ID="rfvNewsletterDisplayName" runat="server" ControlToValidate="txtNewsletterDisplayName:textbox"
                    Display="dynamic" EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel runat="server" ID="lblNewsletterName" EnableViewState="false"
                    ResourceString="Newsletter_Edit.NewsletterNameLabel" DisplayColon="true" AssociatedControlID="txtNewsletterName" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtNewsletterName" runat="server" CssClass="TextBoxField" MaxLength="250" />
                <cms:CMSRequiredFieldValidator ID="rfvNewsletterName" runat="server" ControlToValidate="txtNewsletterName"
                    Display="dynamic" EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel runat="server" ID="lblNewsletterSenderName" EnableViewState="false"
                    ResourceString="Newsletter_Edit.NewsletterSenderNameLabel" DisplayColon="true"
                    AssociatedControlID="txtNewsletterSenderName" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtNewsletterSenderName" runat="server" CssClass="TextBoxField"
                    MaxLength="200" />
                <cms:CMSRequiredFieldValidator ID="rfvNewsletterSenderName" runat="server" ErrorMessage=""
                    ControlToValidate="txtNewsletterSenderName" EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel runat="server" ID="lblNewsletterSenderEmail" EnableViewState="false"
                    ResourceString="Newsletter_Edit.NewsletterSenderEmailLabel" DisplayColon="true"
                    AssociatedControlID="txtNewsletterSenderEmail" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtNewsletterSenderEmail" runat="server" CssClass="TextBoxField"
                    MaxLength="200" />
                <cms:CMSRequiredFieldValidator ID="rfvNewsletterSenderEmail" runat="server" ErrorMessage=""
                    ControlToValidate="txtNewsletterSenderEmail" EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel runat="server" ID="lblNewsletterBaseUrl" EnableViewState="false"
                    ResourceString="Newsletter_Configuration.NewsletterBaseUrl" DisplayColon="true"
                    AssociatedControlID="txtNewsletterBaseUrl" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtNewsletterBaseUrl" runat="server" CssClass="TextBoxField" MaxLength="500" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel runat="server" ID="lblNewsletterUnsubscribeUrl" EnableViewState="false"
                    ResourceString="Newsletter_Configuration.NewsletterUnsubscribeUrl" DisplayColon="true"
                    AssociatedControlID="txtNewsletterUnsubscribeUrl" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtNewsletterUnsubscribeUrl" runat="server" CssClass="TextBoxField"
                    MaxLength="1000" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel runat="server" ID="lblSubscriptionTemplate" EnableViewState="false"
                    ResourceString="Newsletter_Edit.SubscriptionTemplate" DisplayColon="true" AssociatedControlID="subscriptionTemplate" />
            </td>
            <td>
                <cms:NewsletterTemplateSelector ID="subscriptionTemplate" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel runat="server" ID="lblUnsubscriptionTemplate" EnableViewState="false"
                    ResourceString="Newsletter_Edit.UnsubscriptionTemplate" DisplayColon="true" AssociatedControlID="unsubscriptionTemplate" />
            </td>
            <td>
                <cms:NewsletterTemplateSelector ID="unsubscriptionTemplate" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel ID="lblDraftEmails" runat="server" EnableViewState="false" ResourceString="newsletter.draftemails"
                    DisplayColon="true" AssociatedControlID="txtDraftEmails" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtDraftEmails" runat="server" CssClass="TextBoxField" MaxLength="450" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel ID="lblUseEmailQueue" runat="server" EnableViewState="false"
                    ResourceString="newsletter.useemailqueue" DisplayColon="true" AssociatedControlID="chkUseEmailQueue" />
            </td>
            <td>
                <asp:CheckBox ID="chkUseEmailQueue" runat="server" />
            </td>
        </tr>
        <tr style="height: 20px;">
        </tr>
        <asp:PlaceHolder ID="plcOnlineMarketing" runat="server">
            <tr>
                <td class="FieldLabel" colspan="2" style="font-weight: bold">
                    <cms:LocalizedLabel ID="lblOnlineMarketing" runat="server" EnableViewState="false"
                        ResourceString="onlinemarketing.general" DisplayColon="true" />
                </td>
            </tr>
            <tr style="height: 10px">
                <td colspan="2">
                </td>
            </tr>
            <tr>
                <td class="FieldLabel">
                    <cms:LocalizedLabel ID="lblTrackOpenedEmails" runat="server" EnableViewState="false"
                        ResourceString="newsletter.trackopenedemails" DisplayColon="true" AssociatedControlID="chkTrackOpenedEmails" />
                </td>
                <td>
                    <asp:CheckBox ID="chkTrackOpenedEmails" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="FieldLabel">
                    <cms:LocalizedLabel ID="lblTrackClickedLinks" runat="server" EnableViewState="false"
                        ResourceString="newsletter.trackclickedlinks" DisplayColon="true" AssociatedControlID="chkTrackClickedLinks" />
                </td>
                <td>
                    <asp:CheckBox ID="chkTrackClickedLinks" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="FieldLabel">
                    <cms:LocalizedLabel ID="lblLogActivity" runat="server" EnableViewState="false" ResourceString="newsletter.trackactivities"
                        DisplayColon="true" AssociatedControlID="chkLogActivity" />
                </td>
                <td>
                    <asp:CheckBox ID="chkLogActivity" runat="server" />
                </td>
            </tr>
            <tr style="height: 15px">
                <td colspan="2">
                </td>
            </tr>
        </asp:PlaceHolder>
        <tr>
            <td colspan="2" style="font-weight: bold">
                <cms:LocalizedLabel ID="lblTemplateBased" runat="server" EnableViewState="false"
                    ResourceString="Newsletter_Configuration.TemplateBased" DisplayColon="true" />
                <cms:LocalizedLabel ID="lblDynamic" runat="server" EnableViewState="false" ResourceString="Newsletter_Configuration.Dynamic"
                    DisplayColon="true" />
            </td>
        </tr>
        <tr style="height: 15px">
            <td colspan="2">
            </td>
        </tr>
        <asp:Panel ID="pnlDynamic" runat="server">
            <tr>
                <td class="FieldLabel">
                    <cms:LocalizedLabel ID="lblSubject" runat="server" EnableViewState="false" ResourceString="general.subject"
                        DisplayColon="true" />
                </td>
                <td>
                    <cms:LocalizedRadioButton ID="radPageTitle" runat="server" GroupName="Subject" ResourceString="Newsletter_Configuration.PageTitleSubject"
                        AutoPostBack="True" OnCheckedChanged="radPageTitle_CheckedChanged" TextAlign="Right" />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    <cms:LocalizedRadioButton ID="radFollowing" runat="server" GroupName="Subject" ResourceString="Newsletter_Configuration.PageTitleFollowing"
                        AutoPostBack="True" OnCheckedChanged="radFollowing_CheckedChanged" TextAlign="Right" />
                    <br />
                    <cms:LocalizableTextBox ID="txtSubject" runat="server" MaxLength="500" CssClass="TextBoxField"
                        Style="margin-top: 5px" />
                </td>
            </tr>
        </asp:Panel>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel runat="server" ID="lblIssueTemplate" EnableViewState="false"
                    ResourceString="Newsletter_Edit.NewsletterTemplate" DisplayColon="true" AssociatedControlID="issueTemplate" />
            </td>
            <td>
                <cms:NewsletterTemplateSelector ID="issueTemplate" runat="server" />
            </td>
        </tr>
        <asp:PlaceHolder ID="plcUrl" runat="server">
            <tr style="height: 5px;">
                <td colspan="2">
                </td>
            </tr>
            <tr>
                <td class="FieldLabel">
                    <cms:LocalizedLabel runat="server" ID="lblNewsletterDynamicURL" EnableViewState="false"
                        ResourceString="Newsletter_Edit.SourcePageURL" DisplayColon="true" AssociatedControlID="txtNewsletterDynamicURL" />
                </td>
                <td>
                    <cms:CMSTextBox ID="txtNewsletterDynamicURL" runat="server" CssClass="TextBoxField"
                        MaxLength="500" />
                    <cms:ServerChecker runat="server" ID="serverChecker" TextBoxControlID="txtNewsletterDynamicURL" />
                </td>
            </tr>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="plcInterval" runat="server">
            <tr>
                <td class="FieldLabel" style="vertical-align: top;">
                    <cms:LocalizedLabel runat="server" ID="lblSchedule" EnableViewState="false" ResourceString="Newsletter_Edit.Schedule"
                        DisplayColon="true" AssociatedControlID="chkSchedule" />
                </td>
                <td>
                    <asp:CheckBox ID="chkSchedule" runat="server" Checked="true" AutoPostBack="true"
                        OnCheckedChanged="chkSchedule_CheckedChanged" /><br />
                    <cms:ScheduleInterval ID="schedulerInterval" runat="server" />
                </td>
            </tr>
        </asp:PlaceHolder>
        <tr style="height: 20px;">
            <td colspan="2">
            </td>
        </tr>
        <tr>
            <td colspan="2" style="font-weight: bold">
                <cms:LocalizedLabel ID="lblOptIn" runat="server" EnableViewState="false" ResourceString="Newsletter_Configuration.OptIn"
                    DisplayColon="true" />
            </td>
        </tr>
        <tr style="height: 15px">
            <td colspan="2">
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel ID="lblEnableOptIn" runat="server" EnableViewState="false" ResourceString="newsletter_configuration.enableoptin"
                    DisplayColon="true" AssociatedControlID="chkEnableOptIn" />
            </td>
            <td>
                <asp:CheckBox ID="chkEnableOptIn" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel runat="server" ID="lblOptInTemplate" EnableViewState="false"
                    ResourceString="newsletter_configuration.optnintemplate" DisplayColon="true"
                    AssociatedControlID="optInSelector" />
            </td>
            <td>
                <cms:NewsletterTemplateSelector ID="optInSelector" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel runat="server" ID="lblOptInURL" EnableViewState="false" ResourceString="newsletter_configuration.optinurl"
                    DisplayColon="true" AssociatedControlID="txtOptInURL" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtOptInURL" runat="server" CssClass="TextBoxField" MaxLength="500" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel ID="lblSendOptInConfirmation" runat="server" EnableViewState="false"
                    ResourceString="newsletter_configuration.sendoptinconfirmation" DisplayColon="true"
                    AssociatedControlID="chkSendOptInConfirmation" />
            </td>
            <td>
                <asp:CheckBox ID="chkSendOptInConfirmation" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <cms:LocalizedButton runat="server" ID="btnOk" OnClick="btnOK_Click" EnableViewState="false"
                    ResourceString="General.OK" CssClass="SubmitButton" />
            </td>
        </tr>
    </table>
</asp:Content>
