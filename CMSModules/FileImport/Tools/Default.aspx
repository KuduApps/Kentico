<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="CMSModules_FileImport_Tools_Default"
    Title="Tools - File import" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Tools - File import</title>
</head>
<frameset id="fileImportRowsFrameset" rows="<%= TabsFrameHeight %>, *" border="0">
    <frame name="fileImportHeader" src="Header.aspx<%= Request.Url.Query %>" frameborder="0" scrolling="no" noresize="noresize" />
    <frame name="fileImportContent" src="ImportFromComputer.aspx" frameborder="0" />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
