<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Notifications_FormControls_NotificationTemplateSelector" CodeFile="NotificationTemplateSelector.ascx.cs" %>
<%@ Register Src="~/CMSAdminControls/UI/UniSelector/UniSelector.ascx" TagName="UniSelector"
    TagPrefix="cms" %>
<cms:CMSUpdatePanel ID="pnlUpdate" runat="server">
    <ContentTemplate>
        <cms:UniSelector ID="uniSelector" runat="server" DisplayNameFormat="{%TemplateName%}"
            AllowEditTextBox="true" SelectionMode="SingleTextBox" ObjectType="notification.template"
            ResourcePrefix="notificationtemplateselector" FilterControl="~/CMSFormControls/Filters/SiteFilter.ascx" />
    </ContentTemplate>
</cms:CMSUpdatePanel>
