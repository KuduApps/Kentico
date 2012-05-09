<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Newsletters_Tools_Subscribers_Subscriber_Frameset"
    CodeFile="Subscriber_Frameset.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Newsletters</title>
</head>
<frameset border="0" rows="<%= TabsBreadFrameHeight %>, *" id="rowsFrameset">
    <frame name="subscriberMenu" src="Subscriber_Header.aspx?subscriberid=<%= Request.QueryString["subscriberid"] %> "
        frameborder="0" scrolling="no" noresize="noresize" />
    <frame name="subscriberContent" src="Subscriber_General.aspx?subscriberid=<%= Request.QueryString["subscriberid"] %>&saved=<%= Request.QueryString["saved"] %> "
        frameborder="0" />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
