<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_PortalEngine_UI_WebContainers_Container_Edit_Frameset"
    CodeFile="Container_Edit_Frameset.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Module Edit</title>
</head>
<frameset border="0" rows="<%=mHeight %>, *" id="rowsFrameset">
    <frame name="menu" src="Container_Edit_Header.aspx<%= Request.Url.Query %>" scrolling="no"
        frameborder="0" noresize="auto" />
    <frame name="content" src="Container_Edit_General.aspx<%= Request.Url.Query %>" frameborder="0" />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
