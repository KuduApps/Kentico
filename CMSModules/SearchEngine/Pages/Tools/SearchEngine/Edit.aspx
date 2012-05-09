<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master"
    Title="Search engine properties" Inherits="CMSModules_SearchEngine_Pages_Tools_SearchEngine_Edit" Theme="Default" CodeFile="Edit.aspx.cs" %>
<%@ Register Src="~/CMSModules/SearchEngine/Controls/UI/SearchEngine/Edit.ascx"
    TagName="SearchEngineEdit" TagPrefix="cms" %>
    
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <cms:SearchEngineEdit ID="editElem" runat="server" IsLiveSite="false" />
</asp:Content>