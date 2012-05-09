<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Reporting_Tools_SavedReports_SavedReport_View" Theme="Default" CodeFile="SavedReport_View.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/PageElements/PageTitle.ascx" TagName="PageTitle"
    TagPrefix="cms" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>SavedReport View</title>
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
    <asp:Literal runat="server" ID="ltlScript" EnableViewState="false" />
    <asp:Panel runat="server" ID="pnlHeader" CssClass="PageHeader SimpleHeader">
        <cms:PageTitle runat="server" ID="PageTitle" />
    </asp:Panel>
    <asp:Panel ID="pnlPrint" runat="server" CssClass="TabsEditMenu">
        <table>
            <tr>
                <td>
                    <asp:LinkButton ID="btnPrint" runat="server" CssClass="MenuItemEdit" EnableViewState="false">
                        <asp:Image ID="imgPrint" runat="server" EnableViewState="false" />
                        <%=mPrint%>
                    </asp:LinkButton>
                </td>
                <td>
                </td>
                <td>
                    <asp:LinkButton ID="btnSendToEMail" runat="server" CssClass="MenuItemEdit" EnableViewState="false"
                        Visible="false">
                        <asp:Image ID="imgSendTo" runat="server" EnableViewState="false" />
                        <%=mSendToEmail%>
                    </asp:LinkButton>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnlUsers" runat="server" CssClass="PageContent">
        <asp:Literal runat="server" ID="ltlHtml" EnableViewState="false" />
        <asp:Literal runat="server" ID="ltlModal" EnableViewState="false" />
    </asp:Panel>
    </form>
</body>
</html>
