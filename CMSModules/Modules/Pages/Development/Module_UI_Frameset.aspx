<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Modules_Pages_Development_Module_UI_Frameset"
    CodeFile="Module_UI_Frameset.aspx.cs" %>

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
<frameset border="0" cols="240,*" runat="server" id="uiFrameset" enableviewstate="false">
    <frame name="tree" runat="server" id="treeFrame" scrolling="no" frameborder="0" noresize="noresize"
        enableviewstate="false" />
    <frame name="uicontent" runat="server" id="contentFrame" frameborder="0" noresize="noresize"
        enableviewstate="false" />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
