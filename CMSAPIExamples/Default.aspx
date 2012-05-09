<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Theme="Default"
    Inherits="CMSAPIExamples_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>CMS API Examples</title>
</head>
<frameset border="0" rows="55,*">
    <frame name="cmsheader" src="<%=headerpage%>" scrolling="no"
        noresize="noresize" frameborder="0" />        
  <frameset border="0" cols="250,*" runat="server" id="colsFrameset">
    <frame runat="server" id="frmMenu" name="menu" noresize="noresize" frameborder="0" />
    <frame name="content" runat="server" noresize="noresize" frameborder="0" />
    </frameset>
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
