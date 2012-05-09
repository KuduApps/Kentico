<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Support_Pages_Default"
    CodeFile="Default.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Site Manager</title>
</head>
<frameset border="0" cols="225,*" runat="server" id="colsFrameset" enableviewstate="false"
    framespacing="0">
    <frame name="supporttree" frameborder="0" scrolling="no" runat="server" id="frameTree"
        class="TreeFrame" />
    <frame name="frameMain" src="support.aspx" frameborder="0" runat="server" id="frameMain" />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
