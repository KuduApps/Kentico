<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_ImportExport_SiteManager_NewSite_DefineSiteStructure_frameset"
    CodeFile="frameset.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>NewSite - Define site structure</title>
</head>
<asp:literal id="ltrScript" runat="server" />
<frameset border="0" frameborder="0" runat="server" id="rowsFrameset" rows="74,*">
    <frame name="definestructuremenu" scrolling="no" noresize="noresize" frameborder="0"
        border="0" runat="server" id="frameMenu" />
    <frameset border="0" cols="201,*" frameborder="0" id="colsFrameset" runat="server">
        <frame name="definestructuretree" scrolling="no" frameborder="0" border="0" runat="server"
            id="frameTree" />
        <frame name="definestructureview" frameborder="0" border="0" noresize="noresize"
            scrolling="no" runat="server" id="frameView" />
    </frameset>
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
