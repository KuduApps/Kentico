<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Frameset.aspx.cs" Inherits="CMSModules_Integration_Pages_Administration_Frameset" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Integration</title>
</head>
<frameset border="0" rows="<%= TabsFrameHeight %>, *" id="rowsFrameset">
    <frame name="integrationMenu" id="integrationMenu" src="Header.aspx"
        frameborder="0" scrolling="no" noresize="noresize" />
    <frame name="integrationContent" id="integrationContent" src="OutcomingTasks/List.aspx" frameborder="0" />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
