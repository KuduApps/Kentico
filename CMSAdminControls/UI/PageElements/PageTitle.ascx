<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PageTitle.ascx.cs" Inherits="CMSAdminControls_UI_PageElements_PageTitle" %>
<%@ Register Src="~/CMSAdminControls/UI/PageElements/Help.ascx" TagName="Help" TagPrefix="cms" %>
<asp:Panel runat="server" ID="pnlBody" CssClass="PageTitleBody">
    <asp:Panel runat="server" ID="pnlTitle" CssClass="PageTitleHeader" Visible="false">
        <table runat="server" id="titleTable" cellpadding="0" cellspacing="0" border="0">
            <tr>
                <td runat="server" id="titleRow">
                    <asp:Image ID="imgTitle" runat="server" Visible="false" CssClass="PageTitleImage"
                        EnableViewState="false" />
                    <asp:Label ID="lblTitle" runat="server" CssClass="PageTitle" EnableViewState="false" />
                </td>
                <td class="TextRight">
                    <asp:PlaceHolder runat="server" ID="plcMisc" />
                </td>
                <td style="vertical-align: top;">
                    <cms:Help ID="helpElem" runat="server" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlBreadCrumbs" CssClass="PageTitleBreadCrumbs" Visible="false">
        <table cellpadding="0" cellspacing="0" border="0" style="width: 100%;">
            <tr>
                <td style="width: 100%;" class="PageTitleBreadCrumbsPadding">
                    <asp:PlaceHolder ID="plcBreadcrumbs" runat="server" />
                </td>
                <td>
                    <cms:Help ID="helpBreadcrumbs" runat="server" Visible="false" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Literal runat="server" ID="ltlScript" EnableViewState="false" />
</asp:Panel>
