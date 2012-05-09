<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_ProjectManagement_Controls_UI_Project_Security" CodeFile="Security.ascx.cs" %>
<%@ Register Src="~/CMSAdminControls/UI/UniControls/UniMatrix.ascx" TagName="UniMatrix"
    TagPrefix="cms" %>
    
<cms:LocalizedLabel runat="server" ID="lblInfo" CssClass="InfoLabel" />
<cms:LocalizedLabel runat="server" ID="lblError" CssClass="ErrorLabel" />
<asp:Table runat="server" ID="tblMatrix" GridLines="horizontal" CssClass="PermissionMatrix"
    CellPadding="3" CellSpacing="0">
</asp:Table>
<br />
<cms:UniMatrix ID="gridMatrix" runat="server" QueryName="PM.ProjectRolePermission.getpermissionMatrix"
    RowItemIDColumn="RoleID" ColumnItemIDColumn="PermissionID" RowItemCodeNameColumn="RoleName" RowItemDisplayNameColumn="RoleDisplayName" 
    ColumnItemDisplayNameColumn="PermissionDisplayName" RowTooltipColumn="RowDisplayName"
    ColumnTooltipColumn="PermissionDescription" ItemTooltipColumn="PermissionDescription"
    FirstColumnsWidth="28" FixedWidth="12" UsePercentage="true" AddGlobalObjectSuffix="true" SiteIDColumnName="SiteID" />
<br />
