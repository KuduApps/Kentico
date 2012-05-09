<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Module_Edit_PermissionName_Edit_Frameset.aspx.cs"
    Inherits="CMSModules_Modules_Pages_Development_Module_Edit_PermissionName_Edit_Frameset" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server" enableviewstate="false">
    <title>Module Edit - Permissions</title>
    <style type="text/css">
        body
        {
            margin: 0px;
            padding: 0px;
            height: 100%;
        }
    </style>
</head>
<frameset border="0" rows="<%= TabsBreadFrameHeight %>, *" id="rowsFrameset">
    <frame name="header" src="Module_Edit_PermissionName_Edit_Header.aspx?moduleId=<%=Request.QueryString["moduleId"]%>&permissionId=<%=Request.QueryString["permissionId"]%>"
        scrolling="no" frameborder="0" noresize="noresize" />
    <frame name="editcontent" id="editContent" runat="server" scrolling="auto" frameborder="0"
        noresize="noresize" />
            <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
    </frameset>
</html>
