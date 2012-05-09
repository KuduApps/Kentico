<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_EventLog_Controls_EventFilter"
    CodeFile="EventFilter.ascx.cs" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/Filters/TextSimpleFilter.ascx" TagName="TextSimpleFilter"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/Filters/TimeSimpleFilter.ascx" TagName="TimeSimpleFilter"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSFormControls/System/EventLogTypeSelector.ascx" TagName="EventLogTypeSelector"
    TagPrefix="cms" %>
<asp:Panel ID="pnl" runat="server" DefaultButton="btnSearch">
    <table cellpadding="0" cellspacing="2">
        <tr>
            <td>
                <cms:LocalizedLabel ID="lblType" runat="server" ResourceString="general.type" DisplayColon="true" />
            </td>
            <td>
                <cms:EventLogTypeSelector ID="drpEventLogType" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <cms:LocalizedLabel ID="lblSource" runat="server" ResourceString="Unigrid.EventLog.Columns.Source"
                    DisplayColon="true" />
            </td>
            <td>
                <cms:TextSimpleFilter ID="fltSource" runat="server" Column="Source" />
            </td>
        </tr>
        <tr>
            <td>
                <cms:LocalizedLabel ID="lblEventCode" runat="server" ResourceString="Unigrid.EventLog.Columns.EventCode"
                    DisplayColon="true" />
            </td>
            <td>
                <cms:TextSimpleFilter ID="fltEventCode" runat="server" Column="EventCode" />
            </td>
        </tr>
        <asp:PlaceHolder ID="plcAdvancedSearch" runat="server" Visible="false">
            <tr runat="server">
                <td>
                    <cms:LocalizedLabel ID="lblUserName" runat="server" ResourceString="general.username"
                        DisplayColon="true" />
                </td>
                <td>
                    <cms:TextSimpleFilter ID="fltUserName" runat="server" Column="UserName" />
                </td>
            </tr>
            <tr runat="server">
                <td>
                    <cms:LocalizedLabel ID="lblIPAddress" runat="server" ResourceString="Unigrid.EventLog.Columns.IPAdress"
                        DisplayColon="true" />
                </td>
                <td>
                    <cms:TextSimpleFilter ID="fltIPAddress" runat="server" Column="IPAddress" />
                </td>
            </tr>
            <tr runat="server">
                <td>
                    <cms:LocalizedLabel ID="lblDocumentName" runat="server" ResourceString="general.documentname"
                        DisplayColon="true" />
                </td>
                <td>
                    <cms:TextSimpleFilter ID="fltDocumentName" runat="server" Column="DocumentName" />
                </td>
            </tr>
            <tr runat="server">
                <td>
                    <cms:LocalizedLabel ID="lblMachineName" runat="server" ResourceString="Unigrid.EventLog.Columns.MachineName"
                        DisplayColon="true" />
                </td>
                <td>
                    <cms:TextSimpleFilter ID="fltMachineName" runat="server" Column="EventMachineName" />
                </td>
            </tr>
            <tr runat="server">
                <td>
                    <cms:LocalizedLabel ID="lblEventURL" runat="server" ResourceString="eventlogdetails.eventurl" />
                </td>
                <td>
                    <cms:TextSimpleFilter ID="fltEventURL" runat="server" Column="EventUrl" />
                </td>
            </tr>
            <tr runat="server">
                <td>
                    <cms:LocalizedLabel ID="lblURLReferrer" runat="server" ResourceString="EventLogDetails.UrlReferrer"
                        DisplayColon="true" />
                </td>
                <td>
                    <cms:TextSimpleFilter ID="fltEventURLRef" runat="server" Column="EventUrlReferrer" />
                </td>
            </tr>
            <tr runat="server">
                <td>
                    <cms:LocalizedLabel ID="lblDescription" runat="server" ResourceString="general.description"
                        DisplayColon="true" />
                </td>
                <td>
                    <cms:TextSimpleFilter ID="fltDescription" runat="server" Column="EventDescription" />
                </td>
            </tr>
            <tr runat="server">
                <td>
                    <cms:LocalizedLabel ID="lblUserAgent" runat="server" ResourceString="EventLogDetails.UserAgent"
                        DisplayColon="true" />
                </td>
                <td>
                    <cms:TextSimpleFilter ID="fltUserAgent" runat="server" Column="EventUserAgent" />
                </td>
            </tr>
        </asp:PlaceHolder>
        <tr>
            <td>
                <cms:LocalizedLabel ID="lblTimeBetween" runat="server" ResourceString="eventlog.timebetween"
                    DisplayColon="true" />
            </td>
            <td>
                <cms:TimeSimpleFilter ID="fltTimeBetween" runat="server" Column="EventTime" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <cms:LocalizedButton ID="btnSearch" runat="server" CssClass="ContentButton" ResourceString="general.search" />
            </td>
        </tr>
    </table>
    <br />
</asp:Panel>
<asp:Panel ID="pnlAdvanced" runat="server" Visible="true">
    <asp:Image runat="server" ID="imgShowSimpleFilter" CssClass="NewItemImage" />
    <asp:LinkButton ID="lnkShowSimpleFilter" runat="server" OnClick="lnkShowSimpleFilter_Click" />
</asp:Panel>
<asp:Panel ID="pnlSimple" runat="server" Visible="false">
    <asp:Image runat="server" ID="imgShowAdvancedFilter" CssClass="NewItemImage" />
    <asp:LinkButton ID="lnkShowAdvancedFilter" runat="server" OnClick="lnkShowAdvancedFilter_Click" />
</asp:Panel>
