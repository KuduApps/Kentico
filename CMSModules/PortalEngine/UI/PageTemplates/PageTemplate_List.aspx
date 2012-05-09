<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_PortalEngine_UI_PageTemplates_PageTemplate_List"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" CodeFile="PageTemplate_List.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<asp:Content ContentPlaceHolderID="plcContent" ID="content" runat="server">

    <script type="text/javascript">
        //<![CDATA[
        function EditPageTemplate(templateId) {
            parent.location.replace("PageTemplate_Edit.aspx?templateid=" + templateId + "&categoryid=" + "<%=categoryId%>");
            parent.parent.frames['pt_tree'].location.href = "PageTemplate_Tree.aspx?templateid=" + templateId;
        }
        //]]>
    </script>

    <asp:Literal runat="server" ID="ltlScript" />
    <cms:UniGrid runat="server" ID="pageTemplatesGrid" GridName="PageTemplate_List.xml"
        OrderBy="DisplayName" Columns="ObjectID, DisplayName" IsLiveSite="false" />
</asp:Content>
