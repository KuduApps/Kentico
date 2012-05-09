<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_SystemTables_Pages_Development_AlternativeForms_Frameset"
    CodeFile="Frameset.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Alternative forms - Properties</title>
</head>
<frameset border="0" rows="<%= TabsBreadFrameHeight %>, *" id="rowsFrameset">
    <frame name="altFormsMenu" src="Header.aspx?classid=<%= classId %>&altformid=<%= altFormId %>"
        frameborder="0" scrolling="no" noresize="noresize" />
    <frame name="altFormsContent" src="Edit_General.aspx?altformid=<%= altFormId %>&saved=<%= saved %>"
        frameborder="0" />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
