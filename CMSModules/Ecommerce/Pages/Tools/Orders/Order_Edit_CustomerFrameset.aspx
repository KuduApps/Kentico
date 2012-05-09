<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Ecommerce_Pages_Tools_Orders_Order_Edit_CustomerFrameset"
    CodeFile="Order_Edit_CustomerFrameset.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Order - edit - CustomerFrameset</title>
</head>
<frameset border="0" rows="<%= TabsFrameHeight %>, *" id="rowsFrameset">
    <frame name="CustomerHeader" src="../Customers/Customer_Edit_Header.aspx?customerid=<%=Request.QueryString["customerid"]%>&showtitle=1&hidebreadcrumbs=1"
        frameborder="0" scrolling="no" noresize="noresize" />
    <frame name="CustomerContent" src="../Customers/Customer_Edit_General.aspx?customerid=<%=Request.QueryString["customerid"]%>"
        frameborder="0" />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
