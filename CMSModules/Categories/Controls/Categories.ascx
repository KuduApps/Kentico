<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Categories.ascx.cs" Inherits="CMSModules_Categories_Controls_Categories" %>
<%@ Register Src="~/CMSAdminControls/UI/Trees/UniTree.ascx" TagName="UniTree" TagPrefix="cms" %>
<%@ Register Src="~/CMSFormControls/Sites/SiteSelector.ascx" TagName="SiteSelector"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Categories/Controls/CategoryEdit.ascx" TagName="CategoryEdit"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/AdminControls/Controls/Documents/Documents.ascx" TagName="Documents"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSFormControls/Filters/DocumentFilter.ascx" TagName="DocumentFilter"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/PageElements/PageTitle.ascx" TagName="PageTitle"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<%@ Register Namespace="CMS.UIControls.UniGridConfig" TagPrefix="ug" Assembly="CMS.UIControls" %>
<asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />

<script type="text/javascript" language="javascript">
    //<![CDATA[
    var selectedTreeNode = '';

    function SelectNode(elementName) {
        // Set selected item in tree
        selectedTreeNode = elementName;
        $j('span[id^="node_"]').each(function() {
            var jThis = $j(this);
            jThis.removeClass('ContentTreeSelectedItem');
            if (!jThis.hasClass('ContentTreeItem')) {
                jThis.addClass('ContentTreeItem');
            }
            if (this.id == 'node_' + elementName) {
                jThis.addClass('ContentTreeSelectedItem');
            }
        });
    }

    function NodeSelected(elementId, parentId) {
        // Set menu actions value
        var menuElem = $j('#' + menuHiddenId);
        if (menuElem.length = 1) {
            menuElem[0].value = elementId + '|' + parentId;
        }

        RaiseHiddenPostBack();
    }
    //]]>
</script>

<cms:CMSUpdatePanel ID="pnlUpdateHidden" runat="server" RenderMode="Inline">
    <ContentTemplate>
        <asp:HiddenField ID="hidSelectedElem" runat="server" />
        <asp:Button ID="hdnButton" runat="server" OnClick="hdnButton_Click" CssClass="HiddenButton"
            EnableViewState="false" />
    </ContentTemplate>
</cms:CMSUpdatePanel>
<asp:PlaceHolder ID="plcSelectSite" runat="server">
    <div class="PageHeaderLine SiteHeaderLine">
        <cms:LocalizedLabel ID="lblSite" runat="server" ResourceString="General.Site" DisplayColon="true"></cms:LocalizedLabel>
        <cms:SiteSelector ID="SelectSite" runat="server" IsLiveSite="false" AllowAll="false"
            AllowEmpty="false" AllowGlobal="true" />
    </div>
