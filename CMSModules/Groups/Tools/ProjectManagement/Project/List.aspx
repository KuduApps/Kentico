<%@ Page Language="C#" AutoEventWireup="true"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Project list"
    Inherits="CMSModules_Groups_Tools_ProjectManagement_Project_List" Theme="Default" CodeFile="List.aspx.cs" %>
<%@ Register Src="~/CMSModules/ProjectManagement/Controls/UI/Project/List.ascx" TagName="ProjectList" TagPrefix="cms" %>
    
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <cms:ProjectList ID="listElem" runat="server" IsLiveSite="false" />
</asp:Content>
