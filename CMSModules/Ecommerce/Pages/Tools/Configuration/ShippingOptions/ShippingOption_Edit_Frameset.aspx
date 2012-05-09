<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Ecommerce_Pages_Tools_Configuration_ShippingOptions_ShippingOption_Edit_Frameset"
    CodeFile="ShippingOption_Edit_Frameset.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server" enableviewstate="false">
    <title>Shopping option - Edit</title>
</head>
<frameset border="0" rows="<%= TabsBreadHeadFrameHeight %>, *" id="rowsFrameset">
    <frame name="shippingOptionHeader" src="ShippingOption_Edit_Header.aspx?shippingOptionID=<%=Request.QueryString["shippingOptionID"]%>&siteId=<%=SiteID%>"
        scrolling="no" frameborder="0" noresize="noresize" />
    <frame name="shippingOptionContent" src="ShippingOption_Edit_General.aspx?ShippingOptionID=<%=Request.QueryString["ShippingOptionID"]%>&saved=<%=Request.QueryString["saved"]%> "
        scrolling="auto" frameborder="0" noresize="noresize" />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
