<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSSiteManager_Sites_Site_Edit_Frameset"
    CodeFile="Site_Edit_Frameset.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Site Manager - Site Edit</title>
</head>
<frameset border="0" rows="<%= TabsBreadHeadFrameHeight %>, *" id="rowsFrameset">
    <frame name="menu" src="Site_Edit.aspx<%=Request.Url.Query%> " scrolling="no" frameborder="0"
        noresize="noresize" />
    <frame name="content" src="Site_Edit_General.aspx<%=Request.Url.Query%>" frameborder="0"
        noresize="noresize" />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
