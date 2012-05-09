<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HierarchicalTransformations_Frameset.aspx.cs"
    Inherits="CMSModules_DocumentTypes_Pages_Development_HierarchicalTransformations_Frameset" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<frameset border="0" rows="<%=mHeight %>, *" id="rowsFrameset">
    <frame name="hierarchicalheader" scrolling="no" noresize="noresize" frameborder="0"
         id="frameHeader" src="HierarchicalTransformations_Header.aspx?<%=Request.QueryString %>" />
    <frame name="content" frameborder="0" noresize="noresize" scrolling="yes"  src="HierarchicalTransformations_Transformations.aspx?<%=Request.QueryString %>"
        id="content" />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
