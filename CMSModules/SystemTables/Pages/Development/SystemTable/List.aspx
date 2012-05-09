<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_SystemTables_Pages_Development_SystemTable_List"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="System tables - List"
    CodeFile="List.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <cms:UniGrid ID="UniGridSysTables" runat="server" GridName="List.xml" OrderBy="ClassDisplayName"
        IsLiveSite="false" />
</asp:Content>
