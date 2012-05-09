<%@ Page Language="C#" AutoEventWireup="true" Title="Content personalization variant list"
    Inherits="CMSModules_OnlineMarketing_Pages_Content_ContentPersonalizationVariant_List" Theme="Default" CodeFile="List.aspx.cs" %>
<%@ Register Src="~/CMSModules/OnlineMarketing/Controls/UI/ContentPersonalizationVariant/List.ascx" TagName="ContentPersonalizationVariantList" TagPrefix="cms" %>
<%@ Register src="~/CMSAdminControls/UI/PageElements/Help.ascx" tagname="Help" tagprefix="cms" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server" enableviewstate="false">
    <title>Content personalization variants</title>
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
    <asp:Panel ID="pnlBody" runat="server" CssClass="VerticalTabsPageBody">
        <asp:Panel ID="pnlMenu" runat="server" CssClass="ContentEditMenu">
            <table width="100%">
                <tr>
                    <td>
                        <div style="height:24px;padding: 5px;"></div>
                    </td>
                    <td class="TextRight">
                        <cms:Help ID="helpElem" runat="server" TopicName="personalization_variants" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
            <asp:Panel runat="server" ID="pnlWarning" Visible="false" CssClass="PageHeaderLine">
        <asp:Label runat="server" ID="lblWarning"  />
    </asp:Panel>
        <asp:Panel ID="pnlContent" runat="server" CssClass="PageContent">
            <asp:Label ID="lblInfo" runat="server" CssClass="InfoLabel" EnableViewState="false" />
            <asp:Panel ID="pnlLanguages" runat="server" CssClass="Languages">
                <cms:ContentPersonalizationVariantList ID="listElem" runat="server" IsLiveSite="false" />
            </asp:Panel>
        </asp:Panel>
    </asp:Panel>
    </form>
</body>
</html>
