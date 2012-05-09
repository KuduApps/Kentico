<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Polls_Tools_Polls_Edit"
    CodeFile="Polls_Edit.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Poll properties</title>
</head>
<frameset border="0" rows="<%= TabsBreadHeadFrameHeight %>, *" id="rowsFrameset">
    <frame name="pollHeader" src="Polls_Header.aspx<%= Request.Url.Query %>" scrolling="no"
        frameborder="0" noresize="noresize" id="pollMenu" />
    <frame name="pollContent" src="Polls_General.aspx<%= Request.Url.Query %>" frameborder="0"
        id="pollContent" />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
