<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Reporting_Tools_Ecommerce_Reports"
    Theme="Default" CodeFile="Reports.aspx.cs" Title="E-commerce - Reports" EnableEventValidation="false" %>

<%@ Register Src="~/CMSModules/WebAnalytics/Controls/GraphPreLoader.ascx" TagName="GraphPreLoader"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/WebAnalytics/Controls/AnalyticsReportViewer.ascx"
    TagPrefix="cms" TagName="ReportViewer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head id="head" runat="server" enableviewstate="false">
    <style type="text/css">
        body
        {
            margin: 0px;
            padding: 0px;
            height: 100%;
        }
    </style>
</head>
<body class="<%=mBodyClass%>">
    <form runat="server" id="frm">
    <asp:Panel runat="server" ID="pnlTitle" CssClass="PageTitleHeader">
        <table cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <asp:Image runat="server" ID="imgTitleImage" CssClass="PageTitleImage" EnableViewState="false" />
                    <cms:LocalizedLabel runat="server" ID="lblTitle" CssClass="PageTitle" EnableViewState="false" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlMenu" CssClass="PageHeaderLine">
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
    <asp:Panel runat="server" ID="pnlBody" >
        <asp:PlaceHolder runat="server" ID="plcContainerManager" />
        <cms:ReportViewer runat="server" ID="ucReportViewer" />
        <cms:GraphPreLoader runat="server" ID="ucGraphPreLoader" />
        <asp:Literal runat="server" ID="ltlModal" EnableViewState="false" />
    </asp:Panel>
    </form>
</body>
</html>
