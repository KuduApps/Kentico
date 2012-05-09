<%@ Page Language="C#" AutoEventWireup="true"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Projectstatus list"
    Inherits="CMSModules_ProjectManagement_Pages_Tools_Configuration_ProjectStatus_List" Theme="Default" CodeFile="List.aspx.cs" %>
<%@ Register Src="~/CMSModules/ProjectManagement/Controls/UI/Projectstatus/List.ascx" TagName="ProjectstatusList" TagPrefix="cms" %>
    
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <cms:ProjectstatusList ID="listElem" runat="server" IsLiveSite="false" />
</asp:Content>
