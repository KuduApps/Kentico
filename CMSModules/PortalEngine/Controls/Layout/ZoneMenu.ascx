<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_PortalEngine_Controls_Layout_ZoneMenu"
    CodeFile="ZoneMenu.ascx.cs" %>
<%@ Register TagPrefix="cms" Namespace="CMS.UIControls" Assembly="CMS.UIControls" %>
<cms:ContextMenu runat="server" ID="menuMoveTo" MenuID="zoneMoveToMenu" VerticalPosition="Bottom"
    HorizontalPosition="Left" OffsetX="25" ActiveItemCssClass="ItemSelected" MenuLevel="1"
    ShowMenuOnMouseOver="true" MouseButton="Both">
    <asp:Panel runat="server" ID="Panel1" CssClass="PortalContextMenu ZoneContextMenu">
        <asp:Repeater runat="server" ID="repZones">
            <ItemTemplate>
                <cms:ContextMenuContainer runat="server" ID="cmcZoneVariants" MenuID="moveToZoneVariants"
                    Parameter="GetContextMenuParameter('selectedMoveToZoneId')">
                    <cms:ContextMenuItem runat="server" ID="zoneElem" onmouseover='<%# (((CMSWebPartZone)Container.DataItem).HasVariants) ? "SetContextMenuParameter(\"selectedMoveToZoneId\", \"" + ((CMSWebPartZone)Container.DataItem).ID + "\");" : "CM_Close(\"moveToZoneVariants\");" %>'
                        onclick='<%# "CM_Close(\"webPartZoneMenu\"); ContextMoveWebPartsToZone(GetContextMenuParameter(\"webPartZoneMenu\"), \"" + ((CMSWebPartZone)Container.DataItem).ID + "\");" %>'>
                        &nbsp;
                        <asp:Label runat="server" ID="lblZone" CssClass="Name" EnableViewState="false" Text='<%# (((CMSWebPartZone)Container.DataItem).ZoneTitle != "" ? HTMLHelper.HTMLEncode(((CMSWebPartZone)Container.DataItem).ZoneTitle) : ((CMSWebPartZone)Container.DataItem).ID) + (((CMSWebPartZone)Container.DataItem).HasVariants ? "..." : "") %>' />
                    </cms:ContextMenuItem>
                </cms:ContextMenuContainer>
            </ItemTemplate>
        </asp:Repeater>
    </asp:Panel>
</cms:ContextMenu>
<cms:ContextMenu runat="server" ID="menuZoneMVTVariants" MenuID="zoneAllMVTVariants"
    VerticalPosition="Bottom" HorizontalPosition="Left" OffsetX="25" ActiveItemCssClass="ItemSelected"
    MenuLevel="1" Dynamic="true" ShowMenuOnMouseOver="true" MouseButton="Both">
    <asp:Panel runat="server" ID="pnlAllMVTVariants" CssClass="PortalContextMenu ZoneContextMenu">
        <asp:PlaceHolder ID="plcAddMVTVariant" runat="server">
            <asp:Panel runat="server" ID="pnlAddMVTVariant" CssClass="Item">
                <asp:Panel runat="server" ID="pnlAddMVTVariantPadding" CssClass="ItemPadding">
                    <asp:Image runat="server" ID="imgAddMVTVariant" CssClass="Icon" EnableViewState="false" />&nbsp;
                    <asp:Label runat="server" ID="lblAddMVTVariant" CssClass="Name" EnableViewState="false" />
                </asp:Panel>
            </asp:Panel>
            <asp:Panel runat="server" ID="Panel2" CssClass="Separator">
                &nbsp;
            </asp:Panel>
        </asp:PlaceHolder>
        <asp:Panel runat="server" ID="pnlNoZoneMVTVariants" CssClass="Item" Visible="false">
            <asp:Panel runat="server" ID="Panel5" CssClass="ItemPadding">
                <asp:Image runat="server" ID="imgNoZoneMVTVariants" EnableViewState="false" CssClass="Icon" />&nbsp;
                <asp:Label runat="server" ID="lblNoZoneMVTVariants" EnableViewState="false" />
            </asp:Panel>
        </asp:Panel>
        <asp:Repeater runat="server" ID="repZoneMVTVariants">
            <ItemTemplate>
                <asp:Panel runat="server" ID="pnlVariantItem" CssClass="Item">
                    <asp:Panel runat="server" ID="pnlItemPadding" CssClass="ItemPadding">
                        <asp:Image runat="server" ID="imgVariantItem" EnableViewState="false" CssClass="Icon" />&nbsp;
                        <asp:Label runat="server" ID="lblVariantItem" CssClass="Name" EnableViewState="false" />
                    </asp:Panel>
                </asp:Panel>
            </ItemTemplate>
        </asp:Repeater>
    </asp:Panel>
