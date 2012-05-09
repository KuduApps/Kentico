<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Forums_Content_Properties_Default"
    CodeFile="Default.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>CMSDesk - Forums</title>
</head>
<frameset border="0" rows="37,*" id="rowsFrameset" runat="server">
    <frame name="header" src="header.aspx<%=Request.Url.Query%>" scrolling="no" noresize="noresize"
        frameborder="0" />
    <frameset border="0" cols="180,*" id="colsFrameset" runat="server">
        <frame name="tree" runat="server" scrolling="no" frameborder="0" id="tree" />
        <frame name="main" runat="server" scrolling="auto" frameborder="0" id="main" />
    </frameset>
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
