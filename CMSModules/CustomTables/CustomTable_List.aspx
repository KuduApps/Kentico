<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_CustomTables_CustomTable_List" Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master"
    Title="Custom Tables List" CodeFile="CustomTable_List.aspx.cs" %>
<%@ Register src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" tagname="UniGrid" tagprefix="cms" %>

<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:Label runat="server" ID="lblError" ForeColor="red" EnableViewState="false" Visible="false" />
    <cms:UniGrid runat="server" ID="uniGrid" GridName="CustomTable_List.xml" OrderBy="ClassDisplayName"
        IsLiveSite="false" Columns="ClassID,ClassDisplayName,ClassName,ClassTableName" />
</asp:Content>
