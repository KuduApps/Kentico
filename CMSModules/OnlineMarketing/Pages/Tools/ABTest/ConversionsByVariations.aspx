<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConversionsByVariations.aspx.cs"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="A/B test variations source pages"
    Theme="Default" Inherits="CMSModules_OnlineMarketing_Pages_Tools_ABTest_ConversionsByVariations"
    EnableEventValidation="false" %>

<%@ Register Src="~/CMSModules/OnlineMarketing/FormControls/SelectVariation.ascx"
    TagPrefix="cms" TagName="VariationsSelector" %>
<%@ Register Src="~/CMSModules/WebAnalytics/Controls/SelectGraphTypeAndPeriod.ascx"
    TagName="GraphType" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/OnlineMarketing/FormControls/SelectABTest.ascx" TagName="ABTests"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/WebAnalytics/Controls/GraphPreLoader.ascx" TagName="GraphPreLoader"
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
    <asp:Panel runat="server" ID="pnlDisabled" CssClass="PageHeaderLine" Visible="false">
        <asp:Label runat="server" ID="lblDisabled" EnableViewState="false" />
        <asp:Label runat="server" ID="lblABTestingDisabled" EnableViewState="false" />
    </asp:Panel>
</asp:Content>
<asp:Content runat="server" ID="cntBody" ContentPlaceHolderID="plcContent">
    <div class="PageHeaderLine">
        <cms:GraphPreLoader runat="server" ID="ucGraphPreLoader" />
        <cms:GraphType runat="server" ID="ucGraphType" />
    </div>
    <div class="ReportBody">
        <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" Visible="false" EnableViewState="false" />
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
                    <cms:LocalizedLabel ID="lblConversions" runat="server" ResourceString="abtesting.variants"
                        DisplayColon="true" />
                </td>
                <td>
                    <cms:VariationsSelector ID="ucSelectVariation" runat="server"  />
                </td>
            </tr>
        </table>
        <br />
        <asp:PlaceHolder runat="server" ID="pnlDisplayReport"></asp:PlaceHolder>
        <asp:Label CssClass="ErrorLabel" ID="lblErrorConversions" runat="server" EnableViewState="false"
            Visible="false" />
    </div>
</asp:Content>
