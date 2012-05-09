<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_ImportExport_SiteManager_ExportHistory_ExportHistory_Edit"
    EnableViewState="false" CodeFile="ExportHistory_Edit.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Site - Export history</title>
</head>
<frameset border="0" rows="103, *" id="rowsFrameset">
    <frame name="ExportHistoryHeader" src="ExportHistory_Edit_Header.aspx<%= Request.Url.Query %>"
        frameborder="0" scrolling="no" noresize="noresize" />
    <frame name="ExportHistoryContent" src="ExportHistory_Edit_History.aspx<%= Request.Url.Query %>"
        frameborder="0" />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
