<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_PortalEngine_UI_WebParts_WebPartProperties_properties"
    Theme="default" EnableEventValidation="false" ValidateRequest="false" CodeFile="WebPartProperties_properties.aspx.cs" %>

<%@ Register Src="~/CMSModules/PortalEngine/Controls/WebParts/WebpartProperties.ascx"
    TagName="WebpartProperties" TagPrefix="cms" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Web part properties</title>
    <base target="_self" />
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

        function ChangeWebPart(zoneId, webPartId, aliasPath) {
            if (parent.ChangeWebPart) {
                parent.ChangeWebPart(zoneId, webPartId, aliasPath);
            }
        }

        function RefreshPage() {
            wopener = parent.wopener;
            if (wopener.RefreshPage) {
                wopener.RefreshPage();
                SetRefresh(false);
            }
        }

        function UpdateVariantPosition(itemCode, variantId) {
            wopener = parent.wopener;
            if (wopener.UpdateVariantPosition) {
                wopener.UpdateVariantPosition(itemCode, variantId);
            }
        }

        //]]>
    </script>

</head>
<body class="<%=mBodyClass%> WebpartProperties">
    <form id="form1" runat="server" onsubmit="window.isPostBack = true; return true;">
    <asp:Panel runat="server" ID="pnlBody">
        <ajaxToolkit:ToolkitScriptManager ID="manScript" runat="server" />
        <cms:WebpartProperties ID="webPartProperties" runat="server" />
    </asp:Panel>
    </form>
</body>
</html>
