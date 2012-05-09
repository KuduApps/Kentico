<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Support_Pages_leftmenu"
    Theme="Default" CodeFile="leftmenu.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/Trees/TreeBorder.ascx" TagName="TreeBorder"
    TagPrefix="cms" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Support - Menu</title>
    <style type="text/css">
        body
        {
            margin: 0px;
            padding: 0px;
            height: 100%;
        }
    </style>
</head>
<body class="TreeBody <%=mBodyClass%>">
    <form id="form1" runat="server">
    <asp:Panel runat="server" ID="pnlBody" CssClass="ContentTree">
        <cms:TreeBorder ID="borderElem" runat="server" MinSize="10,*" FramesetName="colsFrameset" />
        <div class="TreeArea">
            <div class="TreeAreaTree">
                <asp:TreeView ID="TreeViewAdministration" runat="server" ShowLines="true" ShowExpandCollapse="true"
                    CssClass="ContentTree" EnableViewState="false" />
            </div>
        </div>
    </asp:Panel>
    </form>

    <script type="text/javascript">
        //<![CDATA[
        var currentNode = document.getElementById('treeSelectedNode');

        function ShowDesktopContent(contentUrl, nodeElem, fullContent) {
            if (fullContent) {
                window.top.location.href = contentUrl;
            } else {
                if ((currentNode != null) && (nodeElem != null)) {
                    currentNode.className = 'ContentTreeItem';
                }

                parent.frames['frameMain'].location.href = contentUrl;

                spans = document.getElementsByTagName("span");
                for (var s = 0; s < spans.length; s++) {
                    if (spans[s].className == 'ContentTreeSelectedItem') {
                        spans[s].className = 'ContentTreeItem';
                    }
                }

                if (nodeElem != null) {
                    currentNode = nodeElem;
                    currentNode.className = 'ContentTreeSelectedItem';
                }
            }
        }


        function SelectNode(elementName) {
            // Set selected item in tree
            $j('span[name=treeNode]').each(function() {
                var jThis = $j(this);
                jThis.removeClass('ContentTreeSelectedItem');
                if (!jThis.hasClass('ContentTreeItem')) {
                    jThis.addClass('ContentTreeItem');
                }
                if (this.id == 'node_' + elementName) {
                    jThis.addClass('ContentTreeSelectedItem');
                }
            });
        }

        //]]>
    </script>

    <asp:Literal runat="server" ID="LiteralScript" />
</body>
</html>
