<%@ Page Title="Module edit - User interface - Roles" Language="C#" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master"
    AutoEventWireup="true" Inherits="CMSModules_Modules_Pages_Development_Module_UI_Roles"
    Theme="Default" CodeFile="Module_UI_Roles.aspx.cs" %>

<%@ Register Src="~/CMSFormControls/Sites/SiteSelector.ascx" TagName="SiteSelector"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/UniControls/UniMatrix.ascx" TagName="UniMatrix"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Membership/FormControls/Users/SelectUser.ascx" TagName="UserSelector"
    TagPrefix="cms" %>
<asp:Content ID="cntFilter" ContentPlaceHolderID="plcBeforeContent" runat="Server">
    <asp:Panel ID="pnlFilter" runat="server" CssClass="PageHeaderLine SiteHeaderLine">
        <table>
            <asp:PlaceHolder runat="server" ID="plcSiteSelector">
                <tr>
                    <td class="FieldLabel">
                        <cms:LocalizedLabel ID="lblSites" runat="server" ResourceString="general.site" DisplayColon="true" />
                    </td>
                    <td colspan="2">
                        <cms:SiteSelector ID="siteSelector" runat="server" AllowAll="false" OnlyRunningSites="false" />
                    </td>
                </tr>
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="plcUser" runat="server">
                <tr>
                    <td class="FieldLabel">
                        <cms:LocalizedLabel ID="lblUser" runat="server" ResourceString="Administration-Permissions_Header.User"
                            DisplayColon="false" EnableViewState="false" AssociatedControlID="userSelector" />
                    </td>
                    <td>
                        <cms:UserSelector ID="userSelector" runat="server" SelectionMode="SingleDropDownList" IsLiveSite="false" />
                    </td>
                    <td>
                        <td>
                            <cms:CMSUpdatePanel ID="pnlUpdateUsers" runat="server" UpdateMode="Always">
                                <ContentTemplate>
                                    <cms:LocalizedCheckBox ID="chkUserOnly" runat="server" AutoPostBack="true" />
                                </ContentTemplate>
                            </cms:CMSUpdatePanel>
                        </td>
                    </td>
                </tr>
            </asp:PlaceHolder>
        </table>
    </asp:Panel>
</asp:Content>
<asp:Content ID="cntMatrix" ContentPlaceHolderID="plcContent" runat="Server">
    <cms:CMSUpdatePanel ID="pnlUpdate" runat="server">
        <ContentTemplate>
            <cms:LocalizedLabel ID="lblGlobalAdmin" runat="server" CssClass="InfoLabel" EnableViewState="false" />
            <cms:LocalizedLabel ID="lblDisabled" runat="server" EnableViewState="false" CssClass="InfoLabel"
                ResourceString="resource.ui.disabled" />
            <cms:LocalizedLabel ID="lblInfo" runat="server" CssClass="InfoLabel" EnableViewState="false" />
            <asp:PlaceHolder ID="plcUpdate" runat="server">
                <cms:UniMatrix ID="gridMatrix" runat="server" QueryName="CMS.UIElement.getpermissionmatrix"
                    RowItemIDColumn="RoleID" ColumnItemIDColumn="ElementID" RowItemDisplayNameColumn="RoleDisplayName"
                    ColumnItemDisplayNameColumn="ElementDisplayName" RowTooltipColumn="RowDisplayName"
                    ColumnTooltipColumn="PermissionDescription" ItemTooltipColumn="PermissionDescription"
                    FixedWidth="0" FirstColumnsWidth="0" LastColumnFullWidth="true" />
            </asp:PlaceHolder>
        </ContentTemplate>
    </cms:CMSUpdatePanel>
</asp:Content>
