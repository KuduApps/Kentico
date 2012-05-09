<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Ecommerce_Pages_Tools_Configuration_Configuration_Frameset"
    CodeFile="Configuration_Frameset.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>E-commerce - Configuration</title>
</head>
<frameset border="0" cols="126, *" frameborder="0" runat="server" id="colsFrameset">
    <frame name="configHeader" scrolling="no" noresize="noresize"
        frameborder="0" runat="server" id="configHeader" />
    <frame name="configEdit" src="StoreSettings/StoreSettings_Frameset.aspx" noresize="noresize"
        frameborder="0" runat="server" id="configEdit" scrolling="auto" />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
