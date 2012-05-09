<%@ Page Language="C#" AutoEventWireup="true" CodeFile="~/CMSModules/Reporting/Tools/ReportCategory_Frameset.aspx.cs"
    Inherits="CMSModules_Reporting_Tools_ReportCategory_Frameset" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server" enableviewstate="false">
    <title>Report Category </title>
    <style type="text/css">
        body
        {
            margin: 0px;
            padding: 0px;
            height: 100%;
        }
    </style>
</head>
<frameset border="0" rows="38, *" framespacing="0" frameborder="0" id="rowsFrameset">
    <frame name="reportcategoryheader" src="ReportCategory_Header.aspx" scrolling="no"
        frameborder="0" noresize="noresize" />
    <frameset border="0" cols="270,*" framespacing="0" runat="server" id="colsFrameset">
        <frame ID="treeFrame" name="reportcategorytree" src="ReportCategory_Tree.aspx" scrolling="no" frameborder="0" runat="server" />
        <frame name="reportedit" src="" scrolling="auto" frameborder="0" runat="server" />
    </frameset>
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
