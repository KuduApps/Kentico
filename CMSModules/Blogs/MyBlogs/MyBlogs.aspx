<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Blogs_MyBlogs_MyBlogs"
    CodeFile="MyBlogs.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>My Blogs - Default</title>
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
    <frame name="blogsMenu" src="MyBlogs_Header.aspx" scrolling="no" frameborder="0"
        noresize="noresize" />
    <frame name="blogsContent" src="MyBlogs_Comments_List.aspx" frameborder="0" noresize="noresize" />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
