<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Staging_Tools_Frameset"
    EnableViewState="false" CodeFile="Frameset.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Content staging</title>
</head>
<frameset border="0" rows="<%= TabsFrameHeight %>, *" id="rowsFrameset">
    <frame name="stagingHeader" src="Header.aspx<%= Request.Url.Query %>" frameborder="0" scrolling="no" noresize="noresize" />
    <frame name="stagingContent" src="<%= contentPage %>" frameborder="0" />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
