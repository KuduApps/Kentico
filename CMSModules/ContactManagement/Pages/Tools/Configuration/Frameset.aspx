<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Frameset.aspx.cs" 
    Inherits="CMSModules_ContactManagement_Pages_Tools_Configuration_Frameset" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server" enableviewstate="false">
    <title>Content Management</title>
</head>
<frameset border="0" cols="126, *" frameborder="0" runat="server" id="colsFrameset">
    <frame name="configurationMenu" src="Header.aspx"  scrolling="no" noresize="noresize"
        frameborder="0" runat="server" id="configurationMenu" />
    <frame name="configurationContent" src="AccountStatus/List.aspx" noresize="noresize"
        frameborder="0" runat="server" scrolling="auto" id="configurationContent" />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>