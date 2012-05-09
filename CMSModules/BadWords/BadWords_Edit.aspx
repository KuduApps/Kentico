<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_BadWords_BadWords_Edit"
    CodeFile="BadWords_Edit.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Administration - Bad words</title>
</head>
<frameset border="0" rows="<%= TabsBreadHeadFrameHeight %>, *" id="rowsFrameset">
    <frame name="badwordsMenu" src="BadWords_Edit_Header.aspx<%=Request.Url.Query%>"
        frameborder="0" scrolling="no" noresize="noresize" />
    <frame name="badwordsContent" src="BadWords_Edit_General.aspx<%=Request.Url.Query%>"
        frameborder="0" />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
