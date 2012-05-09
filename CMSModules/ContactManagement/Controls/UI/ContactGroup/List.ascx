<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="CMSModules_ContactManagement_Controls_UI_ContactGroup_List" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<%@ Register Namespace="CMS.UIControls.UniGridConfig" TagPrefix="ug" Assembly="CMS.UIControls" %>
<cms:CMSUpdatePanel ID="pnlUpdate" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <cms:UniGrid runat="server" ID="gridElem" ObjectType="om.contactgroup" OrderBy="ContactGroupDisplayName"
            Columns="ContactGroupID,ContactGroupDisplayName,ContactGroupSiteID" IsLiveSite="false">
            <GridActions Parameters="ContactGroupID">
                <ug:Action Name="edit" Caption="$General.Edit$" Icon="Edit.png" />
                <ug:Action Name="delete" ExternalSourceName="delete" Caption="$General.Delete$" Icon="Delete.png"
                    Confirmation="$General.ConfirmDelete$" ModuleName="CMS.ContactManagement" Permissions="ModifyContactGroups" />
            </GridActions>
            <GridColumns>
                <ug:Column Source="ContactGroupDisplayName" Caption="$om.contactgroup.displayname$"
                    Wrap="false">
                    <Filter Type="text" Size="200" />
                </ug:Column>
                <ug:Column Source="ContactGroupSiteID" AllowSorting="false" Caption="$general.site$"
                    ExternalSourceName="#sitenameorglobal" Name="sitename" Wrap="false" Localize="true" />
                <ug:Column Width="100%" />
            </GridColumns>
            <GridOptions DisplayFilter="true" />
        </cms:UniGrid>
    </ContentTemplate>
</cms:CMSUpdatePanel>
