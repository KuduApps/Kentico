<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_Forums_Controls_Posts_PostView"
    CodeFile="PostView.ascx.cs" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/PageElements/PageTitle.ascx" TagName="PageTitle"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Forums/Controls/Posts/ForumPost.ascx" TagName="ForumPost"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Forums/Controls/Posts/PostEdit.ascx" TagName="PostEdit"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/Silverlight/MultiFileUploader/MultiFileUploader.ascx"
    TagPrefix="cms" TagName="MultiFileUploader" %>

<asp:PlaceHolder ID="plcManager" runat="server" />
<asp:Panel ID="PanelNewThread" runat="server" Visible="false" CssClass="PageBody">
    <cms:PageTitle ID="NewThreadTitle" runat="server" />
    <div class="ForumFlat">
        <cms:PostEdit ID="PostEdit1" runat="server" />
    </div>
</asp:Panel>
<asp:Panel ID="pnlBody" runat="server" CssClass="PageBody">
    <asp:Panel ID="pnlTitle" runat="server" CssClass="PageHeader">
        <cms:PageTitle ID="PostTitle" runat="server" />
    </asp:Panel>
    <asp:Panel ID="pnlListing" runat="server" Visible="false">
        <asp:HyperLink ID="lnkBackListing" runat="server" />
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlMenu" CssClass="ContentEditMenu">
        <table class="PostMenuTable" style="padding-top: 5px;" cellpadding="0" cellspacing="0">
            <tr>
                <td class="MenuItemEditSmall">
                    <asp:HyperLink runat="server" ID="lnkEditImg" /><asp:HyperLink runat="server" ID="lnkEdit" />
                </td>
                <td class="MenuItemEditSmall">
                    <asp:HyperLink runat="server" ID="lnkDeleteImg" /><asp:HyperLink runat="server" ID="lnkDelete" />
                </td>
                <td class="MenuItemEditSmall">
                    <asp:HyperLink runat="server" ID="lnkReplyImg" /><asp:HyperLink runat="server" ID="lnkReply" />
                </td>
                <asp:PlaceHolder ID="plcRoot" runat="server">
                    <td class="MenuItemEditSmall">
                        <asp:HyperLink runat="server" ID="lnkStickImg" /><asp:HyperLink runat="server" ID="lnkStick" />
                    </td>
                    <td class="MenuItemEditSmall">
                        <asp:HyperLink runat="server" ID="lnkLockImg" /><asp:HyperLink runat="server" ID="lnkLock" />
                    </td>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="plcSplit" runat="server">
                    <td class="MenuItemEditSmall">
                        <asp:HyperLink runat="server" ID="lnkSplitImg" /><asp:HyperLink runat="server" ID="lnkSplit" />
                    </td>
                </asp:PlaceHolder>
                <td class="MenuItemEditSmall">
                    <asp:HyperLink runat="server" ID="lnkApproveRejectImg" /><asp:HyperLink runat="server"
                        ID="lnkApproveReject" />
                </td>
                <td class="MenuItemEditSmall">
                    <asp:HyperLink runat="server" ID="lnkApproveSubImg" /><asp:HyperLink runat="server"
                        ID="lnkApproveSub" />
                </td>
                <td class="MenuItemEditSmall">
                    <asp:HyperLink runat="server" ID="lnkMoveThreadImg" /><asp:HyperLink runat="server"
                        ID="lnkMoveThread" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlMove" Visible="false" CssClass="ForumMoveThreadArea">
        <asp:PlaceHolder runat="server" ID="plcThreadMove"></asp:PlaceHolder>
    </asp:Panel>
    <asp:Panel ID="pnlContent" runat="server" CssClass="PageContent" Visible="true">
        <div class="ForumFlat">
            <cms:ForumPost ID="ForumPost1" runat="server" EnableSignature="true" />
        </div>
        <br />
        <cms:CMSButton ID="btnDelete" runat="server" CssClass="HiddenButton" EnableViewState="false"
            OnClick="btnDelete_Click" />
        <cms:CMSButton ID="btnApprove" runat="server" CssClass="HiddenButton" EnableViewState="false"
            OnClick="btnApprove_Click" />
        <cms:CMSButton ID="btnApproveSubTree" runat="server" CssClass="HiddenButton" EnableViewState="false"
            OnClick="btnApproveSubTree_Click" />
        <cms:CMSButton ID="btnRejectSubTree" runat="server" CssClass="HiddenButton" EnableViewState="false"
            OnClick="btnRejectSubTree_Click" />
        <cms:CMSButton ID="btnStickThread" runat="server" CssClass="HiddenButton" EnableViewState="false"
            OnClick="btnStickThread_Click" />
        <cms:CMSButton ID="btnSplitThread" runat="server" CssClass="HiddenButton" EnableViewState="false"
            OnClick="btnSplitThread_Click" />
        <cms:CMSButton ID="btnLockThread" runat="server" CssClass="HiddenButton" EnableViewState="false"
            OnClick="btnLockThread_Click" />
        <cms:CMSButton runat="server" ID="btnMoveThread" CssClass="HiddenButton" EnableViewState="false"
            OnClick="btnMoveThread_Click" />
    </asp:Panel>
    <asp:Panel ID="pnlAttachmentTitle" runat="server" CssClass="PageHeader">
        <cms:PageTitle ID="PostAttachmentTitle" runat="server" TitleCssClass="SubTitleHeader" />
    </asp:Panel>
    <asp:Panel ID="pnlAttachmentContent" runat="server" CssClass="PageContent" Visible="true">
        <table cellpadding="0" cellspacing="0" class="ForumUploadTable" style="margin-bottom: 10px;">
            <tr>
                <td>
                    <div class="Uploader" style="border-width: 0px; width: 400px;">
                        <cms:LocalizedLiteral runat="server" ID="ltrUpload" ResourceString="general.upload"
                            DisplayColon="true" />
                        <cms:CMSFileUpload runat="server" ID="upload" />
                        <cms:LocalizedButton ResourceString="general.upload" OnClick="btnUpload_Click" ValidationGroup="NewPostforum"
                            runat="server" ID="btnUpload" CssClass="ContentButton" />
                    </div>
                </td>
            </tr>
        </table>
        <asp:Label runat="server" CssClass="ErrorLabel" ID="lblError" EnableViewState="false"
            Visible="false" />
        <cms:UniGrid runat="server" ID="UniGrid" GridName="~/CMSModules/Forums/Controls/Posts/AttachmentList.xml"
            Visible="false" OrderBy="AttachmentFileName" />
    </asp:Panel>
</asp:Panel>
<asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />
