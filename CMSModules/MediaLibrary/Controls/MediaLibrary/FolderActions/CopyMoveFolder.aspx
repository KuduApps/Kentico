<%@ Page Language="C#" AutoEventWireup="true" Theme="Default" Inherits="CMSModules_MediaLibrary_Controls_MediaLibrary_FolderActions_CopyMoveFolder"
    CodeFile="CopyMoveFolder.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/PageElements/PageTitle.ascx" TagName="PageTitle"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/AsyncControl.ascx" TagName="AsyncControl" TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/AsyncBackground.ascx" TagName="AsyncBackground"
    TagPrefix="cms" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Media Library Copy Move</title>

    <script type="text/javascript" language="javascript">
        //<![CDATA[
        // Initialize gray overlay in dialog
        function InitializeLog() {
            var content = window.top.frames['selectFolderContent'];
            var footer = window.top.frames['selectFolderFooter'];
            if (content != null) {
                var header = content.document.getElementById('selectFolder_pnlBody');
                if (header != null) {
                    var headerOverlay = content.document.createElement('DIV');
                    headerOverlay.id = 'headerOverlay';
                    headerOverlay.style.zIndex = '2500';
                    headerOverlay.style.height = header.offsetHeight + 'px';
                    headerOverlay.className = 'AsyncLogBackground';
                    content.document.body.insertBefore(headerOverlay, content.document.body.firstChild);
                }
            }
            if (footer != null) {
                var footerOverlay = footer.document.createElement('DIV');
                footerOverlay.id = 'footerOverlay';
                footerOverlay.style.zIndex = '2500';
                footerOverlay.className = 'AsyncLogBackground';
                footer.document.body.insertBefore(footerOverlay, footer.document.body.firstChild);
            }
            if (window.parent.expandIframe) {
                window.parent.expandIframe();
            }
        }

        // Remove gray overlay in dialog
        function DestroyLog() {
            var content = window.top.frames['selectFolderContent'];
            var footer = window.top.frames['selectFolderFooter'];
            if (content != null) {
                var header = content.document.getElementById('selectFolder_pnlBody');
                if (header != null) {
                    var headerOverlay = content.document.getElementById('headerOverlay');
                    if (headerOverlay != null) {
                        content.document.body.removeChild(headerOverlay);
                    }
                }
            }
            if (footer != null) {
                var footerOverlay = footer.document.getElementById('footerOverlay');
                if (footerOverlay != null) {
                    footer.document.body.removeChild(footerOverlay);
                }
            }
            if (window.parent.collapseIframe) {
                window.parent.collapseIframe();
            }
        }
        //]]>
    </script>

</head>
<body class="<%= mBodyClass %>">
    <form id="form1" runat="server">
    <div>
        <cms:FileSystemDataSource ID="fileSystemDataSource" runat="server" />
        <asp:Panel runat="server" ID="pnlLog" Visible="false">
            <cms:AsyncBackground ID="backgroundElem" runat="server" />
            <div class="AsyncLogArea" style="height: 88%;">
                <div>
                    <asp:Panel ID="pnlAsyncBody" runat="server" CssClass="PageBody">
                        <asp:Panel ID="pnlTitleAsync" runat="server" CssClass="PageHeader">
                            <cms:PageTitle ID="titleElemAsync" runat="server" />
                        </asp:Panel>
                        <asp:Panel ID="pnlCancel" runat="server" CssClass="PageHeaderLine">
                            <cms:CMSButton runat="server" ID="btnCancel" CssClass="SubmitButton" />
                        </asp:Panel>
                        <asp:Panel ID="pnlAsyncContent" runat="server" CssClass="PageContent">
                            <cms:AsyncControl ID="ctlAsync" runat="server" />
                        </asp:Panel>
                    </asp:Panel>
                </div>
            </div>
        </asp:Panel>
        <div id="ContentDiv">
            <asp:Panel runat="server" ID="pnlEmpty" Visible="true" CssClass="DialogInfoArea">
                <asp:Label runat="server" ID="lblEmpty" EnableViewState="false" />
            </asp:Panel>
            <asp:Panel runat="server" ID="pnlInfo" CssClass="DialogInfoArea">
                <asp:Label ID="lblError" runat="server" EnableViewState="false" CssClass="ErrorLabel"
                    Visible="false" />
                <table style="width: 100%;">
                    <tr>
                        <td style="white-space: nowrap;" class="FolderEditLabelArea">
                            <strong>
                                <cms:LocalizedLabel ID="lblFolderName" runat="server" CssClass="FieldLabel" DisplayColon="true"
                                    ResourceString="media.folder.targetfolder" EnableViewState="false"></cms:LocalizedLabel>
                            </strong>
                        </td>
                        <td style="width: 100%;">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="white-space: nowrap;" class="FolderEditLabelArea">
                            <asp:Label ID="lblFolder" runat="server" CssClass="FieldLabel" EnableViewState="false" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="white-space: nowrap;" class="FolderEditLabelArea">
                            <strong>
                                <cms:LocalizedLabel ID="lblFilesToCopy" runat="server" CssClass="FieldLabel" DisplayColon="true"
                                    ResourceString="media.folder.filestocopy" EnableViewState="false" />
                            </strong>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="white-space: nowrap;" class="FolderEditLabelArea">
                            <div class="ScrollableList">
                                <asp:Label ID="lblFileList" runat="server" CssClass="FieldLabel" EnableViewState="false" />
                            </div>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Literal runat="server" ID="ltlScript" EnableViewState="false" />
        </div>
    </div>
    </form>
</body>
</html>
