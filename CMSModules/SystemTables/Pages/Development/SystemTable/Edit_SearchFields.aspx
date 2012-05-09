<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_SystemTables_Pages_Development_SystemTable_Edit_SearchFields" Theme="Default"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="System table search fields" CodeFile="Edit_SearchFields.aspx.cs" %>

<%@ Register Src="~/CMSModules/SmartSearch/Controls/Edit/SearchFields.ascx" TagName="SearchFields"
    TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <cms:SearchFields ID="searchFields" IsLiveSite="false" runat="server" />
</asp:Content>
