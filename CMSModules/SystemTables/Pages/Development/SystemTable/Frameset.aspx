<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_SystemTables_Pages_Development_SystemTable_Frameset"
    CodeFile="Frameset.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>System tables - Edit</title>
    <style type="text/css">
        body
        {
            margin: 0px;
            padding: 0px;
            height: 100%;
        }
    </style>
</head>
<frameset border="0" rows="<%= TabsBreadFrameHeight %>, *" id="rowsFrameset">
    <frame name="menu" src="Header.aspx?classid=<% Response.Write(Request.QueryString["classid"]); %> "
        scrolling="no" frameborder="0" noresize />
    <frame name="content" src="Edit_Fields.aspx?classid=<% Response.Write(Request.QueryString["classid"]); %> "
        frameborder="0" noresize />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
