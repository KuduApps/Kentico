<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ForumPost.ascx.cs" Inherits="CMSModules_ContactManagement_Controls_UI_ActivityDetails_ForumPost" %>
<table>
    <tr>
        <td class="ActivityDetailsLabel">
            <cms:LocalizedLabel runat="server" ID="lblDocID" ResourceString="om.activitydetails.documenturl"
                EnableViewState="false" DisplayColon="true" />
        </td>
        <td>
            <asp:Label runat="server" ID="lblDocIDVal" />
        </td>
    </tr>
    <asp:PlaceHolder runat="server" ID="plcComment" Visible="false" EnableViewState="false">
        <tr>
            <td class="ActivityDetailsLabel" style="vertical-align: top;">
                <cms:LocalizedLabel runat="server" ID="lblPostSubject" ResourceString="om.activitydetails.forumpostsubject"
                    EnableViewState="false" DisplayColon="true" />
            </td>
            <td>
                <asp:Label runat="server" ID="lblPostSubjectVal" />           
            </td>
        </tr>    
        <tr>
            <td class="ActivityDetailsLabel" style="vertical-align: top;">
                <cms:LocalizedLabel runat="server" ID="lblText" ResourceString="om.activitydetails.forumpost"
                    EnableViewState="false" DisplayColon="true" />
            </td>
            <td>
                <cms:CMSTextBox runat="server" ID="txtPost" TextMode="MultiLine" ReadOnly="true" CssClass="ActivityDetailsCommentBox"
                    Wrap="false" />
            </td>
        </tr>
    </asp:PlaceHolder>
</table>
