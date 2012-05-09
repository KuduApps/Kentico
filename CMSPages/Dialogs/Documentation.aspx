<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSPages_Dialogs_Documentation"
    CodeFile="Documentation.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>WebParts documentation</title>
    <style media="print" type="text/css">
        .noprint
        {
            display: none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <a name="top"></a>
    <table style="width: 100%; background-color: #EDF4F9;">
        <tr>
            <td>
                <h1>
                    <%= documentationTitle%></h1>
            </td>
        </tr>
    </table>
    <asp:Panel ID="pnlContent" runat="server" >
        <asp:Literal runat="server" ID="ltlContent" />
    </asp:Panel>
    <asp:Panel ID="pnlInfo" runat="server" Visible="false" >
        <br />
        <b>
            <cms:LocalizedLabel runat="server" ID="lblInfoTitle" ResourceString="Webpartdocumentation.titleinfo" />
        </b>
        <br />
        <br />
        <cms:LocalizedLabel runat="server" ID="lblWebparts" ResourceString="Webpartdocumentation.webparts"
            DisplayColon="true" />
        <a href="documentation.aspx?allwebparts=true">Web parts</a>
        <br />
        <cms:LocalizedLabel runat="server" ID="lblWidgets" ResourceString="Webpartdocumentation.widgets"
            DisplayColon="true" />
        <a href="documentation.aspx?allwidgets=true">Widgets</a>
    </asp:Panel>
    </form>
</body>
</html>
