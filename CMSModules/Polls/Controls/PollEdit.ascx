<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_Polls_Controls_PollEdit"
    CodeFile="PollEdit.ascx.cs" %>
<%@ Register Src="~/CMSModules/Polls/Controls/PollProperties.ascx" TagName="PollProperties"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Polls/Controls/AnswerList.ascx" TagName="AnswerList"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Polls/Controls/AnswerEdit.ascx" TagName="AnswerEdit"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Polls/Controls/PollSecurity.ascx" TagName="PollSecurity"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Polls/Controls/PollView.ascx" TagName="PollView" TagPrefix="cms" %>
<asp:Panel runat="server" ID="pnlBody" CssClass="PollEdit">
    <div class="TabsHeader">
        <cms:BasicTabControl ID="tabMenu" runat="server" Visible="true" />
    </div>
    <div class="TabsContent PollsBox">
        <asp:Panel runat="server" ID="headerLinks" CssClass="PageHeaderLinks">
            <asp:Panel runat="server" ID="pnlPollsBreadcrumbs" CssClass="PollsBreadcrumbs">
                <cms:LocalizedLinkButton ID="btnBreadCrumbs" runat="server" CssClass="TitleBreadCrumb" />
                <span class="TitleBreadCrumbSeparator">&nbsp;</span>
                <cms:LocalizedLabel ID="lblAnswer" runat="server" CssClass="TitleBreadCrumbLast" />
            </asp:Panel>
            <asp:Panel runat="server" ID="pnlPollsLinks">
                <table>
                    <tr class="PollsLinks">
                        <td>
                            <asp:Panel runat="server" ID="pnlNewAnswer" CssClass="PageHeaderItem">
                                <asp:Image ID="imgNewAnswer" runat="server" CssClass="NewItemImage" />
                                <cms:LocalizedLinkButton ID="btnNewAnswer" runat="server" CssClass="NewItemLink" />
                            </asp:Panel>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:Panel runat="server" ID="pnlResetAnswers" CssClass="PageHeaderItem">
                                <asp:Image ID="imgResetAnswers" runat="server" CssClass="NewItemImage" />
                                <cms:LocalizedLinkButton ID="btnResetAnswers" runat="server" CssClass="NewItemLink" />
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:Panel>
        <div>
            <cms:PollProperties ID="PollProperties" runat="server" />
            <cms:AnswerEdit ID="AnswerEdit" runat="server" />
            <cms:AnswerList ID="AnswerList" runat="server" />
            <cms:PollSecurity ID="PollSecurity" runat="server" />
            <cms:PollView ID="PollView" runat="server" CountType="Percentage" CheckVoted="false"
                CheckOpen="false" />
        </div>
    </div>
</asp:Panel>
