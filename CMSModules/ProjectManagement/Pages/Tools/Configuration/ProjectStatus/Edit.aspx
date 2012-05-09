<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master"
    Title="Projectstatus properties" Inherits="CMSModules_ProjectManagement_Pages_Tools_Configuration_ProjectStatus_Edit" Theme="Default" CodeFile="Edit.aspx.cs" %>
<%@ Register Src="~/CMSModules/ProjectManagement/Controls/UI/Projectstatus/Edit.ascx"
    TagName="ProjectstatusEdit" TagPrefix="cms" %>
    
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <cms:ProjectstatusEdit ID="editElem" runat="server" IsLiveSite="false" />
</asp:Content>