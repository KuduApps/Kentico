<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Widgets_Controls_WidgetZoneMenu" CodeFile="WidgetZoneMenu.ascx.cs" %>
<%@ Register TagPrefix="cms" Namespace="CMS.UIControls" Assembly="CMS.UIControls" %>
<asp:Panel runat="server" ID="pnlZoneMenu" CssClass="PortalContextMenu ZoneContextMenu">
    <cms:UIPlaceHolder ID="pnlUIWebWebPart" runat="server" ModuleName="CMS.Content" ElementName="Design.AddWebParts">
        <asp:Panel runat="server" ID="pnlNewWebPart" CssClass="Item">
            <asp:Panel runat="server" ID="pnlNewWebPartPadding" CssClass="ItemPadding">
                <asp:Image runat="server" ID="imgNewWebPart" CssClass="Icon" EnableViewState="false" />&nbsp;
                <asp:Label runat="server" ID="lblNewWebPart" CssClass="Name" EnableViewState="false"
                    Text="NewWebPart" />
            </asp:Panel>
        </asp:Panel>
    </cms:UIPlaceHolder>
    <cms:UIPlaceHolder ID="pnlUIProperties" runat="server" ModuleName="CMS.Content" ElementName="Design.WebPartZoneProperties">
        <asp:Panel runat="server" ID="pnlSep1" CssClass="Separator">
            &nbsp;
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlConfigureZone" CssClass="ItemLast">
            <asp:Panel runat="server" ID="pnlConfigureZonePadding" CssClass="ItemPadding">
                <asp:Image runat="server" ID="imgConfigureZone" CssClass="Icon" EnableViewState="false" />&nbsp;
                <asp:Label runat="server" ID="lblConfigureZone" CssClass="Name" EnableViewState="false"
                    Text="ConfigureZone" />
            </asp:Panel>
        </asp:Panel>
    </cms:UIPlaceHolder>
    <cms:UIPlaceHolder ID="pnlUIDelete" runat="server" ModuleName="CMS.Content" ElementName="Design.RemoveWebParts">
        <asp:Panel runat="server" ID="pnlSep2" CssClass="Separator">
            &nbsp;
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlDelete" CssClass="ItemLast">
            <asp:Panel runat="server" ID="pnlDeletePadding" CssClass="ItemPadding">
                <asp:Image runat="server" ID="imgDelete" CssClass="Icon" EnableViewState="false" />&nbsp;
                <asp:Label runat="server" ID="lblDelete" CssClass="Name" EnableViewState="false"
                    Text="Delete" />
            </asp:Panel>
        </asp:Panel>
    </cms:UIPlaceHolder>
</asp:Panel>

<script type="text/javascript">
    //<![CDATA[
    function ContextNewWidget(definition) {
        NewWidget(escape(definition[0]), encodeURIComponent(definition[1]).replace(/%2F/g, "%2f"), escape(definition[2]), escape(definition[3]));
    }

    function ContextRemoveAllWidgets(definition) {
        RemoveAllWidgets(definition[0], definition[1]);
    }
    //]]>
</script>

