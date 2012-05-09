<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_Forums_Controls_Posts_PostApprove"
    CodeFile="PostApprove.ascx.cs" %>
    
<div class="ForumPostApprove">
    <table>
        <tr>
            <td class="ItemLabel">
                <cms:localizedlabel id="lblUserTitle" runat="server" displaycolon="true" resourcestring="general.user" />
            </td>
            <td class="Post">
                <asp:label id="lblUser" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="ItemLabel">
                <cms:localizedlabel id="lblSubjectTitle" runat="server" displaycolon="true" resourcestring="general.subject" />
            </td>
            <td class="Post">
                <asp:label id="lblSubject" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="ItemLabel">
                <cms:localizedlabel id="lblDateTitle" runat="server" displaycolon="true" resourcestring="general.date" />
            </td>
            <td class="Post">
                <asp:label id="lblDate" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="ItemLabel">
                <cms:localizedlabel id="lblTextTitle" runat="server" displaycolon="true" resourcestring="general.text" />
            </td>
            <td class="Post">
                <div class="PostText">
                    <cms:resolvedliteral id="ltrText" runat="server" />
                </div>
            </td>
        </tr>
    </table>
</div>
