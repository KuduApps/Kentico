<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master"
    Title="Campaign properties – General" Inherits="CMSModules_WebAnalytics_Pages_Tools_Campaign_Tab_General"
    Theme="Default" CodeFile="Tab_General.aspx.cs" %>

<%@ Register Src="~/CMSModules/WebAnalytics/Controls/UI/Campaign/Edit.ascx" TagName="CampaignEdit"
    TagPrefix="cms" %>
<asp:Content ID="cntSave" runat="server" ContentPlaceHolderID="plcBeforeContent">
    <asp:Panel ID="pnlActions" runat="server" CssClass="PageHeaderLine" EnableViewState="false">
        <asp:LinkButton ID="lnkSave" runat="server" OnClick="lnkSave_Click" CssClass="WebAnalitycsMenuItemEdit"
            EnableViewState="false">
            <asp:Image ID="imgSave" runat="server" EnableViewState="false" CssClass="WebAnalitycsMenuItemImage" />
            <%=mSave%>
        </asp:LinkButton>
    </asp:Panel>
</asp:Content>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:Panel ID="pnlContent" runat="server">
        <cms:CampaignEdit ID="editElem" runat="server" IsLiveSite="false" />
    </asp:Panel>
</asp:Content>
