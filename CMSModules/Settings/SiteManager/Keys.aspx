<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Settings_SiteManager_Keys"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="SiteManager - Settings"
    Theme="Default" CodeFile="Keys.aspx.cs" %>
<%@ Register TagPrefix="cms" TagName="SettingsGroupViewer" Src="~/CMSModules/Settings/Controls/SettingsGroupViewer.ascx" %>

<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:Panel runat="server" ID="pnlSearch" Visible="false">
        <asp:TextBox ID="txtSearch" runat="server" class="VeryLongTextBox" EnableViewState="true" />
        <asp:Button ID="btnSearch" runat="server" Text="Search" class="ContentButton" OnClick="btnSearch_OnClick" EnableViewState="true" />
        <span class="UniFlatSearchCheckBox"><cms:LocalizedCheckBox ID="chkDescription" runat="server" ResourceString="webparts.searchindescription" Checked="false" /></span>
        <br /><br />
    </asp:Panel>
    <cms:LocalizedLabel ID="lblNoSettings" CssClass="InfoLabel" runat="server" ResourceString="Development.Settings.NoSettingsInCat"
        DisplayColon="false" EnableViewState="false"
        Visible="false" />
    <cms:settingsgroupviewer id="SettingsGroupViewer" ShortID="s" runat="server" />
</asp:Content>
