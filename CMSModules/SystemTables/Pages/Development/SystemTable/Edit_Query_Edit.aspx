<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_SystemTables_Pages_Development_SystemTable_Edit_Query_Edit"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="System tables - Edit query"
    Theme="Default"
    CodeFile="Edit_Query_Edit.aspx.cs" %>

<%@ Register Src="~/CMSModules/AdminControls/Controls/Class/QueryEdit.ascx" TagName="QueryEdit" TagPrefix="cms" %>
    
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <cms:QueryEdit ID="queryEdit" Visible="true" runat="server" />
</asp:Content>