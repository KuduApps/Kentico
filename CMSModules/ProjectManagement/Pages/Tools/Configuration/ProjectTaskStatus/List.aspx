<%@ Page Language="C#" AutoEventWireup="true"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Projecttaskstatus list"
    Inherits="CMSModules_ProjectManagement_Pages_Tools_Configuration_ProjectTaskStatus_List" Theme="Default" CodeFile="List.aspx.cs" %>
<%@ Register Src="~/CMSModules/ProjectManagement/Controls/UI/Projecttaskstatus/List.ascx" TagName="ProjecttaskstatusList" TagPrefix="cms" %>
    
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <cms:ProjecttaskstatusList ID="listElem" runat="server" IsLiveSite="false" />
</asp:Content>
