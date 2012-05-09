<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Content_CMSDesk_ContentEditFrameset"
    CodeFile="ContentEditFrameset.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Content - Edit</title>
</head>
<frameset border="0" rows="35,*,0,0">
    <frame name="contenteditheader" src="<%=tabspage%>" scrolling="no"
        noresize="noresize" frameborder="0" />
    <frame name="contenteditview" src="<%=viewpage%>" scrolling="auto" frameborder="0" />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
