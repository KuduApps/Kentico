<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_Notifications_Controls_UserNotifications"
    CodeFile="UserNotifications.ascx.cs" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
    Visible="false" />
<div class="UserNotifications">
    <cms:UniGrid ID="gridElem" runat="server" GridName="~/CMSModules/Notifications/Controls/UserNotifications.xml"
        OrderBy="SubscriptionTime" Columns="SubscriptionID, SubscriptionTarget, SubscriptionTime, SubscriptionEventDisplayName" />
</div>
