<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Content_CMSDesk_Properties_Categories"
    Theme="Default" CodeFile="Categories.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/PageElements/Help.ascx" TagName="Help" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Categories/Controls/MultipleCategoriesSelector.ascx"
    TagName="MultipleCategoriesSelector" TagPrefix="cms" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Categories</title>
    <style type="text/css">
        body
        {
            margin: 0px;
            padding: 0px;
            height: 100%;
        }
        .MenuItemEdit, .MenuItemEditSmall
        {
            padding-right: 0px !important;
        }
    </style>
</head>
<body class="VerticalTabsBody <%=mBodyClass%>">
    <form id="form1" runat="server">
    <%-- Placeholder for uniselector context menu to be inserted --%>
    <asp:PlaceHolder ID="PlaceHolder1" runat="server" />
    <asp:Panel runat="server" ID="pnlBody" CssClass="VerticalTabsPageBody">
        <asp:Panel runat="server" ID="pnlTab" CssClass="VerticalTabsPageContent">
            <asp:Panel runat="server" ID="pnlMenu" CssClass="ContentEditMenu">
                <table width="100%">
                    <tr>
                        <td style="width: 100%;">
                            <div style="height: 24px; padding: 5px;">
                            </div>
                        </td>
                        <td>
                            <cms:Help ID="helpElem" runat="server" TopicName="category" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnlContent" runat="server" CssClass="PageContent PropertiesPanel">
                <asp:Label ID="lblCategoryInfo" runat="server" CssClass="InfoLabel" EnableViewState="false"
                    Visible="false" />
                <strong>
                    <cms:LocalizedLabel ID="lblTitle" runat="server" ResourceString="categories.documentassignedto"
                        DisplayColon="true" EnableViewState="false" CssClass="InfoLabel" /></strong>
                <cms:MultipleCategoriesSelector ID="categoriesElem" runat="server" IsLiveSite="false" />
            </asp:Panel>
            <asp:Panel ID="pnlUserCatgerories" runat="server" Style="padding: 10px 10px;">
            </asp:Panel>
        </asp:Panel>
    </asp:Panel>
    </form>
</body>
</html>
