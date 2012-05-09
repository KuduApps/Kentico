<%@ Page Language="C#" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" AutoEventWireup="true"
    Inherits="CMSModules_SystemTables_Pages_Development_Views_Views_List" Title="Views - List" Theme="Default"
    CodeFile="Views_List.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/Filters/TextSimpleFilter.ascx" TagName="SimpleFilter"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/SystemTables/Controls/Views/SQLList.ascx" TagName="SQLList" TagPrefix="cms" %>
    
<asp:Content ID="Content1" ContentPlaceHolderID="plcContent" runat="Server">
    <asp:Label ID="lblInfo" runat="server" Visible="false" CssClass="InfoLabel" EnableViewState="false" />
    <cms:SQLList ID="lstSQL" runat="server" />
</asp:Content>
