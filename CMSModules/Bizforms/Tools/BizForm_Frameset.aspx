<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_BizForms_Tools_BizForm_Frameset"
    CodeFile="BizForm_Frameset.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>BizForm Frameset</title>
</head>
<frameset border="0" rows="<%= TabsBreadHeadFrameHeight %>, *" id="rowsFrameset">
    <frame name="BizFormHeader" src="BizForm_Edit_Header.aspx?formid=<%= formId %>&newform=<%= newForm %>"
        frameborder="0" scrolling="no" noresize="noresize" />
    <frame name="BizFormContent" src="<%= defaultTab %>" frameborder="0" />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