</asp:PlaceHolder>
<div id="categories" class="Categories">
    <asp:Panel ID="pnlLeftContent" runat="server" CssClass="DialogLeftBlock">
        <cms:CMSUpdatePanel ID="pnlUpdateActions" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:PlaceHolder ID="plcFolderActions" runat="server">
                    <div class="MenuSubBox">
                        <div class="TreeMenuPadding" id="pnlMenu">
                            <div class="TreeMenuContent" id="pnlMenuContent">
                                <table style="width: 100%; padding: 0px;">
                                    <tbody>
                                        <tr>
                                            <td style="width: 50%; height: 24px;">
                                                <div>
                                                    <asp:Image ID="imgNewCategory" runat="server" CssClass="NewItemImage"/><asp:LinkButton ID="btnNew" runat="server" OnClick="btnNewElem_Click" EnableViewState="false" />
                                                </div>
                                            </td>
                                            <td style="width: 50%">
                                                <div>
                                                    <asp:Image ID="imgMoveUp" runat="server" CssClass="NewItemImage"/><asp:LinkButton ID="btnUp" runat="server" OnClick="btnUpElem_Click" EnableViewState="false" />
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 50%; height: 24px;">
                                                <div>
                                                    <asp:Image ID="imgDeleteCategory" runat="server" CssClass="NewItemImage"/><asp:LinkButton ID="btnDelete" runat="server" OnClick="btnDeleteElem_Click" EnableViewState="false"
                                                        OnClientClick="return deleteConfirm();" />
                                                </div>
                                            </td>
                                            <td style="width: 50%">
                                                <div>
                                                    <asp:Image ID="imgMoveDown" runat="server" CssClass="NewItemImage"/><asp:LinkButton ID="btnDown" runat="server" OnClick="btnDownElem_Click" EnableViewState="false" />
                                                </div>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </asp:PlaceHolder>
            </ContentTemplate>
        </cms:CMSUpdatePanel>
        <asp:Panel ID="Panel1" runat="server" class="DialogTreeArea" Height="100%">
            <div class="DialogTree" style="overflow: auto;">
                <cms:CMSUpdatePanel ID="pnlUpdateTree" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Panel ID="pnlTreeArea" runat="server" class="DialogTreeArea">
                            <cms:UniTree runat="server" ID="treeElemG" ShortID="tg" Localize="true" IsLiveSite="false"
                                UsePostBack="false" EnableRootAction="false" />
                            <cms:UniTree runat="server" ID="treeElemP" ShortID="tp" Localize="true" IsLiveSite="false"
                                UsePostBack="false" EnableRootAction="false" />
                        </asp:Panel>
                    </ContentTemplate>
                </cms:CMSUpdatePanel>
            </div>
        </asp:Panel>
    </asp:Panel>
    <div class="DialogTreeAreaSeparator">
    </div>
    <asp:Panel ID="pnlRightContent" runat="server" CssClass="DialogRightBlock">
        <cms:CMSUpdatePanel ID="pnlUpdateContent" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Panel ID="pnlHeader" runat="server" CssClass="PageHeader" EnableViewState="false">
                    <cms:PageTitle ID="titleElem" ShortID="pt" runat="server" />
                </asp:Panel>
                <div id="divDialogView" runat="server">
                    <asp:PlaceHolder ID="plcEdit" runat="server" Visible="false">
                        <cms:JQueryTabContainer ID="pnlTabs" runat="server" CssClass="Dialog_Tabs LightTabs BreadTabs">
                            <cms:JQueryTab ID="tabCategories" runat="server">
                                <ContentTemplate>
                                    <div class="HeaderSeparator">
                                        &nbsp;</div>
                                    <div class="PageContent">
                                        <cms:UniGrid runat="server" ID="gridSubCategories" ShortID="g" OrderBy="CategorySiteID, CategoryOrder"
                                            Columns="CategoryID, CategoryDisplayName, CategoryUserID, CategorySiteID, CategoryEnabled, (SELECT COUNT(DocumentID) FROM CMS_DocumentCategory WHERE CMS_DocumentCategory.CategoryID = CMS_Category.CategoryID) AS DocumentCount"
                                            ObjectType="cms.category" ShowObjectMenu="false" CheckRelative="true">
                                            <GridActions Parameters="CategoryID">
                                                <ug:Action Name="edit" ExternalSourceName="Edit" Caption="$general.edit$" Icon="Edit.png" />
                                                <ug:Action Name="delete" ExternalSourceName="Delete" Caption="$General.Delete$" Icon="Delete.png" Confirmation="$General.ConfirmDelete$" />
                                            </GridActions>
                                            <GridColumns>
                                                <ug:Column Source="CategoryDisplayName" Caption="$categories.category$" Wrap="false"
                                                    Localize="true">
                                                    <Filter Type="text" />
                                                </ug:Column>
                                                <ug:Column Source="CategoryEnabled" ExternalSourceName="#yesno" Caption="$general.enabled$"
                                                    Wrap="false" Localize="true">
                                                </ug:Column>
                                                <ug:Column Source="DocumentCount" Caption="$category_edit.documents$" Wrap="false"
                                                    AllowSorting="false">
                                                </ug:Column>
                                                <ug:Column Width="100%" />
                                            </GridColumns>
                                            <GridOptions DisplayFilter="true" />
                                        </cms:UniGrid>
                                    </div>
                                </ContentTemplate>
                            </cms:JQueryTab>
                            <cms:JQueryTab ID="tabGeneral" runat="server">
                                <ContentTemplate>
                                    <div class="HeaderSeparator">
                                        &nbsp;</div>
                                    <div class="PageContent">
                                        <cms:CategoryEdit ID="catEdit" runat="server" Visible="true" AllowDisabledParents="true" />
                                    </div>
                                </ContentTemplate>
                            </cms:JQueryTab>
                            <cms:JQueryTab ID="tabDocuments" runat="server">
                                <ContentTemplate>
                                    <div class="HeaderSeparator">
                                        &nbsp;</div>
                                    <div class="PageContent">
                                        <asp:PlaceHolder ID="plcFilter" runat="server">
                                            <cms:DocumentFilter ID="filterDocuments" runat="server" LoadSites="true" />
                                            <br />
                                            <br />
                                        </asp:PlaceHolder>
                                        <strong>
                                            <cms:LocalizedLabel ID="lblTitle" runat="server" ResourceString="categories_edit.documents.used"
                                                DisplayColon="true" EnableViewState="false" CssClass="InfoLabel" /></strong>
                                        <cms:Documents ID="gridDocuments" runat="server" ListingType="CategoryDocuments" />
                                    </div>
                                </ContentTemplate>
                            </cms:JQueryTab>
                        </cms:JQueryTabContainer>
                    </asp:PlaceHolder>
                    <asp:PlaceHolder ID="plcNew" runat="server" Visible="false">
                        <div class="PageContent">
                            <cms:CategoryEdit ID="catNew" runat="server" Visible="true" AllowDisabledParents="true" />
                        </div>
                    </asp:PlaceHolder>
                    <asp:PlaceHolder ID="plcInfo" runat="server" Visible="false">
                        <div class="PageContent">
                            <cms:LocalizedLabel ID="lblInfo" runat="server" ResourceString="categories.SelectOrCreateInfo"
                                CssClass="InfoLabel" EnableViewState="false" />
                        </div>
                    </asp:PlaceHolder>
                </div>
            </ContentTemplate>
        </cms:CMSUpdatePanel>
    </asp:Panel>
