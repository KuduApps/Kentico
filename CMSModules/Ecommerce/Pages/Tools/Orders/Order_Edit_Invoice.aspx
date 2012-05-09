<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Ecommerce_Pages_Tools_Orders_Order_Edit_Invoice" Theme="Default" CodeFile="Order_Edit_Invoice.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Order edit - Invoice</title>
    <style type="text/css">
        body
        {
            margin: 0px;
            padding: 0px;
            height: 100%;
            width: 100%;
            overflow: hidden;
        }
    </style>
</head>
<body class="TabsBody <%=mBodyClass%>">
    <form id="form2" runat="server">
    <asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />
    <asp:Panel runat="server" ID="pnlBody" CssClass="TabsPageBody" EnableViewState="false">
        <asp:Panel runat="server" ID="pnlContainer" CssClass="TabsPageContainer" EnableViewState="false">
            <asp:Panel runat="server" ID="pnlScroll" CssClass="TabsPageScrollArea2" EnableViewState="false">
                <asp:Panel runat="server" ID="pnlTab" CssClass="TabsPageContent" EnableViewState="false">
                    <asp:Panel ID="pnlHeader" runat="server" CssClass="PageHeaderLine SiteHeaderLine" EnableViewState="false">
                        <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
                            Visible="false" />
                        <asp:Label ID="lblInvoiceNumber" runat="server" EnableViewState="false" />
                        <cms:CMSTextBox ID="txtInvoiceNumber" runat="server" MaxLength="200" EnableViewState="false" /><cms:CMSButton
                            ID="btnGenerate" runat="server" OnClick="btnGenerate_Click1" CssClass="LongButton"
                            EnableViewState="false" /><cms:CMSButton ID="btnPrintPreview" runat="server" OnClientClick="showPrintPreview(); return false;"
                                CssClass="LongButton" EnableViewState="false" />
                    </asp:Panel>
                    <asp:Panel ID="pnlContent" runat="server" CssClass="PageContent" EnableViewState="false">
                        <asp:Label ID="lblInvoice" runat="server" EnableViewState="false" /></asp:Panel>
                </asp:Panel>
            </asp:Panel>
        </asp:Panel>
    </asp:Panel>
    </form>
</body>
</html>
