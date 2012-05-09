<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Modules_Pages_Development_Module_Edit_Frameset"
    CodeFile="Module_Edit_Frameset.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Module Edit</title>
</head>
<frameset border="0" rows="102, *" id="rowsFrameset">
    <frame name="menu" src="Module_Edit_Header.aspx?<%=Request.QueryString%>"
        scrolling="no" frameborder="0" noresize="noresize" />
    <frame name="content" src="<%= contentUrl %>"
        frameborder="0" noresize="noresize" />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
