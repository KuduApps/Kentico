<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Groups_Tools_Forums_Forums_Forum_Frameset"
    CodeFile="Forum_Frameset.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Forum-Frameset</title>
</head>
<frameset border="0" rows="<%= TabsBreadFrameHeight %>, *" id="rowsFrameset" >
    <frame name="forumsHeader" src="<%= forumsHeaderUrl %> " frameborder="0" scrolling="no"
        noresize="noresize" />
    <frame name="forumsContent" src="<%= forumsContentUrl %> " frameborder="0" />
    <div class="TabsHeaderSeparator">
    </div>
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
