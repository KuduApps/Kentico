<%@ Page Title="" Language="C#" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" 
AutoEventWireup="true" CodeFile="SettingsKey_Edit.aspx.cs" Inherits="CMSModules_Settings_Development_CustomSettings_SettingsKey_Edit" Theme="Default" %>
<%@ Register TagPrefix="cms" TagName="SettingsKeyEdit" Src="~/CMSModules/Settings/Controls/SettingsKeyEdit.ascx" %>

<asp:Content ID="Content5" ContentPlaceHolderID="plcContent" runat="server">
<cms:SettingsKeyEdit ID="skeEditKey" runat="server" />
</asp:Content>
