<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Blogs_Controls_BlogCommentDetail" CodeFile="BlogCommentDetail.ascx.cs" %>
<%@ Register Src="~/CMSAdminControls/UI/UserPicture.ascx" TagName="UserPicture" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/AbuseReport/Controls/InlineAbuseReport.ascx" TagName="InlineAbuseReport"
    TagPrefix="cms" %>
<div class="CommentDetail">
    <table width="100%">
        <tr>
            <td rowspan="4">
                <cms:UserPicture ID="userPict" runat="server" EnableViewState="false" />
            </td>
            <td style="width: 100%">
                <asp:Label ID="lblName" runat="server" CssClass="CommentUserName" EnableViewState="false" />
                <asp:HyperLink ID="lnkName" runat="server" CssClass="CommentUserName" EnableViewState="false"
                    Target="_blank" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblText" runat="server" CssClass="CommentText" EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblDate" runat="server" CssClass="CommentDate" EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td align="right">
                <div class="buttonpedding">
                    <asp:LinkButton ID="lnkEdit" Visible="false" runat="server" EnableViewState="false"
                        CssClass="CommentAction" />
                    <asp:LinkButton ID="lnkDelete" Visible="false" runat="server" CssClass="CommentAction"
                        OnClick="lnkDelete_Click" EnableViewState="false" OnClientClick="return ConfirmDelete();" />
                    <asp:LinkButton ID="lnkApprove" Visible="false" runat="server" CssClass="CommentAction"
                        OnClick="lnkApprove_Click" EnableViewState="false" />
                    <asp:LinkButton ID="lnkReject" Visible="false" runat="server" CssClass="CommentAction"
                        OnClick="lnkReject_Click" EnableViewState="false" />
                    <cms:InlineAbuseReport ID="ucInlineAbuseReport" ReportObjectType="blog.comment" runat="server" />
                </div>
            </td>
        </tr>
    </table>
</div>

<script type="text/javascript">
    //<![CDATA[
    // Opens modal dialog with comment edit page
    function EditComment(editPageUrl) {
        modalDialog(editPageUrl, "CommentEdit", 500, 440);
    }
    //]]>
</script>

