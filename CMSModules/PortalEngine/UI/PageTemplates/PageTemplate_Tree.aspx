<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_PortalEngine_UI_PageTemplates_PageTemplate_Tree"
    Theme="Default" CodeFile="PageTemplate_Tree.aspx.cs" %>

<%@ Register Src="~/CMSModules/PortalEngine/Controls/Layout/PageTemplateTree.ascx"
    TagName="PageTemplateTree" TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/Trees/TreeBorder.ascx" TagName="TreeBorder"
    TagPrefix="cms" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Development - Page templates</title>
    <style type="text/css">
        body
        {
            margin: 0px;
            padding: 0px;
            height: 100%;
            width: 100%;
            overflow: hidden;
        }
        .MenuBox
        {
            position: absolute;
            left: -20px;
            width: 100%;
            z-index: 2;
            top: 0px;
        }
        .TreeMenuPadding
        {
            height: 68px;
        }
        .MenuSubBox
        {
            position: absolute;
            left: 20px;
        }
        .Gecko3 .MenuSubBox
        {
            position: absolute;
            left: 19px;
        }
        .Safari .MenuSubBox
        {
            position: absolute;
            left: 18px;
        }
        .RTL .Safari .MenuSubBox
        {
            position: absolute;
            left: 1px;
        }
        .Safari .MenuBox
        {
            top: 0px;
        }
    </style>

    <script type="text/javascript" language="javascript">
        //<![CDATA[

        var selectedItemId = 0;
        var selectedItemType = '';
        var selectedItemParent = 0;
        var selectedIsReusable = 1;

        function UpdateMenu() {
            var isPageTemplate = ((selectedItemId > 0) && (selectedItemType == 'pagetemplate'));
            document.getElementById('lnkNewTemplate').style.color = ((isPageTemplate || (selectedItemParent == 0)) ? '#AAAAAA' : 'black');
            document.getElementById('lnkNewCategory').style.color = (isPageTemplate ? '#AAAAAA' : 'black');
            document.getElementById('lnkDeleteItem').style.color = (selectedItemParent == 0 ? '#AAAAAA' : 'black');
            document.getElementById('lnkExportObject').style.color = ((!isPageTemplate || !selectedIsReusable) ? '#AAAAAA' : 'black');
        }

        function SelectNode(elementId, type, parentId, reusable) {
            selectedItemId = elementId;
            selectedItemType = type;
            selectedItemParent = parentId;
            selectedIsReusable = (reusable == 'false') ? 0 : 1;

            // Set selected item in tree
            $j('span[name=treeNode]').each(function() {
                var jThis = $j(this);

                jThis.removeClass('ContentTreeSelectedItem');

                if (this.id == type + '_' + elementId) {
                    jThis.addClass('ContentTreeSelectedItem');
                }
            });

            // Update frames URLs
            if ((window.parent != null) && (window.parent.frames['pt_edit'] != null)) {
                if (doNotReloadContent) {
                    doNotReloadContent = false;
                } else {
                    var contentFrame = window.parent.frames['pt_edit'];
                    if (type == 'pagetemplate') {
                        contentFrame.location = pageTemplateURL + '?templateid=' + elementId + '&parentcategoryid=' + parentId;
                    }
                    // parentID
                    else {
                        contentFrame.location = pageTemplateCategoryURL + '?categoryid=' + elementId + '&parentcategoryid=' + parentId;
                    }
                }
            }

            // Update menu links
            UpdateMenu();
        }

        function NewItem(type) {
            if ((selectedItemId <= 0) || (selectedItemType == 'pagetemplate')) return;
            // Under template category (not root)
            if ((type == 'pagetemplate') && (selectedItemParent > 0)) {
                if ((window.parent != null) && (window.parent.frames['pt_edit'] != null)) {
                    parent.frames['pt_edit'].location = pageTemplateNewURL + '?parentcategoryid=' + selectedItemId;
                }
            }
            else if (type == 'pagetemplatecategory') {

                if ((window.parent != null) && (window.parent.frames['pt_edit'] != null)) {
                    parent.frames['pt_edit'].location = pageTemplateCategoryNewURL + '?parentcategoryid=' + selectedItemId;
                }
            }
        }

        function ExportObject() {
            if ((selectedItemId > 0) && (selectedItemType == 'pagetemplate') && selectedIsReusable) {
                OpenExportObject('cms.pagetemplate', selectedItemId);
            }
        }

        function ReloadPage(templateid) {
            if (parent.frames != null && parent.frames['pt_tree'] != null) {
                parent.frames['pt_tree'].location.replace(pageTemplateTreeURL + '?templateid=' + templateid);
            }
        }
        //]]>
    </script>

</head>
<body class="TreeBody <%=mBodyClass%>">
    <form id="form1" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="scriptManager" runat="server" />
    <div class="MenuBox">
        <asp:Panel ID="pnlSubBox" runat="server">
            <div class="MenuSubBox">
                <asp:Panel ID="pnlMenu" runat="server" CssClass="TreeMenu TreeMenuPadding">
                    <asp:Panel ID="pnlMenuContent" runat="server" CssClass="TreeMenuContent">
                        <table style="width: 100%">
                            <tr>
                                <td style="width: 50%">
                                    <div>
                                        <asp:Image ID="imgNewTemplate" runat="server" CssClass="NewItemImage" />
                                        <asp:HyperLink ID="lnkNewTemplate" runat="server" NavigateUrl="#" CssClass="NewItemLink" />
                                    </div>
                                </td>
                                <td>
                                    <div>
                                        <asp:Image ID="imgDeleteItem" runat="server" CssClass="NewItemImage" />
                                        <asp:HyperLink ID="lnkDeleteItem" runat="server" NavigateUrl="#" CssClass="NewItemLink" />
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div>
                                        <asp:Image ID="imgNewCategory" runat="server" CssClass="NewItemImage" />
                                        <asp:HyperLink ID="lnkNewCategory" runat="server" NavigateUrl="#" CssClass="NewItemLink" />
                                    </div>
                                </td>
                                <td>
                                    <div>
                                        <asp:Image ID="imgExportObject" runat="server" CssClass="NewItemImage" />
                                        <asp:HyperLink ID="lnkExportObject" runat="server" NavigateUrl="#" CssClass="NewItemLink" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </asp:Panel>
            </div>
        </asp:Panel>
    </div>
    <asp:Panel runat="server" ID="pnlBody" CssClass="ContentTree">
        <cms:TreeBorder ID="borderElem" runat="server" MinSize="10,*" FramesetName="colsFrameset" />
        <div class="TreeArea">
            <div class="TreeMenuPadding TreeMenu">
                &nbsp;
            </div>
            <div class="TreeAreaTree">
                <cms:PageTemplateTree ID="treeElem" runat="server" IsLiveSite="false" />
            </div>
        </div>
    </asp:Panel>
    <asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />
    </form>
</body>
</html>
