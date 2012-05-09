<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_PortalEngine_UI_WebParts_WebPartProperties_header"
    Theme="Default" CodeFile="WebPartProperties_header.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/PageElements/Help.ascx" TagName="Help" TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/PageElements/PageTitle.ascx" TagName="PageTitle"
    TagPrefix="cms" %>
<%@ Register TagPrefix="cms" Namespace="CMS.UIControls" Assembly="CMS.UIControls" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Untitled Page</title>
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
    <asp:Panel runat="server" ID="pnlBody" CssClass="WebpartTabsPageHeader">
        <asp:Panel runat="server" ID="Panel1">
            <cms:PageTitle ID="PageTitle" runat="server" />
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlTabs" CssClass="">
            <asp:Panel runat="server" ID="pnlLeft" CssClass="TabsLeft">
                &nbsp;
            </asp:Panel>
            <asp:Panel runat="server" ID="pnlPropTabs" CssClass="TabsTabs LightTabs">
                <asp:Panel runat="server" ID="pnlWhite" CssClass="TabsWhite">
                    <cms:UITabs ID="tabsElem" runat="server" UseClientScript="true" ModuleName="CMS.Content"
                        ElementName="Design.WebPartProperties" />
                </asp:Panel>
            </asp:Panel>
            <asp:Panel runat="server" ID="pnlRight" CssClass="FullTabsRight">
                <div class="RightAlign PropertiesContextHelp" id="pnlHelp">
                    <cms:Help ID="helpElem" runat="server" TopicName="page_tab" HelpName="helpTopic"
                        EnableViewState="false" />
                </div>
            </asp:Panel>
        </asp:Panel>
        <asp:Panel runat="server" ID="PanelSeparator" CssClass="HeaderSeparator">
            &nbsp;
        </asp:Panel>
    </asp:Panel>

    <script type="text/javascript">
        function SetTabsContext(mode) {
            if (mode == 'variants') {
                document.getElementById('pnlHelp').style.display = 'block';
            } else {
                document.getElementById('pnlHelp').style.display = 'none';
            }
        } 
    </script>

    </form>
</body>
</html>
