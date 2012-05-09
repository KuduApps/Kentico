<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_PortalEngine_UI_WebContainers_Container_List"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Theme="Default" Title="Web part container"
    CodeFile="Container_List.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<asp:Content ContentPlaceHolderID="plcContent" ID="content" runat="server">
    <cms:UniGrid runat="server" ID="UniGrid" GridName="Container_List.xml" OrderBy="ContainerDisplayName"
        IsLiveSite="false" />
</asp:Content>
