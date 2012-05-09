<%@ Page Language="C#" AutoEventWireup="true"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Search engine list"
    Inherits="CMSModules_SearchEngine_Pages_Tools_SearchEngine_List" Theme="Default" CodeFile="List.aspx.cs" %>
<%@ Register Src="~/CMSModules/SearchEngine/Controls/UI/SearchEngine/List.ascx" TagName="SearchEngineList" TagPrefix="cms" %>
    
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <cms:SearchEngineList ID="listElem" runat="server" IsLiveSite="false" />
</asp:Content>
