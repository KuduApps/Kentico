<%@ Page Language="C#" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" AutoEventWireup="true"
    Inherits="CMSModules_TagGroups_Pages_Development_TagGroup_Edit_Tags" Title="Tag group - Tags"
    Theme="Default" CodeFile="TagGroup_Edit_Tags.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<%@ Register Namespace="CMS.UIControls.UniGridConfig" TagPrefix="ug" Assembly="CMS.UIControls" %>
<asp:Content ContentPlaceHolderID="plcContent" runat="Server">
    <asp:Label ID="lblInfo" runat="server" Visible="false" CssClass="InfoLabel" EnableViewState="false" />
    <cms:UniGrid ID="gridTags" runat="server" OrderBy="TagName" IsLiveSite="false" Columns="TagID, TagName, TagCount"
        ObjectType="cms.tag">
        <GridActions>
            <ug:Action Name="viewDocuments" Caption="$General.View$" Icon="View.png" />
        </GridActions>
        <GridColumns>
            <ug:Column Source="TagName" ExternalSourceName="TagName" Caption="$tags.taggroup_edit_tags.tag$"
                Wrap="false">
                <Filter Type="text" />
            </ug:Column>
            <ug:Column Source="TagCount" Caption="$general.count$" Wrap="false" />
            <ug:Column Width="100%" />
        </GridColumns>
        <GridOptions DisplayFilter="true" />
    </cms:UniGrid>
</asp:Content>
