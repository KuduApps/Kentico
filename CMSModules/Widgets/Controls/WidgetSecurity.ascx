<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_Widgets_Controls_WidgetSecurity"
    CodeFile="WidgetSecurity.ascx.cs" %>
<%@ Register Src="~/CMSAdminControls/UI/UniControls/UniMatrix.ascx" TagName="UniMatrix"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSFormControls/Sites/SiteSelector.ascx" TagName="SiteSelector"
    TagPrefix="cms" %>
<asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />
<cms:LocalizedLabel runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
    Visible="false" />
<asp:Table runat="server" ID="tblMatrix" GridLines="horizontal" CssClass="PermissionMatrix"
    CellPadding="3" CellSpacing="0" EnableViewState="false">
    <asp:TableFooterRow CssClass="MatrixHeader">
        <asp:TableCell>
            <cms:LocalizedLabel ID="lblUsedInGroupZones" runat="server" ResourceString="widgets.security.usedingroupzones"
                EnableViewState="false" />
        </asp:TableCell><asp:TableCell CssClass="SecurityMatrixCheckbox">
            <asp:CheckBox runat="server" ID="chkUsedInGroupZones" />
        </asp:TableCell><asp:TableCell></asp:TableCell></asp:TableFooterRow>
    <asp:TableFooterRow CssClass="MatrixHeader">
        <asp:TableCell>
            <cms:LocalizedLabel ID="lblUsedInEditorZones" runat="server" ResourceString="widgets.security.usedineditorzones"
                EnableViewState="false"></cms:LocalizedLabel>
        </asp:TableCell>
        <asp:TableCell CssClass="SecurityMatrixCheckbox">
            <asp:CheckBox runat="server" ID="chkUsedInEditorZones" />
        </asp:TableCell>
        <asp:TableCell>
        </asp:TableCell>
    </asp:TableFooterRow>
    <asp:TableFooterRow CssClass="MatrixHeader">
        <asp:TableCell>
            <cms:LocalizedLabel ID="lblUsedInUserZones" runat="server" ResourceString="widgets.security.usedinuserzones"
                EnableViewState="false"></cms:LocalizedLabel>
        </asp:TableCell>
        <asp:TableCell CssClass="SecurityMatrixCheckbox">
            <asp:CheckBox runat="server" ID="chkUsedInUserZones" />
        </asp:TableCell>
        <asp:TableCell></asp:TableCell>
    </asp:TableFooterRow>
    <asp:TableFooterRow CssClass="MatrixHeader">
        <asp:TableCell>
            <cms:LocalizedLabel ID="lblUsedInDashboard" runat="server" ResourceString="widgets.security.usedindashboardzones"
                EnableViewState="false"></cms:LocalizedLabel>
        </asp:TableCell>
        <asp:TableCell CssClass="SecurityMatrixCheckbox">
            <asp:CheckBox runat="server" ID="chkUsedInDashboard" />
        </asp:TableCell>
        <asp:TableCell></asp:TableCell>
    </asp:TableFooterRow>
    <asp:TableFooterRow CssClass="MatrixHeader">
        <asp:TableCell>
            <cms:LocalizedLabel ID="lblUsedAsInlineWidget" runat="server" ResourceString="widgets.security.usedasinline"
                EnableViewState="false"></cms:LocalizedLabel>
        </asp:TableCell>
        <asp:TableCell CssClass="SecurityMatrixCheckbox">
            <asp:CheckBox runat="server" ID="chkUsedAsInlineWidget" />
        </asp:TableCell>
        <asp:TableCell>
        </asp:TableCell>
    </asp:TableFooterRow>
    <asp:TableFooterRow>
        <asp:TableCell ColumnSpan="3">&nbsp;</asp:TableCell>
    </asp:TableFooterRow>
</asp:Table>
<div style="padding-bottom: 10px; padding-top: 25px;">
    <cms:LocalizedLabel ID="lblRolesInfo" runat="server" ResourceString="SecurityMatrix.RolesAvailability"
        Visible="false" EnableViewState="false" />
</div>
<div style="padding-bottom: 10px;">
    <cms:LocalizedLabel ID="lblSite" runat="server" ResourceString="general.site" DisplayColon="true"
        EnableViewState="false" AssociatedControlID="siteSelector" />
    <cms:SiteSelector ID="siteSelector" runat="server" AllowAll="false" OnlyRunningSites="false"
        IsLiveSite="false" />
</div>
<cms:CMSUpdatePanel ID="pnlUpdate" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <cms:UniMatrix ID="gridMatrix" runat="server" QueryName="cms.widgetrole.getpermissionmatrix"
            RowItemIDColumn="RoleID" ColumnItemIDColumn="PermissionID" RowItemCodeNameColumn="RoleName" RowItemDisplayNameColumn="RoleDisplayName"
            ColumnItemDisplayNameColumn="PermissionDisplayName" RowTooltipColumn="RowDisplayName"
            ColumnTooltipColumn="PermissionDescription" ItemTooltipColumn="PermissionDescription"
            FixedWidth="0" />
        <br />
    </ContentTemplate>
</cms:CMSUpdatePanel>
