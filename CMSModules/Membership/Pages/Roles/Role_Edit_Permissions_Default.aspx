<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Role_Edit_Permissions_Default.aspx.cs"
    Inherits="CMSModules_Membership_Pages_Roles_Role_Edit_Permissions_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server" enableviewstate="false">
    <title>Administration - Roles</title>
</head>
<frameset border="0" rows="53, *">
    <frame name="header" src="Role_Edit_Permissions_Header.aspx<%= Request.Url.Query %>"
        scrolling="no" frameborder="0" />
    <frame name="content" scrolling="auto" frameborder="0" noresize="noresize" />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
