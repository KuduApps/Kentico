<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Ecommerce_Pages_Tools_Configuration_InternalStatus_InternalStatus_List" Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Internal status - List" CodeFile="InternalStatus_List.aspx.cs" %>
<%@ Register src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" tagname="UniGrid" tagprefix="cms" %>

<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:Label ID="lblError" runat="server" Visible="false" EnableViewState="false" CssClass="ErrorLabel" />
    <asp:Label ID="lblGlobalInfo" runat="server" Visible="false" EnableViewState="false" Font-Bold="true" CssClass="InfoLabel" />
    <cms:UniGrid runat="server" ID="gridElem" GridName="InternalStatus_List.xml" OrderBy="InternalStatusDisplayName"
        IsLiveSite="false" Columns="InternalStatusID, InternalStatusDisplayName, InternalStatusEnabled" />
</asp:Content>
