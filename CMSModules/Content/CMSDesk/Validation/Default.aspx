<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Content_CMSDesk_Validation_Default"
    CodeFile="Default.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Content - Validation</title>
</head>
<frameset border="0" cols="126,*" runat="server" id="colsFrameset" enableviewstate="false">
    <frame name="validationheader" scrolling="no" noresize="noresize" frameborder="0" runat="server"
        id="frameHeader" />
    <frame name="validationedit" noresize="noresize" frameborder="0" runat="server"
        id="frameEdit" />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
