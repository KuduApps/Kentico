<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Ecommerce_Pages_Tools_Configuration_ShippingOptions_ShippingOption_List"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" CodeFile="ShippingOption_List.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<%@ Register Src="~/CMSFormControls/Sites/SiteOrGlobalSelector.ascx" TagName="SiteSelector"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/PageElements/HeaderActions.ascx" TagName="HeaderActions"
    TagPrefix="cms" %>
<asp:Content ID="cntControls" runat="server" ContentPlaceHolderID="plcSiteSelector">
    <cms:LocalizedLabel runat="server" ID="lblSite" EnableViewState="false" DisplayColon="true"
        ResourceString="General.Site" />
    <cms:SiteSelector ID="SelectSite" runat="server" IsLiveSite="false" />
</asp:Content>
<asp:Content ID="cntActions" runat="server" ContentPlaceHolderID="plcActions">
    <cms:CMSUpdatePanel ID="pnlActons" runat="server">
        <ContentTemplate>
            <div class="LeftAlign">
                <cms:HeaderActions ID="hdrActions" runat="server" />
            </div>
            <cms:LocalizedLabel ID="lblWarnNew" runat="server" ResourceString="com.chooseglobalorsite"
                EnableViewState="false" Visible="false" CssClass="ActionsInfoLabel" />
            <div class="ClearBoth">
            </div>
        </ContentTemplate>
    </cms:CMSUpdatePanel>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="plcContent" runat="Server">
    <cms:LocalizedLabel ID="lblMissingRate" runat="server" CssClass="ErrorLabel" EnableViewState="false"
        ResourceString="com.NeedExchangeRateFromGlobal" Visible="false" />
    <cms:CMSUpdatePanel ID="pnlUpdate" runat="server">
        <ContentTemplate>
            <asp:Label ID="lblError" runat="server" Visible="false" EnableViewState="false" CssClass="ErrorLabel" />
            <cms:UniGrid runat="server" ID="UniGrid" GridName="ShippingOption_List.xml" OrderBy="ShippingOptionDisplayName"
                IsLiveSite="false" Columns="ShippingOptionID,ShippingOptionDisplayName,ShippingOptionCharge,ShippingOptionEnabled,ShippingOptionSiteID" />
        </ContentTemplate>
    </cms:CMSUpdatePanel>
</asp:Content>
