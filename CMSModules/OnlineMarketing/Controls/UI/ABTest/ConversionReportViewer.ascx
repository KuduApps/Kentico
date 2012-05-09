<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ConversionReportViewer.ascx.cs"
    Inherits="CMSModules_OnlineMarketing_Controls_UI_ABTest_ConversionReportViewer" %>
<%@ Register Src="~/CMSModules/WebAnalytics/FormControls/SelectConversion.ascx" TagName="SelectConversion"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/WebAnalytics/Controls/SelectGraphTypeAndPeriod.ascx"
    TagName="GraphType" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/OnlineMarketing/FormControls/SelectABTest.ascx" TagName="ABTests"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/WebAnalytics/Controls/GraphPreLoader.ascx" TagName="GraphPreLoader"
    TagPrefix="cms" %>
<cms:GraphPreLoader runat="server" ID="ucGraphPreLoader" />
<div class="PageHeaderLine">
    <cms:GraphType runat="server" ID="ucGraphType" />
</div>
<div class="ReportBody">
    <asp:Label ID="lblInfo" runat="server" Visible="false" EnableViewState="false" CssClass="InfoLabel" />
    <table>
        <tr>
            <td>
                <cms:LocalizedLabel ID="lblTests" runat="server" ResourceString="om.abtest" DisplayColon="true" />
            </td>
            <td>
                <cms:ABTests runat="server" ID="ucABTests" />
            </td>
            <td>
                &nbsp;&nbsp;
                <cms:LocalizedLabel ID="lblConversions" runat="server" ResourceString="abtesting.conversions"
                    DisplayColon="true" />
            </td>
            <td>
                <cms:SelectConversion runat="server" ID="ucConversions" SelectionMode="SingleDropDownList" />
            </td>
        </tr>
    </table>
    <br />
    <asp:PlaceHolder runat="server" ID="pnlDisplayReport"></asp:PlaceHolder>
    <asp:Label CssClass="ErrorLabel" ID="lblErrorConversions" runat="server" EnableViewState="false"
        Visible="false" />
</div>
