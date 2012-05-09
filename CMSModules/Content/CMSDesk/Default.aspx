<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Content_CMSDesk_Default"
    CodeFile="Default.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>CMSDesk - Content</title>

    <script type="text/javascript">
        //<![CDATA[
        var IsCMSDesk = true;
        
        function CheckChanges() {
            if (window.frames['contentview'].CheckChanges) {
                return window.frames['contentview'].CheckChanges();
            }
            else {
                return true;
            }
        }
        //]]>
    </script>

</head>
<frameset border="0" rows="74,*,10" id="rowsFrameset">
    <frame name="contentmenu" src="<%=menuUrl%>" scrolling="no" noresize="noresize" frameborder="0" />
    <frameset border="0" cols="265,*" frameborder="0" framespacing="0" runat="server"
        id="colsFrameset" enableviewstate="false">
        <frame name="contenttree" scrolling="no" frameborder="0" framespacing="0" border="0"
            runat="server" id="frameTree" class="TreeFrame" />
        <frame name="contentview" frameborder="0" border="0" framespacing="0" runat="server"
            id="frameView" scrolling="auto" />
    </frameset>
    <frame name="contentfooter" src="footer.aspx" scrolling="no" noresize="noresize"
        frameborder="0" />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
