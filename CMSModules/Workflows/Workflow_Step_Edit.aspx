<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Workflows_Workflow_Step_Edit"
    EnableViewState="false" CodeFile="Workflow_Step_Edit.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Workflows - Workflow Steps</title>
</head>
<frameset border="0" rows="<%= TabsBreadFrameHeight %>, *" id="rowsFrameset">
    <frame name="wfStepMenu" src="Workflow_Step_Header.aspx?workflowstepid=<%=Request.QueryString["workflowstepid"]%> "
        scrolling="no" frameborder="0" noresize="noresize" />
    <frame name="wfStepContent" src="Workflow_Step_General.aspx?workflowstepid=<%=Request.QueryString["workflowstepid"]%> "
        frameborder="0" noresize="noresize" />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
