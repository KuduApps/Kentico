<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Modules_Pages_Development_Module_List"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Modules - Module List"
    CodeFile="Module_List.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <cms:UniGrid ID="UniGridModules" runat="server" GridName="Module_List.xml" OrderBy="ResourceDisplayName"
        IsLiveSite="false" />
</asp:Content>
