<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Modules_Pages_Development_Module_Edit_PermissionNames" Theme="Default"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Module Edit - Permission Names" CodeFile="Module_Edit_PermissionNames.aspx.cs" %>
<%@ Register src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" tagname="UniGrid" tagprefix="cms" %>

<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <cms:UniGrid ID="unigridPermissionList" runat="server" GridName="Permission_List.xml"
        OrderBy="PermissionOrder" Columns="PermissionID, PermissionDisplayName, PermissionName, PermissionOrder"
        IsLiveSite="false" />
</asp:Content>
