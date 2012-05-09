<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Frameset.aspx.cs" Inherits="CMSModules_EmailTemplates_Pages_Frameset" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<frameset border="0" rows="<%= TabsBreadHeadFrameHeight %>, *" id="rowsFrameset">
    <frame name="header" scrolling="no" noresize="noresize" frameborder="0"
         id="frameHeader" src="Header.aspx?<%=Request.QueryString %>" />
    <frame name="content" frameborder="0" noresize="noresize" scrolling="yes" src="Edit.aspx?<%=Request.QueryString %>"
        id="content" />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
