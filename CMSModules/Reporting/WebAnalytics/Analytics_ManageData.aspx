<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Reporting_WebAnalytics_Analytics_ManageData"
    MasterPageFile="~/CMSMasterPages/UI/Dialogs/ModalDialogPage.master" Title="Analytics - Delete data"
    Theme="Default" CodeFile="Analytics_ManageData.aspx.cs" %>

<%@ Register Src="~/CMSModules/WebAnalytics/FormControls/SelectCampaign.ascx" TagName="Campaigns"
    TagPrefix="cms" %>
<asp:Content ID="cntBody" ContentPlaceHolderID="plcContent" runat="server">
    <cms:CMSUpdatePanel ID="pnlUpdate" runat="server">
        <ContentTemplate>
            <asp:Timer ID="timeRefresh" runat="server" Interval="2000" EnableViewState="false"
                Enabled="false" />
            <div class="PageContent">
                <asp:Literal runat="server" ID="ltrProgress" EnableViewState="false" />
                <asp:Label ID="lblError" runat="server" CssClass="ErrorLabel" Visible="false" EnableViewState="false" />
                <div style="height: 120px; width: 100%; overflow: auto;">
                    <table>
                        <asp:PlaceHolder runat="server" ID="pnlABTestSelector" Visible="false">
                            <tr>
                                <td>
                                    <cms:LocalizedLabel ID="lblTests" runat="server" ResourceString="om.abtest" DisplayColon="true" />
                                </td>
                                <td>
                                    <asp:PlaceHolder runat="server" ID="pnlABTests" />
                                </td>
                            </tr>
                        </asp:PlaceHolder>
                        <asp:PlaceHolder runat="server" ID="pnlMVTSelector" Visible="false">
                            <tr>
                                <td>
                                    <cms:LocalizedLabel ID="lblMVTests" runat="server" ResourceString="mvtest" DisplayColon="true" />
                                </td>
                                <td>
                                    <asp:PlaceHolder runat="server" ID="pnlMVTests" />
                                </td>
                            </tr>
                        </asp:PlaceHolder>
                        <asp:PlaceHolder runat="server" ID="pnlCampaigns" Visible="false">
                            <tr>
                                <td>
                                    <cms:LocalizedLabel ID="lblCampaign" runat="server" ResourceString="campaign.campaign.list"
                                        DisplayColon="true" />
                                </td>
                                <td>
                                    <cms:Campaigns runat="server" ID="usCampaigns" />
                                </td>
                            </tr>
                        </asp:PlaceHolder>
                        <tr>
                            <td>
                                <cms:LocalizedLabel ID="lblFrom" runat="server" EnableViewState="false" ResourceString="AnalyticsManageData.FromDate" />
                            </td>
                            <td>
                                <cms:DateTimePicker ID="pickerFrom" runat="server" EditTime="false" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <cms:LocalizedLabel ID="lblTo" runat="server" EnableViewState="false" ResourceString="AnalyticsManageData.ToDate" />
                            </td>
                            <td>
                                <cms:DateTimePicker ID="pickerTo" runat="server" EditTime="false" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </ContentTemplate>
    </cms:CMSUpdatePanel>
</asp:Content>
<asp:Content ID="cntFooter" runat="server" ContentPlaceHolderID="plcFooter">
    <div class="FloatRight">
        <cms:LocalizedButton ID="btnDelete" runat="server" OnClick="btnDelete_Click" CssClass="SubmitButton"
            EnableViewState="false" ResourceString="general.delete" />
        <cms:LocalizedButton ID="btnCancel" runat="server" OnClientClick="window.close(); return false;"
            CssClass="SubmitButton" EnableViewState="false" ResourceString="general.cancel" />
    </div>
</asp:Content>
