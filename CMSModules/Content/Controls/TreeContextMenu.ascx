<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_Content_Controls_TreeContextMenu"
    CodeFile="TreeContextMenu.ascx.cs" %>
<%@ Register Assembly="CMS.UIControls" Namespace="CMS.UIControls" TagPrefix="cms" %>
<cms:ContextMenu runat="server" ID="menuNew" MenuID="newMenu" VerticalPosition="Bottom"
    HorizontalPosition="Left" OffsetX="25" ActiveItemCssClass="ItemSelected" CssClass="TreeContextMenu TreeNewContextMenu"
    Dynamic="true" MenuLevel="1" ShowMenuOnMouseOver="true">
    <asp:Panel runat="server" ID="pnlNewMenu" CssClass="TreeNewContextMenu">
        <asp:Panel runat="server" ID="pnlNoChild" CssClass="ItemPadding" Visible="false">
            <asp:Literal runat="server" ID="ltlNoChild" EnableViewState="false" />
        </asp:Panel>
        <asp:Repeater runat="server" ID="repNew">
            <ItemTemplate>
                <asp:Panel runat="server" ID="pnlItem" CssClass="Item">
                    <asp:Panel runat="server" ID="pnlItemPadding" CssClass="ItemPadding">
                        <asp:Image runat="server" ID="imgItem" CssClass="IconBig" EnableViewState="false"
                            ImageUrl='<%# GetDocumentTypeIconUrl(Convert.ToString(DataBinder.Eval(Container.DataItem, "ClassName"))) %>' />&nbsp;
                        <asp:Label runat="server" ID="lblItem" CssClass="Name" EnableViewState="false" Text='<%# HttpUtility.HtmlEncode(ResHelper.LocalizeString(DataBinder.Eval(Container.DataItem, "ClassDisplayName") as String)) %>' />
                    </asp:Panel>
                </asp:Panel>
            </ItemTemplate>
        </asp:Repeater>
        <cms:UIPlaceHolder runat="server" ID="plcNewLinkNew" ElementName="New" ModuleName="CMS.Content">
            <cms:UIPlaceHolder runat="server" ID="plcNewLink" ElementName="New.LinkExistingDocument"
                ModuleName="CMS.Content">
                <cms:ContextMenuSeparator runat="server" ID="pnlSepNewLinked" />
                <asp:Panel runat="server" ID="pnlNewLinked" CssClass="ItemLast">
                    <asp:Panel runat="server" ID="pnlNewLinkedPadding" CssClass="ItemPadding">
                        <asp:Image runat="server" ID="imgNewLinked" CssClass="IconBig" EnableViewState="false" />
                        &nbsp;<asp:Label runat="server" ID="lblNewLinked" CssClass="Name" EnableViewState="false"
                            Text="Link an existing document" />
                    </asp:Panel>
                </asp:Panel>
            </cms:UIPlaceHolder>
        </cms:UIPlaceHolder>
        <cms:UIPlaceHolder runat="server" ID="plcNewVariantNew" ElementName="New" ModuleName="CMS.Content">
            <cms:UIPlaceHolder runat="server" ID="plcNewVariant" ElementName="New.ABTestVariant"
                ModuleName="CMS.Content">
                <cms:ContextMenuSeparator runat="server" ID="pnlNewVariantSeparator" Visible="false" />
                <asp:Panel runat="server" ID="pnlNewVariant" CssClass="ItemLast">
                    <asp:Panel runat="server" ID="pnlNewVariantPadding" CssClass="ItemPadding">
                        <asp:Image runat="server" ID="imgNewVariant" CssClass="IconBig" EnableViewState="false" />
                        &nbsp;<asp:Label runat="server" ID="lblNewVariant" CssClass="Name" EnableViewState="false"
                            Text="A/B test page variant" />
                    </asp:Panel>
                </asp:Panel>
            </cms:UIPlaceHolder>
        </cms:UIPlaceHolder>
    </asp:Panel>
