<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Newsletters_Tools_Newsletters_Newsletter_Issue_Frameset"
    CodeFile="Newsletter_Issue_Frameset.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Newsletter issue</title>
</head>
<frameset border="0" rows="<%= TabsFrameHeight %>,*" id="rowsFrameset">
    <frame name="newsletterIssueMenu" src="Newsletter_Issue_Header.aspx?issueid=<%= Request.QueryString["issueid"] %> "
        frameborder="0" scrolling="no" noresize="noresize" />
    <frame name="newsletterIssueContent" src="<%= issueContentUrl %> " scrolling="auto"
        frameborder="0" noresize="noresize" />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
