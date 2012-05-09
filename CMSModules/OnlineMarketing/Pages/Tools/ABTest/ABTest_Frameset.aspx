<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ABTest_Frameset.aspx.cs"
    Inherits="CMSModules_OnlineMarketing_Pages_Tools_AbTest_ABTest_Frameset" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server" enableviewstate="false">
    <title>Ab test properties</title>

    <script type="text/javascript">
        function updateTabHeader() {
            frames['header'].updateTabHeader();
        }

    </script>
</head>
<frameset border="0" rows="<%= TabsFrameHeight %>, *" id="rowsFrameset">
    <frame name="header" src="ABTest_Header.aspx<%= Request.Url.Query %>" scrolling="no"
        frameborder="0" noresize="noresize" />
    <frame name="content"  frameborder="0" runat="server" ID="frmContent" />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