</cms:ContextMenu>
<cms:ContextMenu runat="server" ID="menuUp" MenuID="upMenu" VerticalPosition="Bottom"
    HorizontalPosition="Left" OffsetX="25" ActiveItemCssClass="ItemSelected" MouseButton="Right"
    MenuLevel="1" ShowMenuOnMouseOver="true">
    <asp:Panel runat="server" ID="pnlUpMenu" CssClass="TreeContextMenu">
        <asp:Panel runat="server" ID="pnlTop" CssClass="ItemLast">
            <asp:Panel runat="server" ID="pnlTopPadding" CssClass="ItemPadding">
                &nbsp;<asp:Label runat="server" ID="lblTop" CssClass="Name" EnableViewState="false"
                    Text="Top" />
            </asp:Panel>
        </asp:Panel>
    </asp:Panel>
</cms:ContextMenu>
<cms:ContextMenu runat="server" ID="menuDown" MenuID="downMenu" VerticalPosition="Bottom"
    HorizontalPosition="Left" OffsetX="25" ActiveItemCssClass="ItemSelected" MouseButton="Right"
    MenuLevel="1" ShowMenuOnMouseOver="true">
    <asp:Panel runat="server" ID="pnlDownMenu" CssClass="TreeContextMenu">
        <asp:Panel runat="server" ID="pnlBottom" CssClass="ItemLast">
            <asp:Panel runat="server" ID="pnlBottomPadding" CssClass="ItemPadding">
                &nbsp;<asp:Label runat="server" ID="lblBottom" CssClass="Name" EnableViewState="false"
                    Text="Bottom" />
            </asp:Panel>
        </asp:Panel>
    </asp:Panel>
</cms:ContextMenu>
<cms:ContextMenu runat="server" ID="menuSort" MenuID="sortMenu" VerticalPosition="Bottom"
    HorizontalPosition="Left" OffsetX="25" ActiveItemCssClass="ItemSelected" MouseButton="Both"
    MenuLevel="1" ShowMenuOnMouseOver="true">
    <asp:Panel runat="server" ID="pnlSortMenu" CssClass="TreeContextMenu">
        <asp:Panel runat="server" ID="pnlAlphaAsc" CssClass="Item">
            <asp:Panel runat="server" ID="pnlAlphaAscPadding" CssClass="ItemPadding">
                &nbsp;<asp:Label runat="server" ID="lblAlphaAsc" CssClass="Name" EnableViewState="false"
                    Text="Alphabetically A-Z" />
            </asp:Panel>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlAlphaDesc" CssClass="Item">
            <asp:Panel runat="server" ID="pnlAlphaDescPadding" CssClass="ItemPadding">
                &nbsp;<asp:Label runat="server" ID="lblAlphaDesc" CssClass="Name" EnableViewState="false"
                    Text="Alphabetically Z-A" />
            </asp:Panel>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlDateAsc" CssClass="Item">
            <asp:Panel runat="server" ID="pnlDateAscPadding" CssClass="ItemPadding">
                &nbsp;<asp:Label runat="server" ID="lblDateAsc" CssClass="Name" EnableViewState="false"
                    Text="By date asc." />
            </asp:Panel>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlDateDesc" CssClass="ItemLast">
            <asp:Panel runat="server" ID="pnlDateDescPadding" CssClass="ItemPadding">
                &nbsp;<asp:Label runat="server" ID="lblDateDesc" CssClass="Name" EnableViewState="false"
                    Text="By date desc." />
            </asp:Panel>
        </asp:Panel>
    </asp:Panel>
