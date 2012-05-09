<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_System_Files_System_FilesFrameset"
    CodeFile="System_FilesFrameset.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Administration - System - Files</title>
</head>
<frameset border="0" rows="<%= TabsBreadFrameHeight %>, *" id="rowsFrameset">
    <frame name="systemMenu" src="System_FilesHeader.aspx" frameborder="0" scrolling="no"
        noresize="noresize" />
    <frame name="systemFiles" src="System_FilesTest.aspx" frameborder="0" />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
