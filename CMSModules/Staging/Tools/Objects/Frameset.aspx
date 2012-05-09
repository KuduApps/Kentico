<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Staging_Tools_Objects_Frameset"
    EnableViewState="false" CodeFile="Frameset.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Content staging</title>
</head>
<frameset border="0" rows="31, *" id="rowsFrameset">
    <frame name="tasksHeader" src="Header.aspx" frameborder="0" scrolling="no" noresize="noresize" />
    <frameset border="0" cols="235, *" id="colsFrameset" runat="server">
        <frame name="tasksTree" src="Tree.aspx" scrolling="no" frameborder="0" runat="server" />
        <frame name="tasksContent" src="Tasks.aspx?siteid=-1" scrolling="auto" frameborder="0"
            runat="server" />
    </frameset>
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
