<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_SmartSearch_SearchIndex_Cultures" Theme="Default"
    MasterPageFile="~/CMSMasterPages/UI/EmptyPage.master" Title="Search Index - Cultures List" CodeFile="SearchIndex_Cultures.aspx.cs" %>

<%@ Register Src="~/CMSModules/SmartSearch/Controls/UI/SearchIndex_Cultures.ascx"
    TagName="CulturesList" TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:Panel runat="server" ID="pnlTabsBody" CssClass="TabsPageBody">
        <asp:Panel runat="server" ID="pnlTabsScroll" CssClass="TabsPageScrollArea">
            <asp:Panel runat="server" ID="pnltab" CssClass="TabsPageContent">
                <cms:CulturesList ID="CulturesList" runat="server" />
            </asp:Panel>
        </asp:Panel>
    </asp:Panel>
</asp:Content>
