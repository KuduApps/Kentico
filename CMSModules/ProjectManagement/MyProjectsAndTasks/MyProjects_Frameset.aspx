<%@ Page Language="C#" AutoEventWireup="true" Theme="Default" Inherits="CMSModules_ProjectManagement_MyProjectsAndTasks_MyProjects_Frameset" CodeFile="MyProjects_Frameset.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server" enableviewstate="false">
    <title>My Projects</title>
</head>
<frameset border="0" rows="<%= TabsFrameHeight %>, *" id="rowsFrameset">
    <frame name="projectsMenu" src="MyProjects_Header.aspx" frameborder="0" scrolling="no" noresize="noresize" />
    <frame name="projectsContent" src="MyProjects_TasksassignedToMe.aspx" frameborder="0" />
    <noframes>
        <body>
            <p id="p1">
                This HTML frameset displays multiple Web pages. To view this frameset, use a Web
                browser that supports HTML 4.0 and later.
            </p>
        </body>
    </noframes>
</frameset>
</html>
