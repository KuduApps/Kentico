<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="CMSDesk_Tools_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>CMSDesk / Tools</title>
    <asp:Literal runat="server" ID="ltlScript" EnableViewState="false" />
</head>
<frameset border="0" rows="74,*">
    <frame name="toolsheader" src="Header.aspx" scrolling="no" noresize="noresize" frameborder="0" />
    <frame name="toolscontent" src="Menu.aspx" frameborder="0" runat="server" id="toolscontent" />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
