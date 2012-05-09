<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Content_CMSDesk_Properties_Advanced_EditableContent_Default"
    CodeFile="Default.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>CMSDesk - Editable content</title>
</head>
<frameset border="0" rows="35,*, 43" id="rowsFrameset" runat="server">
    <frame name="header" src="header.aspx<%=Request.Url.Query%>" scrolling="no" noresize="noresize"
        frameborder="0" />
    <frameset border="0" cols="263,*" id="colsFrameset" runat="server">
        <frame name="tree" src="tree.aspx" runat="server" scrolling="auto" frameborder="0"
            id="tree" />
        <frame name="main" src="main.aspx" runat="server" scrolling="auto" frameborder="0"
            id="main" />
    </frameset>
    <frame name="contentfooter" src="footer.aspx" scrolling="no" noresize="noresize"
        frameborder="0" />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
