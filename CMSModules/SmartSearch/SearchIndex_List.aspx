<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_SmartSearch_SearchIndex_List" Theme="Default"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Search Index - Index List" CodeFile="SearchIndex_List.aspx.cs" %>

<%@ Register Src="~/CMSModules/SmartSearch/Controls/UI/SearchIndex_List.ascx"
    TagName="IndexList" TagPrefix="cms" %>
<asp:Content ContentPlaceHolderID="plcContent" runat="server">
    <cms:IndexList ID="indexListElem" runat="server" />
</asp:Content>
