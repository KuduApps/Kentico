<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master"
    Title="Projecttaskpriority properties" Inherits="CMSModules_ProjectManagement_Pages_Tools_Configuration_ProjectTaskPriority_Edit" Theme="Default" CodeFile="Edit.aspx.cs" %>
<%@ Register Src="~/CMSModules/ProjectManagement/Controls/UI/Projecttaskpriority/Edit.ascx"
    TagName="ProjecttaskpriorityEdit" TagPrefix="cms" %>
    
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <cms:ProjecttaskpriorityEdit ID="editElem" runat="server" IsLiveSite="false" />
</asp:Content>