</cms:ContextMenu>
<cms:ContextMenu runat="server" ID="menuProperties" MenuID="propertiesMenu" VerticalPosition="Bottom"
    HorizontalPosition="Left" OffsetX="25" ActiveItemCssClass="ItemSelected" MenuLevel="1"
    ShowMenuOnMouseOver="true">
    <asp:Panel runat="server" ID="pnlPropertiesMenu" CssClass="TreeContextMenu">
        <cms:UIPlaceHolder ID="pnlUIGeneral" runat="server" ModuleName="CMS.Content" ElementName="Properties.General">
            <asp:Panel runat="server" ID="pnlGeneral" CssClass="Item">
                <asp:Panel runat="server" ID="pnlGeneralPadding" CssClass="ItemPadding">
                    &nbsp;
                    <asp:Label runat="server" ID="lblGeneral" CssClass="Name" EnableViewState="false"
                        Text="General" />
                </asp:Panel>
            </asp:Panel>
        </cms:UIPlaceHolder>
        <cms:UIPlaceHolder ID="pnlUIUrls" runat="server" ModuleName="CMS.Content" ElementName="Properties.URLs">
            <asp:Panel runat="server" ID="pnlUrls" CssClass="Item">
                <asp:Panel runat="server" ID="pnlUrlsPadding" CssClass="ItemPadding">
                    &nbsp;
                    <asp:Label runat="server" ID="lblUrls" CssClass="Name" EnableViewState="false" Text="URLs" />
                </asp:Panel>
            </asp:Panel>
        </cms:UIPlaceHolder>
        <cms:UIPlaceHolder ID="pnlUITemplate" runat="server" ModuleName="CMS.Content" ElementName="Properties.Template">
            <asp:Panel runat="server" ID="pnlTemplate" CssClass="Item">
                <asp:Panel runat="server" ID="pnlTemplatePadding" CssClass="ItemPadding">
                    &nbsp;
                    <asp:Label runat="server" ID="lblTemplate" CssClass="Name" EnableViewState="false"
                        Text="Template" />
                </asp:Panel>
            </asp:Panel>
        </cms:UIPlaceHolder>
        <cms:UIPlaceHolder ID="pnlUIMetadata" runat="server" ModuleName="CMS.Content" ElementName="Properties.MetaData">
            <asp:Panel runat="server" ID="pnlMetadata" CssClass="Item">
                <asp:Panel runat="server" ID="pnlMetadataPadding" CssClass="ItemPadding">
                    &nbsp;
                    <asp:Label runat="server" ID="lblMetadata" CssClass="Name" EnableViewState="false"
                        Text="Metadata" />
                </asp:Panel>
            </asp:Panel>
        </cms:UIPlaceHolder>
        <cms:UIPlaceHolder ID="pnlUICategories" runat="server" ModuleName="CMS.Content" ElementName="Properties.Categories">
            <asp:Panel runat="server" ID="pnlCategories" CssClass="Item">
                <asp:Panel runat="server" ID="pnlCategoriesPadding" CssClass="ItemPadding">
                    &nbsp;
                    <asp:Label runat="server" ID="lblCategories" CssClass="Name" EnableViewState="false"
                        Text="Categories" />
                </asp:Panel>
            </asp:Panel>
        </cms:UIPlaceHolder>
        <cms:UIPlaceHolder ID="pnlUIMenu" runat="server" ModuleName="CMS.Content" ElementName="Properties.Menu">
            <asp:Panel runat="server" ID="pnlMenu" CssClass="Item">
                <asp:Panel runat="server" ID="pnlMenuPadding" CssClass="ItemPadding">
                    &nbsp;
                    <asp:Label runat="server" ID="lblMenu" CssClass="Name" EnableViewState="false" Text="General" />
                </asp:Panel>
            </asp:Panel>
        </cms:UIPlaceHolder>
        <cms:UIPlaceHolder ID="pnlUIWorkflow" runat="server" ModuleName="CMS.Content" ElementName="Properties.Workflow">
            <asp:Panel runat="server" ID="pnlWorkflow" CssClass="Item">
                <asp:Panel runat="server" ID="pnlWorkflowPadding" CssClass="ItemPadding">
                    &nbsp;
                    <asp:Label runat="server" ID="lblWorkflow" CssClass="Name" EnableViewState="false"
                        Text="General" />
                </asp:Panel>
            </asp:Panel>
        </cms:UIPlaceHolder>
        <cms:UIPlaceHolder ID="pnlUIVersions" runat="server" ModuleName="CMS.Content" ElementName="Properties.Versions">
            <asp:Panel runat="server" ID="pnlVersions" CssClass="Item">
                <asp:Panel runat="server" ID="pnlVersionsPadding" CssClass="ItemPadding">
                    &nbsp;
                    <asp:Label runat="server" ID="lblVersions" CssClass="Name" EnableViewState="false"
                        Text="Versions" />
                </asp:Panel>
            </asp:Panel>
        </cms:UIPlaceHolder>
        <cms:UIPlaceHolder ID="pnlUIRelated" runat="server" ModuleName="CMS.Content" ElementName="Properties.RelatedDocs">
            <asp:Panel runat="server" ID="pnlRelated" CssClass="Item">
                <asp:Panel runat="server" ID="pnlRelatedPadding" CssClass="ItemPadding">
                    &nbsp;
                    <asp:Label runat="server" ID="lblRelated" CssClass="Name" EnableViewState="false"
                        Text="Related docs" />
                </asp:Panel>
            </asp:Panel>
        </cms:UIPlaceHolder>
        <cms:UIPlaceHolder ID="pnlUILinked" runat="server" ModuleName="CMS.Content" ElementName="Properties.LinkedDocs">
            <asp:Panel runat="server" ID="pnlLinked" CssClass="Item">
                <asp:Panel runat="server" ID="pnlLinkedPadding" CssClass="ItemPadding">
                    &nbsp;
                    <asp:Label runat="server" ID="lblLinked" CssClass="Name" EnableViewState="false"
                        Text="Linked docs" />
                </asp:Panel>
            </asp:Panel>
        </cms:UIPlaceHolder>
        <cms:UIPlaceHolder ID="pnlUISecurity" runat="server" ModuleName="CMS.Content" ElementName="Properties.Security">
            <asp:Panel runat="server" ID="pnlSecurity" CssClass="Item">
                <asp:Panel runat="server" ID="pnlSecurityPadding" CssClass="ItemPadding">
                    &nbsp;
                    <asp:Label runat="server" ID="lblSecurity" CssClass="Name" EnableViewState="false"
                        Text="Security" />
                </asp:Panel>
            </asp:Panel>
        </cms:UIPlaceHolder>
        <cms:UIPlaceHolder ID="pnlUIAttachments" runat="server" ModuleName="CMS.Content"
            ElementName="Properties.Attachments">
            <asp:Panel runat="server" ID="pnlAttachments" CssClass="Item">
                <asp:Panel runat="server" ID="pnlAttachmentsPadding" CssClass="ItemPadding">
                    &nbsp;
                    <asp:Label runat="server" ID="lblAttachments" CssClass="Name" EnableViewState="false"
                        Text="Attachments" />
                </asp:Panel>
            </asp:Panel>
        </cms:UIPlaceHolder>
        <cms:UIPlaceHolder ID="pnlUILanguages" runat="server" ModuleName="CMS.Content" ElementName="Properties.Languages">
            <asp:Panel runat="server" ID="pnlLanguages" CssClass="Item">
                <asp:Panel runat="server" ID="pnlLanguagesPadding" CssClass="ItemPadding">
                    &nbsp;
                    <asp:Label runat="server" ID="lblLanguages" CssClass="Name" EnableViewState="false"
                        Text="Languages" />
                </asp:Panel>
            </asp:Panel>
        </cms:UIPlaceHolder>
        <cms:UIPlaceHolder ID="pnlUICPVariants" runat="server" ModuleName="CMS.Content" ElementName="Properties.Variants">
            <asp:Panel runat="server" ID="pnlCPVariants" CssClass="Item">
                <asp:Panel runat="server" ID="pnlCPVariantsPadding" CssClass="ItemPadding">
                    &nbsp;
                    <cms:LocalizedLabel ID="lblCPVariants" runat="server" EnableViewState="false" CssClass="Name" ResourceString="content.ui.propertiesvariants" />
                </asp:Panel>
            </asp:Panel>
        </cms:UIPlaceHolder>
    </asp:Panel>
