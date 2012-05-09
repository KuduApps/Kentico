<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSSiteManager_Administration_default"
    CodeFile="default.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Site Manager - Administration</title>
    <asp:Literal runat="server" ID="ltlScript" EnableViewState="false" />
</head>
<frameset border="0" cols="225, *" framespacing="0" runat="server" id="colsFrameset"
    enableviewstate="false">
    <frame name="admintree" frameborder="0" scrolling="no" src="leftmenu.aspx" runat="server"
        class="TreeFrame" />
    <frame name="frameMain" frameborder="0" scrolling="auto" src="administration.aspx"
        runat="server" />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
