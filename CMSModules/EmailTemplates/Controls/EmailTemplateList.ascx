<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_EmailTemplates_Controls_EmailTemplateList"
    CodeFile="EmailTemplateList.ascx.cs" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<%@ Register Namespace="CMS.UIControls.UniGridConfig" TagPrefix="ug" Assembly="CMS.UIControls" %>
<cms:UniGrid runat="server" ID="gridElem" OrderBy="EmailTemplateDisplayName" ObjectType="cms.emailtemplate"
    Columns="EmailTemplateID, EmailTemplateDisplayName, EmailTemplateSiteID" ShortID="g"
    IsLiveSite="false">
    <GridActions Parameters="EmailTemplateID;EmailTemplateSiteID">
        <ug:Action Name="edit" Caption="$General.Edit$" Icon="Edit.png" />
        <ug:Action Name="delete" Caption="$General.Delete$" Icon="Delete.png" Confirmation="$General.ConfirmDelete$" />
    </GridActions>
    <GridColumns>
        <ug:Column Source="EmailTemplateDisplayName" Caption="$Unigrid.EmailTemplateList.Columns.EmailTemplateName$"
            Wrap="false" Localize="true">
            <Filter Type="text" />
        </ug:Column>
        <ug:Column Width="100%">
        </ug:Column>
    </GridColumns>
    <GridOptions DisplayFilter="true" />
</cms:UniGrid>
