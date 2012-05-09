<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_ImportExport_SiteManager_ExportHistory_ExportHistory_Edit_Tasks"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Export History - Tasks" CodeFile="ExportHistory_Edit_Tasks.aspx.cs" %>
<%@ Register src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" tagname="UniGrid" tagprefix="cms" %>

<%@ Register Src="~/CMSFormControls/Sites/SiteSelector.ascx" TagName="SiteSelector" TagPrefix="cms" %>
<asp:Content ID="cntControls" runat="server" ContentPlaceHolderID="plcSiteSelector">
    <table style="width: 100%">
        <tr>
            <td class="TextLeft">
                <cms:LocalizedLabel ID="lblSite" runat="server" ResourceString="general.site" DisplayColon="true"
                    EnableViewState="false" />
                <cms:SiteSelector ID="siteSelector" runat="server" IsLiveSite="false" />
            </td>
            <td class="TextRight">
                <cms:CMSUpdatePanel ID="pnlUpdateLink" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        <cms:LocalizedLinkButton ID="lnkDeleteAll" runat="server" ResourceString="exporthistory.deletealltasks"
                            OnClick="lnkDeleteAll_Click" EnableViewState="false" />
                    </ContentTemplate>
                </cms:CMSUpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ContentPlaceHolderID="plcContent" ID="content" runat="server">
    <cms:CMSUpdatePanel ID="pnlUpdate" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <cms:UniGrid runat="server" ID="UniGrid" GridName="ExportHistory_Edit_Tasks_List.xml"
                OrderBy="TaskTime" IsLiveSite="false" ExportFileName="export_task"/>
        </ContentTemplate>
    </cms:CMSUpdatePanel>
</asp:Content>
