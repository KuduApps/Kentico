<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Membership_Pages_Users_User_Edit_Frameset"
    CodeFile="User_Edit_Frameset.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Users</title>
</head>
<frameset border="0" rows="<%= TabsBreadFrameHeight %>, *" id="rowsFrameset">
    <frame name="menu" src="User_Edit.aspx?userid=<%= Request.QueryString["userid"] %>&siteid=<%= Request.QueryString["siteid"] %>"
        frameborder="0" scrolling="no" />
    <frame name="content" src="User_Edit_General.aspx?userid=<%= Request.QueryString["userid"]%>&siteid=<%= Request.QueryString["siteid"] %>"
        frameborder="0" noresize="noresize" />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
