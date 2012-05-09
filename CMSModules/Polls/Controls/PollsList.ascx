<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_Polls_Controls_PollsList" CodeFile="PollsList.ascx.cs" %>
<%@ Register src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" tagname="UniGrid" tagprefix="cms" %>
<asp:Panel ID="pnlBody" runat="server">
    <cms:UniGrid runat="server" ID="UniGrid" GridName="~/CMSModules/Polls/Controls/PollsList.xml"
        OrderBy="PollDisplayName" />
</asp:Panel>
