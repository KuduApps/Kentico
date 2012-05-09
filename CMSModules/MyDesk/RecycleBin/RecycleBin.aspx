<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_MyDesk_RecycleBin_RecycleBin"
    Theme="Default" Title="My Desk - Recycle bin" CodeFile="RecycleBin.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server" enableviewstate="false">
    <title>My Desk - Recycle bin</title>
</head>
<frameset border="0" rows="<%= TabsFrameHeight %>, *" id="rowsFrameset">
    <frame name="recbin_menu" src="RecycleBin_Header.aspx"
        scrolling="no" frameborder="0" noresize="noresize" />
    <frame name="recbin_content" frameborder="0" />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