</cms:ContextMenu>
<cms:ContextMenu runat="server" ID="menuZoneCPVariants" MenuID="zoneAllCPVariants"
    VerticalPosition="Bottom" HorizontalPosition="Left" OffsetX="25" ActiveItemCssClass="ItemSelected"
    MenuLevel="1" Dynamic="true" ShowMenuOnMouseOver="true" MouseButton="Both">
    <asp:Panel runat="server" ID="pnlAllCPVariants" CssClass="PortalContextMenu ZoneContextMenu">
        <asp:PlaceHolder ID="plcAddCPVariant" runat="server">
            <asp:Panel runat="server" ID="pnlAddCPVariant" CssClass="Item">
                <asp:Panel runat="server" ID="pnlAddCPVariantPadding" CssClass="ItemPadding">
                    <asp:Image runat="server" ID="imgAddCPVariant" CssClass="Icon" EnableViewState="false" />&nbsp;
                    <asp:Label runat="server" ID="lblAddCPVariant" CssClass="Name" EnableViewState="false" />
                </asp:Panel>
            </asp:Panel>
            <asp:Panel runat="server" ID="Panel6" CssClass="Separator">
                &nbsp;
            </asp:Panel>
        </asp:PlaceHolder>
        <asp:Panel runat="server" ID="pnlNoZoneCPVariants" CssClass="Item" Visible="false">
            <asp:Panel runat="server" ID="Panel3" CssClass="ItemPadding">
                <asp:Image runat="server" ID="imgNoZoneCPVariants" EnableViewState="false" CssClass="Icon" />&nbsp;
                <asp:Label runat="server" ID="lblNoZoneCPVariants" EnableViewState="false" />
            </asp:Panel>
        </asp:Panel>
        <asp:Repeater runat="server" ID="repZoneCPVariants">
            <ItemTemplate>
                <asp:Panel runat="server" ID="pnlVariantItem" CssClass="Item">
                    <asp:Panel runat="server" ID="pnlItemPadding" CssClass="ItemPadding">
                        <asp:Image runat="server" ID="imgVariantItem" EnableViewState="false" CssClass="Icon" />&nbsp;
                        <asp:Label runat="server" ID="lblVariantItem" CssClass="Name" EnableViewState="false" />
                    </asp:Panel>
                </asp:Panel>
            </ItemTemplate>
        </asp:Repeater>
    </asp:Panel>
</cms:ContextMenu>
<cms:ContextMenu runat="server" ID="menuMoveToZoneVariants" MenuID="moveToZoneVariants"
    OffsetY="68" VerticalPosition="Bottom" HorizontalPosition="Left" OffsetX="50"
    ActiveItemCssClass="ItemSelected" MenuLevel="2" Dynamic="true" ShowMenuOnMouseOver="true"
    MouseButton="Both">
    <asp:Panel runat="server" ID="pnlZoneVariants" CssClass="PortalContextMenu ZoneContextMenu">
        <asp:Panel runat="server" ID="pnlNoZoneVariants" CssClass="ItemPadding" Visible="false">
            <asp:Literal runat="server" ID="ltlNoZoneVariants" EnableViewState="false" />
        </asp:Panel>
        <cms:UIRepeater runat="server" ID="repMoveToZoneVariants" ShortID="zv">
            <ItemTemplate>
                <asp:Panel runat="server" ID="pnlZoneVariantItem" CssClass="Item">
                    <asp:Panel runat="server" ID="pnlZoneItemPadding" CssClass="ItemPadding">
                        <asp:Image runat="server" ID="imgZoneVariantItem" EnableViewState="false" CssClass="Icon" />&nbsp;
                        <asp:Label runat="server" ID="lblZoneVariantItem" CssClass="Name" EnableViewState="false" />
                    </asp:Panel>
                </asp:Panel>
            </ItemTemplate>
        </cms:UIRepeater>
    </asp:Panel>
