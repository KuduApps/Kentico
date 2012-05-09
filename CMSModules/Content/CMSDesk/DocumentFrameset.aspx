<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Content_CMSDesk_DocumentFrameset"
    CodeFile="DocumentFrameset.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Content - Edit</title>

    <script type="text/javascript">
    //<![CDATA[
        var IsCMSDesk = true;
    //]]>
    </script>

    <asp:Literal runat="server" ID="ltlScript" EnableViewState="false" />
</head>
<frameset border="0" rows="0,*" runat="server" id="documentframeset">
    <frame name="documentheader" id="documentheader" runat="server" src="about:blank" scrolling="no"
        noresize="noresize" frameborder="0" />
    <frame name="documentview" src="<%=viewpage%>" scrolling="auto" noresize="noresize"
        frameborder="0" />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
