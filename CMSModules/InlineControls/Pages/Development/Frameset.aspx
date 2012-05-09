<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_InlineControls_Pages_Development_Frameset"
    CodeFile="Frameset.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Untitled Page</title>
</head>
<frameset border="0" rows="<%= TabsBreadHeadFrameHeight %>, *" id="rowsFrameset">
    <frame name="menu" src="Header.aspx?inlinecontrolid=<%=Request.QueryString["inlinecontrolid"]%> "
        scrolling="no" frameborder="0" noresize="noresize" />
    <frame name="content" src="General.aspx?inlinecontrolid=<%=Request.QueryString["inlinecontrolid"]%>&saved=<%=Request.QueryString["saved"]%> "
        frameborder="0" noresize="noresize" />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
