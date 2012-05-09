<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Notifications_FormControls_MultipleNotificationGatewaySelector" CodeFile="MultipleNotificationGatewaySelector.ascx.cs" %>
<%@ Register Src="~/CMSAdminControls/UI/UniSelector/UniSelector.ascx" TagName="UniSelector"
    TagPrefix="cms" %>
<cms:CMSUpdatePanel ID="pnlUpdate" runat="server">
    <ContentTemplate>
        <cms:UniSelector ID="uniSelector" runat="server" IsLiveSite="false" ObjectType="notification.gateway"
            SelectionMode="MultipleTextBox" AdditionalColumns="GatewayEnabled" GridName="~/CMSModules/Notifications/FormControls/NotificationControlItemList.xml"
            DialogGridName="~/CMSModules/Notifications/FormControls/NotificationDialogItemList.xml" DialogWindowWidth="650" />
    </ContentTemplate>
</cms:CMSUpdatePanel>
