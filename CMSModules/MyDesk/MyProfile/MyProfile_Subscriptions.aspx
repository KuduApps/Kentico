<%@ Page Language="C#" AutoEventWireup="true"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Inherits="CMSModules_MyDesk_MyProfile_MyProfile_Subscriptions"
    Theme="Default" CodeFile="MyProfile_Subscriptions.aspx.cs" %>

<%@ Register Src="~/CMSModules/Membership/Controls/Subscriptions.ascx" TagName="Subscriptions"
    TagPrefix="cms" %>
<asp:Content ContentPlaceHolderID="plcContent" ID="content" runat="server">
    <cms:Subscriptions ID="elemSubscriptions" runat="server" IsLiveSite="false" />
</asp:Content>
