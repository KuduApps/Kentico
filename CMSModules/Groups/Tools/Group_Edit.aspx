<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Groups_Tools_Group_Edit"
    CodeFile="Group_Edit.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Groups frameset</title>
</head>
<frameset border="0" rows="<%= TabsBreadHeadFrameHeight %>, *" id="rowsFrameset">
    <frame name="menu" src="Group_Edit_Header.aspx?groupID=<%=Request.QueryString["groupID"] %> "
        scrolling="no" frameborder="0" noresize="noresize" />
    <frame name="content" src="Group_Edit_General.aspx?groupID=<%=Request.QueryString["groupID"]%> "
        frameborder="0" noresize="noresize" />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
