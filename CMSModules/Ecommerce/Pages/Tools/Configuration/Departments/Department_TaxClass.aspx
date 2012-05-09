<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Ecommerce_Pages_Tools_Configuration_Departments_Department_TaxClass" Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" CodeFile="Department_TaxClass.aspx.cs" %>
<%@ Register src="~/CMSAdminControls/UI/UniSelector/UniSelector.ascx" tagname="UniSelector" tagprefix="cms" %>


<asp:Content ID="Content1" ContentPlaceHolderID="plcContent" runat="Server">
    <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
        Visible="false" />
    <asp:Label ID="lblAvialable" runat="server" CssClass="BoldInfoLabel" EnableViewState="false" />
    <cms:UniSelector ID="uniSelector" runat="server" IsLiveSite="false" ObjectType="ecommerce.taxclass"
        SelectionMode="Multiple" ResourcePrefix="taxselect" />
</asp:Content>
