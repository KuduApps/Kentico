<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_UICultures_Pages_Development_UICulture_List"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Theme="Default" CodeFile="List.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<%@ Register Namespace="CMS.UIControls.UniGridConfig" TagPrefix="ug" Assembly="CMS.UIControls" %>

<asp:Content ContentPlaceHolderID="plcContent" ID="content" runat="server">    
    <cms:UniGrid ID="UniGridUICultures" runat="server" ShortID="g" ObjectType="cms.uiculture" 
        IsLiveSite="false" OrderBy="UICultureName" Columns="UICultureID, UICultureCode, UICultureName">
        <GridActions>
            <ug:Action Name="edit" Caption="$general.edit$" Icon="Edit.png" />
            <ug:Action Name="delete" Caption="$general.delete$" Icon="Delete.png" confirmation="$general.confirmdelete$" />
        </GridActions>
        <GridColumns>
            <ug:Column Source="##ALL##" ExternalSourceName="culturename" Caption="$Unigrid.UICultureList.Columns.UICultureName$" Localize="true" Wrap="false">
              <Filter Type="text" />
            </ug:Column>
            <ug:Column Source="UICultureCode" Caption="$Unigrid.UICultureList.Columns.UICultureCode$" Wrap="false">
                <Filter Type="text" />
            </ug:Column>
            <ug:Column Width="100%" />
        </GridColumns>
        <GridOptions DisplayFilter="true" />
    </cms:UniGrid>
</asp:Content>