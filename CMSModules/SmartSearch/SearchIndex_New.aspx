<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_SmartSearch_SearchIndex_New" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master"
    Theme="Default" Title="Search Index - New" CodeFile="SearchIndex_New.aspx.cs" %>

<%@ Register Src="~/CMSModules/SmartSearch/Controls/UI/SearchIndex_New.ascx" TagName="IndexNew" TagPrefix="cms" %>
<asp:Content ID="cntBody" ContentPlaceHolderID="plcContent" runat="server">
    <cms:IndexNew ID="IndexNew" runat="server" Visible="true" />
</asp:Content>
