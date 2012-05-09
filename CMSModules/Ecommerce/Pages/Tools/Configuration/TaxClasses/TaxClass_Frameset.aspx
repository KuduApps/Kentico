<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Ecommerce_Pages_Tools_Configuration_TaxClasses_TaxClass_Frameset"
    CodeFile="TaxClass_Frameset.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server" enableviewstate="false">
    <title>Tax class - Edit</title>
</head>
<frameset border="0" id="frameTaxClass" runat="server">
    <frame name="taxClassHeader" src="TaxClass_Header.aspx?taxclassid=<%=Request.QueryString["taxclassid"]%>&siteId=<%=SiteID%><%=HeaderUrlParams%>"
        scrolling="no" frameborder="0" noresize="noresize" />
    <frame name="taxClassContent" 
        scrolling="auto" frameborder="0" noresize="noresize" />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
