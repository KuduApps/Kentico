<%@ Page Title="" Language="C#" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master"
    AutoEventWireup="true" CodeFile="CustomSettingsCategory_Edit.aspx.cs" Inherits="CMSModules_Settings_Development_CustomSettings_CustomSettingsCategory_Edit"
    Theme="Default" %>

<%@ Register Src="~/CMSModules/Settings/Controls/SettingsCategoryEdit.ascx" TagName="SettingsCategoryEdit"
    TagPrefix="cms" %>
<asp:Content ID="Content5" ContentPlaceHolderID="plcContent" runat="server">
    <asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />
    <cms:SettingsCategoryEdit ID="catEdit" runat="server"></cms:SettingsCategoryEdit>
    <asp:HyperLink ID="lnkExportSettings" runat="server" Visible="false" EnableViewState="false" />
    <asp:HyperLink ID="lnkExportCategories" runat="server" Visible="false" EnableViewState="false" />
</asp:Content>
