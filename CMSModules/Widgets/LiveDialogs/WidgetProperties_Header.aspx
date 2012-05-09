<%@ Page Language="C#" AutoEventWireup="true" Theme="Default" 
    Inherits="CMSModules_Widgets_LiveDialogs_WidgetProperties_Header" CodeFile="WidgetProperties_Header.aspx.cs" %>
    
<%@ Register src="~/CMSAdminControls/UI/PageElements/PageTitle.ascx" tagname="PageTitle" tagprefix="cms" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Widget properties - Header</title>
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
    <asp:Panel runat="server" ID="pnlBody" CssClass="WidgetTabsPageHeader">
        <cms:PageTitle ID="PageTitle" runat="server" />
    </asp:Panel>
    <asp:Panel runat="server" ID="PanelSeparator" CssClass="HeaderSeparator">
        &nbsp;
    </asp:Panel>
    <asp:HiddenField ID="hdnSelected" runat="server" />
    <asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />
    <cms:CMSButton ID="btnHidden" runat="server" EnableViewState="false" Style="display: none;"
        OnClick="btnHidden_Click" />
    </form>
</body>
</html>
