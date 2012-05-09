<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_System_System"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Site Manager - System"
    CodeFile="System.aspx.cs" %>

<%@ Register Src="~/CMSModules/System/Controls/System.ascx" TagName="SystemInformation"
    TagPrefix="cms" %>
    
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent" EnableViewState="false">
    <cms:SystemInformation ID="sysInfo" runat="server" />
</asp:Content>
