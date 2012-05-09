<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Notifications_Development_Templates_Template_Edit"
    CodeFile="Template_Edit.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Templates</title>
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
    <frame name="templatesMenu" src="Template_Edit_Header.aspx<%=Request.Url.Query%>"
        scrolling="no" frameborder="0" noresize />
    <frame name="templatesContent" src="Template_Edit_General.aspx<%=Request.Url.Query%>"
        frameborder="0" noresize />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
