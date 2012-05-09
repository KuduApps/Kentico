<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Groups_Tools_Roles_Role_Edit"
    CodeFile="Role_Edit.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Roles</title>
</head>
<frameset border="0" rows="58, *" id="rowsFrameset">
    <frame name="menu" src="Role_Edit_Header.aspx<%=parameters %>" scrolling="no" frameborder="0"
        noresize="noresize" />
    <frame name="content" src="Role_Edit_General.aspx<%=parameters %>" frameborder="0"
        noresize="noresize" />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
