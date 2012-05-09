<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_MediaLibrary_Tools_Library_Edit"
    CodeFile="Library_Edit.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Media library properties</title>
</head>
<frameset border="0" rows="<%= TabsBreadHeadFrameHeight %>, *" id="rowsFrameset">
    <frame name="mediaHeader" src="Library_Edit_Header.aspx<%=Request.Url.Query%>" scrolling="no"
        frameborder="0" noresize="noresize" id="mediaMenu" />
    <frame name="mediaContent" src="Library_Edit_Files.aspx<%=Request.Url.Query%>" frameborder="0"
        id="mediaContent" />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
