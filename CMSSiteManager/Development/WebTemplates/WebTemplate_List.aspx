<%@ Page Language="C#" AutoEventWireup="true"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Inherits="CMSSiteManager_Development_WebTemplates_WebTemplate_List"
    Theme="Default" CodeFile="WebTemplate_List.aspx.cs" %>
<%@ Register src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" tagname="UniGrid" tagprefix="cms" %>

<asp:Content ContentPlaceHolderID="plcContent" ID="content" runat="server">
    <cms:UniGrid ID="UniGridModules" runat="server" GridName="WebTemplate_List.xml" OrderBy="WebTemplateOrder ASC"
        IsLiveSite="false" />
</asp:Content>
