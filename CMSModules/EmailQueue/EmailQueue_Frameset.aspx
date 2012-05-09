<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_EmailQueue_EmailQueue_Frameset"
    CodeFile="EmailQueue_Frameset.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Administration - Email Queue</title>
</head>
<frameset border="0" rows="104, *" id="rowsFrameset">
    <frame name="header" src="EmailQueue_Header.aspx" scrolling="no" frameborder="0"
        noresize="noresize" />
    <frame name="content" src="EmailQueue.aspx" frameborder="0" />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