</cms:ContextMenu>
<asp:Panel runat="server" ID="pnlZoneMenu" CssClass="PortalContextMenu ZoneContextMenu">
    <cms:UIPlaceHolder ID="pnlUINewWebPart" runat="server" ModuleName="CMS.Content" ElementName="Design.AddWebParts">
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
        <asp:Panel runat="server" ID="pnlConfigureZone" CssClass="Item">
            <asp:Panel runat="server" ID="pnlConfigureZonePadding" CssClass="ItemPadding">
                <asp:Image runat="server" ID="imgConfigureZone" CssClass="Icon" EnableViewState="false" />&nbsp;
                <asp:Label runat="server" ID="lblConfigureZone" CssClass="Name" EnableViewState="false"
                    Text="ConfigureZone" />
            </asp:Panel>
        </asp:Panel>
    </cms:UIPlaceHolder>
    <cms:ContextMenuContainer runat="server" ID="cmcMoveTo" MenuID="zoneMoveToMenu">
        <asp:Panel runat="server" ID="pnlMoveTo" CssClass="Item">
            <asp:Panel runat="server" ID="pnlMoveToPadding" CssClass="ItemPadding">
                <asp:Image runat="server" ID="imgMoveTo" CssClass="Icon" EnableViewState="false" />&nbsp;
                <asp:Label runat="server" ID="lblMoveTo" CssClass="NameInactive" EnableViewState="false"
                    Text="MoveTo" />
            </asp:Panel>
        </asp:Panel>
    </cms:ContextMenuContainer>
    <cms:UIPlaceHolder ID="pnlUIDelete" runat="server" ModuleName="CMS.Content" ElementName="Design.RemoveWebParts">
        <asp:Panel runat="server" ID="pnlSep2" CssClass="Separator">
            &nbsp;
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlDelete" CssClass="Item">
            <asp:Panel runat="server" ID="pnlDeletePadding" CssClass="ItemPadding">
                <asp:Image runat="server" ID="imgDelete" CssClass="Icon" EnableViewState="false" />&nbsp;
                <asp:Label runat="server" ID="lblDelete" CssClass="Name" EnableViewState="false"
                    Text="Delete" />
            </asp:Panel>
        </asp:Panel>
    </cms:UIPlaceHolder>
    <cms:UIPlaceHolder ID="pnlUIMVTVariants" runat="server" ModuleName="CMS.Content"
        ElementName="WebPartZoneProperties.Variant">
        <asp:Panel ID="pnlContextMenuMVTVariants" runat="server" Visible="false">
            <asp:Panel runat="server" ID="pnlSep3" CssClass="Separator">
                &nbsp;
            </asp:Panel>
            <cms:ContextMenuContainer runat="server" ID="cmcAllMVTVariants" MenuID="zoneAllMVTVariants"
                Parameter="GetContextMenuParameter('webPartZoneMenu')">
                <asp:Panel runat="server" ID="pnlMVTVariants" CssClass="Item">
                    <asp:Panel runat="server" ID="pnlMVTVariantsPadding" CssClass="ItemPadding">
                        <asp:Image runat="server" ID="imgMVTVariants" CssClass="Icon" EnableViewState="false" />&nbsp;
                        <asp:Label runat="server" ID="lblMVTVariants" CssClass="NameInactive" EnableViewState="false" />
                    </asp:Panel>
                </asp:Panel>
            </cms:ContextMenuContainer>
        </asp:Panel>
    </cms:UIPlaceHolder>
    <cms:UIPlaceHolder ID="pnlUICPVariants" runat="server" ModuleName="CMS.Content" ElementName="WebPartZoneProperties.Variant">
        <asp:Panel ID="pnlContextMenuCPVariants" runat="server" Visible="false">
            <asp:Panel runat="server" ID="Panel4" CssClass="Separator">
                &nbsp;
            </asp:Panel>
            <cms:ContextMenuContainer runat="server" ID="cmcAllCPVariants" MenuID="zoneAllCPVariants"
                Parameter="GetContextMenuParameter('webPartZoneMenu')">
                <asp:Panel runat="server" ID="pnlCPVariants" CssClass="Item">
                    <asp:Panel runat="server" ID="pnlCPVariantsPadding" CssClass="ItemPadding">
                        <asp:Image runat="server" ID="imgCPVariants" CssClass="Icon" EnableViewState="false" />&nbsp;
                        <asp:Label runat="server" ID="lblCPVariants" CssClass="NameInactive" EnableViewState="false" />
                    </asp:Panel>
                </asp:Panel>
            </cms:ContextMenuContainer>
        </asp:Panel>
    </cms:UIPlaceHolder>
</asp:Panel>

<script type="text/javascript">
    //<![CDATA[
    function ContextNewWebPart(definition) {
        NewWebPart(escape(definition[0]), escape(definition[1]), escape(definition[2]));
    }

    function ContextConfigureWebPartZone(definition) {
        ConfigureWebPartZone(escape(definition[0]), escape(definition[1]), escape(definition[2]));
    }

    function ContextRemoveAllWebParts(definition) {
        RemoveAllWebParts(definition[0], definition[1]);
    }

    function ContextMoveWebPartsToZone(definition, targetZoneId) {
        MoveAllWebParts(definition[0], definition[1], targetZoneId);
    }

    function ContextAddWebPartZoneMVTVariant(definition) {
        AddMVTVariant(definition[0], '', definition[1], '', definition[2], 'zone', '');
    }

    function ContextAddWebPartZoneCPVariant(definition) {
        AddPersonalizationVariant(definition[0], '', definition[1], '', definition[2], 'zone', '');
    }

    //]]>
</script>

