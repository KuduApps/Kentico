<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NewsletterTemplate_Frameset.aspx.cs" Inherits="CMSModules_Newsletters_Tools_Templates_NewsletterTemplate_Frameset" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server" enableviewstate="false">
    <title>Newsletter template - edit</title>
</head>
<frameset border="0" rows="<%= TabsBreadFrameHeight %>, *" id="rowsFrameset">
    <frame name="Menu" src="NewsletterTemplate_Header.aspx?<%=Request.QueryString%>"
        scrolling="no" frameborder="0" noresize="noresize" />
    <frame name="Content" src="NewsletterTemplate_Edit.aspx?<%=Request.QueryString%>"
        frameborder="0" noresize="noresize" />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
