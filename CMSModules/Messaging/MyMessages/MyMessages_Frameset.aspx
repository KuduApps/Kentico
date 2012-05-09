<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Messaging_MyMessages_MyMessages_Frameset"
    Title="Untitled Page" ValidateRequest="false" CodeFile="MyMessages_Frameset.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>My desk - My messages</title>
</head>
<frameset border="0" rows="<%= TabsFrameHeight %>, *" id="rowsFrameset">
    <frame name="messagesMenu" src="MyMessages_Header.aspx?userid=<%=userId%>" frameborder="0"
        scrolling="no" noresize="noresize" />
    <frame name="messagesContent" src="MyMessages_Inbox.aspx?userid=<%=userId%>" frameborder="0" />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
