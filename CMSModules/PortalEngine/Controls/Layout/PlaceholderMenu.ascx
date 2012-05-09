<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_PortalEngine_Controls_Layout_PlaceholderMenu"
    CodeFile="PlaceholderMenu.ascx.cs" %>
<%@ Register TagPrefix="cms" Namespace="CMS.UIControls" Assembly="CMS.UIControls" %>
<cms:ContextMenu runat="server" ID="menuLayout" MenuID="layoutMenu" VerticalPosition="Bottom"
    HorizontalPosition="Left" OffsetX="25" ActiveItemCssClass="ItemSelected" MouseButton="Right"
    MenuLevel="1" ShowMenuOnMouseOver="true" Visible="false" >
    <asp:Panel runat="server" ID="pnlSharedLayoutMenu" CssClass="PortalContextMenu PlaceholderContextMenu">
        <asp:Panel runat="server" ID="pnlSharedLayout" CssClass="ItemLast">
            <asp:Panel runat="server" ID="pnlSharedLayoutPadding" CssClass="ItemPadding">
                &nbsp;<asp:Label runat="server" ID="lblSharedLayoutVersions" CssClass="Name" EnableViewState="false"
                    Text="Shared layout versions" />
            </asp:Panel>
        </asp:Panel>
    </asp:Panel>
</cms:ContextMenu>
<cms:ContextMenu runat="server" ID="menuTemplate" MenuID="templateMenu" VerticalPosition="Bottom"
    HorizontalPosition="Left" OffsetX="25" ActiveItemCssClass="ItemSelected" MouseButton="Right"
    MenuLevel="1" ShowMenuOnMouseOver="true" Visible="false">
    <asp:Panel runat="server" ID="pnlTemplateMenu" CssClass="PortalContextMenu PlaceholderContextMenu">
        <asp:Panel runat="server" ID="pnlTemplateVersions" CssClass="ItemLast">
            <asp:Panel runat="server" ID="pnlTemplateVersionsPadding" CssClass="ItemPadding">
                &nbsp;<asp:Label runat="server" ID="lblTemplateVersions" CssClass="Name" EnableViewState="false"
                    Text="Template versions" />
            </asp:Panel>
        </asp:Panel>
    </asp:Panel>
</cms:ContextMenu>
<asp:Panel runat="server" ID="pnlPlaceholderMenu" CssClass="PortalContextMenu PlaceholderContextMenu">
    <cms:UIPlaceHolder ID="pnlUILayout" runat="server" ModuleName="CMS.Content" ElementName="Design.EditLayout">
        <cms:ContextMenuContainer runat="server" ID="cmcLayoutVersions" MenuID="layoutMenu">
            <asp:Panel runat="server" ID="pnlLayout" CssClass="Item">
                <asp:Panel runat="server" ID="pnlLayoutPadding" CssClass="ItemPadding">
                    <asp:Image runat="server" ID="imgLayout" CssClass="Icon" EnableViewState="false" />&nbsp;
                    <asp:Label runat="server" ID="lblLayout" CssClass="Name" EnableViewState="false"
                        Text="Edit layout" />
                </asp:Panel>
            </asp:Panel>
        </cms:ContextMenuContainer>
    </cms:UIPlaceHolder>
    <cms:UIPlaceHolder ID="pnlUITemplate" runat="server" ModuleName="CMS.Content" ElementName="Design.EditTemplateProperties">
        <cms:ContextMenuContainer runat="server" ID="cmcTemplateVersions" MenuID="templateMenu">
            <asp:Panel runat="server" ID="pnlTemplate" CssClass="Item">
                <asp:Panel runat="server" ID="pnlTemplatePadding" CssClass="ItemPadding">
                    <asp:Image runat="server" ID="imgTemplate" CssClass="Icon" EnableViewState="false" />&nbsp;
                    <asp:Label runat="server" ID="lblTemplate" CssClass="Name" EnableViewState="false"
                        Text="Edit template" />
                </asp:Panel>
            </asp:Panel>
        </cms:ContextMenuContainer>
    </cms:UIPlaceHolder>
    <cms:UIPlaceHolder ID="pnlUIClone" runat="server" ModuleName="CMS.Content" ElementName="Design.CloneAdHoc">
        <asp:Panel runat="server" ID="pnlClone" CssClass="ItemLast">
            <asp:Panel runat="server" ID="pnlClonePadding" CssClass="ItemPadding">
                <asp:Image runat="server" ID="imgClone" CssClass="Icon" EnableViewState="false" />&nbsp;
                <asp:Label runat="server" ID="lblClone" CssClass="Name" EnableViewState="false" Text="Clone template as ad-hoc" />
            </asp:Panel>
        </asp:Panel>
    </cms:UIPlaceHolder>
    <asp:Panel runat="server" ID="pnlSep1" CssClass="Separator">
        &nbsp;
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlRefresh" CssClass="ItemLast">
        <asp:Panel runat="server" ID="pnlRefreshPadding" CssClass="ItemPadding">
            <asp:Image runat="server" ID="imgRefresh" CssClass="Icon" EnableViewState="false" />&nbsp;<asp:Label
                runat="server" ID="lblRefresh" CssClass="Name" EnableViewState="false" Text="Refresh page" />
        </asp:Panel>
    </asp:Panel>
</asp:Panel>
