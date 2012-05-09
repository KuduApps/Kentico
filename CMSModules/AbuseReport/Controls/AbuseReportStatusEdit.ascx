<%@ Control Language="C#" AutoEventWireup="true" CodeFile="~/CMSModules/AbuseReport/Controls/AbuseReportStatusEdit.ascx.cs"
    Inherits="CMSModules_AbuseReport_Controls_AbuseReportStatusEdit" %>
<div>
    <cms:LocalizedLabel ID="lblSaved" runat="server" ResourceString="general.changessaved"
        Visible="false" CssClass="FieldLabel" EnableViewState="false" />
    <cms:LocalizedLabel ID="lblError" runat="server" ResourceString="abuse.errors.comment"
        Visible="false" CssClass="ErrorLabel" EnableViewState="false" />
</div>
<table>
    <tr>
        <td class="FieldLabel">
            <cms:LocalizedLabel ID="lblTitle" runat="server" ResourceString="general.title" DisplayColon="true"
                EnableViewState="false" />
        </td>
        <td class="FieldValue">
            <asp:Label ID="lblTitleValue" runat="server" EnableViewState="false" />
        </td>
    </tr>
    <tr>
        <td class="FieldLabel">
            <cms:LocalizedLabel ID="lblUrl" runat="server" ResourceString="general.url" DisplayColon="true"
                EnableViewState="false" />
        </td>
        <td class="FieldValue">
            <asp:HyperLink ID="lnkUrlValue" runat="server" EnableViewState="false" />
        </td>
    </tr>
    <tr>
        <td class="FieldLabel">
            <cms:LocalizedLabel ID="lblCulture" runat="server" ResourceString="general.culture"
                DisplayColon="true" EnableViewState="false" />
        </td>
        <td class="FieldValue">
            <asp:Label ID="lblCultureValue" runat="server" EnableViewState="false" />
        </td>
    </tr>
    <tr>
        <td class="FieldLabel">
            <cms:LocalizedLabel ID="lblObjectType" runat="server" ResourceString="abuse.objecttype"
                DisplayColon="true" EnableViewState="false" />
        </td>
        <td class="FieldValue">
            <cms:LocalizedLabel ID="lblObjectTypeValue" runat="server" EnableViewState="false" />
        </td>
    </tr>
    <tr>
        <td class="FieldLabel">
            <cms:LocalizedLabel ID="lblObjectName" runat="server" ResourceString="abuse.objectname"
                DisplayColon="true" EnableViewState="false" />
        </td>
        <td class="FieldValue">
            <cms:LocalizedLabel ID="lblObjectNameValue" runat="server" EnableViewState="false" />
        </td>
    </tr>
    <tr>
        <td class="FieldLabel">
            <cms:LocalizedLabel ID="lblReportedBy" runat="server" ResourceString="abuse.reportedby"
                DisplayColon="true" EnableViewState="false" />
        </td>
        <td class="FieldValue">
            <cms:LocalizedLabel ID="lblReportedByValue" runat="server" EnableViewState="false" />
        </td>
    </tr>
    <tr>
        <td class="FieldLabel">
            <cms:LocalizedLabel ID="lblReportedWhen" runat="server" ResourceString="abuse.reportedwhen"
                DisplayColon="true" EnableViewState="false" />
        </td>
        <td class="FieldValue">
            <cms:LocalizedLabel ID="lblReportedWhenValue" runat="server" EnableViewState="false" />
        </td>
    </tr>
    <tr>
        <td class="FieldLabel">
            <cms:LocalizedLabel ID="lblSite" runat="server" ResourceString="general.site" DisplayColon="true"
                EnableViewState="false" />
        </td>
        <td class="FieldValue">
            <asp:Label ID="lblSiteValue" runat="server" EnableViewState="false" />
        </td>
    </tr>
    <tr>
        <td class="FieldLabel">
            <cms:LocalizedLabel ID="lblStatus" runat="server" ResourceString="abuse.status" DisplayColon="true"
                EnableViewState="false" />
        </td>
        <td class="FieldValue">
            <cms:LocalizedDropDownList ID="drpStatus" runat="server" DataTextField="name" DataValueField="number"
                CssClass="DropDownField" AutoPostBack="false" />
        </td>
    </tr>
    <tr>
        <td class="FieldLabel" style="vertical-align: top;">
            <cms:LocalizedLabel ID="lblComment" runat="server" ResourceString="abuse.comment"
                DisplayColon="true" EnableViewState="false" />
        </td>
        <td class="FieldValue">
            <cms:CMSTextBox ID="txtCommentValue" runat="server" TextMode="MultiLine" CssClass="TextAreaField"
                Width="600" Height="300" MaxLength="1000" EnableViewState="false" />
            <cms:CMSRequiredFieldValidator ID="rfvText" runat="server" ControlToValidate="txtCommentValue"
                ValidationGroup="RequiredAbuse" EnableViewState="false" />
        </td>
    </tr>
    <asp:PlaceHolder ID="pnlLink" runat="server" Visible="false">
        <tr>
            <td>
            </td>
            <td class="FieldLabelHigh">
                <cms:LocalizedHyperlink ID="lnkShowDetails" runat="server" ResourceString="abuse.details"
                    NavigateUrl="AbuseReport_Edit.aspx" Target="_blank" Visible="false" EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabelHigh" colspan="2">
                &nbsp;
            </td>
        </tr>
    </asp:PlaceHolder>
    <tr>
        <td class="FieldLabel">
        </td>
        <td class="FieldValue">
            <cms:LocalizedButton ID="btnOk" runat="server" ResourceString="general.OK" ValidationGroup="RequiredAbuse"
                CssClass="SubmitButton" EnableViewState="false" />
        </td>
    </tr>
</table>
