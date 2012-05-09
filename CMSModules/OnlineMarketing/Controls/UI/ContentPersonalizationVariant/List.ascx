<%@ Control Language="C#" AutoEventWireup="True" CodeFile="List.ascx.cs" Inherits="CMSModules_OnlineMarketing_Controls_UI_ContentPersonalizationVariant_List" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<%@ Register Namespace="CMS.UIControls.UniGridConfig" TagPrefix="ug" Assembly="CMS.UIControls" %>
<%@ Register Src="~/CMSModules/ContactManagement/Controls/UI/Account/Filter.ascx"
    TagName="Filter" TagPrefix="cms" %>

<cms:LocalizedLabel runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
    Visible="false" />
<cms:UniGrid runat="server" ID="gridElem" ObjectType="om.personalizationvariant" OrderBy="VariantPosition"
    Columns="VariantID,VariantEnabled,VariantDisplayName,VariantName" IsLiveSite="false" EditActionUrl="Edit.aspx?variantId={0}">
    <GridActions Parameters="VariantID">
        <ug:Action Name="edit" Caption="$General.Edit$" Icon="Edit.png" />
        <ug:Action Name="delete" Caption="$General.Delete$" Icon="Delete.png" Confirmation="$General.ConfirmDelete$" />
        <ug:Action Name="up" Caption="$General.Up$" Icon="Up.png" />
        <ug:Action Name="down" Caption="$General.Down$" Icon="Down.png" />
    </GridActions>
    <GridColumns>
        <ug:Column Source="VariantDisplayName" Caption="$general.displayName$" Wrap="false" />
        <ug:Column Source="VariantEnabled" Caption="$general.enabled$" Wrap="false" ExternalSourceName="#yesno" />
        <ug:Column Width="100%" />
    </GridColumns>
</cms:UniGrid>
