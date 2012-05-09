<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Tab_Campaigns.aspx.cs" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master"
    Theme="Default" Inherits="CMSModules_WebAnalytics_Pages_Tools_Conversion_Tab_Campaigns" %>

<%@ Register Src="~/CMSAdminControls/UI/UniSelector/UniSelector.ascx" TagName="UniSelector"
    TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
        Visible="false" />
    <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" Visible="false" EnableViewState="false" />
    <asp:PlaceHolder ID="plcTable" runat="server">
        <asp:Label ID="lblAvialable" runat="server" CssClass="BoldInfoLabel" EnableViewState="false" />
        <cms:UniSelector ID="usCampaigns" runat="server" IsLiveSite="false" ObjectType="analytics.campaign"
            SelectionMode="Multiple" ResourcePrefix="campaignselect" />
    </asp:PlaceHolder>
</asp:Content>
