<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_OnlineMarketing_Pages_Widgets_WidgetProperties_Variant" Theme="default"
    EnableEventValidation="false" ValidateRequest="false" CodeFile="WidgetProperties_Variant.aspx.cs" %>
    
<%@ Register Src="~/CMSModules/OnlineMarketing/Controls/UI/ContentPersonalizationVariant/Edit.ascx"
    TagName="ContentPersonalizationVariantEdit" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/OnlineMarketing/Controls/UI/MVTVariant/Edit.ascx"
    TagName="MvtVariantEdit" TagPrefix="cms" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Widget properties</title>

    <script type="text/javascript">
        //<![CDATA[
        var wopener = parent.wopener;

        function RefreshPage() {
            wopener = parent.parent.wopener;
            wopener = parent.wopener;
            if (wopener.RefreshPage) {
                wopener.RefreshPage();
            }
        }
        //]]>
    </script>

</head>
<body class="TabsBody <%=mBodyClass%>">
    <form id="form1" runat="server" onsubmit="window.isPostBack = true; return true;">
        <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false" />
        <cms:ContentPersonalizationVariantEdit ID="cpEditElem" runat="server" IsLiveSite="false"
            Visible="false" />
        <cms:MvtVariantEdit ID="mvtEditElem" runat="server" IsLiveSite="false" Visible="false" />
        
        <asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />
        <cms:CMSButton ID="btnOnApply" runat="server" Visible="false" />
        <cms:CMSButton ID="btnOnOK" runat="server" Visible="false" />
        <asp:HiddenField runat="server" ID="hidRefresh" Value="0" />
    </form>
</body>
</html>
