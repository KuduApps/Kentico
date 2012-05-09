<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Configuration_Frameset.aspx.cs" Inherits="CMSModules_Ecommerce_Pages_SiteManager_Configuration_Frameset" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>E-commerce - Configuration</title>
</head>
<frameset border="0" rows="44, *" frameborder="0" runat="server" id="rowsFrameset" name="rowsFrameset">
    <frame name="configHeader" src="Configuration_Header.aspx" scrolling="no" noresize="noresize"
        frameborder="0" runat="server" id="configHeader" />
    <frame name="configEdit" noresize="noresize"
        frameborder="0" runat="server" id="configEdit" scrolling="auto" />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
