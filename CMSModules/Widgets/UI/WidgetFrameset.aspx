<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Widgets_UI_WidgetFrameset"
    CodeFile="WidgetFrameset.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Development - Widgets</title>
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
    <frame name="webpartmenu" src="Widget_Header.aspx" scrolling="no" frameborder="0"
        noresize="noresize" />
    <frameset border="0" cols="270,*" framespacing="0" runat="server" id="colsFrameset">
        <frame id="widgettree" name="widgettree" src="WidgetTree.aspx" scrolling="no" frameborder="0"
            runat="server" />
        <frame name="widgetedit" src="" scrolling="auto" frameborder="0" runat="server" />
    </frameset>
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
