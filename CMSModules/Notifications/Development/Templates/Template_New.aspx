<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Notifications_Development_Templates_Template_New"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Templates - New" CodeFile="Template_New.aspx.cs" %>

<%@ Register Src="~/CMSModules/Notifications/Controls/TemplateEdit.ascx"
    TagName="TemplateEdit" TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <cms:TemplateEdit ID="templateEditElem" runat="server" />
</asp:Content>
