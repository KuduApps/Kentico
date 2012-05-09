<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_WebAnalytics_Controls_UI_Conversion_List" CodeFile="List.ascx.cs" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<%@ Register Namespace="CMS.UIControls.UniGridConfig" TagPrefix="ug" Assembly="CMS.UIControls" %>

<cms:UniGrid runat="server" ID="gridElem" ObjectType="analytics.conversion" OrderBy="ConversionDisplayName"
    Columns="ConversionID,ConversionDisplayName, ISNULL(HitsCount,0) AS HitsCount, ISNULL (HitsValues,0) AS HitsValues" IsLiveSite="false" EditActionUrl="Frameset.aspx?conversionId={0}" Query="Analytics.Conversion.selectwithviews">
    <GridActions Parameters="ConversionID">
        <ug:Action Name="edit" Caption="$General.Edit$" Icon="Edit.png"  OnClick="if (parent.updateTabHeader != null) parent.updateTabHeader();"/>
        <ug:Action Name="#delete" Caption="$General.Delete$" Icon="Delete.png" Confirmation="$General.ConfirmDelete$" ModuleName="CMS.WebAnalytics" Permissions="modify" />
    </GridActions>
    <GridColumns>
        <ug:Column Source="ConversionDisplayName" Caption="$conversion.name$" Wrap="false">
            <Filter Type="text" />
        </ug:Column>
        <ug:Column Source="HitsCount" Caption="$general.count$" Wrap="false" />
        <ug:Column Source="HitsValues" Caption="$general.value$" Wrap="false" />
        <ug:Column Width="100%" />
    </GridColumns>
    <GridOptions DisplayFilter="true" />
</cms:UniGrid>
