<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_WebAnalytics_Tools_Default"
    CodeFile="Default.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Analytics</title>

    <script type="text/javascript">
        function selectTreeNode(nodeName) {
            frames['analyticsTree'].selectTreeNode(nodeName);
        }
    </script>

</head>
<frameset id="rowsFrameset" runat="server" border="0" rows="38,*">
    <frame name="webAnalyticsHeader" scrolling="no" frameborder="0" src="Header.aspx" />
    <frameset border="0" cols="295,*" id="colsFramesetAnalytics" runat="server">
        <frame name="analyticsTree" ID="analyticsTree" runat="server" scrolling="no" frameborder="0" />
        <frame name="analyticsDefault" ID="analyticsDefault" runat="server" />
        <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
    </frameset>
</frameset>
</html>
