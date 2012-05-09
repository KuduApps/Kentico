<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Content_CMSDesk_View_Preview"
    CodeFile="Preview.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server" enableviewstate="false">
    <title>Content - Preview</title>

    <script type="text/javascript">
        //<![CDATA[
        var IsCMSDesk = true;

        // Refresh tree
        function RefreshTree(expandNodeId, selectNodeId) {
            if (parent.RefreshTree) {
                parent.RefreshTree(expandNodeId, selectNodeId);
            }
        }
        //]]>
    </script>

</head>
<frameset border="0" rows="<%=headersize%>,*" border="0" id="previewframeset">
    <frame name="previewmenu" src="previewmenu.aspx<%=Request.Url.Query%>" scrolling="no"
        noresize="noresize" frameborder="0" />
    <frame name="previewview" src="<%=viewpage%>" scrolling="auto" noresize="noresize"
        frameborder="0" />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
