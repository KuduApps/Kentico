<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_PortalEngine_UI_WebParts_Development_WebPart_List"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" CodeFile="WebPart_List.aspx.cs" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>

<asp:Content ContentPlaceHolderID="plcContent" ID="content" runat="server">

    <script type="text/javascript">
        //<![CDATA[
        function EditWebPart(webPartId) {
            parent.location.replace("WebPart_Edit_Frameset.aspx?webpartid=" + webPartId + "&categoryid=" + "<%=categoryId%>");
            parent.parent.frames['webparttree'].location.href = "WebPart_Tree.aspx?webpartId=" + webPartId;
        }
        //]]>
    </script>

    <asp:Literal runat="server" ID="ltlScript" />
    <cms:UniGrid runat="server" ID="webpartGrid" GridName="WebPart_List.xml"
        OrderBy="DisplayName" Columns="ObjectID, DisplayName" IsLiveSite="false" />
</asp:Content>
