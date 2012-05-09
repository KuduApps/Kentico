<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_WebFarm_Pages_WebFarm_Frameset"
    CodeFile="WebFarm_Frameset.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Administration - WebFarm</title>
</head>
<frameset border="0" rows="<%= TabsFrameHeight %>, *">
    <frame name="header" src="WebFarm_Header.aspx" scrolling="no" frameborder="0" />
    <frame name="content" src="WebFarm_Server_List.aspx" scrolling="auto" frameborder="0" />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
