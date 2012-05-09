<%@ Page Title="Module edit - User interface - General" Language="C#" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master"
    AutoEventWireup="true" Inherits="CMSModules_Modules_Pages_Development_Module_UI_General" Theme="Default" CodeFile="Module_UI_General.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/UIProfiles/UIElementEdit.ascx" TagName="UIElementEdit"
    TagPrefix="cms" %>
<asp:Content ID="Content1" ContentPlaceHolderID="plcBeforeBody" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="plcControls" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="plcContent" runat="Server">
    <cms:UIElementEdit ID="editElem" runat="server" />
</asp:Content>
