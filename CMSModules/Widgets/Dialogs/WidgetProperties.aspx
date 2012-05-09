<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Widgets_Dialogs_WidgetProperties"
    CodeFile="WidgetProperties.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
        function ChangeWidget(zoneId, widgetId, aliasPath) {
            window.close();
            wopener.ConfigureWidget(zoneId, widgetId, aliasPath);
        }
        //]]>
    </script>

</head>
<frameset border="0" rows="35,*" runat="server" id="rowsFrameset">
    <frame name="widgetpropertiesheader" scrolling="no" noresize="noresize" frameborder="0"
        runat="server" id="frameHeader" />
    <frame name="widgetpropertiescontent" frameborder="0" noresize="noresize" scrolling="no"
        runat="server" id="frameContent" />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
