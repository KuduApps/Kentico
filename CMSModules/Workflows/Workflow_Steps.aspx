<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Workflows_Workflow_Steps"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Theme="Default" Title="Workflows - Workflow Steps" CodeFile="Workflow_Steps.aspx.cs" %>
<%@ Register src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" tagname="UniGrid" tagprefix="cms" %>

<asp:Content ContentPlaceHolderID="plcContent" ID="content" runat="server">
    <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
        Visible="false" />
    <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
    <cms:UniGrid ID="stepsGrid" runat="server" GridName="Workflow_Steps.xml" OrderBy="StepOrder"
        IsLiveSite="false" />
</asp:Content>
