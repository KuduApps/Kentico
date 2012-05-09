<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_Widgets_Controls_WidgetMenu" CodeFile="WidgetMenu.ascx.cs" %>
<%@ Register TagPrefix="cms" Namespace="CMS.UIControls" Assembly="CMS.UIControls" %>
<cms:ContextMenu runat="server" ID="menuUp" MenuID="upMenuWidget" VerticalPosition="Bottom"
    HorizontalPosition="Left" OffsetX="25" ActiveItemCssClass="ItemSelected" MouseButton="Right"
    MenuLevel="1" ShowMenuOnMouseOver="true">
    <asp:Panel runat="server" ID="pnlUpMenu" CssClass="PortalContextMenu WebPartContextMenu">
        <asp:Panel runat="server" ID="pnlTop" CssClass="Item">
            <asp:Panel runat="server" ID="pnlTopPadding" CssClass="ItemPadding">
                &nbsp;
                <asp:Label runat="server" ID="lblTop" CssClass="Name" EnableViewState="false" Text="Top" />
            </asp:Panel>
        </asp:Panel>
    </asp:Panel>
</cms:ContextMenu>
<cms:ContextMenu runat="server" ID="menuDown" MenuID="downMenuWidget" VerticalPosition="Bottom"
    HorizontalPosition="Left" OffsetX="25" ActiveItemCssClass="ItemSelected" MouseButton="Right"
    MenuLevel="1" ShowMenuOnMouseOver="true">
    <asp:Panel runat="server" ID="pnlDownMenu" CssClass="PortalContextMenu WebPartContextMenu">
        <asp:Panel runat="server" ID="pnlBottom" CssClass="Item">
            <asp:Panel runat="server" ID="pnlBottomPadding" CssClass="ItemPadding">
                &nbsp;
                <asp:Label runat="server" ID="lblBottom" CssClass="Name" EnableViewState="false"
                    Text="Bottom" />
            </asp:Panel>
        </asp:Panel>
    </asp:Panel>
</cms:ContextMenu>
<asp:Panel runat="server" ID="pnlWidgetMenu" CssClass="PortalContextMenu WebPartContextMenu">
    <cms:UIPlaceHolder ID="pnlUIProperties" runat="server" ModuleName="CMS.Content" ElementName="Design.WebPartProperties">
        <asp:Panel runat="server" ID="pnlProperties" CssClass="ItemLast">
            <asp:Panel runat="server" ID="pnlPropertiesPadding" CssClass="ItemPadding">
                <asp:Image runat="server" ID="imgProperties" CssClass="Icon" EnableViewState="false" />&nbsp;
                <asp:Label runat="server" ID="lblProperties" CssClass="Name" EnableViewState="false"
                    Text="Properties" />
            </asp:Panel>
        </asp:Panel>
    </cms:UIPlaceHolder>
    <asp:Panel runat="server" ID="pnlSep1" CssClass="Separator">
        &nbsp;
    </asp:Panel>
    <cms:ContextMenuContainer runat="server" ID="cmcUp" MenuID="upMenuWidget">
        <asp:Panel runat="server" ID="pnlUp" CssClass="Item">
            <asp:Panel runat="server" ID="pnlUpPadding" CssClass="ItemPadding">
                <asp:Image runat="server" ID="imgUp" CssClass="Icon" EnableViewState="false" />&nbsp;
                <asp:Label runat="server" ID="lblUp" CssClass="Name" EnableViewState="false" Text="Up" />
            </asp:Panel>
        </asp:Panel>
    </cms:ContextMenuContainer>
    <cms:ContextMenuContainer runat="server" ID="cmcDown" MenuID="downMenuWidget">
        <asp:Panel runat="server" ID="pnlDown" CssClass="Item">
            <asp:Panel runat="server" ID="pnlDownPadding" CssClass="ItemPadding">
                <asp:Image runat="server" ID="imgDown" CssClass="Icon" EnableViewState="false" />&nbsp;
                <asp:Label runat="server" ID="lblDown" CssClass="Name" EnableViewState="false" Text="Down" />
            </asp:Panel>
        </asp:Panel>
    </cms:ContextMenuContainer>
    <cms:UIPlaceHolder ID="pnlUIClone" runat="server" ModuleName="CMS.Content" ElementName="Design.AddWebParts">
        <asp:Panel runat="server" ID="pnlClone" CssClass="ItemLast">
            <asp:Panel runat="server" ID="pnlClonePadding" CssClass="ItemPadding">
                <asp:Image runat="server" ID="imgClone" CssClass="Icon" EnableViewState="false" />&nbsp;
                <asp:Label runat="server" ID="lblClone" CssClass="Name" EnableViewState="false" Text="Down" />
            </asp:Panel>
        </asp:Panel>
    </cms:UIPlaceHolder>
    <asp:Panel runat="server" ID="pnlSep3" CssClass="Separator">
        &nbsp;
    </asp:Panel>
    <cms:UIPlaceHolder ID="pnlUIDelete" runat="server" ModuleName="CMS.Content" ElementName="Design.RemoveWebParts">
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
    function ContextConfigureWidget(definition) {
        ConfigureWidget(escape(definition[0]), escape(definition[1]), escape(definition[2]), escape(definition[3]), escape(definition[4]));
    }

    function ContextMoveWidgetUp(definition) {
        MoveWebPartUp(definition[0], definition[1], definition[2], definition[3]);
    }

    function ContextMoveWidgetDown(definition) {
        MoveWebPartDown(definition[0], definition[1], definition[2], definition[3]);
    }

    function ContextRemoveWidget(definition) {
        RemoveWidget(definition[0], definition[1], definition[2], definition[3]);
    }

    function ContextCloneWidget(definition) {
        CloneWebPart(definition[0], definition[1], definition[2], definition[3]);
    }

    function ContextMoveWidgetTop(definition) {
        MoveWebPart(definition[0], definition[1], definition[2], definition[3], definition[0], 0);
    }

    function ContextMoveWidgetBottom(definition) {
        MoveWebPart(definition[0], definition[1], definition[2], definition[3], definition[0], 1000);
    }
    //]]>
</script>

