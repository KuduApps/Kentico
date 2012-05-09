<%@ Page Language="C#" AutoEventWireup="true" CodeFile="~/CMSModules/Reporting/Tools/ReportCategory_Edit_Frameset.aspx.cs" Inherits="CMSModules_Reporting_Tools_ReportCategory_Edit_Frameset" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server" enableviewstate="false">
    <title>Report category properties</title>
</head>
<frameset border="0" rows="<%= TabsBreadFrameHeight %>, *" id="rowsFrameset">
    <frame name="categoryHeader" frameborder="0" scrolling="no" noresize="noresize" runat="server" id="frmHeader"  />
    <frame name="categoryContent"  frameborder="0" runat="server" id="frmGeneral" />
        <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
    </frameset>
</html>