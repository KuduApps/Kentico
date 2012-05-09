<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WebPartProperties_layout_frameset_frameset.aspx.cs"
    Inherits="CMSModules_PortalEngine_UI_WebParts_WebPartProperties_layout_frameset_frameset" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server" enableviewstate="false">
    <title>Untitled Page</title>
    <style type="text/css">
        body
        {
            margin: 0px;
            height: 100%;
            width: 100%;
            overflow: hidden;
        }
    </style>

    <script type="text/javascript">
        //<![CDATA[
        var wopener = parent.wopener;
        //]]>
    </script>

</head>
<frameset border="0" rows="35,*" runat="server" id="rowsFrameset">
    <frame name="webpartlayoutheader" frameborder="0" noresize="noresize" scrolling="no"
         id="frameHeader" src="WebPartProperties_layout_frameset_header.aspx?<%=Request.QueryString%>" />
    <frame name="webpartlayoutcontent" frameborder="0" noresize="noresize" 
         id="frameContent" src="webpartproperties_layout.aspx?<%=Request.QueryString%>" />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
