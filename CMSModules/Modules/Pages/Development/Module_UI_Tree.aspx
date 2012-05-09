<%@ Page Title="Module edit - User interface - Tree" Language="C#" MasterPageFile="~/CMSMasterPages/UI/EmptyPage.master"
    AutoEventWireup="true" Inherits="CMSModules_Modules_Pages_Development_Module_UI_Tree" Theme="Default" CodeFile="Module_UI_Tree.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/UIProfiles/MenuActions.ascx" TagName="MenuActions" TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/Trees/UniTree.ascx" TagName="UniTree" TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/Trees/TreeBorder.ascx" TagName="TreeBorder" TagPrefix="cms" %>
<asp:Content ID="Content1" ContentPlaceHolderID="plcContent" runat="Server">
    <cms:MenuActions ID="menuElem" runat="server" />
    <cms:TreeBorder ID="borderElem" runat="server" MinSize="10,*" FramesetName="uiFrameset" />
    <div class="UITreeArea">
        <div class="TreeAreaTree">
            <cms:UniTree ID="uniTree" CssClass="ContentTree" runat="server" Localize="true" IsLiveSite="false" />
        </div>
    </div>
    <asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />

    <script type="text/javascript" language="javascript">
        //<![CDATA[
        function SelectNode(elementId, parentId, moduleId) {
            // Set selected item in tree
            $j('span[name=treeNode]').each(function() {
                var jThis = $j(this);
                jThis.removeClass('ContentTreeSelectedItem');
                if (!jThis.hasClass('ContentTreeItem')) {
                    jThis.addClass('ContentTreeItem');
                }
                if (this.id == 'node_' + elementId) {
                    jThis.addClass('ContentTreeSelectedItem');
                }
            });
            // Update frames URLs
            if (window.parent != null) {
                if (window.parent.frames['uicontent'] != null) {
                    var query = '?moduleID=' + moduleId + '&elementId=' + elementId + '&parentId=' + parentId;
                    if (window.tabIndex) {
                        query += '&tabIndex=' + window.tabIndex;
                    }
                    if (parentId) {
                        window.parent.frames['uicontent'].location = frameURL + query;
                    }
                    else {
                        window.parent.frames['uicontent'].location = newURL + query;
                    }
                }
            }
            // Set menu actions value
            var menuElem = $j('#' + menuHiddenId);
            if (menuElem.length = 1) {
                menuElem[0].value = elementId + '|' + parentId;
                if (window.tabIndex) {
                    menuElem[0].value += '|' + window.tabIndex;
                }
                // Disable menu items for root element
                var menuItems = menuElem.parent().find('.UIProfile_MenuItem, .UIProfile_MenuItem_Disabled');
                setMenuAction(menuItems, parentId)
            }
        }

        function setMenuAction(menuItems, parentId) {
            if (menuItems.length > 0) {
                menuItems.each(function(i) {
                    if (i > 0) {
                        var jThis = $j(this);
                        var lnk = jThis.find('a');
                        if (parentId == 0) {
                            jThis.removeClass('UIProfile_MenuItem');
                            jThis.addClass('UIProfile_MenuItem_Disabled');
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
                            jThis.removeClass('UIProfile_MenuItem_Disabled');
                            jThis.addClass('UIProfile_MenuItem');
                            lnk.attr('href', lnk.attr('_href'));
                            if (lnk.attr('_confirm')) {
                                lnk.prop("onclick", null);
                                lnk.unbind('click');
                                lnk.bind('click', function() { return deleteConfirm() });
                            }
                        }
                    }
                });
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

        var menuItems = $j('#' + menuHiddenId).parent().find('.UIProfile_MenuItem, .UIProfile_MenuItem_Disabled');
        setMenuAction(menuItems, postParentId);

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

</asp:Content>
