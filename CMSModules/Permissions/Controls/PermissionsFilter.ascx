<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PermissionsFilter.ascx.cs"
    Inherits="CMSModules_Permissions_Controls_PermissionsFilter" %>
<%@ Register Src="~/CMSFormControls/Sites/SiteSelector.ascx" TagName="SiteSelector"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSFormControls/SelectModule.ascx" TagName="ModuleSelector" TagPrefix="cms" %>
<%@ Register Src="~/CMSFormControls/Classes/SelectClass.ascx" TagName="ClassSelector"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Membership/FormControls/Users/SelectUser.ascx" TagName="UserSelector"
    TagPrefix="cms" %>
<asp:Panel ID="PanelOptions" runat="server" CssClass="PageHeaderLine SiteHeaderLine">
    <table>
        <asp:PlaceHolder ID="plcSite" runat="server">
            <tr>
                <td class="FieldLabel">
                    <cms:LocalizedLabel ID="lblSite" runat="server" ResourceString="general.site" DisplayColon="true"
                        EnableViewState="false" />
                </td>
                <td colspan="2">
                    <asp:Panel ID="PanelSites" runat="server">
                        <cms:SiteSelector ID="siteSelector" runat="server" IsLiveSite="false" />
                    </asp:Panel>
                </td>
            </tr>
        </asp:PlaceHolder>
        <tr>
            <td>
                <cms:LocalizedLabel ID="lblPermissionType" runat="server" ResourceString="Administration-Permissions_Header.PermissionType"
                    DisplayColon="false" EnableViewState="false" AssociatedControlID="drpPermissionType" />
            </td>
            <td>
                <cms:CMSUpdatePanel ID="pnlUpdateType" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        <asp:DropDownList ID="drpPermissionType" runat="server" OnSelectedIndexChanged="drpPermissionType_SelectedIndexChanged"
                            AutoPostBack="True" CssClass="DropDownField" />
                    </ContentTemplate>
                </cms:CMSUpdatePanel>
            </td>
            <td>
                <cms:CMSUpdatePanel ID="pnlUpdateSelectors" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        <cms:ModuleSelector runat="server" ID="moduleSelector" IsLiveSite="false" DisplayOnlyWithPermission="true"
                            DisplayOnlyForGivenSite="true" DisplayAllModules="true" />
                        <cms:ClassSelector runat="server" ID="docTypeSelector" IsLiveSite="false" OnlyDocumentTypes="true" />
                        <cms:ClassSelector runat="server" ID="customTableSelector" IsLiveSite="false" OnlyCustomTables="true" />
                    </ContentTemplate>
                </cms:CMSUpdatePanel>
            </td>
        </tr>
        <asp:PlaceHolder ID="plcUser" runat="server">
            <tr>
                <td class="FieldLabel">
                    <cms:LocalizedLabel ID="lblUser" runat="server" ResourceString="Administration-Permissions_Header.User"
                        DisplayColon="false" EnableViewState="false" AssociatedControlID="userSelector" />
                </td>
                <td>
                    <cms:UserSelector ID="userSelector" runat="server" SelectionMode="SingleDropDownList" />
                </td>
                <td>
                    <cms:CMSUpdatePanel ID="pnlUpdateUsers" runat="server" UpdateMode="Always">
                        <ContentTemplate>
                            <cms:LocalizedCheckBox ID="chkUserOnly" runat="server" AutoPostBack="true" Enabled="false"/>
                        </ContentTemplate>
                    </cms:CMSUpdatePanel>
                </td>
            </tr>
        </asp:PlaceHolder>
    </table>
</asp:Panel>
