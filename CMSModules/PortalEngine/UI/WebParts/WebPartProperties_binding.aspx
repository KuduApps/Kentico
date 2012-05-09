<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_PortalEngine_UI_WebParts_WebPartProperties_binding" Theme="Default" EnableEventValidation="false" CodeFile="WebPartProperties_binding.aspx.cs" %>

<%@ Register Src="~/CMSModules/PortalEngine/Controls/WebParts/WebpartBinding.ascx" TagName="WebpartBinding" TagPrefix="cms" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Untitled Page</title>
    <style type="text/css">
        body
        {
            margin: 0px;
            height: 100%;
            width: 100%;
            overflow: hidden;
        }
    </style>
</head>
<body class="<%=mBodyClass%>">
    <form id="form1" runat="server">
        <asp:Panel runat="server" ID="pnlBody" CssClass="TabsPageBody">
            <cms:WebpartBinding ID="webPartBinding" runat="server" />
        </asp:Panel>
    </form>
</body>
</html>