</div>

<script type="text/javascript" language="javascript">
    //<![CDATA[
    function initDesign() {
        var docHeight = $j('body').height();

        var container = $j('#categories').parent('.TabsContent')
        if (container.length) {
            container.css('padding', '0px'); ;
            container.height(500);
            docHeight = 500;
        }

        var titleHeight = $j('.PageHeader').outerHeight();
        var siteHeight = $j('.PageHeaderLine').outerHeight();
        var contentHeight = docHeight - titleHeight - siteHeight;

        $j('#categories').height(contentHeight);
        $j('.DialogTreeAreaSeparator').height(contentHeight);
        var menuHeight = $j('.MenuSubBox').outerHeight();
        $j('.DialogTree').height(contentHeight - menuHeight - 3);

        var tabsHeight = $j('div.JqueryUITabs>ul.ui-tabs-nav').outerHeight();
        var breadcrumbsHeight = $j('.PageTitleBreadCrumbsPadding').outerHeight();
        $j('.JqueryUITabs>div').height(contentHeight - tabsHeight - breadcrumbsHeight);
        $j('.ui-tabs-panel').height(contentHeight - tabsHeight - breadcrumbsHeight - 12);
        $j('.ui-tabs-panel').css('overflow', 'auto');
    }

    $j(function() { initDesign(); });
    $j(window).resize(function() { initDesign(); });
    //]]>
</script>

