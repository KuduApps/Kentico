<%@ Page Language="C#" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="CMSSiteManager_Tools_default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server" enableviewstate="false">
    <title>Site Manager - Tools</title>
    <asp:Literal runat="server" ID="ltlScript" EnableViewState="false" />
</head>
<frameset border="0" cols="260, *" framespacing="0" runat="server" id="colsFrameset"
    enableviewstate="false">
    <frame name="admintree" frameborder="0" scrolling="no" src="leftmenu.aspx" runat="server"
        class="TreeFrame" />
    <frame name="frameMain" frameborder="0" scrolling="auto" src="tools.aspx"
        runat="server" />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
