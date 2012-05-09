<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_OnlineMarketing_Controls_Content_MenuAddWidgetVariant"
    CodeFile="MenuAddWidgetVariant.ascx.cs" %>
<%@ Register TagPrefix="cms" Namespace="CMS.UIControls" Assembly="CMS.UIControls" %>

<asp:Panel runat="server" ID="pnlWebPartMenu" CssClass="PortalContextMenu WebPartContextMenu">
    <cms:UIPlaceHolder ID="pnlUIAddMVTVariant" runat="server" ModuleName="CMS.Content"
        ElementName="WebPartProperties.Variant">
        <cms:ContextMenuItem runat="server" ID="iAddMVTVariant" />
        <cms:ContextMenuSeparator runat="server" ID="sep1" />
    </cms:UIPlaceHolder>
    <cms:UIPlaceHolder ID="pnlUIAddCPVariant" runat="server" ModuleName="CMS.Content"
        ElementName="WebPartProperties.Variant">
        <cms:ContextMenuItem runat="server" ID="iAddCPVariant" />
    </cms:UIPlaceHolder>
</asp:Panel>

<script type="text/javascript">
    //<![CDATA[
    function ContextAddWebPartMVTVariant(definition) {
        AddMVTVariant(definition[0], definition[1], definition[2], definition[3], definition[4], 'widget', '');
    }

    function ContextAddWebPartCPVariant(definition) {
        AddPersonalizationVariant(definition[0], definition[1], definition[2], definition[3], definition[4], 'widget', '');
    }
    //]]>
</script>

