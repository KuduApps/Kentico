<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Groups_Tools_Forums_Posts_ForumPost_Frameset"
    CodeFile="ForumPost_Frameset.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Forums - Post frameset</title>
</head>
<frameset border="0" cols="260, *" runat="server" id="colsFrameset">
    <frame frameborder="0" scrolling="auto" runat="server" ID="frameTree" />
    <frame frameborder="0" runat="server" ID="frameEdit" />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
