<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_MyDesk_Default"
    CodeFile="Default.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>CMSDesk - MyDesk</title>
    <asp:Literal runat="server" ID="ltlScript" EnableViewState="false" />
</head>
<frameset border="0" rows="*,6" runat="server">
    <frameset border="0" rows="74,*" runat="server" id="rowsFrameset" enableviewstate="false">
        <frame name="frameMenu" scrolling="no" frameborder="0" runat="server" id="frameMenu" />
        <frame name="frameMain" frameborder="0" runat="server" id="frameMain" />
    </frameset>
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
