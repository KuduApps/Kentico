<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Inherits="CMSModules_Newsletters_Tools_Subscribers_Subscriber_General"
    Theme="Default" Title="Tools - Newsletter subscriber edit" CodeFile="Subscriber_General.aspx.cs" %>

<asp:Content ContentPlaceHolderID="plcContent" ID="content" runat="server">
    <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
        Visible="false" /><asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
            Visible="false" />
    <cms:DataForm ID="formCustomFields" runat="server" ClassName="newsletter.subscriber"
        IsLiveSite="false" />
</asp:Content>
