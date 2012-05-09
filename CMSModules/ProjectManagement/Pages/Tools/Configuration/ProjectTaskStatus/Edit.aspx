<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master"
    Title="Projecttaskstatus properties" Inherits="CMSModules_ProjectManagement_Pages_Tools_Configuration_ProjectTaskStatus_Edit" Theme="Default" CodeFile="Edit.aspx.cs" %>
<%@ Register Src="~/CMSModules/ProjectManagement/Controls/UI/Projecttaskstatus/Edit.ascx"
    TagName="ProjecttaskstatusEdit" TagPrefix="cms" %>
    
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <cms:ProjecttaskstatusEdit ID="editElem" runat="server" IsLiveSite="false" />
</asp:Content>