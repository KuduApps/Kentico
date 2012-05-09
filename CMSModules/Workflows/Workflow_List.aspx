<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Workflows_Workflow_List"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Theme="Default" CodeFile="Workflow_List.aspx.cs" %>
<%@ Register src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" tagname="UniGrid" tagprefix="cms" %>

<asp:Content ContentPlaceHolderID="plcContent" ID="content" runat="server">
    <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
        Visible="false" />
    <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
    <cms:UniGrid ID="UniGridWorkflows" runat="server" GridName="Workflow_List.xml" OrderBy="WorkflowDisplayName"
        IsLiveSite="false" />
</asp:Content>
