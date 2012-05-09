<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Frameset.aspx.cs" Inherits="CMSModules_Scoring_Pages_Frameset" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Scoring properties</title>
</head>
<frameset border="0" rows="<%= QueryHelper.GetBoolean("dialogmode", false) ? TabsFrameHeight : TabsBreadHeadFrameHeight %>, *" id="rowsFrameset">
    <frame name="header" scrolling="no" noresize="noresize" frameborder="0"
         id="frameHeader" src="Header.aspx?<%=Request.QueryString %>" />
    <frame name="content" frameborder="0" noresize="noresize" scrolling="yes" src="<%= contentUrl %>"
        id="content" />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
