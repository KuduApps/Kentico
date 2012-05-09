<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSSiteManager_Development_Default"
    CodeFile="Default.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Site Manager - Development</title>
    <asp:Literal runat="server" ID="ltlScript" EnableViewState="false" />
</head>
<frameset border="0" cols="225, *" framespacing="0" runat="server" id="colsFrameset"
    enableviewstate="false">
    <frame name="devtree" src="leftmenu.aspx" scrolling="no" frameborder="0" runat="server"
        class="TreeFrame" />
    <frame name="frameMain" src="development.aspx" id="frameMain" scrolling="auto" frameborder="0"
        runat="server" />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
