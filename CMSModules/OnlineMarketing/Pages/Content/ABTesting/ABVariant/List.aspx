<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master"
    Title="Variant list" Inherits="CMSModules_OnlineMarketing_Pages_Content_ABTesting_ABVariant_List"
    Theme="Default" CodeFile="List.aspx.cs" %>

<%@ Register Src="~/CMSModules/OnlineMarketing/Controls/UI/ABVariant/List.ascx" TagName="VariantList"
    TagPrefix="cms" %>
<asp:Content ID="cntHeader" runat="server" ContentPlaceHolderID="plcBeforeContent">
    <asp:Panel runat="server" ID="pnlDisabled" CssClass="PageHeaderLine" Visible="false">
        <asp:Label runat="server" ID="lblDisabled" EnableViewState="false" />
        <asp:Label runat="server" ID="lblABTestingDisabled" EnableViewState="false" />
    </asp:Panel>
</asp:Content>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <cms:VariantList ID="listElem" runat="server" IsLiveSite="false" />
</asp:Content>
