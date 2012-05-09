<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_Polls_Controls_AnswerList" CodeFile="AnswerList.ascx.cs" %>
<%@ Register src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" tagname="UniGrid" tagprefix="cms" %>
<asp:Panel ID="pnlBody" runat="server">
    <cms:UniGrid runat="server" ID="uniGrid" GridName="~/CMSModules/Polls/Controls/AnswerList.xml"
        OrderBy="AnswerOrder ASC" />
</asp:Panel>
