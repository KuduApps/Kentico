<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Groups_Tools_Polls_Polls_Edit"
    CodeFile="Polls_Edit.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Groups frameset</title>
</head>
<frameset border="0" rows="<%= TabsBreadFrameHeight %>, *" id="rowsFrameset">
    <frame name="menu" src="Polls_Edit_Header.aspx<%= Request.Url.Query %>" scrolling="no"
        frameborder="0" noresize="noresize" />
    <frame name="content" src="Polls_Edit_General.aspx<%= Request.Url.Query %>" frameborder="0"
        noresize="noresize" />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
