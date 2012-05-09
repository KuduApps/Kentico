<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Frameset.aspx.cs" Inherits="CMSModules_ContactManagement_Pages_Tools_Frameset" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server" enableviewstate="false">
    <title>Content Management</title>
</head>
<frameset border="0" id="rowsFrameset" runat="server">
    <frame name="contactManagementMenu" id="contactManagementMenu"  frameborder="0" scrolling="no" noresize="noresize" runat="server" />
    <frame name="contactManagementContent" id="contactManagementContent" frameborder="0" runat="server" />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
