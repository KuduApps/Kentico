<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="CMSMessages_BannedIP" Theme="Default" CodeFile="BannedIP.aspx.cs" %>
<%@ Register src="~/CMSAdminControls/UI/PageElements/PageTitle.ascx" tagname="PageTitle" tagprefix="cms" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>CMS - Banned IP</title>
    <style type="text/css">
        body
        {
            margin: 0px;
            padding: 0px;
            height: 100%;
        }
    </style>
</head>
<body class="<%=mBodyClass%>">
    <form id="form1" runat="server">
        <asp:Panel ID="PanelBody" runat="server" CssClass="PageBody">
            <asp:Panel ID="PanelTitle" runat="server" CssClass="PageHeader">
                <cms:PageTitle ID="titleElem" runat="server" />
            </asp:Panel>
            <asp:Panel ID="PanelContent" runat="server" CssClass="PageContent">
                <asp:Label ID="lblMessage" runat="server" />
            </asp:Panel>
        </asp:Panel>
    </form>
</body>
</html>
