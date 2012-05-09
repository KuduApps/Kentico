<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_MyDesk_Header"
    Theme="Default" CodeFile="Header.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/PageElements/FrameResizer.ascx" TagName="FrameResizer" TagPrefix="cms" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>CMS Desk - Tools</title>
    <style type="text/css">
        body
        {
            margin: 0px;
            padding: 0px;
            height:100%;
        }
    </style>
</head>
<body class="<%=mBodyClass%>">
    <form id="form1" runat="server" enableviewstate="false">
        <cms:FrameResizer ID="frmResizer" runat="server" MinSize="0, 0, *" Vertical="True"
            CssPrefix="Vertical" ParentLevel="2" />
        <asp:Panel runat="server" ID="pnlSeparator" CssClass="HeaderSeparator">
            &nbsp;
        </asp:Panel>
    </form>
</body>
</html>
