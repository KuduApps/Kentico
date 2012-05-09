<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="CMSModules_ContactManagement_Controls_UI_Ip_List" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<%@ Register Namespace="CMS.UIControls.UniGridConfig" TagPrefix="ug" Assembly="CMS.UIControls" %>
<cms:unigrid runat="server" id="gridElem" objecttype="om.ip" orderby="IPCreated DESC"
    columns="IPID,IPAddress,IPCreated,ContactFullName,ContactSiteID,ContactMergedWithContactID"
    islivesite="false" editactionurl="Frameset.aspx?ipId={0}" ShowObjectMenu="false">
    <GridActions Parameters="IPID">
        <ug:Action Name="delete" Caption="$General.Delete$" Icon="Delete.png" Confirmation="$General.ConfirmDelete$" ExternalSourceName="delete" />
    </GridActions>
    <GridColumns>
        <ug:Column Source="IPAddress" Caption="$om.ip.address$" Wrap="false" />
        <ug:Column Source="IPCreated" Caption="$om.ip.created$" Wrap="false" />
        <ug:Column Source="ContactFullName" Caption="$om.contact.name$" Wrap="false" Name="ContactFullName" />
        <ug:Column Source="ContactSiteID" AllowSorting="false" Caption="$general.sitename$" ExternalSourceName="#sitenameorglobal" Name="ContactSiteID"
            Wrap="false" />
        <ug:Column Width="100%" />
    </GridColumns>
</cms:unigrid>
