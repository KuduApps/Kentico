<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AbuseReport.ascx.cs" Inherits="CMSModules_ContactManagement_Controls_UI_ActivityDetails_AbuseReport" %>
<table>
    <tr>
        <td class="ActivityDetailsLabel">
            <cms:LocalizedLabel runat="server" ID="lblDoc" ResourceString="om.activitydetails.documenturl"
                EnableViewState="false" DisplayColon="true" />
        </td>
        <td>
            <asp:Label runat="server" ID="lblDocIDVal" />
        </td>
    </tr>
    <tr>
        <td class="ActivityDetailsLabel" style="vertical-align: top;">
            <cms:LocalizedLabel runat="server" ID="lblComment" ResourceString="om.activitydetails.abusecomment"
                EnableViewState="false" DisplayColon="true" />
        </td>
        <td>
            <cms:CMSTextBox runat="server" ID="txtComment" TextMode="MultiLine" CssClass="ActivityDetailsCommentBox"
                ReadOnly="true" />
        </td>
    </tr>
</table>
