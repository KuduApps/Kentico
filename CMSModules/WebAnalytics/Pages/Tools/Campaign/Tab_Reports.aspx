<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Tab_Reports.aspx.cs" Inherits="CMSModules_WebAnalytics_Pages_Tools_Campaign_Tab_Reports"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Campaign reports"
    EnableEventValidation="false" Theme="Default" %>

<%@ Register Src="~/CMSModules/WebAnalytics/Controls/SelectGraphTypeAndPeriod.ascx"
    TagName="GraphType" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/WebAnalytics/Controls/GraphPreLoader.ascx" TagName="GraphPreLoader"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/WebAnalytics/FormControls/SelectConversion.ascx" TagName="SelectConversion"
    TagPrefix="cms" %>
<asp:Content ID="cntHeader" runat="server" ContentPlaceHolderID="plcBeforeContent">
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
                <td>
                    <asp:LinkButton ID="lnkDeleteData" runat="server" CssClass="WebAnalitycsMenuItemEdit"
                        Visible="false" EnableViewState="false">
                        <asp:Image ID="imgManageData" runat="server" EnableViewState="false" CssClass="WebAnalitycsMenuItemImage" />
                        <%=mDeleteData%>
                    </asp:LinkButton>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="plcContent">
    <div class="PageHeaderLine">
        <cms:GraphPreLoader runat="server" ID="ucGraphPreLoader" />
        <cms:GraphType runat="server" ID="ucGraphType" />
    </div>
    <div class="PageHeaderLine" runat="server" id="pnlRadios">
        <table>
            <tr>
                <td>
                    <cms:LocalizedRadioButton runat="server" ID="rbViews" ResourceString="reports_campaign.label_num"
                        AutoPostBack="true" CssClass="PageReportRadioButton" GroupName="Radio" />
                </td>
                <td>
                    <cms:LocalizedRadioButton runat="server" ID="rbCount" ResourceString="conversion.count"
                        AutoPostBack="true" CssClass="PageReportRadioButton" GroupName="Radio" />
                </td>
                <td>
                    <cms:LocalizedRadioButton runat="server" ID="rbValue" ResourceString="conversions.value"
                        AutoPostBack="true" CssClass="PageReportRadioButton" GroupName="Radio" />
                </td>
                <td>
                    <cms:LocalizedRadioButton runat="server" ID="rbDetail" ResourceString="campaign.detail"
                        AutoPostBack="true" CssClass="PageReportRadioButton" GroupName="Radio" />
                </td>
            </tr>
            <tr>
                <td>
                    <cms:LocalizedRadioButton runat="server" ID="rbGoalView" ResourceString="campaign.goals.visitors.title"
                        AutoPostBack="true" CssClass="PageReportRadioButton" GroupName="Radio" />
                </td>
                <td>
                    <cms:LocalizedRadioButton runat="server" ID="rbGoalCount" ResourceString="campaign.goals.count.title"
                        AutoPostBack="true" CssClass="PageReportRadioButton" GroupName="Radio" />
                </td>
                <td>
                    <cms:LocalizedRadioButton runat="server" ID="rbGoalValue" ResourceString="campaign.goals.value.title"
                        AutoPostBack="true" CssClass="PageReportRadioButton" GroupName="Radio" />
                </td>
                <td>
                    <cms:LocalizedRadioButton runat="server" ID="rbValuePerVisitor" ResourceString="campaign.goals.valuepervisitor.title"
                        AutoPostBack="true" CssClass="PageReportRadioButton" GroupName="Radio" />
                </td>
            </tr>
        </table>
    </div>
    <div class="ReportBody">
        <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" Visible="false" EnableViewState="false" />
        <asp:PlaceHolder runat="server" ID="pnlConversions">
            <table>
                <tr>
                    <td>
                        <cms:LocalizedLabel ID="lblConversions" runat="server" ResourceString="abtesting.conversions"
                            DisplayColon="true" />
                    </td>
                    <td>
                        <cms:SelectConversion runat="server" ID="ucConversions" SelectionMode="SingleDropDownList" />
                    </td>
                </tr>
            </table>
        </asp:PlaceHolder>
        <asp:Panel runat="server" ID="pnlContent">
        </asp:Panel>
    </div>
</asp:Content>
