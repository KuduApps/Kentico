<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Notifications_Development_Templates_Template_Edit_General"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Templates edit - General" CodeFile="Template_Edit_General.aspx.cs" %>

<%@ Register Src="~/CMSModules/Notifications/Controls/TemplateEdit.ascx"
    TagName="TemplateEdit" TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent" EnableViewState="false">
    <cms:TemplateEdit ID="templateEditElem" runat="server" />
</asp:Content>
