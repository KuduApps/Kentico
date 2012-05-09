<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_PortalEngine_UI_WebParts_WebpartProperties"
    CodeFile="WebPartProperties.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>CMS Desk - Web Part properties</title>
    <script type="text/javascript">
        //<![CDATA[
        function ChangeWebPart(zoneId, webPartId, aliasPath) {
            window.close();
            wopener.ConfigureWebPart(zoneId, webPartId, aliasPath);
        }
        //]]>
    </script>
</head>
<frameset border="0" id="rowsFrameset" runat="server">
    <frame name="webpartpropertiesheader" scrolling="no" noresize="noresize" frameborder="0"
         id="frameHeader" src="webpartproperties_header.aspx<%=Request.Url.Query%>" />
    <frame name="webpartpropertiescontent" frameborder="0" noresize="noresize" scrolling="yes"
        runat="server" id="frameContent" />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
