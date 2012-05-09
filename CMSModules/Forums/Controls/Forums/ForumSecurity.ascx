<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Forums_Controls_Forums_ForumSecurity" CodeFile="ForumSecurity.ascx.cs" %>
<%@ Register Src="~/CMSAdminControls/UI/UniControls/UniMatrix.ascx" TagName="UniMatrix" TagPrefix="cms" %>
<cms:LocalizedLabel runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false" />
<cms:LocalizedLabel runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false" />
<asp:Table runat="server" ID="tblMatrix" GridLines="horizontal" CssClass="PermissionMatrix"
    CellPadding="3" CellSpacing="0">
</asp:Table>
<br />
<cms:UniMatrix ID="gridMatrix" runat="server" QueryName="Forums.ForumRole.getpermissionMatrix"
    RowItemIDColumn="RoleID" ColumnItemIDColumn="PermissionID" RowItemCodeNameColumn="RoleName" RowItemDisplayNameColumn="RoleDisplayName"
    ColumnItemDisplayNameColumn="PermissionDisplayName" RowTooltipColumn="RowDisplayName"
    ColumnTooltipColumn="PermissionDescription" ItemTooltipColumn="PermissionDescription"
    FirstColumnsWidth="28" FixedWidth="12" UsePercentage="true" AddGlobalObjectSuffix="true" SiteIDColumnName="SiteID" />
<br />
<cms:LocalizedCheckBox runat="server" ID="chkChangeName" ResourceString="forum_security.changename"
    AutoPostBack="true" />
