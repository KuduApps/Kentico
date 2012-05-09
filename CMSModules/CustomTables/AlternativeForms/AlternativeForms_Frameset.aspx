<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_CustomTables_AlternativeForms_AlternativeForms_Frameset"
    EnableViewState="false" CodeFile="AlternativeForms_Frameset.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Alternative forms - Properties</title>
</head>
<frameset border="0" rows="<%= TabsBreadFrameHeight %>, *" id="rowsFrameset">
    <frame name="altFormsMenu" src="AlternativeForms_Header.aspx?classid=<%= classId %>&altformid=<%= altFormId %>"
        frameborder="0" scrolling="no" noresize="noresize" />
    <frame name="altFormsContent" src="AlternativeForms_Edit_General.aspx?altformid=<%= altFormId %>&saved=<%= saved %>"
        frameborder="0" />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
