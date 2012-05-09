<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_UICultures_Pages_Development_UICulture_Frameset"
    CodeFile="Frameset.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>SiteManager - UI Culture</title>
</head>
<frameset border="0" rows="<%= TabsBreadFrameHeight %>, *" id="rowsFrameset">
    <frame name="menu" src="Header.aspx?uicultureid=<%=Request.QueryString["UIcultureID"]%> "
        scrolling="no" frameborder="0" noresize="noresize" />
    <frame name="content" src="../ResourceString/List.aspx?uicultureid=<%=Request.QueryString["UIcultureID"]%>&update=<%=Request.QueryString["update"]%>  "
        frameborder="0" noresize="noresize" />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
