<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_CustomTables_Controls_CustomTableDataList"
    CodeFile="CustomTableDataList.ascx.cs" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<asp:Label ID="lblError" runat="server" Visible="false" CssClass="ErrorLabel" EnableViewState="false" />
<cms:UniGrid runat="server" ID="gridData" GridName="~/CMSModules/CustomTables/Controls/CustomTableDataList.xml"
    OrderBy="ItemID" IsLiveSite="false" />
<asp:Literal ID="ltlScript" runat="server" />