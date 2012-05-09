<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Notifications_Development_Gateways_Gateway_List"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Theme="Default" Title="Notifications - Gateways list" CodeFile="Gateway_List.aspx.cs" %>
<%@ Register src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" tagname="UniGrid" tagprefix="cms" %>

<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <cms:UniGrid runat="server" ID="unigridGatewayList" GridName="Gateway_List.xml" OrderBy="GatewayDisplayName"
        IsLiveSite="false" Columns="GatewayID, GatewayDisplayName, GatewayEnabled" />
</asp:Content>
