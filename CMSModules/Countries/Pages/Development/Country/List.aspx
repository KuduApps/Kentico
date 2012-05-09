<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Countries_Pages_Development_Country_List"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Countries"
    CodeFile="List.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<%@ Register Namespace="CMS.UIControls.UniGridConfig" TagPrefix="ug" Assembly="CMS.UIControls" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <cms:UniGrid runat="server" ID="UniGrid" ShortID="g" OrderBy="CountryDisplayName"
        Columns="CountryID, CountryDisplayName" IsLiveSite="false" EditActionUrl="Frameset.aspx?countryid={0}"
        ObjectType="cms.country">
        <GridActions Parameters="CountryID">
            <ug:Action Name="edit" Caption="$General.Edit$" Icon="Edit.png" />
            <ug:Action Name="delete" Caption="$General.Delete$" Icon="Delete.png" Confirmation="$country.ConfirmDelete$" />
        </GridActions>
        <GridColumns>
            <ug:Column Source="CountryDisplayName" Caption="$Unigrid.Country.Columns.CountryDisplayName$"
                Wrap="false" Localize="true">
                <Filter Type="text" />
            </ug:Column>
            <ug:Column Width="100%" />
        </GridColumns>
        <GridOptions DisplayFilter="true" />
    </cms:UniGrid>
</asp:Content>
