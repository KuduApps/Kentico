<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_WebFarm_Pages_WebFarm_Task_List" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master"
    Title="Web farm server - List" Theme="Default" CodeFile="WebFarm_Task_List.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/UniSelector/UniSelector.ascx" TagName="UniSelector"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <cms:CMSUpdatePanel ID="pnlUpdate" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlSites" runat="server" CssClass="PageHeaderLine SiteHeaderLine">
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <asp:Label runat="server" ID="lblServer" AssociatedControlID="uniSelector" />
                            <cms:UniSelector ID="uniSelector" runat="server" ObjectType="cms.webfarmserver" ResourcePrefix="webfarmserver"
                                AllowEmpty="false" IsLiveSite="false" OrderBy="ServerDisplayName" ReturnColumnName="ServerName" />
                        </td>
                        <td class="TextRight">
                            <cms:CMSButton ID="btnRunTasks" runat="server" CssClass="ContentButton" OnClick="btnRunTasks_Click" />
                            <cms:CMSButton ID="btnEmptyTasks" runat="server" CssClass="LongButton" OnClick="btnEmptyTasks_Click" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnlContent" runat="server" CssClass="PageContent">
                <asp:Label ID="lblInfo" CssClass="InfoLabel" runat="Server" Visible="false" EnableViewState="false" />
                <cms:UniGrid runat="server" ID="UniGrid" GridName="WebFarm_Task_List.xml" OrderBy="ServerDisplayName" IsLiveSite="false" />
            </asp:Panel>
        </ContentTemplate>
    </cms:CMSUpdatePanel>
</asp:Content>
