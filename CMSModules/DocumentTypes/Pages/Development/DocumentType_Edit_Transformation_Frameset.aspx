<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DocumentType_Edit_Transformation_Frameset.aspx.cs"
    Inherits="CMSModules_DocumentTypes_Pages_Development_DocumentType_Edit_Transformation_Frameset"
    Theme="Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server" enableviewstate="false">
    <title>Development - Document types - Edit transformation</title>
</head>
<frameset border="0" rows="<%=mHeight %>, *" id="rowsFrameset">
    <frame name="t_edit_menu" src="DocumentType_Edit_Transformation_Header.aspx?<%=Request.QueryString %>"
        scrolling="no" frameborder="0" noresize="noresize" />
    <frame name="t_edit_content" src="DocumentType_Edit_Transformation_Edit.aspx?<%=Request.QueryString %>"
        frameborder="0" />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
