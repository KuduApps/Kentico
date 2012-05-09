<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_SmartSearch_SearchIndex_Frameset"
    Title="Search Index - Frameset" CodeFile="SearchIndex_Frameset.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server" enableviewstate="false">
    <title>Search Index</title>
</head>
<frameset border="0" rows="<%= TabsBreadHeadFrameHeight %>, *" id="rowsFrameset">
    <frame name="menu" src="SearchIndex_Header.aspx<%=parameters %>" scrolling="no" frameborder="0"
        noresize="noresize" />
    <frame name="content" src="SearchIndex_General.aspx<%=parameters %> " frameborder="0"
        noresize="noresize" />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
