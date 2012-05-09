<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Content_CMSDesk_OnlineMarketing_default_frameset"
    CodeFile="default_frameset.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>CMSDesk - Content</title>
</head>
<frameset border="0" rows="<%= TabsBreadFrameHeight %>, *" id="colsFrameset">
    <frame name="header" src="header.aspx<%= Request.Url.Query %>" scrolling="no" noresize="noresize" frameborder="0"
        id="frameHeader" />
    <frame name="edit" noresize="noresize" frameborder="0" scrolling="auto" runat="server"
        id="frameEdit" />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>