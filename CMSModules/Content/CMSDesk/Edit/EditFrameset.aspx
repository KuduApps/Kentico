<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Content_CMSDesk_Edit_EditFrameset"
    CodeFile="EditFrameset.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server" enableviewstate="false">
    <title>Content - Edit</title>
    <base target="_self" />    
</head>

<frameset border="0" id="editFrameset" rows="<%=menuSize%>,*" border="0">
    <frame name="editheader" src="<%=menupage%>" scrolling="no" noresize="noresize" frameborder="0" />
    <frame name="editview" src="<%=viewpage%>" scrolling="auto" frameborder="0" />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>