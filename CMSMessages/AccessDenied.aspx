<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSMessages_AccessDenied"
    Theme="Default" EnableEventValidation="false" CodeFile="AccessDenied.aspx.cs" %>
<%@ Register src="~/CMSAdminControls/UI/PageElements/PageTitle.ascx" tagname="PageTitle" tagprefix="cms" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>CMS - Access denied</title>
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
                <asp:Label ID="LabelMessage" runat="server" Text="Label" /><br />
                <br />
                <asp:HyperLink ID="lnkGoBack" runat="server" NavigateUrl="~/default.aspx" /><br />
                <br />
                <br />
                <cms:LocalizedButton ID="btnSignOut" Visible="false" runat="server" CssClass="SubmitButton"
                    OnClick="btnSignOut_Click" ResourceString="signoutbutton.signout" />
                <cms:LocalizedButton ID="btnLogin" Visible="false" runat="server" CssClass="SubmitButton"
                    OnClick="btnLogin_Click" ResourceString="webparts_membership_signoutbutton.signin" />
                <asp:Literal ID="LiteralScript" runat="server" Text="" />
            </asp:Panel>
        </asp:Panel>
    </form>
</body>
</html>
