<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_TimeZones_Pages_TimeZone_List"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" CodeFile="TimeZone_List.aspx.cs" %>
    
<%@ Register src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" tagname="UniGrid" tagprefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:Label ID="lblError" runat="server" Visible="false" EnableViewState="false" CssClass="ErrorLabel" />
    <cms:UniGrid runat="server" ID="uniGrid" GridName="TimeZone_List.xml" OrderBy="TimeZoneGMT"
        IsLiveSite="false" Columns="TimeZoneID,TimeZoneDisplayName,TimeZoneGMT,TimeZoneDaylight" ExportFileName="cms_timezone" />
</asp:Content>
