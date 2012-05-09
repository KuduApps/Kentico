<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Ecommerce_Pages_Content_Product_Product_Edit_Frameset"
    CodeFile="Product_Edit_Frameset.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Product - edit - Frameset</title>
</head>
<frameset border="0" rows="60,*,10" runat="server" id="rowsFrameset">
    <frame name="ProductHeader" src="Product_Edit_Header.aspx<%=Request.Url.Query%>"
        frameborder="0" scrolling="no" noresize="noresize" />
    <frame name="ProductContent" frameborder="0" />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
