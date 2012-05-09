<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_SystemTables_Pages_Development_SystemTable_Edit_Search" Theme="Default"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="System table search fields" CodeFile="Edit_Search.aspx.cs" %>

<%@ Register Src="~/CMSModules/SmartSearch/Controls/Edit/ClassFields.ascx" TagName="ClassFields"
    TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:Label ID="lblError" runat="server" EnableViewState="false" />
    <cms:LocalizedLabel runat="server" ID="lblInfo" ResourceString="systbl.search.warninfo"
        CssClass="InfoLabel" />
        <br />
    <cms:ClassFields ID="ClassFields" runat="server" />
</asp:Content>
