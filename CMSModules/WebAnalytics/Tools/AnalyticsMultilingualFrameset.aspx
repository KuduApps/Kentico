<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AnalyticsMultilingualFrameset.aspx.cs" Inherits="CMSModules_WebAnalytics_Tools_AnalyticsMultilingualFrameset" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server" enableviewstate="false">
    <title>Web analytics multilingual report</title>
</head>
<frameset border="0" rows="<%= TabsFrameHeight %>, *" id="rowsFrameset">
    <frame name="reportHeader" src="AnalyticsMultilingualHeader.aspx<%= Request.Url.Query %>" frameborder="0" scrolling="no"
        noresize="noresize" />
    <frame name="reportContent" src="Analytics_Report.aspx<%= Request.Url.Query %>&displaytitle=0" frameborder="0" />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>