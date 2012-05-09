<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_CustomTables_CustomTable_Edit_SearchFields"  
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Custom Table Edit - Search" CodeFile="CustomTable_Edit_SearchFields.aspx.cs" %>

<%@ Register Src="~/CMSModules/SmartSearch/Controls/Edit/SearchFields.ascx"
    TagName="SearchFields" TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">  
<asp:Label ID="lblError" runat="server" EnableViewState="false" CssClass="ErrorLabel" />
  <cms:SearchFields runat="server" ID="SearchFields" LoadActualValues="true" />
</asp:Content>
