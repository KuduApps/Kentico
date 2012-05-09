<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Ecommerce_Pages_Tools_DiscountLevels_DiscountLevel_Edit_Departments"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Discount level - edit - departments" CodeFile="DiscountLevel_Edit_Departments.aspx.cs" %>
<%@ Register src="~/CMSAdminControls/UI/UniSelector/UniSelector.ascx" tagname="UniSelector" tagprefix="cms" %>


<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
        Visible="false" />
    <asp:Label ID="lblAvialable" runat="server" CssClass="BoldInfoLabel" EnableViewState="false" />
    <cms:UniSelector ID="uniSelector" runat="server" IsLiveSite="false" ObjectType="ecommerce.department"
        SelectionMode="Multiple" ResourcePrefix="departmentsselector" />
</asp:Content>
