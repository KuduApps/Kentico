<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Ecommerce_Pages_Tools_Orders_Order_Edit"
    CodeFile="Order_Edit.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Order - Properties</title>

    <script type="text/javascript">
        var IsCMSDesk = true;
    </script>

</head>
<frameset border="0" id="rowsFrameset" runat="server">
    <frame name="orderHeader" src="Order_Edit_Header.aspx<%= Request.Url.Query %>" frameborder="0"
        scrolling="no" noresize="noresize" />
    <frame name="orderContent" src="Order_Edit_General.aspx<%= Request.Url.Query %>"
        frameborder="0" />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
