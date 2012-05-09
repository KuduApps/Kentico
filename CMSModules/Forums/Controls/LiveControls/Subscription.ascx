<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Forums_Controls_LiveControls_Subscription" CodeFile="Subscription.ascx.cs" %>
<%@ Register src="~/CMSAdminControls/UI/PageElements/HeaderActions.ascx" tagname="HeaderActions" tagprefix="cms" %>
<%@ Register Src="~/CMSModules/Forums/Controls/Subscriptions/SubscriptionEdit.ascx" TagName="SubscriptionEdit" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Forums/Controls/Subscriptions/SubscriptionList.ascx" TagName="SubscriptionList" TagPrefix="cms" %>
<asp:PlaceHolder ID="plcList" runat="server">
    <cms:HeaderActions ID="actionsElem" runat="server" />
    <cms:SubscriptionList ID="subscriptionList" runat="server" />
</asp:PlaceHolder>
<asp:PlaceHolder ID="plcEdit" runat="server" Visible="false">
    <asp:LinkButton ID="lnkEditBack" runat="server" CausesValidation="false" /><asp:Label
        ID="lblEditBack" runat="server" /><br />
    <cms:SubscriptionEdit ID="subscriptionEdit" runat="server" />
</asp:PlaceHolder>
<asp:PlaceHolder ID="plcNew" runat="server" Visible="false">
    <asp:LinkButton ID="lnkNewBack" runat="server" CausesValidation="false" /><asp:Label
        ID="lblNewBack" runat="server" /><br />
    <cms:SubscriptionEdit ID="subscriptionNew" runat="server" />
</asp:PlaceHolder>
