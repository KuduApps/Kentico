<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_PortalEngine_UI_PageLayouts_PageLayout_List" Theme="Default"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Page Layouts - Page Layout List" CodeFile="PageLayout_List.aspx.cs" %>
<%@ Register src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" tagname="UniGrid" tagprefix="cms" %>

<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
    <cms:UniGrid ID="gridLayouts" runat="server" GridName="PageLayout_List.xml" OrderBy="LayoutDisplayName"
        IsLiveSite="false" />
</asp:Content>
