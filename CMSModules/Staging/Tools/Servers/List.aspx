<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Staging_Tools_Servers_List"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Staging - Servers"
    Theme="Default" CodeFile="List.aspx.cs" %>
<%@ Register src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" tagname="UniGrid" tagprefix="cms" %>

<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
<asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false" />
    <cms:UniGrid runat="server" ID="UniGrid" GridName="Server_List.xml" OrderBy="ServerDisplayName"
        IsLiveSite="false" />
</asp:Content>
