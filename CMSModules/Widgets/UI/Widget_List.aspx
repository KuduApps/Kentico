<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Widgets_UI_Widget_List"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" CodeFile="Widget_List.aspx.cs" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>

<asp:Content ContentPlaceHolderID="plcContent" ID="content" runat="server">

    <script type="text/javascript">
        //<![CDATA[
        function EditWidget(widgetId) {
            parent.location.replace("Widget_Edit_Frameset.aspx?widgetid=" + widgetId + "&categoryid=" + "<%=categoryId%>");
            parent.parent.frames['widgettree'].location.href = "WidgetTree.aspx?widgetid=" + widgetId;
        }
        //]]>
    </script>

    <asp:Literal runat="server" ID="ltlScript" />
    <cms:UniGrid runat="server" ID="widgetGrid" GridName="Widget_List.xml"
        OrderBy="DisplayName" Columns="ObjectID, DisplayName" IsLiveSite="false" />
</asp:Content>
