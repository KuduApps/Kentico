<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Reporting_Tools_Category_Frameset"
    CodeFile="Category_Frameset.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Report category properties</title>
</head>
<frameset border="0" rows="<%= TabsBreadHeadFrameHeight %>, *" id="rowsFrameset">
    <frame name="categoryHeader" src="<%= categoryHeaderUrl %>" frameborder="0" scrolling="no"
        noresize="noresize" />
    <frame name="categoryContent" src="<%= categoryContentUrl %>" frameborder="0" />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
