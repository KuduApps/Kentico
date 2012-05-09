<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Content_CMSDesk_Footer" Theme="Default"  CodeFile="Footer.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server" enableviewstate="false">
    <title>Content - Footer</title>
    <style type="text/css">
        body
        {
            margin: 0px;
            padding: 0px;
        }
    </style>
</head>
<body class="<%=mBodyClass%>">
    <form id="form1" runat="server">
        <asp:Panel runat="server" ID="pnlBody" CssClass="ContentFooter" EnableViewState="false">
            <asp:Panel runat="server" ID="pnlLeft" CssClass="FooterLeft">
                &nbsp;
            </asp:Panel>        
            <asp:Panel runat="server" ID="pnlRight" CssClass="FooterRight">
                &nbsp;
            </asp:Panel>
        </asp:Panel>
    </form>
</body>
</html>
