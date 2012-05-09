<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Settings_SiteManager_Default"
    CodeFile="Default.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server" enableviewstate="false">
    <title>Site Manager - Settings</title>

    <script type="text/javascript">
        //<![CDATA[
        document.titlePart = 'Settings';
        //]]>
    </script>

</head>
<frameset border="0" cols="300, *" runat="server" id="colsFrameset" enableviewstate="false"
    framespacing="0">
    <frame name="categories" src="Categories.aspx" scrolling="no" frameborder="0" runat="server"
        class="TreeFrame" />
    <frame name="keys" src="Keys.aspx" frameborder="0" runat="server" />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
