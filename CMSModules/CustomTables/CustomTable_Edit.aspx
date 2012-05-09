<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_CustomTables_CustomTable_Edit"
    EnableViewState="false" CodeFile="CustomTable_Edit.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Custom tables - Edit</title>
    <style type="text/css">
        body
        {
            margin: 0px;
            padding: 0px;
            height: 100%;
        }
    </style>
</head>
<frameset border="0" rows="<%= TabsBreadHeadFrameHeight %>, *" id="rowsFrameset">
    <frame name="menu" src="CustomTable_Edit_Header.aspx?customtableid=<% Response.Write(Request.QueryString["customtableid"]); %> "
        scrolling="no" frameborder="0" noresize />
    <frame name="content" src="CustomTable_Edit_General.aspx?customtableid=<% Response.Write(Request.QueryString["customtableid"]); %> "
        frameborder="0" noresize />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
