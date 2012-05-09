<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_DocumentTypes_Pages_Development_DocumentType_Edit_SearchFields"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Document Type Edit - Search" CodeFile="DocumentType_Edit_SearchFields.aspx.cs" %>

<%@ Register Src="~/CMSModules/SmartSearch/Controls/Edit/SearchFields.ascx"
    TagName="SearchFields" TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:Label ID="lblError" runat="server" EnableViewState="false" />
    <cms:SearchFields runat="server" ID="SearchFields" />
</asp:Content>
