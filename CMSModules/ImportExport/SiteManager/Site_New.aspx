<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_ImportExport_SiteManager_Site_New"
    Theme="Default" CodeFile="Site_New.aspx.cs" %>
<%@ Register src="~/CMSAdminControls/UI/PageElements/PageTitle.ascx" tagname="PageTitle" tagprefix="cms" %>

<%@ Register Src="~/CMSModules/ImportExport/Controls/NewSiteWizard.ascx" TagName="NewSiteWizard" TagPrefix="cms" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Site Manager - New Site</title>
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
            <asp:Panel ID="PanelTitle" runat="server" CssClass="PageHeader SimpleHeader">
                <cms:PageTitle ID="ptNewSite" runat="server" HelpTopicName="step_1" />
            </asp:Panel>
            <asp:Panel ID="PanelNewSite" runat="server" CssClass="PageContent">
                <cms:NewSiteWizard id="NewSiteWizard" runat="server" />
            </asp:Panel>
        </asp:Panel>
    </form>
</body>
</html>
