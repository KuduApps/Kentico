<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Ecommerce_Pages_Tools_Configuration_StoreSettings_StoreSettings_GlobalObjects"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" CodeFile="StoreSettings_GlobalObjects.aspx.cs" %>

<%@ Register TagPrefix="cms" TagName="SettingsGroupViewer" Src="~/CMSModules/Settings/Controls/SettingsGroupViewer.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="plcContent" runat="Server">
    <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
    <cms:SettingsGroupViewer ID="SettingsGroupViewer" runat="server" />
</asp:Content>
