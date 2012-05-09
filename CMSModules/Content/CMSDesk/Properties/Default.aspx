<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Content_CMSDesk_Properties_Default"
    CodeFile="Default.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Content - Properties</title>

    <script type="text/javascript">
        //<![CDATA[
        if ((parent.frames['contenteditheader'] != null) && parent.frames['contenteditheader'].SetTabsContext) {
            parent.frames['contenteditheader'].SetTabsContext('properties');
        }
        else {
            parent.SetTabsContext('properties');
        }

        function RefreshTree(expandNodeId, selectNodeId) {
            // Update tree
            parent.RefreshTree(expandNodeId, selectNodeId);
        }

        function SelectNode(nodeId) {
            parent.parent.frames['contentmenu'].SelectNode(nodeId);
        }

        //]]>
    </script>

</head>
<frameset border="0" cols="126,*" runat="server" id="colsFrameset" enableviewstate="false">
    <frame name="propheader" scrolling="no" noresize="noresize" frameborder="0" runat="server"
        id="frameHeader" />
    <frame name="propedit" noresize="noresize" frameborder="0" scrolling="auto" runat="server"
        id="frameEdit" />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
