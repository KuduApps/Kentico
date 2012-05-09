<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CampaignReportHeader.ascx.cs"
    Inherits="CMSModules_WebAnalytics_Controls_CampaignReportHeader" %>
<%@ Register Src="~/CMSModules/WebAnalytics/FormControls/SelectCampaign.ascx" TagName="SelectCampaign"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/WebAnalytics/FormControls/SelectConversion.ascx" TagName="SelectConversion"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSFormControls/Sites/SiteSelector.ascx" TagName="SelectSite"
    TagPrefix="cms" %>
<table>
    <tr>
        <asp:PlaceHolder runat="server" ID="pnlCampaign">
            <td>
                <cms:LocalizedLabel ID="lblCampaign" runat="server" ResourceString="analytics.campaign"
                    DisplayColon="true" />
            </td>
            <td>
                <cms:SelectCampaign runat="server" ID="ucSelectCampaign" AllowAll="true" PostbackOnChange="true"
                    CssClass="CampaignReportSelector" />
            </td>
        </asp:PlaceHolder>
        <asp:PlaceHolder runat="server" ID="pnlConversion">
            <td>
                <cms:LocalizedLabel ID="lblConversion" runat="server" ResourceString="analytics.conversion"
                    DisplayColon="true" />
            </td>
            <td>
                <cms:SelectConversion runat="server" ID="usSelectConversion" PostbackOnDropDownChange="true"
                    CssClass="CampaignReportSelector" />
            </td>
        </asp:PlaceHolder>
        <asp:PlaceHolder runat="server" ID="pnlSite" Visible="false">
            <td>
                <cms:LocalizedLabel ID="lblSelectSite" runat="server" ResourceString="general.site"
                    DisplayColon="true" />
            </td>
            <td>
                <!-- Firefox 5 issue  -->
                <div class="CampaignReportSelector">
                    <cms:SelectSite runat="server" ID="usSite" IsLiveSite="false" PostbackOnDropDownChange="true" />
                </div>
            </td>
        </asp:PlaceHolder>
        <asp:PlaceHolder runat="server" ID="pnlGoal" Visible="false">
            <td>
                <cms:LocalizedLabel ID="lblGoal" runat="server" ResourceString="campaign.goal" DisplayColon="true" />
            </td>
            <td>
                <asp:DropDownList runat="server" ID="drpGoals" />
            </td>
        </asp:PlaceHolder>
    </tr>
</table>
