<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Forums_Controls_Subscriptions_SubscriptionList" CodeFile="SubscriptionList.ascx.cs" %>
<%@ Register src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" tagname="UniGrid" tagprefix="cms" %>
<cms:UniGrid runat="server" ID="gridElem" GridName="~/CMSModules/Forums/Controls/Subscriptions/ForumSubscription_List.xml" Columns="SubscriptionID, SubscriptionEmail, PostSubject" DelayedReload="true" />
<asp:Panel runat="server" ID="pnlSendConfirmationEmail" Visible="true">
    <div style="padding: 10px;">
    </div>
    <asp:CheckBox runat="server" ID="chkSendConfirmationEmail" Checked="true" />
    <cms:LocalizedLabel runat="server" ID="lblSendConfirmation" CssClass="ContentLabel"
        ResourceString="forums.forumsubscription.sendemaildelete" />
</asp:Panel>
