<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_SystemTables_Pages_Development_Views_ViewEdit_Frameset"
    CodeFile="ViewEdit_Frameset.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Views - Edit</title>
    <style type="text/css">
        body
        {
            margin: 0px;
            padding: 0px;
            height: 100%;
        }
    </style>
</head>
<frameset border="0" rows="64, *" id="rowsFrameset">
    <frame name="viewsmenu" src="ViewEdit_Header.aspx<%= Request.Url.Query %>"
        scrolling="no" frameborder="0" noresize />
    <frame name="viewscontent" src="View_Edit.aspx<%= Request.Url.Query %>"
        frameborder="0" noresize />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
