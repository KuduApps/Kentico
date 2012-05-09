<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Widgets_LiveDialogs_WidgetProperties_Properties" Theme="default"
    EnableEventValidation="false" ValidateRequest="false" CodeFile="WidgetProperties_Properties.aspx.cs" %>

<%@ Register Src="~/CMSModules/Widgets/Controls/WidgetProperties.ascx" TagName="WidgetProperties"
    TagPrefix="cms" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Widget properties</title>
    <style type="text/css">
        body
        {
            margin: 0px;
            padding: 0px;
            height: 100%;
            width: 100%;
        }
    </style>

    <script type="text/javascript">
        //<![CDATA[
        var wopener = parent.wopener;              

        function ChangeWidget(zoneId, widgetId, aliasPath) {
            if (parent.ChangeWidget) {
                parent.ChangeWidget(zoneId, widgetId, aliasPath);
            }
        }

        function RefreshPage() {
            wopener = parent.wopener;
            if (wopener.RefreshPage) {
                wopener.RefreshPage();
            }
        }
        //]]>
    </script>

</head>
<body class="<%=mBodyClass%> WidgetsProperties">
    <form id="form1" runat="server" onsubmit="window.isPostBack = true; return true;">
    <asp:Panel runat="server" ID="pnlBody" CssClass="TabsPageBody">
        <ajaxToolkit:ToolkitScriptManager ID="manScript" runat="server" />
        <cms:widgetproperties id="widgetProperties" runat="server" />
    </asp:Panel>
    </form>
</body>
</html>
