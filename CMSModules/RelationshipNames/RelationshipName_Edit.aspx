<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_RelationshipNames_RelationshipName_Edit"
    CodeFile="RelationshipName_Edit.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Relationship name - edit</title>
</head>
<frameset border="0" rows="<%= TabsBreadHeadFrameHeight %>, *" id="rowsFrameset">
    <frame name="relationshipNameMenu" src="RelationshipName_Header.aspx?relationshipnameid=<%=Request.QueryString["relationshipnameid"]%> "
        scrolling="no" frameborder="0" noresize="noresize" />
    <frame name="relationshipNameContent" src="RelationshipName_General.aspx?relationshipnameid=<%=Request.QueryString["relationshipnameid"]%>&saved=<%=Request.QueryString["saved"]%>"
        frameborder="0" noresize="noresize" />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
