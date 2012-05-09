<%@ Page Language="C#" AutoEventWireup="true"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Projecttaskpriority list"
    Inherits="CMSModules_ProjectManagement_Pages_Tools_Configuration_ProjectTaskPriority_List" Theme="Default" CodeFile="List.aspx.cs" %>
<%@ Register Src="~/CMSModules/ProjectManagement/Controls/UI/Projecttaskpriority/List.ascx" TagName="ProjecttaskpriorityList" TagPrefix="cms" %>
    
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <cms:ProjecttaskpriorityList ID="listElem" runat="server" IsLiveSite="false" />
</asp:Content>
