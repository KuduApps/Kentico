<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Widgets_UI_Widget_Edit_Frameset"
    CodeFile="Widget_Edit_Frameset.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server" enableviewstate="false">
    <title>Widget - Edit</title>
    <style type="text/css">
        body
        {
            margin: 0px;
            padding: 0px;
            height: 100%;
        }
    </style>
</head>
<frameset border="0" rows="<%= TabsBreadFrameHeight %>, *" framespacing="0" id="rowsFrameset">
    <frame name="widgetheader" src="<%= widgetheaderUrl %>" scrolling="no" frameborder="0"
        noresize="noresize" />
    <frame name="widgeteditcontent" src="<%= widgeteditcontentUrl %>" frameborder="0" />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
