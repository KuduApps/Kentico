<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Header.aspx.cs" Theme="Default"
    Inherits="CMSAPIExamples_Pages_Header" %>

<%@ Register Src="~/CMSAdminControls/UI/PageElements/PageTitle.ascx" TagName="PageTitle"
    TagPrefix="cms" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>API examples - header</title>
    <style type="text/css">
        html, body
        {
            margin: 0px;
            padding: 0px;
            height: 100%;
            overflow: hidden;
        }
    </style>
</head>
<body class="<%=mBodyClass%>">
    <form id="form1" runat="server">
    <div class="SM_Header">
        <cms:PageTitle ID="titleElem" runat="server" EnableViewState="false" Visible="false" />
        <asp:HyperLink runat="server" ID="lnkApiExampleLogo" CssClass="ExamplesHeaderLeft"
            EnableViewState="false" Font-Underline="false">
            &nbsp;
        </asp:HyperLink>
        <asp:Panel runat="server" ID="PanelRight" CssClass="HeaderRight" EnableViewState="false">
            <table cellpadding="0" cellspacing="0" class="RightAlign">
                <tr>
                    <td style="padding: 0px 5px">
                        <asp:HyperLink ID="lnkCmsDesk" CssClass="HeaderLink" runat="server" />
                    </td>
                    <td style="padding: 0px 5px">
                        <asp:HyperLink ID="lnkSiteManager" CssClass="HeaderLink" runat="server" />
                    </td>
                    <td style="padding: 0px 5px">
                        <asp:Label ID="lblUser" runat="server" CssClass="HeaderUser" />
                    </td>
                    <td>
                        <asp:Label ID="lblUserInfo" runat="server" CssClass="HeaderUserInfo" />
                    </td>
                    <td style="padding: 0px 10px;">
                        <asp:Label runat="server" ID="lblVersion" EnableViewState="false" CssClass="HeaderVersion" />
                    </td>
                    <td>
                        <asp:Panel runat="server" ID="pnlSignOut" CssClass="HeaderSignOutPnl" EnableViewState="false">
                            <asp:LinkButton runat="server" ID="lnkSignOut" OnClick="btnSignOut_Click" Font-Underline="false"
                                EnableViewState="false">
                                <asp:Label runat="server" ID="lblSignOut" EnableViewState="false" CssClass="HeaderSignOut" />
                            </asp:LinkButton>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
            <asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />
        </asp:Panel>
        <asp:Panel runat="server" ID="PanelSeparator" CssClass="SM_HeaderContentSeparator"
            EnableViewState="false">
            &nbsp;
        </asp:Panel>
    </div>
    </form>
</body>
</html>
