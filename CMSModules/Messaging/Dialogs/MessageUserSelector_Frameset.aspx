<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Messaging_Dialogs_MessageUserSelector_Frameset"
    EnableEventValidation="false" CodeFile="MessageUserSelector_Frameset.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Message user selector</title>

    <script type="text/javascript">
        //<![CDATA[
        function CloseAndRefresh(userId, mText, mId, mId2) {
            wopener.FillUserName(userId, mText, mId, mId2);
            window.close();
        }

        function Refresh() {
            wopener.document.location.replace(wopener.document.location);
        }
        //]]>
    </script>

</head>
<frameset border="0" rows="<%= TabsFrameHeight %>, *" id="rowsFrameset">
    <frame name="MessageUserSelectorHeader" src="MessageUserSelector_Header.aspx?showtab=<%=Request.QueryString["showtab"]%>&hidid=<%=Request.QueryString["hidid"]%>&mid=<%=Request.QueryString["mid"]%>&refresh=<%=Request.QueryString["refresh"]%>"
        frameborder="0" scrolling="no" noresize="noresize" />
    <frame name="MessageUserSelectorContent" src="MessageUserSelector_<%=Request.QueryString["showtab"]%>.aspx?hidid=<%=Request.QueryString["hidid"]%>&mid=<%=Request.QueryString["mid"]%>&refresh=<%=Request.QueryString["refresh"]%>"
        scrolling="auto" frameborder="0" />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
