<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_OnlineMarketing_Pages_Tools_MVTest_Frameset"
    CodeFile="Frameset.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>MVT test properties</title>

    <script type="text/javascript">
        function updateTabHeader() {
            frames['header'].updateTabHeader();
        }

    </script>

</head>
<frameset border="0" rows="<%= TabsFrameHeight %>, *" id="rowsFrameset">
    <frame name="header" src="Header.aspx<%= Request.Url.Query %>" scrolling="no" frameborder="0"
        noresize="noresize" />
    <frame name="content" src="Overview.aspx<%= Request.Url.Query %>" frameborder="0" runat="server" ID="frmContent" />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
