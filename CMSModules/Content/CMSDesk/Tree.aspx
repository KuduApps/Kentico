<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Content_CMSDesk_Tree"
    EnableEventValidation="false" Theme="Default" CodeFile="Tree.aspx.cs" %>

<%@ Register Src="../Controls/TreeContextMenu.ascx" TagName="TreeContextMenu" TagPrefix="cms" %>
<%@ Register Src="../Controls/ContentTree.ascx" TagName="ContentTree" TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/Trees/TreeBorder.ascx" TagName="TreeBorder"
    TagPrefix="cms" %>
<%@ Register TagPrefix="cms" Namespace="CMS.ExtendedControls.DragAndDrop" Assembly="CMS.ExtendedControls" %>

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

<body class="TreeBody <%=mBodyClass%>" onunload="Maximize();" onload="InitFocus();"
    oncontextmenu="return false;">    

    <form id="form1" runat="server">
    <ajaxToolkit:ToolkitScriptManager runat="server" ID="manScript" EnableViewState="false"
        ScriptMode="Release" />
    <asp:Panel runat="server" ID="pnlBody" CssClass="ContentTree" EnableViewState="false">
        <cms:treeborder id="borderElem" runat="server" minsize="10,*" framesetname="colsFrameset" />
        <asp:Panel class="TreeArea" onclick="HideAllContextMenus();" ID="pnlTreeArea" runat="server">
            <cms:contextmenu id="menuNode" runat="server" menuid="nodeMenu" verticalposition="Bottom"
                horizontalposition="Left" offsetx="4" offsety="-2" activeitemcssclass="TreeContextActiveNode">
                <cms:TreeContextMenu ID="contextMenu" runat="server" />
            </cms:contextmenu>
            <div class="TreeAreaTree">
                <div id="pnlRefresh" name="pnlRefresh" class="RefreshTreeIconContainer" onmouseover="ShowRefreshIcon()" onmouseout="HideRefreshIcon()">
                    <span>
                        <asp:Image ID="imgRefresh" runat="server" EnableViewState="false" BorderWidth="1" CssClass="RefreshTreeIcon" />
                    </span>
                </div>
                <cms:contenttree id="treeContent" runat="server" allowdraganddrop="true" ShortID="t" IsLiveSite="false" />
            </div>
        </asp:Panel>
    </asp:Panel>

    <script type="text/javascript">
        //<![CDATA[
        var currentNode = document.getElementById('treeSelectedNode');
        //]]>
    </script>

    <input type="hidden" id="hdnAction" name="hdnAction" />
    <input type="hidden" id="hdnParam1" name="hdnParam1" />
    <input type="hidden" id="hdnParam2" name="hdnParam2" />
    <input type="hidden" id="hdnScroll" name="hdnScroll" />
    </form>
</body>
</html>