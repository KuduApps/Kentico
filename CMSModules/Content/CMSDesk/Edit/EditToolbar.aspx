<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Content_CMSDesk_Edit_EditToolbar"
    Theme="Default" CodeFile="EditToolbar.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Content - Edit</title>
    <style type="text/css">
        body
        {
            margin: 0px;
            padding: 0px;
            height: 100%;
        }
    </style>
</head>
<body class="ToolbarBody <%=mBodyClass%>">
    <form id="form1" runat="server">
    <asp:Panel ID="pnlBody" runat="server">
        <asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />
    </asp:Panel>
    <cms:CMSButton ID="btnSave" runat="server" CssClass="HiddenButton" EnableViewState="false"
        OnClick="btnSave_Click" />
    <cms:CMSButton ID="btnApprove" runat="server" CssClass="HiddenButton" EnableViewState="false"
        OnClick="btnApprove_Click" />
    <cms:CMSButton ID="btnCheckIn" runat="server" CssClass="HiddenButton" EnableViewState="false"
        OnClick="btnCheckIn_Click" />
    </form>
</body>
</html>
