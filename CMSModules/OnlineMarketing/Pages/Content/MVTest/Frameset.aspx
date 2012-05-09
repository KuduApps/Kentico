<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Frameset.aspx.cs" Inherits="CMSModules_OnlineMarketing_Pages_Content_MVTest_Frameset" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>MV test frameset</title>
</head>
<frameset border="0" rows="<%= TabsBreadFrameHeight %>, *" id="rowsFrameset">
    <frame name="header" src="Header.aspx<%= Request.Url.Query %>" scrolling="no" frameborder="0"
        noresize="noresize" />
    <frame name="content" src="edit.aspx<%= Request.Url.Query %>" frameborder="0" />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
