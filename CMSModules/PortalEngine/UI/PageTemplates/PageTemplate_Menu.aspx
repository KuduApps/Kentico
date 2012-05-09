<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_PortalEngine_UI_PageTemplates_PageTemplate_Menu" Theme="Default" CodeFile="PageTemplate_Menu.aspx.cs" %>
<%@ Register src="~/CMSAdminControls/UI/PageElements/PageTitle.ascx" tagname="PageTitle" tagprefix="cms" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server" enableviewstate="false">
    <title>Development - Page templates</title>
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
    <form id="form1" runat="server">
        <asp:Panel ID="PanelBody" runat="server" CssClass="TabsPageHeader">
            <asp:Panel ID="PanelTitle" runat="server" CssClass="PageHeader">
                <cms:PageTitle ID="PageTitlePageTemplate" runat="server" HelpTopicName="page_templates_main" HelpName="helpTopic" />
            </asp:Panel>
         </asp:Panel>   
    </form>
</body>
</html>
