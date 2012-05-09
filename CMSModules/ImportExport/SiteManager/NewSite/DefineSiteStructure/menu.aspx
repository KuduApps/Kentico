<%@ Page Language="C#" AutoEventWireup="true" Theme="Default" 
         Inherits="CMSModules_ImportExport_SiteManager_NewSite_DefineSiteStructure_menu" CodeFile="menu.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/UniMenu/UniMenu.ascx" TagName="UniMenu" TagPrefix="cms" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Content - Menu</title>
    <style type="text/css">
        body
        {
            margin: 0px;
            padding: 0px;
            height: 100%;
        }
    </style>    

</head>
<body class="<%=mBodyClass%>">
    <form id="form1" runat="server">
    <asp:Panel runat="server" ID="pnlBody" CssClass="ContentMenu ReducedButtonsBorder">
        <asp:Panel runat="server" ID="pnlLeft" CssClass="ContentMenuLeft">
            <cms:UniMenu ID="menuLeft" runat="server" />
        </asp:Panel>
    </asp:Panel>
    <asp:HiddenField ID="selectedNodeId" Value="0" runat="server" />
    <asp:HiddenField runat="server" ID="requestPageUrl" Value='<%=ResolveUrl("request.aspx") %>' />
    <asp:Literal ID="ltlData" runat="server" EnableViewState="false" />

    <script type="text/javascript">
        //<![CDATA[
        var delConfirmation = '<%=ResHelper.GetString("general.confirmdelete") %>';
        var selNodeElem = document.getElementById('<%=selectedNodeId.ClientID %>');
        var imagesUrl = '<%=GetImageUrl("CMSModules/CMS_Content/Menu/", false, true) %>';
        var siteName = '<%=QueryHelper.GetString("sitename", string.Empty) %>';
        //]]>
    </script>

    </form>
</body>
</html>
