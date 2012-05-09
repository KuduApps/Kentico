<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master"
    Title="Alternative Forms List" Inherits="CMSModules_SystemTables_Pages_Development_AlternativeForms_List"
    Theme="Default" CodeFile="List.aspx.cs" %>

<%@ Register Src="~/CMSModules/AdminControls/Controls/Class/AlternativeFormList.ascx"
    TagName="AlternativeFormList" TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <cms:AlternativeFormList ID="listElem" runat="server" IsLiveSite="false" />
</asp:Content>
