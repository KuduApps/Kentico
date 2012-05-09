<%@ Page Language="C#" AutoEventWireup="true" CodeFile="~/CMSModules/Reporting/Tools/ReportCategory_Tree.aspx.cs"
    Inherits="CMSModules_Reporting_Tools_ReportCategory_Tree" Theme="Default" %>

<%@ Register Src="~/CMSModules/Reporting/Controls/ReportTree.ascx" TagName="ReportCategoryTree"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/Trees/TreeBorder.ascx" TagName="TreeBorder"
    TagPrefix="cms" %>
<html>
<head id="Head1" runat="server" enableviewstate="false">
    <title>Report categories tree</title>
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
            position: relative;
            left: -20px;
            width: 100%;
            z-index: 2;
            top: 0px;
        }
        .TreeMenuPadding
        {
            height: 68px;
        }
        .IE7 .TreeMenuPadding, .IE8 .TreeMenuPadding, .Opera .TreeMenuPadding
        {
            height: 80px;
        }
        .MenuSubBox
        {
            position: relative;
            left: 20px;
        }
        .Gecko3 .MenuSubBox
        {
            position: relative;
            left: 19px;
        }
        .Safari .MenuSubBox
        {
            position: relative;
            left: 18px;
        }
        .RTL .Safari .MenuSubBox
        {
            position: relative;
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
            var isReport = ((selectedItemId > 0) && (selectedItemType == 'report'));
            document.getElementById('lnkNewReport').style.color = ((isReport || (selectedItemParent == 0)) ? '#AAAAAA' : 'black');
            document.getElementById('lnkNewCategory').style.color = (isReport ? '#AAAAAA' : 'black');
            document.getElementById('lnkDeleteItem').style.color = (selectedItemParent == 0 ? '#AAAAAA' : 'black');
            document.getElementById('lnkExportObject').style.color = (!isReport ? '#AAAAAA' : 'black');
            document.getElementById('lnkCloneReport').style.color = (!isReport ? '#AAAAAA' : 'black');
        }

        function SelectReportNode(elementId, type, parentId, updateReportContent) {
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
            if (updateReportContent) {
                if ((window.parent != null) && (window.parent.frames['reportedit'] != null)) {

                    if (doNotReloadContent) {
                        doNotReloadContent = false;
                    } else {
                        var contentFrame = window.parent.frames['reportedit'];
                        if (type == 'report') {
                            contentFrame.location = reportURL + '?reportid=' + elementId + '&categoryId=' + parentId;
                        }
                        // parentID
                        else {
                            contentFrame.location = categoryURL + '?categoryid=' + elementId + '&parentid=' + parentId;
                        }
                    }
                }
            }

            // Update menu links
            UpdateMenu();
        }

        function NewItem(type) {
            if ((selectedItemId <= 0) || (selectedItemType == 'report')) return;
            // Under report category (not root)
            if ((type == 'report') && (selectedItemParent > 0)) {
                if ((window.parent != null) && (window.parent.frames['reportedit'] != null)) {
                    parent.frames['reportedit'].location = 'report_New.aspx?categoryId=' + selectedItemId;
                }
            }
            else if (type == 'reportcategory') {

                if ((window.parent != null) && (window.parent.frames['reportedit'] != null)) {
                    parent.frames['reportedit'].location = 'reportcategory_new.aspx?parentCategoryID=' + selectedItemId;
                }
            }
        }

        function ExportObject() {
            if ((selectedItemId > 0) && (selectedItemType == 'report')) {
                OpenExportObject('reporting.report', selectedItemId);
            }
        }

        function CloneReport() {
            if ((selectedItemId > 0) && (selectedItemType == 'report')) {
                parent.frames['reportedit'].location = 'report_list.aspx?reportID=' + selectedItemId + '&categoryID=' + selectedItemParent + '&clone=true';
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
                                        <asp:Image ID="imgNewReport" runat="server" CssClass="NewItemImage" />
                                        <asp:HyperLink ID="lnkNewReport" runat="server" NavigateUrl="#" CssClass="NewItemLink" />
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
                                        <asp:Image ID="imgCloneReport" runat="server" CssClass="NewItemImage" />
                                        <asp:HyperLink ID="lnkCloneReport" runat="server" NavigateUrl="#" CssClass="NewItemLink" />
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
                <cms:ReportCategoryTree ID="treeElem" runat="server" />
            </div>
        </div>
    </asp:Panel>
    <asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />
    </form>
</body>
</html>
