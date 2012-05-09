<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustomSettings_Menu.aspx.cs"
    MasterPageFile="~/CMSMasterPages/UI/EmptyPage.master" Inherits="CMSModules_Settings_Development_CustomSettings_CustomSettings_Menu"
    Theme="Default" %>

<%@ Register Src="~/CMSModules/Settings/Controls/SettingsTree.ascx" TagName="SettingsTree"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/Trees/TreeBorder.ascx" TagName="TreeBorder"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Settings/Controls/MenuActions.ascx" TagName="MenuActions"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/Trees/UniTree.ascx" TagName="UniTree" TagPrefix="cms" %>
<asp:Content ID="Content1" ContentPlaceHolderID="plcContent" runat="Server">
    <cms:MenuActions ID="menuElem" runat="server" />
    <cms:TreeBorder ID="borderElem" runat="server" MinSize="10,*" FramesetName="colsFrameset" />
    <div class="UITreeArea">
        <div class="TreeAreaTree">
            <cms:SettingsTree ID="treeSettings" runat="server" CategoryName="CMS.CustomSettings"
                MaxRelativeLevel="10" JavaScriptHandler="NodeSelected" />
        </div>
    </div>
    <asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />

    <script type="text/javascript" language="javascript">
        //<![CDATA[
        function NodeSelected(elementName, elementId, siteId, parentId) {
            // Update frames URLs
            if (window.parent != null) {
                if (window.parent.frames['customsettingsmain'] != null) {
                    var query = '&categoryid=' + elementId + '&siteid=' + siteId;
                    if (window.tabIndex) {
                        query += '&tabIndex=' + window.tabIndex;
                    }
                    window.parent.frames['customsettingsmain'].location = frameURL + query;
                }
            }

            // Set menu actions value
            enableMenu(elementId, parentId);
        }



        function setMenuAction(menuItems, parentId, elementId) {
            if (menuItems.length > 0) {
                menuItems.each(function(i) {
                    if (i > 0) {
                        var jThis = $j(this);
                        var lnk = jThis.find('a');
                        if (parentId == 0 || parentId == rootParentId || ((selectedTreeNode == 'CMS.CustomSettings') && (lnk.attr('id').indexOf('Delete') > 0))) {
                            jThis.removeClass('CustomSettings_MenuItem');
                            jThis.addClass('CustomSettings_MenuItem_Disabled');
                            if (lnk.attr('href') != "#") {
                                lnk.attr('_href', lnk.attr('href'));
                            }
                            lnk.attr('href', '#');
                            if (lnk.attr('onclick') || lnk.attr('_confirm')) {
                                lnk.prop("onclick", null);
                                lnk.removeAttr('onclick');
                                lnk.unbind('click');
                                lnk.attr('_confirm', 'true');
                            }
                        }
                        else {
                            jThis.removeClass('CustomSettings_MenuItem_Disabled');
                            jThis.addClass('CustomSettings_MenuItem');
                            lnk.attr('href', lnk.attr('_href'));
                            if (lnk.attr('_confirm')) {
                                lnk.prop("onclick", null);
                                lnk.removeAttr('onclick');
                                lnk.unbind('click');
                                lnk.bind('click', function() { return deleteConfirm(); });
                            }
                        }
                    }
                });
            }
        }

        function enableMenu(elementId, parentId) {
            // Set menu actions value
            var menuElem = $j('#' + menuHiddenId);
            if (menuElem.length = 1) {
                menuElem[0].value = elementId + '|' + parentId;
                if (window.tabIndex) {
                    menuElem[0].value += '|' + window.tabIndex;
                }
                // Disable menu items for root element
                var menuItems = menuElem.parent().find('.CustomSettings_MenuItem, .CustomSettings_MenuItem_Disabled');
                setMenuAction(menuItems, parentId, elementId)
            }
        }

        function setTab(tabIndex) {
            window.tabIndex = tabIndex;
            var menuElem = $j('#' + menuHiddenId);
            if (menuElem.length = 1) {
                var menuValue = menuElem[0].value.match(/^\d+\|\d+/, '');
                menuElem[0].value = menuValue + '|' + tabIndex;
            }
        }

        var menuItems = $j('#' + menuHiddenId).parent().find('.CustomSettings_MenuItem, .CustomSettings_MenuItem_Disabled');
        setMenuAction(menuItems, postParentId, 0);

        // If frame is minimized swap resize handlers
        if (document.body.clientWidth === 10) {
            var minimizeElem = document.getElementById('imgMinimize');
            var maximizeElem = document.getElementById('imgMaximize');
            if ((minimizeElem != null) && (maximizeElem != null)) {
                minimizeElem.style.display = 'none';
                maximizeElem.style.display = 'inline';
                originalSizes[0] = '240,*';
            }
        }
        //]]>
    </script>

    <asp:Literal ID="ltlAfterScript" runat="server" EnableViewState="false" />
</asp:Content>
