<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Notifications_Development_Notifications_Default"
    Title="Notifications-Default" CodeFile="Notifications_Default.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Notifications - Default</title>
    <style type="text/css">
        body
        {
            margin: 0px;
            padding: 0px;
            height: 100%;
        }
    </style>
</head>
<frameset border="0" rows="<%= TabsFrameHeight %>, *" id="rowsFrameset">
    <frame name="notificationsMenu" src="Notifications_Header.aspx" scrolling="no" frameborder="0"
        noresize />
    <frame name="notificationsContent" src="Gateways/Gateway_List.aspx" frameborder="0"
        noresize />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
