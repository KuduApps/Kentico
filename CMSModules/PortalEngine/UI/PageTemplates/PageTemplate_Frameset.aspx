<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_PortalEngine_UI_PageTemplates_PageTemplate_Frameset"
    CodeFile="PageTemplate_Frameset.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Development - Page templates</title>
</head>
<frameset border="0" rows="38, *" framespacing="0" id="rowsFrameset">
    <frame name="pt_menu" src="PageTemplate_Menu.aspx" frameborder="0" scrolling="no"
        noresize="noresize" />
    <frameset border="0" cols="270,*" framespacing="0" runat="server" id="colsFrameset">
        <frame id="pt_tree" name="pt_tree" src="~/CMSModules/PortalEngine/UI/PageTemplates/PageTemplate_Tree.aspx"
            frameborder="0" scrolling="no" runat="server" />
        <frame name="pt_edit" frameborder="0" runat="server" />
    </frameset>
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
