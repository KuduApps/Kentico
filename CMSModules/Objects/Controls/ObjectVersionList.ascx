<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ObjectVersionList.ascx.cs"
    Inherits="CMSModules_Objects_Controls_ObjectVersionList" %>
<%@ Register TagPrefix="cms" TagName="UniGrid" Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" %>
<%@ Register Namespace="CMS.UIControls.UniGridConfig" TagPrefix="ug" Assembly="CMS.UIControls" %>
<table width="100%" class="ObjectRecycleBin">
    <asp:PlaceHolder ID="plcLabels" runat="server" Visible="false" EnableViewState="false">
        <tr>
            <td colspan="2">
                <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" />
                <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" />
            </td>
        </tr>
    </asp:PlaceHolder>
    <tr>
        <td>
            <cms:LocalizedLabel ID="lblHistory" runat="server" CssClass="BoldInfoLabel" ResourceString="objectversioning.objecthistory"
                EnableViewState="false" DisplayColon="true" />
        </td>
        <td>
            <div class="TextRight">
                <asp:Button ID="btnRefresh" runat="server" CssClass="HiddenButton" OnClick="btnRefresh_Click" />
                <cms:LocalizedButton ID="btnMakeMajor" runat="server" CssClass="XLongButton" OnClick="btnMakeMajor_Click" ResourceString="objectversioning.makemajor" />
                <cms:LocalizedButton ID="btnDestroy" runat="server" CssClass="LongButton" OnClick="btnDestroy_Click" ResourceString="objectversioning.destroyhistory" />
            </div>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <cms:UniGrid ID="gridHistory" runat="server" ObjectType="cms.objectversionhistorylist"
                ShowObjectMenu="false" OrderBy="VersionID DESC">
                   <GridActions Parameters="VersionID">
                    <ug:Action Name="view" ExternalSourceName="view" Caption="$Unigrid.VersionHistory.Actions.View$"
                        Icon="View.png" />
                    <ug:Action Name="rollback" Caption="$Unigrid.VersionHistory.Actions.Rollback$" ExternalSourceName="rollback"
                        Confirmation="$Unigrid.ObjectVersionHistory.Actions.Rollback.Confirmation$" />
                    <ug:Action Name="destroy" ExternalSourceName="allowdestroy" Caption="$General.Delete$"
                        Confirmation="$General.ConfirmDelete$" />
                    <ug:Action Name="contextmenu" ExternalSourceName="contextmenu" Caption="$General.OtherActions$"
                        Icon="menu.png" ContextMenu="~/CMSModules/Objects/Controls/VersionListMenu.ascx"
                        MenuParameter="'{0}'" />
                </GridActions>
                <GridColumns>
                    <ug:Column Source="##ALL##" ExternalSourceName="ModifiedBy" Caption="$Unigrid.VersionHistory.Columns.ModifiedByUserFullName$"
                        Wrap="false" Width="100%" />
                    <ug:Column Source="##ALL##" ExternalSourceName="ModifiedWhen" Caption="$general.modified$"
                        Wrap="false">
                        <Tooltip Source="##ALL##" ExternalSourceName="ModifiedWhentooltip"></Tooltip>
                    </ug:Column>
                    <ug:Column Source="VersionNumber" Caption="$Unigrid.VersionHistory.Columns.VersionNumberLong$"
                        Wrap="false" />
                </GridColumns> 
                <PagerConfig DefaultPageSize="10" />
            </cms:UniGrid>
            <asp:HiddenField ID="hdnValue" runat="server" EnableViewState="false" />
            <asp:Button ID="btnHidden" runat="server" CssClass="HiddenButton" EnableViewState="false"
                OnClick="btnHidden_Click" />
        </td>
    </tr>
</table>
