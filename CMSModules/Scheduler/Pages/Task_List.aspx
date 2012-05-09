<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Scheduler_Pages_Task_List"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Scheduled tasks - Task List"
    CodeFile="Task_List.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<%@ Register Src="~/CMSFormControls/Sites/SiteSelector.ascx" TagName="SiteSelector"
    TagPrefix="cms" %>
<asp:Content ContentPlaceHolderID="plcContent" runat="server">
    <asp:Panel ID="pnlInfo" runat="server" CssClass="PageHeaderLine SiteHeaderLine">
        <table style="width: 100%" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <asp:Panel ID="pnlSites" runat="server">
                        <cms:LocalizedLabel runat="server" ID="lblSites" EnableViewState="false" ResourceString="general.site"
                            DisplayColon="true" />
                        <cms:SiteSelector ID="siteSelector" runat="server" IsLiveSite="false" />
                    </asp:Panel>
                </td>
                <td class="TextRight">
                    <cms:CMSUpdatePanel ID="pnlUpdateTimer" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Panel ID="pnlLastRun" runat="server">
                                <table class="FloatRight">
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblLastRun" runat="server" EnableViewState="false" />
                                        </td>
                                        <td>
                                            <cms:CMSButton ID="btnRestart" runat="server" OnClick="btnRestart_Click" CssClass="LongButton" />
                                            <cms:CMSButton ID="btnRun" runat="server" OnClick="btnRun_Click" CssClass="LongButton" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </ContentTemplate>
                    </cms:CMSUpdatePanel>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <cms:CMSUpdatePanel ID="pnlUpdate" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="PanelNewRole" runat="server" CssClass="PageHeaderLine">
                <asp:Panel runat="server" ID="pnlNew" CssClass="PageHeaderItem">
                    <asp:Image ID="ImageNew" runat="server" CssClass="NewItemImage" />
                    <asp:HyperLink ID="HyperlinkNew" runat="server" CssClass="NewItemLink" />
                </asp:Panel>
                <asp:Panel runat="server" ID="pnlRefresh" CssClass="PageHeaderItem">
                    <asp:Image ID="ImageRefresh" runat="server" CssClass="NewItemImage" />
                    <asp:HyperLink ID="HyperlinkRefresh" runat="server" CssClass="NewItemLink" />
                </asp:Panel>
                <br style="clear: both" />
            </asp:Panel>
            <asp:Panel ID="PanelUsers" runat="server" CssClass="PageContent">
                <asp:Label ID="lblError" CssClass="ErrorLabel" runat="Server" Visible="false" EnableViewState="false" />
                <asp:Label ID="lblInfo" CssClass="InfoLabel" runat="Server" Visible="false" EnableViewState="false" />
                <cms:UniGrid ID="UniGridTasks" runat="server" GridName="Task_List.xml" OrderBy="TaskDisplayName"
                    IsLiveSite="false" />
            </asp:Panel>
        </ContentTemplate>
    </cms:CMSUpdatePanel>
    <asp:Literal runat="server" ID="ltlScript" EnableViewState="false" />
</asp:Content>
