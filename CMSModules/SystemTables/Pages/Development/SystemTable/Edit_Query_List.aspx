<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_SystemTables_Pages_Development_SystemTable_Edit_Query_List"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="System tables - Queries"
    Theme="Default" CodeFile="Edit_Query_List.aspx.cs" %>

<%@ Register Src="~/CMSModules/AdminControls/Controls/Class/ClassQueries.ascx" TagName="ClassQueries"
    TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <cms:ClassQueries ID="classEditQuery" runat="server" IsLiveSite="false" />
</asp:Content>
