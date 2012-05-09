<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Ecommerce_Pages_Tools_EcommerceModule_Frameset"
    CodeFile="EcommerceModule_Frameset.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>E-commerce</title>

    <script type="text/javascript">
        var IsCMSDesk = true;
    </script>

</head>
<frameset border="0" rows="74,*" id="rowsFrameset">
    <frame name="ecommerceMenu" src="Header.aspx" frameborder="0" scrolling="no" noresize="noresize" runat="server" id="frameMenu" />
    <frame name="ecommerceContent" frameborder="0" />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
