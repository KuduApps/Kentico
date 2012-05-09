<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Role_Edit_UI_Frameset.aspx.cs"
    Inherits="CMSModules_Membership_Pages_Roles_Role_Edit_UI_Frameset" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server" enableviewstate="false">
    <title>UI Personalization</title>
</head>
<frameset border="0" rows="<%= TabsBreadFrameHeight %>, *" id="rowsFrameset">
    <frame name="uiMenu" src="Role_Edit_UI_Header.aspx<%= Request.Url.Query %>" frameborder="0"
        scrolling="no" noresize="noresize" />
    <frame name="uiContent" src="Role_Edit_UI_Dialogs.aspx<%= Request.Url.Query %>" frameborder="0" />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
