<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ABTestReports.aspx.cs" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master"
    Title="AB test reports" Inherits="CMSModules_OnlineMarketing_Pages_Content_ABTesting_ABTest_ABTestReports"
    EnableEventValidation="false" Theme="Default" %>

<%@ Register Src="~/CMSModules/WebAnalytics/Controls/SelectGraphTypeAndPeriod.ascx"
    TagName="GraphType" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/WebAnalytics/Controls/GraphPreLoader.ascx" TagName="GraphPreLoader"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/WebAnalytics/FormControls/SelectConversion.ascx" TagName="SelectConversion"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/OnlineMarketing/FormControls/SelectVariation.ascx"
    TagPrefix="cms" TagName="VariationsSelector" %>
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
    <div class="PageHeaderLine" id="pnlRadios" runat="server">
        <cms:LocalizedRadioButton runat="server" ID="rbCount" ResourceString="conversion.count"
            AutoPostBack="true" CssClass="PageReportRadioButton" GroupName="Radio" />
        <cms:LocalizedRadioButton runat="server" ID="rbValue" ResourceString="conversions.value"
            AutoPostBack="true" CssClass="PageReportRadioButton" GroupName="Radio" />
        <cms:LocalizedRadioButton runat="server" ID="rbRate" ResourceString="abtesting.conversionrate"
            AutoPostBack="true" CssClass="PageReportRadioButton" GroupName="Radio" />
        <cms:LocalizedRadioButton runat="server" ID="rbSourcePages" ResourceString="abtesting.conversionssourcepages"
            AutoPostBack="true" CssClass="PageReportRadioButton" GroupName="Radio" />
        <cms:LocalizedRadioButton runat="server" ID="rbVariants" ResourceString="abtesting.conversionsbyvariants"
            AutoPostBack="true" CssClass="PageReportRadioButton" GroupName="Radio" />
    </div>
    <asp:Panel runat="server" ID="pnlWarning" Visible="false" CssClass="PageHeaderLine">
        <asp:Label runat="server" ID="lblWAWarning" />
        <asp:Label runat="server" ID="lblABWarning" />
    </asp:Panel>
    <div class="ReportBody">
        <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" Visible="false" EnableViewState="false" />
        <table>
            <tr>
                <td>
                    <cms:LocalizedLabel ID="lblConversions" runat="server" ResourceString="abtesting.conversions"
                        DisplayColon="true" />
                </td>
                <td>
                    <cms:SelectConversion runat="server" ID="ucConversions" SelectionMode="SingleDropDownList" />
                </td>
                <asp:PlaceHolder runat="server" ID="pnlVariant" Visible="false">
                    <td>
                        &nbsp;&nbsp;
                        <cms:LocalizedLabel ID="lblVariants" runat="server" ResourceString="abtesting.variants"
                            DisplayColon="true" />
                    </td>
                    <td>
                        <cms:VariationsSelector ID="ucSelectVariation" runat="server" />
                    </td>
                </asp:PlaceHolder>
            </tr>
        </table>
        <asp:Panel runat="server" ID="pnlContent">
        </asp:Panel>
    </div>
</asp:Content>
