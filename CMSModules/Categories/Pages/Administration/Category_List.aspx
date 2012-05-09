<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Categories_Pages_Administration_Category_List"
    MasterPageFile="~/CMSMasterPages/UI/EmptyPage.master" Theme="Default" Title="Categories List"
    CodeFile="Category_List.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Categories/Controls/Categories.ascx" TagName="Categories"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/PageElements/PageTitle.ascx" TagName="PageTitle"
    TagPrefix="cms" %>
<asp:Content ContentPlaceHolderID="plcContent" runat="server">
    <style type="text/css">
        .SiteSelector select, .IE9 .SiteSelector select
        {
            width: 190px;
        }
        .TreeRoot
        {
            font-weight: bold;
        }
    </style>
    <asp:Panel ID="pnlHeader" runat="server" CssClass="PageHeader" EnableViewState="false">
        <cms:PageTitle ID="titleElem" ShortID="pt" runat="server" />
    </asp:Panel>
    <cms:Categories ID="CategoriesElem" runat="server" IsLiveSite="false" DisplayPersonalCategories="false" />
</asp:Content>
