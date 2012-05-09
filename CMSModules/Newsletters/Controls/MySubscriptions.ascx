<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Newsletters_Controls_MySubscriptions" CodeFile="MySubscriptions.ascx.cs" %>
<%@ Register Src="~/CMSAdminControls/UI/UniSelector/UniSelector.ascx" TagName="UniSelector"
    TagPrefix="cms" %>
<asp:Label runat="server" ID="lblInfo" Visible="false" />
<asp:PlaceHolder runat="server" ID="plcMain">
    <cms:LocalizedLabel ID="lblInfoMsg" runat="server" Visible="false" CssClass="InfoLabel"
        ResourceString="general.changessaved" EnableViewState="false" />
    <asp:Label runat="server" ID="lblText" EnableViewState="false" CssClass="InfoLabel" />
    <cms:UniSelector ID="usNewsletters" runat="server" ObjectType="Newsletter.Newsletter"
        SelectionMode="Multiple" ResourcePrefix="newsletterselect" />
</asp:PlaceHolder>
