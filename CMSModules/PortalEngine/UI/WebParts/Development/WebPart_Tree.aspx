<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_PortalEngine_UI_WebParts_Development_WebPart_Tree"
    Theme="Default" CodeFile="WebPart_Tree.aspx.cs" %>

<%@ Register Src="~/CMSModules/PortalEngine/Controls/WebParts/WebPartTree.ascx" TagName="WebPartTree"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/Trees/TreeBorder.ascx" TagName="TreeBorder"
    TagPrefix="cms" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Development - Web parts</title>
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
            width: 100%;
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

        function UpdateMenu() {
            var isWebPart = ((selectedItemId > 0) && (selectedItemType == 'webpart'));
            document.getElementById('lnkNewWebPart').style.color = ((isWebPart || (selectedItemParent == 0)) ? '#AAAAAA' : 'black');
            document.getElementById('lnkNewCategory').style.color = (isWebPart ? '#AAAAAA' : 'black');
            document.getElementById('lnkDeleteItem').style.color = (selectedItemParent == 0 ? '#AAAAAA' : 'black');
            document.getElementById('lnkExportObject').style.color = (!isWebPart ? '#AAAAAA' : 'black');
            document.getElementById('lnkCloneWebPart').style.color = (!isWebPart ? '#AAAAAA' : 'black');
        }

        function SelectNode(elementId, type, parentId) {
            selectedItemId = elementId;
            selectedItemType = type;
            selectedItemParent = parentId;

            // Set selected item in tree
            $j('span[name=treeNode]').each(function() {
                var jThis = $j(this);

                jThis.removeClass('ContentTreeSelectedItem');

                if (this.id == type + '_' + elementId) {
                    jThis.addClass('ContentTreeSelectedItem');
                }
            });

            // Update frames URLs
            if ((window.parent != null) && (window.parent.frames['webpartedit'] != null)) {
                if (doNotReloadContent) {
                    doNotReloadContent = false;
                } else {
                    var contentFrame = window.parent.frames['webpartedit'];
                    if (type == 'webpart') {
                        contentFrame.location = webpartURL + '?webpartid=' + elementId + '&parentid=' + parentId;
                    }
                    // parentID
                    else {
                        contentFrame.location = categoryURL + '?categoryid=' + elementId + '&parentid=' + parentId;
                    }
                }
            }

            // Update menu links
            UpdateMenu();
        }

        function NewItem(type) {
            if ((selectedItemId <= 0) || (selectedItemType == 'webpart')) return;
            // Under webpart category (not root)
            if ((type == 'webpart') && (selectedItemParent > 0)) {
                if ((window.parent != null) && (window.parent.frames['webpartedit'] != null)) {
                    parent.frames['webpartedit'].location = newWebpartURL + '?parentid=' + selectedItemId;
                }
            }
            else if (type == 'webpartcategory') {

                if ((window.parent != null) && (window.parent.frames['webpartedit'] != null)) {
                    parent.frames['webpartedit'].location = categoryNewURL + '?parentid=' + selectedItemId;
                }
            }
        }

        function ExportObject() {
            if ((selectedItemId > 0) && (selectedItemType == 'webpart')) {
                OpenExportObject('cms.webpart', selectedItemId);
            }
        }

        function CloneWebPart() {
            if ((selectedItemId > 0) && (selectedItemType == 'webpart')) {
                modalDialog(cloneURL + '?webpartid=' + selectedItemId, 'WebPartClone', 500, 250);
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
                                        <asp:Image ID="imgNewWebPart" runat="server" CssClass="NewItemImage" />
                                        <asp:HyperLink ID="lnkNewWebPart" runat="server" NavigateUrl="#" CssClass="NewItemLink" />
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
                            <tr>
                                <td style="width: 50%">
                                    <div>
                                        <asp:Image ID="imgCloneWebpart" runat="server" CssClass="NewItemImage" />
                                        <asp:HyperLink ID="lnkCloneWebPart" runat="server" NavigateUrl="#" CssClass="NewItemLink" />
                                    </div>
                                </td>
                                <td>
                                    <div>
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
                <cms:WebPartTree ID="treeElem" runat="server" UsePostBack="false" ShowRecentlyUsed="false"
                    SelectWebParts="true" IsLiveSite="false" ShowWidgetOnlyWebparts="true" />
            </div>
        </div>
    </asp:Panel>
    <asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />
    </form>
</body>
</html>
