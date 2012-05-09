<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Settings_SiteManager_Categories"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/EmptyPage.master" Title="SiteManager - Settings"
    CodeFile="Categories.aspx.cs" %>

<%@ Register Src="~/CMSFormControls/Sites/SiteSelector.ascx" TagName="SiteSelector"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/Trees/TreeBorder.ascx" TagName="TreeBorder"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Settings/Controls/SettingsTree.ascx" TagName="SettingsTree"
    TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">

    <script type="text/javascript" language="javascript">
        //<![CDATA[

        var selectedItemId = 0;
        var selectedItemParent = 0;

        function NodeSelected(elementName, categoryId, siteId, parentId) {
            selectedItemId = categoryId;
            selectedItemParent = parentId;
            // Update frames URLs
            if ((window.parent != null) && (window.parent.frames['keys'] != null)) {
                if (doNotReloadContent) {
                    doNotReloadContent = false;
                } else {
                    var contentFrame = window.parent.frames['keys'];
                    var url = categoryURL + '?categoryid=' + categoryId + '&parentid=' + parentId;

                    if (siteId > 0) {
                        url = url + "&siteid=" + siteId;
                    }

                    // If helper method exists
                    if (contentFrame.GetSearchValues != null) {
                        var searchSettings = contentFrame.GetSearchValues();
                        
                        if ((searchSettings != null) && (searchSettings[0] == categoryId)) {
                            // Set search settings to url
                            url = url + "&search=" + searchSettings[1] + "&description=" + searchSettings[2];
                        }
                    }
                  
                    document.getElementById('selectedCategoryId').value = categoryId;
                    contentFrame.location = url;
                }
            }
        }
        //]]>
    </script>

    <cms:TreeBorder ID="borderElem" runat="server" MinSize="10,*" FramesetName="colsFrameset" />
    <cms:CMSUpdatePanel runat="server" ID="pnlUpdate" UpdateMode="Always">
        <ContentTemplate>
            <div class="MenuBox">
                <asp:Panel ID="pnlMenu" runat="server" CssClass="TreeMenu TreeMenuPadding SettingsMenu">
                    <asp:Panel ID="pnlMenuContent" runat="server" CssClass="TreeMenuContent">
                        <strong>
                            <asp:Label ID="lblSite" runat="server" CssClass="ContentLabel" EnableViewState="false" />
                        </strong>
                        <br />
                        <cms:SiteSelector ID="siteSelector" runat="server" IsLiveSite="false" />
                    </asp:Panel>
                </asp:Panel>
            </div>
            <asp:Panel runat="server" ID="pnlBody" CssClass="ContentTree">
                <div class="TreeArea">
                    <div class="TreeMenuPadding TreeMenu SettingsMenu">
                        &nbsp;
                    </div>
                    <div class="TreeAreaTree">
                        <cms:SettingsTree ID="TreeViewCategories" ShortID="t" runat="server" CssClass="ContentTree"
                            CategoryName="CMS.Settings" MaxRelativeLevel="10" JavaScriptHandler="NodeSelected" ShowEmptyCategories="false" />
                    </div>
                </div>
            </asp:Panel>
            <input type="hidden" id="selectedCategoryId" name="selectedCategoryId" value="0" />
        </ContentTemplate>
    </cms:CMSUpdatePanel>
</asp:Content>
