<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSMessages_Information"
    Theme="Default" EnableEventValidation="false" CodeFile="Information.aspx.cs" %>
<%@ Register src="~/CMSAdminControls/UI/PageElements/PageTitle.ascx" tagname="PageTitle" tagprefix="cms" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>CMS - Information</title>
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
    <asp:Panel ID="pnlBody" runat="server" CssClass="PageBody">
        <asp:Panel ID="pnlTitle" runat="server" CssClass="PageHeader">
            <cms:PageTitle ID="titleElem" runat="server" />
        </asp:Panel>
        <asp:Panel ID="pnlContent" runat="server" CssClass="PageContent">
            <asp:Label ID="lblMessage" runat="server" EnableViewState="false" CssClass="InfoLabel" />
        </asp:Panel>
    </asp:Panel>
    </form>
</body>
</html>
