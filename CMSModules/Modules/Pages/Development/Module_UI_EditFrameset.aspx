<%@ Page Title="" Language="C#" AutoEventWireup="true" Inherits="CMSModules_Modules_Pages_Development_Module_UI_EditFrameset"
    CodeFile="Module_UI_EditFrameset.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server" enableviewstate="false">
    <title>Module Edit - User interface</title>
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
    <frame name="header" src="Module_UI_Header.aspx?moduleID=<%=Request.QueryString["moduleID"]%>&elementid=<%=Request.QueryString["elementid"]%>&parentId=<%=Request.QueryString["parentId"]%>&tabIndex=<%=Request.QueryString["tabIndex"]%>"
        scrolling="no" frameborder="0" noresize="noresize" />
    <frame name="editcontent" id="editContent" runat="server" scrolling="auto" frameborder="0"
        noresize="noresize" />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
