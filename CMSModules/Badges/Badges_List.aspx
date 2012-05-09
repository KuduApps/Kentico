<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Badges_Badges_List"
    ValidateRequest="false" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master"
    Theme="Default" CodeFile="Badges_List.aspx.cs" %>
<%@ Register src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" tagname="UniGrid" tagprefix="cms" %>

<asp:Content ID="cntBody" ContentPlaceHolderID="plcContent" runat="server">
    <cms:UniGrid runat="server" ID="UniGrid" GridName="~/CMSModules/Badges/BadgesList.xml"
        OrderBy="BadgeTopLimit DESC" IsLiveSite="false" />
</asp:Content>
