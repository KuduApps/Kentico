<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_SmartSearch_SearchIndex_Fields" Title="Search Index - Search fields"
    MasterPageFile="~/CMSMasterPages/UI/EmptyPage.master" 
    Theme="Default" CodeFile="SearchIndex_Fields.aspx.cs" %>

<%@ Register Src="~/CMSModules/SmartSearch/Controls/Edit/SearchFields.ascx"
    TagName="SearchFields" TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:Panel runat="server" ID="pnlContent" CssClass="PageContent">
        <cms:LocalizedLabel runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
            Visible="false"/>
        <cms:SearchFields ID="searchFields" runat="server" />
    </asp:Panel>
</asp:Content>
