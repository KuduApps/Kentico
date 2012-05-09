<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Reporting_Tools_Report_View"
    Theme="Default" EnableEventValidation="false" MaintainScrollPositionOnPostback="true"
    CodeFile="Report_View.aspx.cs" %>

<%@ Register Src="~/CMSModules/Reporting/Controls/DisplayReport.ascx" TagName="DisplayReport"
    TagPrefix="cms" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Report View</title>
    <style type="text/css">
        body
        {
            margin: 0px;
            padding: 0px;
            height: 100%;
            width: 100%;
        }
    </style>
</head>
<body class="<%=mBodyClass%>">
    <form id="form1" runat="server">
    <asp:PlaceHolder runat="server" ID="scriptManager" />
    <asp:Panel runat="server" ID="pnlMenu" class="TabsEditMenu">
        <table>
            <tr>
                <td>
                    <asp:LinkButton ID="lnkSave" runat="server" OnClick="lnkSave_Click" CssClass="MenuItemEdit"
                        EnableViewState="false">
                        <asp:Image ID="imgSave" runat="server" EnableViewState="false" />
                        <%=mSave%>
                    </asp:LinkButton>
                    <asp:LinkButton ID="lnkSendTo" runat="server" OnClick="lnkSend_Click" CssClass="MenuItemEdit"
                        Visible="false" EnableViewState="false">
                        <asp:Image ID="imgSendTo" runat="server" EnableViewState="false" />
                        <%=mSendToEmail%>
                    </asp:LinkButton>
                </td>
                <td>
                    <asp:LinkButton ID="btnPrint" runat="server" CssClass="MenuItemEdit" EnableViewState="false">
                        <asp:Image ID="imgPrint" runat="server" EnableViewState="false" />
                        <%=mPrint%>
                    </asp:LinkButton>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnlUsers" runat="server" CssClass="PageContent">
        <asp:Label runat="server" ID="lblInfo" EnableViewState="false" Visible="false" />
        <cms:DisplayReport ID="DisplayReport1" runat="server" FormCssClass="ReportFilter"
            BodyCssClass="DisplayReportBody" IsLiveSite="false" RenderCssClasses="true" />
        <asp:Literal runat="server" ID="ltlModal" EnableViewState="false" />
    </asp:Panel>
    </form>
</body>
</html>
