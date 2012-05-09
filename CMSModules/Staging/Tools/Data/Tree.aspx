<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Staging_Tools_Data_Tree"
    EnableEventValidation="false" Theme="Default" CodeFile="tree.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/Trees/ObjectTree.ascx" TagName="ObjectTree" TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/Trees/TreeBorder.ascx" TagName="TreeBorder" TagPrefix="cms" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Content - Tree</title>
    <style type="text/css">
        body
        {
            margin: 0px;
            padding: 0px;
            height: 100%;
            width: 100%;
            overflow: hidden;
        }
    </style>
</head>
<body class="TreeBody <%=mBodyClass%>">
    <form id="form1" runat="server">
    <asp:Panel runat="server" ID="pnlBody" CssClass="ContentTree">
        <cms:TreeBorder ID="borderElem" runat="server" MinSize="10,*" FramesetName="colsFrameset" />
        <div class="TreeArea">
            <div class="TreeAreaTree">
                <asp:TreeView ID="objectTree" runat="server" ShowLines="true" ShowExpandCollapse="true"
                    CssClass="ContentTree" EnableViewState="false" />
            </div>
        </div>
    </asp:Panel>
    <input type="hidden" id="selectedObjectType" name="selectedObjectType" value="" />

    <script type="text/javascript">
        //<![CDATA[
        var currentNode = document.getElementById('treeSelectedNode');

        // Refresh node action
        function RefreshNode(nodeId, selectNodeId, selectSiteId) {
            if (selectNodeId == null) {
                selectNodeId = currentNodeId;
            }
            if (selectSiteId == null) {
                selectSiteId = currentSiteId;
            }
            document.location.replace(treeUrl + "?objecttype=" + selectNodeId + "&siteid=" + siteId);
        }

        function SelectNode(objectType, nodeElem) {
            if ((currentNode != null) && (nodeElem != null)) {
                currentNode.className = 'ContentTreeItem';
            }

            parent.frames['tasksHeader'].SelectNode(objectType);
            document.getElementById('selectedObjectType').value = objectType;

            if (nodeElem != null) {
                currentNode = nodeElem;
                currentNode.className = 'ContentTreeSelectedItem';
            }
        }

        //]]>
    </script>

    </form>
</body>
</html>
