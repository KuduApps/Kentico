<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_CssStylesheets_Pages_CssStylesheet_Edit"
    CodeFile="CssStylesheet_Edit.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Css stylesheets - edit</title>
</head>
<frameset border="0" rows="<%=mHeight %>, *" id="rowsFrameset">
    <frame name="Menu" src="CssStylesheet_Header.aspx?<%=Request.QueryString%>"
        scrolling="no" frameborder="0" noresize="noresize" />
    <frame name="Content" src="CssStylesheet_General.aspx?<%=Request.QueryString%>"
        frameborder="0" noresize="noresize" />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
