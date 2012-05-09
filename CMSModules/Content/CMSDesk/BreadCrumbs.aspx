<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Content_CMSDesk_BreadCrumbs"
    Theme="Default" CodeFile="BreadCrumbs.aspx.cs" %>
<%@ Register src="~/CMSAdminControls/UI/PageElements/PageTitle.ascx" tagname="PageTitle" tagprefix="cms" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>CMSDesk - BreadCrumbs</title>
    <style type="text/css">
        body, html
        {
            margin: 0px;
            padding: 0px;
            height: 100%;
        }
    </style>
</head>
<body class="<%=mBodyClass%>">
    <form id="form1" runat="server">
        <asp:Panel runat="server" ID="pnlBody" CssClass="ContentEditBreadCrumbs" EnableViewState="false">
            <cms:PageTitle ID="titleElem" runat="server" />
        </asp:Panel>
    </form>
</body>
</html>
