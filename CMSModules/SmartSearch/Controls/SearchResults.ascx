<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_SmartSearch_Controls_SearchResults" CodeFile="SearchResults.ascx.cs" %>
<asp:PlaceHolder runat="server" ID="plcBasicRepeater"></asp:PlaceHolder>
<cms:UniPager runat="server" ID="pgrSearch" PageControl="repSearchResults" />
<cms:LocalizedLabel runat="server" ID="lblNoResults" CssClass="ContentLabel" Visible="false"
    EnableViewState="false" />