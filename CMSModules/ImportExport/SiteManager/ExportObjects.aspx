<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_ImportExport_SiteManager_ExportObjects"
    Theme="Default" ValidateRequest="false" EnableEventValidation="false" CodeFile="ExportObjects.aspx.cs" %>
<%@ Register src="~/CMSAdminControls/UI/PageElements/PageTitle.ascx" tagname="PageTitle" tagprefix="cms" %>

<%@ Register Src="~/CMSModules/ImportExport/Controls/ExportWizard.ascx" TagName="ExportWizard"
    TagPrefix="cms" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Export objects</title>
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
            <asp:Panel ID="PanelTitle" runat="server" CssClass="PageHeader SimpleHeader" EnableViewState="false">
                <cms:PageTitle ID="ptExportSiteSettings" runat="server" HelpTopicName="step_12" HelpName="helpTopic" />
            </asp:Panel>
            <asp:Panel ID="PanelExportHistory" runat="server" CssClass="PageHeaderLine" EnableViewState="false">
                <asp:Panel runat="server" ID="pnlNew" CssClass="PageHeaderItem">
                    <asp:Image ID="ImageExportHistory" runat="server" CssClass="NewItemImage" />
                    <asp:HyperLink ID="HyperlinkExportHistory" runat="server" CssClass="NewItemLink" />
                </asp:Panel>
                <br clear="all" />
            </asp:Panel>
            <asp:Panel ID="PanelExportSettings" runat="server" CssClass="PageContent">
                <cms:ExportWizard ID="wzdExport" ShortID="w" runat="server" />
            </asp:Panel>
        </asp:Panel>
    </form>
</body>
</html>
