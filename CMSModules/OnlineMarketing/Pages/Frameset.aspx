<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Frameset.aspx.cs" Inherits="CMSModules_OnlineMarketing_Pages_Frameset" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>On-line marketing</title>
</head>
<frameset border="0" rows="74,*" id="rowsFrameset">
    <frame name="menu" src="Header.aspx" frameborder="0" scrolling="no" noresize="noresize" runat="server" id="frameMenu" />
    <frame name="content" frameborder="0" />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
