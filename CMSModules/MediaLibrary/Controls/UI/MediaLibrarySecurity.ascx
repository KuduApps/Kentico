<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_MediaLibrary_Controls_UI_MediaLibrarySecurity"
    CodeFile="MediaLibrarySecurity.ascx.cs" %>
<%@ Register Src="~/CMSAdminControls/UI/UniControls/UniMatrix.ascx" TagName="UniMatrix"
    TagPrefix="cms" %>
<cms:LocalizedLabel runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
    Visible="false" />
<cms:LocalizedLabel runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
    Visible="false" />
<asp:Table runat="server" ID="tblMatrix" CssClass="PermissionMatrix" CellPadding="3"
    CellSpacing="0" EnableViewState="false" GridLines="Horizontal">
</asp:Table>
<br />
<cms:UniMatrix ID="gridMatrix" runat="server" QueryName="media.library.getpermissionmatrix"
    RowItemIDColumn="RoleID" ColumnItemIDColumn="PermissionID" RowItemCodeNameColumn="RoleName"
    RowItemDisplayNameColumn="RoleDisplayName" ColumnItemDisplayNameColumn="PermissionDisplayName"
    ColumnItemTooltipColumn="PermissionDescription" RowTooltipColumn="RowDisplayName"
    ColumnTooltipColumn="PermissionDescription" ItemTooltipColumn="PermissionDescription"
    FirstColumnsWidth="28" FixedWidth="12" UsePercentage="true" AddGlobalObjectSuffix="true" SiteIDColumnName="SiteID" />
