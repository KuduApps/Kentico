<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConversionsCount.aspx.cs"
    EnableEventValidation="false" Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master"
    Title="AB test conversions count" Inherits="CMSModules_OnlineMarketing_Pages_Tools_ABTest_ConversionsCount" %>

<%@ Register Src="~/CMSModules/OnlineMarketing/Controls/UI/ABTest/ConversionReportViewer.ascx"
    TagPrefix="cms" TagName="ConversionReportViewer" %>
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
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <cms:ConversionReportViewer runat="server" ID="ucConversionReportViewer" />
</asp:Content>
