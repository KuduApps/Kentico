<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Widgets_Dialogs_WidgetProperties_Header"
    Theme="Default" CodeFile="WidgetProperties_Header.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/PageElements/Help.ascx" TagName="Help" TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/PageElements/PageTitle.ascx" TagName="PageTitle"
    TagPrefix="cms" %>
<%@ Register TagPrefix="cms" Namespace="CMS.UIControls" Assembly="CMS.UIControls" %>
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
    <asp:Panel runat="server" ID="pnlBody" CssClass="WebpartTabsPageHeader">
        <cms:PageTitle ID="PageTitle" runat="server" />
        <asp:Panel runat="server" ID="pnlTabs" CssClass="" Visible="false">
            <asp:Panel ID="pnlContainer" runat="server">
                <asp:Panel runat="server" ID="pnlLeft" CssClass="FullTabsLeft">
                    &nbsp;
                </asp:Panel>
                <asp:Panel runat="server" ID="pnlPropTabs" CssClass="TabsTabs LightTabs">
                    <asp:Panel runat="server" ID="pnlWhite" CssClass="TabsWhite">
                        <cms:UITabs ID="tabsElem" runat="server" UseClientScript="true" ModuleName="CMS.Content"
                            ElementName="Design.WidgetProperties" />
                    </asp:Panel>
                </asp:Panel>
                <asp:Panel runat="server" ID="Panel1" CssClass="FullTabsRight">
                    <div class="RightAlign PropertiesContextHelp" id="pnlHelp">
                        <cms:Help ID="helpElem" runat="server" TopicName="page_tab" HelpName="helpTopic"
                            EnableViewState="false" />
                    </div>
                </asp:Panel>
            </asp:Panel>
        </asp:Panel>
        <asp:Panel runat="server" ID="PanelSeparator" CssClass="HeaderSeparator">
            &nbsp;
        </asp:Panel>
    </asp:Panel>
    <asp:HiddenField ID="hdnSelected" runat="server" />
    <asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />
    <cms:CMSButton ID="btnHidden" runat="server" EnableViewState="false" Style="display: none;"
        OnClick="btnHidden_Click" />

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