</cms:ContextMenu>
<asp:Panel runat="server" ID="pnlNodeMenu" CssClass="TreeContextMenu">
    <cms:ContextMenuContainer runat="server" ID="cmcNew" MenuID="newMenu" Parameter="GetContextMenuParameter('nodeMenu')">
        <asp:Panel runat="server" ID="pnlNew" CssClass="Item">
            <asp:Panel runat="server" ID="pnlNewPadding" CssClass="ItemPadding">
                <asp:Image runat="server" ID="imgNew" CssClass="Icon" EnableViewState="false" />&nbsp;<asp:Label
                    runat="server" ID="lblNew" CssClass="Name" EnableViewState="false" Text="New document" />
            </asp:Panel>
        </asp:Panel>
    </cms:ContextMenuContainer>
    <asp:Panel runat="server" ID="pnlDelete" CssClass="ItemLast">
        <asp:Panel runat="server" ID="pnlDeletePadding" CssClass="ItemPadding">
            <asp:Image runat="server" ID="imgDelete" CssClass="Icon" EnableViewState="false" />&nbsp;<asp:Label
                runat="server" ID="lblDelete" CssClass="Name" EnableViewState="false" Text="Delete" />
        </asp:Panel>
    </asp:Panel>
    <cms:ContextMenuSeparator runat="server" ID="pnlSep1" />
    <asp:Panel runat="server" ID="pnlCopy" CssClass="Item">
        <asp:Panel runat="server" ID="pnlCopyPadding" CssClass="ItemPadding">
            <asp:Image runat="server" ID="imgCopy" CssClass="Icon" EnableViewState="false" />&nbsp;<asp:Label
                runat="server" ID="lblCopy" CssClass="Name" EnableViewState="false" Text="Copy" />
        </asp:Panel>
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlMove" CssClass="ItemLast">
        <asp:Panel runat="server" ID="pnlMovePadding" CssClass="ItemPadding">
            <asp:Image runat="server" ID="imgMove" CssClass="Icon" EnableViewState="false" />&nbsp;<asp:Label
                runat="server" ID="lblMove" CssClass="Name" EnableViewState="false" Text="Move" />
        </asp:Panel>
    </asp:Panel>
    <cms:ContextMenuSeparator runat="server" ID="pnlSep2" />
    <cms:ContextMenuContainer runat="server" ID="cmcUp" MenuID="upMenu">
        <asp:Panel runat="server" ID="pnlUp" CssClass="Item">
            <asp:Panel runat="server" ID="pnlUpPadding" CssClass="ItemPadding">
                <asp:Image runat="server" ID="imgUp" CssClass="Icon" EnableViewState="false" />&nbsp;<asp:Label
                    runat="server" ID="lblUp" CssClass="Name" EnableViewState="false" Text="Up" />
            </asp:Panel>
        </asp:Panel>
    </cms:ContextMenuContainer>
    <cms:ContextMenuContainer runat="server" ID="cmcDown" MenuID="downMenu">
        <asp:Panel runat="server" ID="pnlDown" CssClass="Item">
            <asp:Panel runat="server" ID="pnlDownPadding" CssClass="ItemPadding">
                <asp:Image runat="server" ID="imgDown" CssClass="Icon" EnableViewState="false" />&nbsp;<asp:Label
                    runat="server" ID="lblDown" CssClass="Name" EnableViewState="false" Text="Down" />
            </asp:Panel>
        </asp:Panel>
    </cms:ContextMenuContainer>
    <cms:ContextMenuContainer runat="server" ID="cmcSort" MenuID="sortMenu">
        <asp:Panel runat="server" ID="pnlSort" CssClass="ItemLast">
            <asp:Panel runat="server" ID="pnlSortPadding" CssClass="ItemPadding">
                <asp:Image runat="server" ID="imgSort" CssClass="Icon" EnableViewState="false" />&nbsp;<asp:Label
                    runat="server" ID="lblSort" CssClass="NameInactive" EnableViewState="false" Text="Sort" />
            </asp:Panel>
        </asp:Panel>
    </cms:ContextMenuContainer>
    <cms:ContextMenuSeparator runat="server" ID="pnlSep3" />
    <asp:Panel runat="server" ID="pnlRefresh" CssClass="ItemLast">
        <asp:Panel runat="server" ID="pnlRefreshPadding" CssClass="ItemPadding">
            <asp:Image runat="server" ID="imgRefresh" CssClass="Icon" EnableViewState="false" />&nbsp;<asp:Label
                runat="server" ID="lblRefresh" CssClass="Name" EnableViewState="false" Text="Refresh subsection" />
        </asp:Panel>
    </asp:Panel>
    <cms:ContextMenuSeparator runat="server" ID="pnlSep4" />
    <cms:UIPlaceHolder ID="pnlUIcmcProperties" runat="server" ModuleName="CMS.Content"
        ElementName="Properties">
        <cms:ContextMenuContainer runat="server" ID="cmcProperties" MenuID="propertiesMenu">
            <asp:Panel runat="server" ID="pnlProperties" CssClass="ItemLast">
                <asp:Panel runat="server" ID="pnlPropertiesPadding" CssClass="ItemPadding">
                    <asp:Image runat="server" ID="imgProperties" CssClass="Icon" EnableViewState="false" />&nbsp;
                    <asp:Label runat="server" ID="lblProperties" CssClass="Name" EnableViewState="false"
                        Text="Properties" />
                </asp:Panel>
            </asp:Panel>
        </cms:ContextMenuContainer>
    </cms:UIPlaceHolder>
</asp:Panel>
