<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Newsletters_Tools_Subscribers_Subscriber_Subscriptions"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Newsletter subscribtions"
    CodeFile="Subscriber_Subscriptions.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Newsletters/FormControls/NewsletterSelector.ascx" TagName="NewsletterSelector" TagPrefix="cms" %>
<%@ Register Namespace="CMS.UIControls.UniGridConfig" TagPrefix="ug" Assembly="CMS.UIControls" %>
    
<asp:Content ID="contentControls" ContentPlaceHolderID="plcControls" runat="server">
    <div class="PageHeaderItem">
        <cms:NewsletterSelector runat="server" ID="selectNewsletter" />
    </div>
    <div class="ClearBoth">&nbsp;</div>
</asp:Content>
<asp:Content ContentPlaceHolderID="plcContent" ID="content" runat="server">
    <cms:CMSUpdatePanel runat="server" ID="pnlUpdate" UpdateMode="Conditional">
        <ContentTemplate>
            <cms:LocalizedLabel ID="lblError" runat="server" Visible="false" CssClass="ErrorLabel"
                EnableViewState="false" />
            <cms:UniGrid ID="unigridNewsletters" runat="server" ShortID="g" OrderBy="NewsletterDisplayName" IsLiveSite="false"
                Columns="NewsletterID, NewsletterDisplayName, SubscriptionApproved"
                Query="newsletter.subscribernewsletter.selectsubscriptions">
                <GridActions>                 
                    <ug:Action Name="remove" Caption="$General.Remove$" Icon="Delete.png" Confirmation="$Unigrid.Subscribers.Actions.RemoveSubscription.Confirmation$" />
                    <ug:Action Name="approve" ExternalSourceName="approve" Caption="$general.approve$" Icon="Approve.png" Confirmation="$subscribers.approvesubscription$" />
                    <ug:Action Name="reject" ExternalSourceName="reject" Caption="$general.reject$" Icon="Reject.png" Confirmation="$subscribers.rejectsubscription$" />
                </GridActions>
                <GridColumns>
                    <ug:Column Source="NewsletterDisplayName" Localize="true" Caption="$header.newsletter$" Wrap="false">
                        <Filter Type="text" />
                    </ug:Column>
                    <ug:Column Source="SubscriptionApproved" ExternalSourceName="SubscriptionApproved" Caption="$publish.wasapproved$" Wrap="false">
                        <Filter Type="bool" />
                    </ug:Column>
                    <ug:Column Width="100%" />                    
                </GridColumns>
                <GridOptions DisplayFilter="true" ShowSelection="true" />
            </cms:UniGrid>                
            <asp:Panel ID="pnlActions" runat="server" CssClass="SelectActions">
                <cms:LocalizedLabel ID="lblActions" AssociatedControlID="drpActions" ResourceString="general.selecteditems"
                    DisplayColon="true" runat="server" EnableViewState="false" />
                <asp:DropDownList ID="drpActions" runat="server" CssClass="DropDownFieldSmall" />
                <cms:LocalizedButton ID="btnOk" runat="server" CssClass="SubmitButton" OnClick="btnOk_Clicked"
                    EnableViewState="false" ResourceString="general.ok" />
            </asp:Panel>
            <br />
            <cms:LocalizedCheckBox ID="chkSendConfirmation" runat="server" ResourceString="newsletter_subscribers.sendconfirmation" />
            <asp:PlaceHolder runat="server" ID="plcRequireOptIn">
                <br />
                <cms:LocalizedCheckBox ID="chkRequireOptIn" runat="server" ResourceString="newsletter.requireoptin" />
            </asp:PlaceHolder>
        </ContentTemplate>
    </cms:CMSUpdatePanel>
    <asp:Literal ID="ltlScript" runat="server" />
</asp:Content>