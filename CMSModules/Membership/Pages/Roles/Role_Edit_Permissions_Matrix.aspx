<%@ Page Title="" Language="C#" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" AutoEventWireup="true" CodeFile="Role_Edit_Permissions_Matrix.aspx.cs" 
Inherits="CMSModules_Membership_Pages_Roles_Role_Edit_Permissions_Matrix" Theme="Default" %>

<%@ Register Src="~/CMSModules/Permissions/Controls/PermissionsMatrix.ascx" TagName="PermissionsMatrix" TagPrefix="cms" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plcContent" runat="server">
<ajaxToolkit:ToolkitScriptManager runat="server" ID="scriptManager" />
    <asp:Label ID="lblInfo" runat="server" Text="" Visible="false" EnableViewState="false"></asp:Label>
    <cms:PermissionsMatrix ID="prmMatrix" runat="server" QueryName="cms.permission.getpermissionMatrix" ColumnItemTooltipColumn="RoleDisplayName" 
        RowItemIDColumn="PermissionID" ColumnItemIDColumn="RoleID" RowItemDisplayNameColumn="PermissionDisplayName" ColumnItemDisplayNameColumn="RoleDisplayName" 
        RowItemTooltipColumn="PermissionDescription" ItemTooltipColumn="PermissionDescription" PermissionsAsRows="true" />    
</asp:Content>
