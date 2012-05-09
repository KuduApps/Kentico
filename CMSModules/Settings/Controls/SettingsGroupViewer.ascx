<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SettingsGroupViewer.ascx.cs"
    Inherits="CMSModules_Settings_Controls_SettingsGroupViewer" %>
<asp:Label ID="lblSaved" runat="server" CssClass="InfoLabel" EnableViewState="false"
    Visible="false" />
<asp:Label ID="lblInfo" runat="server" CssClass="InfoLabel" EnableViewState="false"
    Visible="false" />
<div class="WebPartForm">
    <asp:PlaceHolder ID="plcContent" runat="server" />
</div>
