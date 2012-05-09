<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Blogs_Tools_Blogs_Default"
    CodeFile="Blogs_Default.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server" enableviewstate="false">
    <title>Blogs - Default</title>
    <style type="text/css">
        body
        {
            margin: 0px;
            padding: 0px;
            height: 100%;
        }
    </style>
</head>
<frameset border="0" rows="<%= TabsFrameHeight %>, *" id="rowsFrameset">
    <frame name="blogsMenu" src="Blogs_Header.aspx<%= Request.Url.Query %>" scrolling="no" frameborder="0" noresize />
    <frame name="blogsContent" src="Blogs_Comments_List.aspx<%= Request.Url.Query %>" frameborder="0" noresize />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
