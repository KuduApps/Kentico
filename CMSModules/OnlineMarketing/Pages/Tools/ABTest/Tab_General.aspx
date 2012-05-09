<%@ Page Language="C#" AutoEventWireup="true"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="AB test properties – General"
    Inherits="CMSModules_OnlineMarketing_Pages_Tools_AbTest_Tab_General" Theme="Default" CodeFile="Tab_General.aspx.cs" %>            
<%@ Register Src="~/CMSModules/OnlineMarketing/Controls/UI/AbTest/Edit.ascx"
    TagName="AbTestEdit" TagPrefix="cms" %>
    <asp:Content ID="cntHeader" runat="server" ContentPlaceHolderID="plcBeforeContent">
    <asp:Panel runat="server" ID="pnlDisabled" CssClass="PageHeaderLine" Visible="false">
        <asp:Label runat="server" ID="lblDisabled" EnableViewState="false" />
        <asp:Label runat="server" ID="lblABTestingDisabled" EnableViewState="false" />
    </asp:Panel>
</asp:Content>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <cms:AbTestEdit ID="editElem" runat="server" IsLiveSite="false" />
</asp:Content>