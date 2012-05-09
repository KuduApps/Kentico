<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_ImportExport_SiteManager_ImportSite"
    Theme="Default" ValidateRequest="false" EnableEventValidation="false" CodeFile="ImportSite.aspx.cs" %>
<%@ Register src="~/CMSAdminControls/UI/PageElements/PageTitle.ascx" tagname="PageTitle" tagprefix="cms" %>

<%@ Register Src="~/CMSModules/ImportExport/Controls/ImportWizard.ascx" TagName="ImportWizard"
    TagPrefix="cms" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Export site</title>
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
        <asp:Panel runat="server" ID="pnlBody" CssClass="PageBody">
            <asp:Panel runat="server" ID="pnlTitle" CssClass="PageHeader SimpleHeader">
                <cms:PageTitle ID="titleElem" runat="server" HelpTopicName="site_import" HelpName="helpTopic" />
            </asp:Panel>
            <asp:Panel ID="pnlImport" runat="server" CssClass="PageContent">
                <cms:ImportWizard ID="wzdImport" ShortID="w" runat="server" />
            </asp:Panel>
        </asp:Panel>
    </form>
</body>
</html>
