<%@ Page Language="C#" AutoEventWireup="true"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="MVTest list"
    Inherits="CMSModules_OnlineMarketing_Pages_Content_MVTest_List" Theme="Default" CodeFile="List.aspx.cs" %>
<%@ Register Src="~/CMSModules/OnlineMarketing/Controls/UI/Mvtest/List.ascx" TagName="MvtestList" TagPrefix="cms" %>

<asp:Content ID="cntHeader" runat="server" ContentPlaceHolderID="plcBeforeContent">
    <asp:Panel runat="server" ID="pnlDisabled" CssClass="PageHeaderLine" Visible="false">
        <asp:Label runat="server" ID="lblDisabled" EnableViewState="false" />
        <asp:Label runat="server" ID="lblMVTestingDisabled" EnableViewState="false" />
        <asp:Label runat="server" ID="lblTrackConversionsDisabled" EnableViewState="false" />
    </asp:Panel>
</asp:Content>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <cms:MvtestList ID="listElem" runat="server" IsLiveSite="false" />
</asp:Content>
