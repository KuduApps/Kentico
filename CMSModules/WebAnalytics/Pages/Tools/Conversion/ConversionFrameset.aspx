<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConversionFrameset.aspx.cs" Inherits="CMSModules_WebAnalytics_Pages_Tools_Conversion_ConversionFrameset" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server" enableviewstate="false">
    <title>Ab test properties</title>
</head>
<frameset border="0" rows="<%= TabsFrameHeight %>, *" id="rowsFrameset">
    <frame name="header"  scrolling="no" ID="frmHeader" runat="server"
        frameborder="0" noresize="noresize" />
    <frame name="content"  frameborder="0" runat="server"  ID="frmContent" />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>