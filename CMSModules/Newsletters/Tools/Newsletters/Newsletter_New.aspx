<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Newsletters_Tools_Newsletters_Newsletter_New"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Tools - New newsletter"
    CodeFile="Newsletter_New.aspx.cs" %>

<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextBox" TagPrefix="cms" %>

<%@ Register Src="~/CMSModules/Newsletters/FormControls/NewsletterTemplateSelector.ascx"
    TagName="NewsletterTemplateSelector" TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/Selectors/ScheduleInterval.ascx" TagName="ScheduleInterval"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/System/ServerChecker.ascx" TagPrefix="cms"
    TagName="ServerChecker" %>

<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
    <table style="vertical-align: top">
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel runat="server" ID="lblNewsletterDisplayName" EnableViewState="false"
                    ResourceString="Newsletter_Edit.NewsletterDisplayNameLabel" DisplayColon="true" />
            </td>
            <td>
                <cms:LocalizableTextBox ID="txtNewsletterDisplayName" runat="server" CssClass="TextBoxField"
                    MaxLength="250" />
                <cms:CMSRequiredFieldValidator ID="rfvNewsletterDisplayName" runat="server" ControlToValidate="txtNewsletterDisplayName:textbox"
                    Display="dynamic" EnableViewState="false"></cms:CMSRequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel runat="server" ID="lblNewsletterName" EnableViewState="false"
                    ResourceString="Newsletter_Edit.NewsletterNameLabel" DisplayColon="true" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtNewsletterName" runat="server" CssClass="TextBoxField" MaxLength="250" />
                <cms:CMSRequiredFieldValidator ID="rfvNewsletterName" runat="server" ControlToValidate="txtNewsletterName"
                    Display="dynamic" EnableViewState="false"></cms:CMSRequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel runat="server" ID="lblSubscriptionTemplate" EnableViewState="false"
                    ResourceString="Newsletter_Edit.SubscriptionTemplate" DisplayColon="true" />
            </td>
            <td>
                <cms:NewsletterTemplateSelector ID="subscriptionTemplate" runat="server" />
                <cms:LocalizedLabel ID="lblSubscriptionError" runat="server" ResourceString="Newsletter_Edit.NoSubscriptionTemplateSelected"
                    Visible="false" CssClass="ErrorLabel" EnableViewState="false" DisplayColon="true" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel runat="server" ID="lblUnsubscriptionTemplate" EnableViewState="false"
                    ResourceString="Newsletter_Edit.UnsubscriptionTemplate" DisplayColon="true" />
            </td>
            <td>
                <cms:NewsletterTemplateSelector ID="unsubscriptionTemplate" runat="server" />
                <cms:LocalizedLabel ID="lblUnsubscriptionError" runat="server" ResourceString="Newsletter_Edit.NoUnsubscriptionTemplateSelected"
                    Visible="false" CssClass="ErrorLabel" EnableViewState="false" DisplayColon="true" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel runat="server" ID="lblNewsletterSenderName" EnableViewState="false"
                    ResourceString="Newsletter_Edit.NewsletterSenderNameLabel" DisplayColon="true" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtNewsletterSenderName" runat="server" CssClass="TextBoxField"
                    MaxLength="200" />
                <cms:CMSRequiredFieldValidator ID="rfvNewsletterSenderName" runat="server" ErrorMessage=""
                    ControlToValidate="txtNewsletterSenderName" EnableViewState="false"></cms:CMSRequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel runat="server" ID="lblNewsletterSenderEmail" EnableViewState="false"
                    ResourceString="Newsletter_Edit.NewsletterSenderEmailLabel" DisplayColon="true" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtNewsletterSenderEmail" runat="server" CssClass="TextBoxField"
                    MaxLength="200" />
                <cms:CMSRequiredFieldValidator ID="rfvNewsletterSenderEmail" runat="server" ErrorMessage=""
                    ControlToValidate="txtNewsletterSenderEmail" EnableViewState="false"></cms:CMSRequiredFieldValidator>
            </td>
        </tr>
        <tr style="height: 20px;">
        </tr>
        <tr>
            <td colspan="2">
                <cms:LocalizedRadioButton ID="radTemplateBased" runat="server" GroupName="NewsletterType"
                    AutoPostBack="True" OnCheckedChanged="rad_CheckedChanged" ResourceString="Newsletter_Edit.TemplateBased" />
            </td>
        </tr>
        <tr style="height: 5px;">
        </tr>
        <tr>
            <td class="FieldLabel">
                <div class="UnderRadioContent">
                    <cms:LocalizedLabel runat="server" ID="lblIssueTemplate" EnableViewState="false"
                        ResourceString="Newsletter_Edit.NewsletterTemplate" DisplayColon="true" /></div>
            </td>
            <td>
                <cms:NewsletterTemplateSelector ID="issueTemplate" runat="server" />
                <cms:LocalizedLabel ID="lblIssueError" runat="server" ResourceString="Newsletter_Edit.NoEmailTemplateSelected"
                    Visible="false" CssClass="ErrorLabel" EnableViewState="false" DisplayColon="true" />
            </td>
        </tr>
        <tr style="height: 15px;">
        </tr>
        <tr>
            <td colspan="2">
                <cms:LocalizedRadioButton ID="radDynamic" runat="server" GroupName="NewsletterType"
                    AutoPostBack="True" OnCheckedChanged="rad_CheckedChanged" ResourceString="Newsletter_Edit.Dynamic" />
            </td>
        </tr>
        <tr style="height: 5px;">
        </tr>
        <tr>
            <td class="FieldLabel">
                <div class="UnderRadioContent">
                    <cms:LocalizedLabel runat="server" ID="lblNewsletterDynamicURL" EnableViewState="false"
                        ResourceString="Newsletter_Edit.SourcePageURL" DisplayColon="true" /></div>
            </td>
            <td>
                <cms:CMSTextBox ID="txtNewsletterDynamicURL" runat="server" CssClass="TextBoxField"
                    MaxLength="500" />
                <cms:ServerChecker runat="server" ID="serverChecker" TextBoxControlID="txtNewsletterDynamicURL" />
            </td>
        </tr>
        <asp:PlaceHolder ID="plcInterval" runat="server">
            <tr>
                <td class="FieldLabel" style="vertical-align: top;">
                    <div class="UnderRadioContent">
                        <cms:LocalizedLabel runat="server" ID="lblSchedule" EnableViewState="false" ResourceString="Newsletter_Edit.Schedule"
                            DisplayColon="true" /></div>
                </td>
                <td>
                    <asp:CheckBox ID="chkSchedule" runat="server" Checked="true" OnCheckedChanged="chkSchedule_CheckedChanged"
                        AutoPostBack="true" /><br />
                    <cms:ScheduleInterval ID="ScheduleInterval" runat="server" />
                </td>
            </tr>
        </asp:PlaceHolder>
        <tr>
            <td>
            </td>
            <td>
                <cms:LocalizedButton runat="server" ID="btnOk" OnClick="btnOK_Click" EnableViewState="false"
                    CssClass="SubmitButton" ResourceString="General.OK" />
            </td>
        </tr>
    </table>
</asp:Content>
