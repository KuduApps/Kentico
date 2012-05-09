<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_OnlineMarketing_Controls_UI_MVTVariant_List" CodeFile="List.ascx.cs" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<%@ Register Namespace="CMS.UIControls.UniGridConfig" TagPrefix="ug" Assembly="CMS.UIControls" %>

<cms:UniGrid ID="gridElem" runat="server" ObjectType="om.mvtvariant" OrderBy="MVTVariantDisplayName"
    Columns="MVTVariantID,MVTVariantDisplayName,MVTVariantName,MVTVariantEnabled" IsLiveSite="false" EditActionUrl="Edit.aspx?mvtvariantid={0}&nodeid={?nodeid?}&varianttype={?varianttype?}">
    <GridActions Parameters="MVTVariantID">
        <ug:Action Name="edit" Caption="$General.Edit$" Icon="Edit.png" />
        <ug:Action Name="delete" Caption="$General.Delete$" Icon="Delete.png" Confirmation="$General.ConfirmDelete$" />
    </GridActions>
    <GridColumns>
        <ug:Column Source="MVTVariantDisplayName" Caption="$general.displayName$" Wrap="false" />
        <ug:Column Source="MVTVariantEnabled" Caption="$general.enabled$" Wrap="false" ExternalSourceName="#yesno" />
        <ug:Column Width="100%" />
    </GridColumns>
</cms:UniGrid>
