<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_MyDesk_MyProfile_MyProfile_Edit"
    CodeFile="MyProfile_Edit.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Untitled Page</title>

    <script type="text/javascript">
        var IsCMSDesk = true;
    </script>

</head>
<frameset border="0" rows="<%= TabsFrameHeight %>, *" id="rowsFrameset">
    <frame name="myProfileMenu" src="MyProfile_Header.aspx" frameborder="0" scrolling="no"
        noresize="auto" />
    <frame name="myProfileContent" src="MyProfile_MyDetails.aspx" frameborder="0" />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
