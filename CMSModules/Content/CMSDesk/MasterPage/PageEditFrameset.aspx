<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Content_CMSDesk_MasterPage_PageEditFrameset"
    CodeFile="PageEditFrameset.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Page edit frameset</title>

    <script type="text/javascript">
        //<![CDATA[
        function SetTabsContext(mode) {
            parent.SetTabsContext(mode);
        }
        function CheckChanges() {
            if (window.frames['masterpagecontent'].CheckChanges) {
                return window.frames['masterpagecontent'].CheckChanges();
            }

            return true;
        }
        //]]>
    </script>

</head>
<frameset border="0" rows="43,*">
    <frame name="masterpageheader" src="PageEditHeader.aspx<%=Request.Url.Query%>" scrolling="no"
        noresize="noresize" frameborder="0" />
    <frame name="masterpagecontent" src="PageEdit.aspx<%=Request.Url.Query%>" scrolling="auto"
        frameborder="0" />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
