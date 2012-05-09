<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="CMSModules_Settings_Development_CustomSettings_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head2" runat="server" enableviewstate="false">
    <title>Development - Custom settings</title>
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
    <frame name="customsettingsheader" src="CustomSettings_Header.aspx?treeroot=<%=Request.QueryString["treeroot"]%>"
        scrolling="no" frameborder="0" noresize="noresize" />
    <frameset border="0" cols="240,*" framespacing="0" runat="server" id="colsFrameset">
        <frame id="frameTree" runat="server" name="customsettingstree" scrolling="no" frameborder="0" />
        <frame id="frameMain" runat="server" name="customsettingsmain" scrolling="auto" frameborder="0" />
    </frameset>
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
