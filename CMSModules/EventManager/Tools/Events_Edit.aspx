<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_EventManager_Tools_Events_Edit"
    CodeFile="Events_Edit.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Events - Edit</title>
</head>
<frameset border="0" rows="<%= TabsBreadHeadFrameHeight %>, *" id="rowsFrameset">
    <frame name="eventsHeader" src="Events_Header.aspx?eventid=<%=Request.QueryString["eventId"]%> "
        scrolling="no" frameborder="0" noresize="noresize" id="pollMenu" />
    <frame name="eventsContent" src="Events_Attendee_List.aspx?eventid=<%=Request.QueryString["eventId"]%>&saved=<%=Request.QueryString["saved"]%> "
        frameborder="0" id="eventContent" />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
