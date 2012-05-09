<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Tab_Membership.aspx.cs"
Inherits="CMSModules_ContactManagement_Pages_Tools_Contact_Membership_Tab_Membership" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server" enableviewstate="false">
    <title>Activities</title>
</head>
<frameset border="0" rows="<%= TabsBreadFrameHeight %>, *" id="rowsFrameset" >
    <frame name="membershipMenu" id="membershipMenu" src="Header.aspx?contactid=<%= contactId %><%= siteManagerParam %>"
        frameborder="0" scrolling="no" noresize="noresize" />
    <frame name="membershipContent" id="membershipContent" src="Users.aspx?contactid=<%= contactId %><%= siteManagerParam %>" frameborder="0" />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>    