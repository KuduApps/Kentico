<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Friends_MyFriends_MyFriends_Frameset"
    CodeFile="MyFriends_Frameset.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server" enableviewstate="false">
    <title>Administration - User edit friends</title>
</head>
<frameset border="0" rows="<%= TabsFrameHeight %>, *" id="rowsFrameset">
    <frame name="friendsMenu" src="MyFriends_Header.aspx" frameborder="0" scrolling="no"
        noresize="noresize" />
    <frame name="friendsContent" src="MyFriends_Approved.aspx" frameborder="0" />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
