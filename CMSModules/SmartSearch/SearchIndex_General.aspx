<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_SmartSearch_SearchIndex_General" Title="Search Index - General"
    ValidateRequest="false" MasterPageFile="~/CMSMasterPages/UI/EmptyPage.master"
    Theme="Default" CodeFile="SearchIndex_General.aspx.cs" %>

<%@ Register Src="~/CMSModules/SmartSearch/Controls/UI/SearchIndex_General.ascx"
    TagName="IndexGeneral" TagPrefix="cms" %>
<asp:Content ID="cntBody" ContentPlaceHolderID="plcContent" runat="server">
    <asp:Panel runat="server" ID="pnlTabsBody" CssClass="TabsPageBody">
        <asp:Panel runat="server" ID="pnlTabsScroll" CssClass="TabsPageScrollArea">
            <asp:Panel runat="server" ID="pnltab" CssClass="TabsPageContent">
                <cms:IndexGeneral ID="IndexGeneral" runat="server" Visible="true" />
            </asp:Panel>
        </asp:Panel>
    </asp:Panel>
</asp:Content>
