<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OverView.aspx.cs" Inherits="CMSModules_OnlineMarketing_Pages_Tools_AbTest_OverView"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="AB test overview"
    Theme="Default" %>

<%@ Register Src="~/CMSModules/OnlineMarketing/Controls/UI/AbTest/List.ascx" TagName="AbTestList"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/WebAnalytics/Controls/SelectGraphTypeAndPeriod.ascx"
    TagName="GraphType" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/OnlineMarketing/FormControls/SelectABTest.ascx" TagName="ABTests"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/WebAnalytics/Controls/GraphPreLoader.ascx" TagName="GraphPreLoader"
    TagPrefix="cms" %>
<asp:Content ID="cntHeader" runat="server" ContentPlaceHolderID="plcBeforeContent">
    <asp:Panel runat="server" ID="pnlDisabled" CssClass="PageHeaderLine" Visible="false">
        <asp:Label runat="server" ID="lblDisabled" EnableViewState="false" />
        <asp:Label runat="server" ID="lblABTestingDisabled" EnableViewState="false" />
    </asp:Panel>
</asp:Content>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <div class="PageHeaderLine">
        <cms:GraphPreLoader runat="server" ID="ucGraphPreLoader" />
        <cms:GraphType runat="server" ID="ucGraphType" />
    </div>
    <div class="ReportBody">
        <table>
            <tr>
                <td>
                    <cms:LocalizedLabel runat="server" ResourceString="om.abtest" DisplayColon="true" />
                </td>
                <td>
                    <cms:ABTests runat="server" ID="ucABTests" />
                </td>
            </tr>
        </table>
        <br />
        <asp:PlaceHolder runat="server" ID="pnlOverview"></asp:PlaceHolder>
        <asp:Label CssClass="ErrorLabel" ID="lblErrorConversions" runat="server" EnableViewState="false"
            Visible="false" />
        <div class="ABTestOverViewList">
            <cms:AbTestList ID="listElem" runat="server" IsLiveSite="false" HideDeleteAction="true" />
        </div>
        <table style="width: 100%">
            <tr>
                <td style="text-align: center; vertical-align: top;">
                    <asp:PlaceHolder runat="server" ID="pnlConversionRate"></asp:PlaceHolder>
                    <asp:Label CssClass="ErrorLabel" ID="lblErrorRate" runat="server" EnableViewState="false"
                        Visible="false" />
                </td>
                <td style="text-align: center;">
                    <asp:PlaceHolder runat="server" ID="pnlConversionValue"></asp:PlaceHolder>
                    <asp:Label CssClass="ErrorLabel" ID="lblErrorValues" runat="server" EnableViewState="false"
                        Visible="false" />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                    <cms:LocalizedHyperlink runat="server" ID="lnkTestRate" ResourceString="om.viewfullreport"
                        CssClass="TestGraphLink" />
                </td>
                <td>
                    &nbsp;
                    <cms:LocalizedHyperlink ID="lnkTestValue" runat="server" ResourceString="om.viewfullreport"
                        CssClass="TestGraphLink" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
