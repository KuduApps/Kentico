<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MVTReport.aspx.cs" Inherits="CMSModules_OnlineMarketing_Pages_Tools_MVTest_MVTReport"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="MV test overview"
    EnableEventValidation="false" Theme="Default" %>

<%@ Register Src="~/CMSModules/WebAnalytics/FormControls/SelectConversion.ascx" TagName="SelectConversion"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/WebAnalytics/Controls/SelectGraphTypeAndPeriod.ascx"
    TagName="GraphType" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/OnlineMarketing/FormControls/SelectMVTest.ascx" TagName="MVTests"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/WebAnalytics/Controls/GraphPreLoader.ascx" TagName="GraphPreLoader"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/OnlineMarketing/FormControls/SelectMVTCombination.ascx"
    TagName="SelectCombinaton" TagPrefix="cms" %>
<%@ Register Src="~/CMSFormControls/Cultures/SiteCultureSelector.ascx" TagName="SelectCulture"
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
        <asp:Label runat="server" ID="lblMVTestingDisabled" EnableViewState="false" />
    </asp:Panel>
</asp:Content>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <div class="PageHeaderLine">
        <cms:GraphPreLoader runat="server" ID="ucGraphPreLoader" />
        <cms:GraphType runat="server" ID="ucGraphType" />
    </div>
    <div class="ReportBody">
        <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" Visible="false" EnableViewState="false" />
        <table>
            <tr>
                <td>
                    <cms:LocalizedLabel ID="lblTests" runat="server" ResourceString="mvtest" DisplayColon="true" />
                </td>
                <td>
                    <cms:MVTests runat="server" ID="ucMVTests" />
                </td>
                <asp:PlaceHolder runat="server" ID="pnlConversion">
                    <td>
                        &nbsp;&nbsp;
                        <cms:LocalizedLabel ID="lblConversions" runat="server" ResourceString="abtesting.conversions"
                            DisplayColon="true" />
                    </td>
                    <td>
                        <cms:SelectConversion runat="server" ID="ucConversions" SelectionMode="SingleDropDownList" />
                    </td>
                </asp:PlaceHolder>
                <asp:PlaceHolder runat="server" ID="pnlCultures">
                    <td>
                        &nbsp;&nbsp;
                        <cms:LocalizedLabel ID="lblCulture" runat="server" ResourceString="general.culture"
                            DisplayColon="true" />
                    </td>
                    <td>
                        <cms:SelectCulture runat="server" ID="usCulture" IsLiveSite="false" AddDefaultRecord="false"
                            AddAllRecord="true" PostbackOnChange="true" />
                    </td>
                </asp:PlaceHolder>
                <asp:PlaceHolder runat="server" ID="pnlCombination">
                    <td>
                        &nbsp;&nbsp;
                        <cms:LocalizedLabel ID="lblCombination" runat="server" ResourceString="mvt.combination"
                            DisplayColon="true" />
                    </td>
                    <td>
                        <cms:SelectCombinaton runat="server" ID="usCombination" PostbackOnChange="true" AllowAll="true"
                            AllowEmpty="false" ReturnColumnName="MVTCombinationName" />
                    </td>
                </asp:PlaceHolder>
            </tr>
        </table>
        <br />
        <asp:PlaceHolder runat="server" ID="pnlDisplayReport">            
        </asp:PlaceHolder>
        <asp:Label CssClass="ErrorLabel" ID="lblErrorConversions" runat="server" EnableViewState="false"
            Visible="false" />
    </div>
</asp:Content>
