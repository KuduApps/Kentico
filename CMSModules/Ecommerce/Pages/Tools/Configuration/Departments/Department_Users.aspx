<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Ecommerce_Pages_Tools_Configuration_Departments_Department_Users" Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" CodeFile="Department_Users.aspx.cs" %>
<%@ Register src="~/CMSAdminControls/UI/UniSelector/UniSelector.ascx" tagname="UniSelector" tagprefix="cms" %>


<asp:Content ID="Content1" ContentPlaceHolderID="plcContent" runat="Server">
    <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
        Visible="false" />
    <asp:Label ID="lblAvialable" runat="server" CssClass="BoldInfoLabel" EnableViewState="false" />
    <cms:UniSelector ID="uniSelector" runat="server" IsLiveSite="false" ObjectType="cms.user"
        SelectionMode="Multiple" ResourcePrefix="selectuser" />
</asp:Content>
