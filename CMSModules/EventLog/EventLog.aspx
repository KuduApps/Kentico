<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_EventLog_EventLog"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="SiteManager - Event log"
    Theme="Default" CodeFile="EventLog.aspx.cs" %>

<%@ Register Src="~/CMSFormControls/Sites/SiteSelector.ascx" TagName="SiteSelector"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/EventLog/Controls/EventLog.ascx" TagName="EventLog"
    TagPrefix="cms" %>
<asp:Content ContentPlaceHolderID="plcBeforeContent" runat="server">
    <asp:Panel ID="pnlTop" runat="server" CssClass="PageHeaderLine SiteHeaderLine">
        <table style="width: 100%" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <asp:Panel ID="pnlSites" runat="server">
                        <asp:Label runat="server" ID="lblSite" EnableViewState="false" />
                        <cms:SiteSelector ID="siteSelector" runat="server" IsLiveSite="false" />
                    </asp:Panel>
                </td>
                <td class="TextRight">
                    <cms:CMSButton runat="server" ID="btnClearLog" OnClick="ClearLogButton_Click" CssClass="LongButton"
                        EnableViewState="false" />
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
<asp:Content ID="plcContent" ContentPlaceHolderID="plcContent" runat="server">
    <cms:EventLog ID="eventLog" ShortID="ev" runat="server" ShowFilter="true" />
</asp:Content>
