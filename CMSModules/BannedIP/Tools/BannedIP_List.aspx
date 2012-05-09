<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master"
    Title="Banned IPs" Inherits="CMSModules_BannedIP_Tools_BannedIP_List" Theme="Default"
    CodeFile="BannedIP_List.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<%@ Register Src="~/CMSFormControls/Sites/SiteSelector.ascx" TagName="SiteSelector"
    TagPrefix="cms" %>
<%@ Register Namespace="CMS.UIControls.UniGridConfig" TagPrefix="ug" Assembly="CMS.UIControls" %>
<asp:Content ID="cntBefore" runat="server" ContentPlaceHolderID="plcSiteSelector">
    <asp:PlaceHolder ID="plcSites" runat="server">
        <cms:LocalizedLabel ID="lblSite" runat="server" EnableViewState="false" ResourceString="general.site"
            DisplayColon="true" />
        <cms:SiteSelector ID="siteSelector" runat="server" IsLiveSite="false" AllowAll="true"
            OnlyRunningSites="false" GlobalRecordValue="0" AllowGlobal="true" />
    </asp:PlaceHolder>
</asp:Content>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <cms:CMSUpdatePanel ID="pnlUpdate" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <cms:UniGrid runat="server" ID="UniGrid" ObjectType="cms.bannedip" Columns="IPAddressID, IPAddress, IPAddressBanType, IPAddressAllowed, IPAddressBanEnabled, IPAddressSiteID"
                IsLiveSite="false" OrderBy="IPAddress">
                <GridActions Parameters="IPAddressID">
                    <ug:Action Name="edit" Caption="$General.Edit$" Icon="Edit.png" />
                    <ug:Action Name="delete" Caption="$General.Delete$" Icon="Delete.png" Confirmation="$banip.DeleteConfirmation$" />
                </GridActions>
                <GridColumns>
                    <ug:Column Source="IPAddress" Caption="$banip.IPAddress$" Wrap="false">
                        <Filter Type="text" Size="100" />
                    </ug:Column>
                    <ug:Column Source="IPAddressBanType" Caption="$banip.IPAddressBanType$" ExternalSourceName="bantype"
                        Wrap="false" Width="100%" />
                    <ug:Column Source="IPAddressAllowed" Caption="$banip.IPAddressAllowed$" ExternalSourceName="#yesno"
                        Wrap="false" />
                    <ug:Column Source="IPAddressBanEnabled" Caption="$general.enabled$" ExternalSourceName="#yesno"
                        Wrap="false" />
                    <ug:Column Source="IPAddressSiteID" Caption="$general.sitename$" ExternalSourceName="#sitename"
                        Wrap="false" />
                </GridColumns>
                <GridOptions DisplayFilter="true" />
            </cms:UniGrid>
        </ContentTemplate>
    </cms:CMSUpdatePanel>
</asp:Content>
