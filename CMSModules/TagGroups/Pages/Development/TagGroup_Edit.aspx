<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_TagGroups_Pages_Development_TagGroup_Edit"
    CodeFile="TagGroup_Edit.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Untitled Page</title>

    <script type="text/javascript">
        var IsCMSDesk = true;
    </script>

</head>
<frameset border="0" rows="<%= TabsBreadHeadFrameHeight %>, *" id="rowsFrameset">
    <frame name="groupHeader" src="TagGroup_Edit_Header.aspx<%= Request.Url.Query %>"
        frameborder="0" scrolling="no" noresize="auto" />
    <frame name="groupContent" src="TagGroup_Edit_General.aspx<%= Request.Url.Query %>"
        frameborder="0" />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>

