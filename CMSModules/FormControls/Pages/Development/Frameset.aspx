<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Frameset.aspx.cs"
    Inherits="CMSModules_FormControls_Pages_Development_Frameset" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Form Controls Frameset</title>
</head>
<frameset border="0" rows="<%= TabsBreadHeadFrameHeight %>, *" id="rowsFrameset">
    <frame name="FormUserControlHeader" src="Header.aspx?controlId=<%= controlId %>"
        frameborder="0" scrolling="no" noresize="noresize" />
    <frame name="FormUserControl" src="Edit.aspx?controlId=<%= controlId %>"
        frameborder="0" />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
