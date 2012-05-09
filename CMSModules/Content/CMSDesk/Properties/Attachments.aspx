<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Content_CMSDesk_Properties_Attachments"
    Theme="Default" CodeFile="Attachments.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/PageElements/Help.ascx" TagName="Help" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Content/Controls/Attachments/DocumentAttachments/DocumentAttachmentsList.ascx"
    TagName="Attachments" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Content/Controls/editmenu.ascx" TagName="editmenu"
    TagPrefix="cms" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Attachments</title>
    <style type="text/css">
        body
        {
            margin: 0px;
            padding: 0px;
            height: 100%;
        }
    </style>

    <script type="text/javascript">
        //<![CDATA[
        function PassiveRefresh() {
            document.location.replace(document.location);
        }

        function RefreshTree(expandNodeId, selectNodeId) {
            // Update tree
            parent.RefreshTree(expandNodeId, selectNodeId);
        }

        function FramesRefresh(refreshTree, selectedNodeId) {
            PassiveRefresh();

            if (refreshTree) {
                RefreshTree(selectedNodeId, selectedNodeId);
            }
        }

        //]]>
    </script>

</head>
<body class="VerticalTabsBody <%=mBodyClass%>">
    <form id="form1" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="ucScriptManager" runat="server" />
    <asp:Panel ID="pnlBody" runat="server" CssClass="VerticalTabsPageBody">
        <asp:Panel ID="pnlContent" runat="server" CssClass="ContentEditMenu">
            <table width="100%">
                <tr>
                    <td>
                        <div style="height: 24px; padding: 5px;">
                            <cms:editmenu ID="menuElem" runat="server" ShowApprove="true" ShowReject="true" ShowSubmitToApproval="true"
                                ShowProperties="false" ShowSave="false" EnablePassiveRefresh="true" />
                        </div>
                    </td>
                    <td class="TextRight">
                        <cms:Help ID="helpElem" runat="server" TopicName="doc_attachments" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlAttachments" runat="server" CssClass="UnsortedInfoPanel">
            <asp:Label ID="lblInfo" runat="server" CssClass="InfoLabel" EnableViewState="false" />
        </asp:Panel>
        <div class="Unsorted">
            <cms:Attachments ID="ucAttachments" runat="server" IsLiveSite="false" InnerDivClass="NewAttachment"
                InnerLoadingDivClass="NewAttachmentLoading" />
            <cms:CMSButton ID="btnRefresh" runat="server" EnableViewState="true" CssClass="HiddenButton"
                OnClick="btnRefresh_Click" />
        </div>
    </asp:Panel>
    </form>
</body>
</html>
