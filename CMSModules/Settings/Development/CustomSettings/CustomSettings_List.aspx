<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustomSettings_List.aspx.cs"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Theme="Default" Inherits="CMSModules_Settings_Development_CustomSettings_CustomSettings_List" %>

<%@ Register Src="~/CMSModules/Settings/Controls/SettingsGroupEdit.ascx" TagName="SettingsGroupEdit"
    TagPrefix="cms" %>
<asp:Content ID="Content1" ContentPlaceHolderID="plcContent" runat="server">
    <asp:Label ID="lblError" runat="server" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
    <div class="WebPartForm">
        <cms:settingsgroupedit id="grpEdit" runat="server" displaymode="Default" islivesite="false" />
    </div>
</asp:Content>
