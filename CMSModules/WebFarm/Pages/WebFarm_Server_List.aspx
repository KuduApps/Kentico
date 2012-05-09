<%@ Page Language="C#" AutoEventWireup="true"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Web farm server - List"
    Inherits="CMSModules_WebFarm_Pages_WebFarm_Server_List" Theme="Default" CodeFile="WebFarm_Server_List.aspx.cs" %>
<%@ Register src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" tagname="UniGrid" tagprefix="cms" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plcBeforeContent" runat="server">
    <asp:Panel ID="pnlContent" runat="server" CssClass="PageContent">
        <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false" />
        <cms:UniGrid runat="server" ID="UniGrid" GridName="WebFarmServers_List.xml" OrderBy="ServerDisplayName"
            IsLiveSite="false" />
    </asp:Panel>
</asp:Content>
