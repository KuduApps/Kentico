<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CampaignGoals.aspx.cs" Inherits="CMSModules_WebAnalytics_Pages_Tools_Campaign_CampaignGoals"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Campaign goals"
    EnableEventValidation="false" Theme="Default" %>

<%@ Register Src="~/CMSModules/WebAnalytics/Controls/SelectGraphTypeAndPeriod.ascx"
    TagName="GraphType" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/WebAnalytics/Controls/GraphPreLoader.ascx" TagName="GraphPreLoader"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/WebAnalytics/FormControls/SelectCampaign.ascx" TagName="SelectCampaign"
    TagPrefix="cms" %>
<asp:Content ID="cntHeader" runat="server" ContentPlaceHolderID="plcBeforeContent">
    <asp:Panel runat="server" ID="pnlDisabled" CssClass="PageHeaderLine" Visible="false">
        <asp:Label runat="server" ID="lblDisabled" EnableViewState="false" />
    </asp:Panel>
    <asp:Panel ID="pnlActions" runat="server" CssClass="PageHeaderLine" EnableViewState="false">
        <table cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <asp:LinkButton ID="lnkSave" runat="server" OnClick="lnkSave_Click" CssClass="WebAnalitycsMenuItemEdit"
                        EnableViewState="false">
                        <asp:Image ID="imgSave" runat="server" EnableViewState="false" CssClass="WebAnalitycsMenuItemImage" />
                        <%=mSave%>
                    </asp:LinkButton>
                </td>
                <td>
                    <asp:LinkButton ID="lnkPrint" runat="server" CssClass="WebAnalitycsMenuItemEdit"
                        EnableViewState="false">
                        <asp:Image ID="imgPrint" runat="server" EnableViewState="false" CssClass="WebAnalitycsMenuItemImage" />
                        <%=mPrint%>
                    </asp:LinkButton>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <cms:GraphPreLoader runat="server" ID="ucGraphPreLoader" />
    <div class="PageHeaderLine">
        <cms:GraphType runat="server" ID="ucGraphType" />
    </div>
    <div class="ReportBody">
        <asp:Label ID="lblInfo" runat="server" CssClass="InfoLabel" EnableViewState="false"
            Visible="false" />
        <table>
            <td>
                <cms:LocalizedLabel ID="lblCampaign" runat="server" ResourceString="analytics.campaign"
                    DisplayColon="true" />
            </td>
            <td>
                <cms:SelectCampaign runat="server" ID="ucSelectCampaign" AllowAll="true" PostbackOnChange="true"
                    CssClass="CampaignReportSelctor" />
            </td>
        </table>
        <br />
        <asp:Label CssClass="ErrorLabel" ID="lblError" runat="server" EnableViewState="false"
            Visible="false" />
        <asp:PlaceHolder runat="server" ID="pnlDisplayReport"></asp:PlaceHolder>
    </div>
</asp:Content>
