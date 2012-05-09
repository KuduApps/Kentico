<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Messaging_Controls_SelectFromContactList" CodeFile="SelectFromContactList.ascx.cs" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<cms:CMSUpdatePanel ID="pnlContactList" runat="server">
    <ContentTemplate>
        <asp:Panel ID="pnlInfo" CssClass="Info" Visible="false" runat="server">
            <asp:Label runat="server" ID="lblInfo" EnableViewState="false" CssClass="InfoLabel" />
            <asp:Label runat="server" ID="lblError" EnableViewState="false" CssClass="ErrorLabel" />
        </asp:Panel>
        <div class="ListPanel">
            <cms:UniGrid ID="gridContactList" runat="server" GridName="~/CMSModules/Messaging/Controls/SelectFromContactList.xml"
                OrderBy="UserName" />
        </div>
    </ContentTemplate>
</cms:CMSUpdatePanel>
