<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_UICultures_Pages_Development_ResourceString_List" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master"
    Theme="Default" CodeFile="List.aspx.cs" %>
<%@ Register src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" tagname="UniGrid" tagprefix="cms" %>

<asp:Content ContentPlaceHolderID="plcContent" ID="content" runat="server">
    <cms:UniGrid ID="gridStrings" runat="server" GridName="List.xml"
        OrderBy="StringKey" IsLiveSite="false" />
</asp:Content>
