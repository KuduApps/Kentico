<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_Polls_Controls_Polls" CodeFile="Polls.ascx.cs" %>
<%@ Register Src="~/CMSModules/Polls/Controls/PollsList.ascx" TagName="PollsList"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Polls/Controls/PollEdit.ascx" TagName="PollEdit" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Polls/Controls/PollNew.ascx" TagName="PollNew" TagPrefix="cms" %>
<asp:Panel runat="server" ID="pnlBody">
    <asp:Panel runat="server" ID="pnlPollsHeaderLinks" CssClass="PollsHeaderLinks" Visible="true">
        <asp:Image ID="imgNewPoll" runat="server" CssClass="NewItemImage" />
        <cms:LocalizedLinkButton ID="btnNewPoll" runat="server" CssClass="NewItemLink" />
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlPollsHeaderBreadCrumbs" CssClass="PollsHeaderBreadCrumbs"
        Visible="false">
        <cms:LocalizedLinkButton ID="btnBreadCrumbs" runat="server" CssClass="TitleBreadCrumb" />
        <span class="TitleBreadCrumbSeparator">&nbsp;</span>
        <cms:LocalizedLabel ID="lblPoll" runat="server" CssClass="TitleBreadCrumbLast" />
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlList" CssClass="PollsList" Visible="true">
        <cms:PollsList ID="PollsList" runat="server" />
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlEdit" CssClass="PollsEdit" Visible="false">
        <cms:PollEdit ID="PollEdit" runat="server" />
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlPollNew" CssClass="PollNew" Visible="false">
        <cms:PollNew ID="PollNew" runat="server" />
    </asp:Panel>
</asp:Panel>
