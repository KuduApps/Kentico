<%@ Control Language="C#" AutoEventWireup="true" CodeFile="System.ascx.cs" Inherits="CMSModules_System_Controls_System" %>
<asp:Panel ID="pnlBody" runat="server" SkinID="Default">
    <cms:CMSUpdatePanel runat="server" ID="plnLabels" CatchErrors="true" EnableViewState="true">
        <ContentTemplate>
            <asp:Label ID="lblError" runat="server" CssClass="ErrorLabel" EnableViewState="true" />
            <asp:Label ID="lblInfo" runat="server" CssClass="InfoLabel" EnableViewState="true" />
        </ContentTemplate>
    </cms:CMSUpdatePanel>
    <table style="width: 860px;" cellspacing="0" cellpadding="0">
        <tr>
            <td style="vertical-align: top;">
                <asp:Panel ID="pnlSystemInfo" runat="server" Width="425" CssClass="SystemPanel">
                    <table>
                        <tr>
                            <td style="white-space: nowrap; width: 180px;">
                                <asp:Label ID="lblMachineName" runat="server" EnableViewState="false" />
                            </td>
                            <td>
                                <asp:Label ID="lblMachineNameValue" runat="server" EnableViewState="false" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblAspAccount" runat="server" EnableViewState="false" />
                            </td>
                            <td>
                                <asp:Label ID="lblValueAspAccount" runat="server" EnableViewState="false" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblAspVersion" runat="server" EnableViewState="false" />
                            </td>
                            <td>
                                <asp:Label ID="lblValueAspVersion" runat="server" EnableViewState="false" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblPool" runat="server" EnableViewState="false" />
                            </td>
                            <td>
                                <asp:Label ID="lblValuePool" runat="server" EnableViewState="false" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblTrustLevel" runat="server" EnableViewState="false" />
                            </td>
                            <td>
                                <asp:Label ID="lblValueTrustLevel" runat="server" EnableViewState="false" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblIP" runat="server" EnableViewState="false" />
                            </td>
                            <td>
                                <asp:Label ID="lblIPValue" runat="server" EnableViewState="false" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
            <td style="vertical-align: top;">
                <asp:Panel ID="pnlDatabaseInfo" runat="server" Width="425" CssClass="SystemPanel">
                    <table>
                        <tr>
                            <td style="white-space: nowrap; width: 180px;">
                                <asp:Label ID="lblServerName" runat="server" EnableViewState="false" />
                            </td>
                            <td>
                                <asp:Label ID="lblServerNameValue" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td style="white-space: nowrap; width: 180px;">
                                <asp:Label ID="lblServerVersion" runat="server" EnableViewState="false" />
                            </td>
                            <td>
                                <asp:Label ID="lblServerVersionValue" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="white-space: nowrap;">
                                <asp:Label ID="lblDBName" runat="server" EnableViewState="false" />
                            </td>
                            <td>
                                <asp:Label ID="lblDBNameValue" runat="server" EnableViewState="false" />
                            </td>
                        </tr>
                        <tr>
                            <td style="white-space: nowrap;">
                                <asp:Label ID="lblDBSize" runat="server" EnableViewState="false" />
                            </td>
                            <td>
                                <asp:Label ID="lblDBSizeValue" runat="server" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
    <br />
    <table style="padding-bottom: 10px">
        <tr>
            <td style="width: 180px;">
                <cms:LocalizedLabel runat="server" ID="lblRefresh" EnableViewState="false" ResourceString="Administration-System.Refresh" />
            </td>
            <td>
                <asp:DropDownList runat="server" ID="drpRefresh" EnableViewState="true" AutoPostBack="true"
                    CssClass="RefreshList">
                    <asp:ListItem Selected="True">1</asp:ListItem>
                    <asp:ListItem>2</asp:ListItem>
                    <asp:ListItem>5</asp:ListItem>
                    <asp:ListItem>10</asp:ListItem>
                    <asp:ListItem>30</asp:ListItem>
                    <asp:ListItem>60</asp:ListItem>
                </asp:DropDownList>
                <cms:LocalizedButton runat="server" ID="btnRefresh" ResourceString="General.Refresh"
                    CssClass="ContentButton" />
            </td>
        </tr>
    </table>
    <cms:CMSUpdatePanel runat="server" ID="pnlUpdate" CatchErrors="true" EnableViewState="true">
        <ContentTemplate>
            <asp:Timer ID="timRefresh" runat="server" Interval="2000" EnableViewState="false" />
            <table style="width: 860px" cellspacing="0" cellpadding="0">
                <tr>
                    <td style="vertical-align: top">
                        <asp:Panel ID="pnlMemory" runat="server" Width="425" CssClass="MediumSystemPanel">
                            <table>
                                <tr>
                                    <td style="white-space: nowrap; width: 180px;">
                                        <asp:Label ID="lblAlocatedMemory" runat="server" EnableViewState="false" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblValueAlocatedMemory" runat="server" EnableViewState="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        &nbsp;
                                    </td>
                                </tr>
                                <asp:PlaceHolder runat="server" ID="plcAdvanced" EnableViewState="false">
                                    <tr>
                                        <td style="white-space: nowrap;">
                                            <asp:Label ID="lblPeakMemory" runat="server" EnableViewState="false" />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblValuePeakMemory" runat="server" EnableViewState="false" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="white-space: nowrap;">
                                            <asp:Label ID="lblPhysicalMemory" runat="server" EnableViewState="false" />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblValuePhysicalMemory" runat="server" EnableViewState="false" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="white-space: nowrap;">
                                            <asp:Label ID="lblVirtualMemory" runat="server" EnableViewState="false" />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblValueVirtualMemory" runat="server" EnableViewState="false" />
                                        </td>
                                    </tr>
                                </asp:PlaceHolder>
                                <tr>
                                    <td colspan="2">
                                        <br />
                                        <cms:CMSButton ID="btnClear" runat="server" CssClass="LongButton" OnClick="btnClear_Click"
                                            EnableViewState="false" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                    <td style="vertical-align: top;">
                        <asp:Panel ID="pnlGC" runat="server" Width="425" CssClass="MediumSystemPanel">
                            <table>
                                <asp:PlaceHolder runat="server" ID="plcGC" EnableViewState="false"></asp:PlaceHolder>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align: top;">
                        <asp:Panel ID="pnlPageViews" runat="server" Width="425" CssClass="LargeSystemPanel">
                            <table>
                                <tr>
                                    <td style="width: 180px;">
                                    </td>
                                    <td>
                                        <strong>
                                            <asp:Label ID="lblPageViewsValues" runat="server" EnableViewState="false" /></strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="white-space: nowrap;">
                                        <asp:Label ID="lblPages" runat="server" EnableViewState="false" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblValuePages" runat="server" EnableViewState="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="white-space: nowrap;">
                                        <asp:Label ID="lblGetFilePages" runat="server" EnableViewState="false" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblValueGetFilePages" runat="server" EnableViewState="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="white-space: nowrap;">
                                        <asp:Label ID="lblSystemPages" runat="server" EnableViewState="false" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblValueSystemPages" runat="server" EnableViewState="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="white-space: nowrap;">
                                        <asp:Label ID="lblNonPages" runat="server" EnableViewState="false" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblValueNonPages" runat="server" EnableViewState="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="white-space: nowrap;">
                                        <asp:Label ID="lblPagesNotFound" runat="server" EnableViewState="false" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblValuePagesNotFound" runat="server" EnableViewState="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="white-space: nowrap;">
                                        <asp:Label ID="lblPending" runat="server" EnableViewState="false" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblValuePending" runat="server" EnableViewState="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <br />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                    <td style="vertical-align: top;">
                        <asp:Panel ID="pnlCache" runat="server" Width="425" CssClass="LargeSystemPanel">
                            <table>
                                <tr>
                                    <td style="white-space: nowrap;">
                                        <asp:Label ID="lblCacheItems" runat="server" EnableViewState="false" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCacheItemsValue" runat="server" EnableViewState="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="white-space: nowrap;">
                                        <asp:Label ID="lblCacheExpired" runat="server" EnableViewState="false" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCacheExpiredValue" runat="server" EnableViewState="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="white-space: nowrap;">
                                        <asp:Label ID="lblCacheRemoved" runat="server" EnableViewState="false" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCacheRemovedValue" runat="server" EnableViewState="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="white-space: nowrap;">
                                        <asp:Label ID="lblCacheDependency" runat="server" EnableViewState="false" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCacheDependencyValue" runat="server" EnableViewState="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="white-space: nowrap;">
                                        <asp:Label ID="lblCacheUnderused" runat="server" EnableViewState="false" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCacheUnderusedValue" runat="server" EnableViewState="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <br />
                                        <cms:CMSButton ID="btnClearCache" runat="server" CssClass="LongButton" OnClick="btnClearCache_Click"
                                            EnableViewState="false" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="vertical-align: top;">
                        <asp:Panel ID="pnlSystemTime" runat="server" Width="425" CssClass="SmallSystemPanel">
                            <table>
                                <tr>
                                    <td style="white-space: nowrap; width: 180px;">
                                        <asp:Label ID="lblRunTime" runat="server" EnableViewState="false" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblRunTimeValue" runat="server" EnableViewState="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="white-space: nowrap;">
                                        <asp:Label ID="lblServerTime" runat="server" EnableViewState="false" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblServerTimeValue" runat="server" EnableViewState="false" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </cms:CMSUpdatePanel>
    <br />
    &nbsp;<cms:CMSButton ID="btnRestart" runat="server" CssClass="LongSubmitButton" OnClick="btnRestart_Click"
        EnableViewState="false" /><cms:CMSButton ID="btnRestartWebfarm" runat="server" CssClass="XLongSubmitButton"
            OnClick="btnRestartWebfarm_Click" /><cms:CMSButton ID="btnRestartServices" runat="server"
                CssClass="XLongSubmitButton" OnClick="btnRestartServices_Click" EnableViewState="false" /><cms:CMSButton
                    ID="btnClearCounters" runat="server" CssClass="LongSubmitButton" OnClick="btnClearCounters_Click"
                    EnableViewState="false" /><cms:CMSButton ID="btnHidden" runat="server" Visible="false"
                        EnableViewState="false" />

    <script type="text/javascript">
        //<![CDATA[
        function alert() {
        }
        //]]>
    </script>

</asp:Panel>
