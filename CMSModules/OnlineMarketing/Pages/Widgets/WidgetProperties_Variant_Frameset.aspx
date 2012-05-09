<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_OnlineMarketing_Pages_Widgets_WidgetProperties_Variant_Frameset"
    EnableEventValidation="false" ValidateRequest="false" CodeFile="WidgetProperties_Variant_Frameset.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Widget Properties</title>
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

        function RefreshPage() {
            wopener.RefreshPage();
        }
        //]]>
    </script>

</head>
<frameset border="0" rows="*,43" runat="server" id="rowsFrameset">
    <frame name="widgetpropertiescontent" frameborder="0" noresize="noresize" scrolling="no"
        runat="server" id="frameContent" />
    <frame name="widgetpropertiesbuttons" frameborder="0" noresize="noresize" scrolling="no"
        runat="server" id="frameButtons" />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
