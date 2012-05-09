<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Ecommerce_Pages_Tools_Customers_Customer_Edit_Frameset"
    CodeFile="Customer_Edit_Frameset.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Customer - edit - Frameset</title>

    <script type="text/javascript">
        var IsCMSDesk = true;
    </script>

</head>
<frameset border="0" rows="<%= TabsBreadHeadFrameHeight %>, *" id="rowsFrameset">
    <frame name="CustomerHeader" src="Customer_Edit_Header.aspx<%=Request.Url.Query%>"
        frameborder="0" scrolling="no" noresize="noresize" />
    <frame name="CustomerContent" frameborder="0" />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
