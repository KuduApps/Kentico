<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Frameset.aspx.cs"
Inherits="CMSModules_ContactManagement_Pages_Tools_Activities_Frameset" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server" enableviewstate="false">
    <title>Activities</title>
</head>
<frameset runat="server" border="0" id="rowsFrameset" >
    <frame name="activitiesMenu" id="activitiesMenu" src="Header.aspx?siteid=<%= siteId %><%= sitemanager %>"
        frameborder="0" scrolling="no" noresize="noresize" />
    <frame name="activitiesContent" id="activitiesContent" src="Activity/List.aspx?siteid=<%= siteId %><%= sitemanager %>" frameborder="0" />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
