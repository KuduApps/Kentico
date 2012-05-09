<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Staging_Tools_Tasks_Tree"
    EnableEventValidation="false" Theme="Default" CodeFile="Tree.aspx.cs" %>

<%@ Register Src="~/CMSModules/Content/Controls/ContentTree.ascx" TagName="ContentTree" TagPrefix="cms" %>
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
                <cms:ContentTree ID="treeContent" runat="server" AllowMarks="false" IsLiveSite="false" />
            </div>
        </div>
        <asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />
    </asp:Panel>

    <script type="text/javascript">
        //<![CDATA[
        var currentNode = document.getElementById(currentNodeId);

        // Refresh node action
        function RefreshNode(nodeId, selectNodeId) {
            if (selectNodeId == null) {
                selectNodeId = currentNodeId;
            }
            document.location.replace(treeUrl + "?expandnodeid=" + nodeId + "&nodeid=" + selectNodeId);
        }

        // Highlight selected node
        function SelectTree(nodeId) {
            if ((currentNode != null) && (nodeId != null)) {
                currentNode.className = 'ContentTreeItem';
            }

            if (nodeId != null) {
                currentNode = document.getElementById(nodeId);
                if (currentNode != null) {
                    currentNode.className = 'ContentTreeSelectedItem';
                }
            }
        }

        // Select node action
        function SelectNode(nodeId, selectMore) {
            SelectTree(nodeId);
            var selectDocuments = parent.frames['tasksHeader'].selectDocuments;
            var completeObj = parent.frames['tasksHeader'].document.getElementById('pnlComplete');
            if (selectDocuments || selectMore) {
                parent.frames['tasksHeader'].SelectDocNode(nodeId);
                parent.frames['tasksHeader'].selectDocuments = true;
                if (completeObj != null) {
                    completeObj.style.display = 'none';
                }
            }
            else {
                parent.frames['tasksHeader'].SelectNode(nodeId);
                if (completeObj != null) {
                    completeObj.style.display = 'block';
                }
            }
            currentNodeId = nodeId;
        }
        //]]>
    </script>

    </form>
</body>
</html>
