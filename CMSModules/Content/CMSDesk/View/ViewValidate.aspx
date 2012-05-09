<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewValidate.aspx.cs" Inherits="CMSModules_Content_CMSDesk_View_ViewValidate"
    Theme="Default" %>

<%@ Register Src="~/CMSAdminControls/UI/PageElements/Help.ascx" TagName="Help" TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/PageElements/FrameResizer.ascx" TagName="FrameResizer"
    TagPrefix="cms" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server" enableviewstate="false">
    <title>Content - Edit</title>
    <style type="text/css">
        body
        {
            margin: 0px;
            padding: 0px;
            height: 100%;
            overflow-y: hidden;
        }
    </style>
    <script type="text/javascript">
        //<![CDATA[
        var IsCMSDesk = true;

        // Refresh tree
        function RefreshTree(expandNodeId, selectNodeId) {
            if (parent.RefreshTree) {
                parent.RefreshTree(expandNodeId, selectNodeId);
            }
        }
        //]]>
    </script>
</head>
<body class="<%=mBodyClass%>">
    <form id="form1" runat="server">
    <asp:Panel ID="pnlContainer" runat="server" CssClass="ViewTabs">
        <cms:FrameResizer ID="allResizer" runat="server" All="true" />
        <cms:JQueryTabContainer ID="pnlTabs" runat="server">
            <cms:JQueryTab ID="tabPreview" runat="server" CssClass="Tab">
                <ContentTemplate>
                    <div class="HeaderSeparator" id="sep1">
                        &nbsp;</div>
                    <iframe width="100%" height="100%" id="pageview" name="pageview" scrolling="auto"
                        frameborder="0" enableviewstate="false" src="<%=viewpage%>" onload="ResizeContentArea();"
                        class="ContentFrame"></iframe>
                </ContentTemplate>
            </cms:JQueryTab>
            <cms:JQueryTab ID="tabValidate" runat="server" CssClass="Tab">
                <ContentTemplate>
                    <div class="HeaderSeparator" id="sep2">
                        &nbsp;</div>
                    <iframe width="100%" height="100%" id="pagevalidate" name="pagevalidate" scrolling="auto"
                        frameborder="0" enableviewstate="false" src="<%=validatepage%>" onload="ResizeContentArea();"
                        class="ContentFrame"></iframe>
                </ContentTemplate>
            </cms:JQueryTab>
        </cms:JQueryTabContainer>
    </asp:Panel>
    <div class="ViewHelp">
        <cms:Help ID="helpElem" runat="server" TopicName="viewvalidate" HelpName="helpTopic"
            EnableViewState="false" />
    </div>
    <asp:Literal runat="server" ID="ltlScript" EnableViewState="false" />

    <script type="text/javascript" language="javascript">
        //<![CDATA[
        var viewElem = null;
        var validateElem = null;
        var tabsHeaderElem = null;
        var separator = null;

        function ResizeContentArea() {

            if (tabsHeaderElem == null) {
                var tabsHeaderElem = document.getElementById('<%= pnlTabs.ClientID + "_header" %>');
            }

            if (viewElem == null) {
                viewElem = document.getElementById('pageview');
            }

            if (validateElem == null) {
                validateElem = document.getElementById('pagevalidate');
            }

            if (separator == null) {
                separator = document.getElementById('sep1');
                if (separator == null) {
                    separator = document.getElementById('sep2');
                }
            }

            if (viewElem && separator && tabsHeaderElem && validateElem) {
                var tabsHeaderHeight = tabsHeaderElem.offsetHeight;

                var height = (document.body.offsetHeight - tabsHeaderHeight - separator.offsetHeight);
                if (height > 0) {
                    var h = (height > 0 ? height : '0') + 'px';
                    if (viewElem.style.height != h) {
                        viewElem.style.height = h;
                    }

                    if (validateElem.style.height != h) {
                        validateElem.style.height = h;
                    }
                }
            }
        }

        $j(window).resize(function() { if (ResizeContentArea) { ResizeContentArea(); } });

        //]]>
    </script>

    </form>
</body>
</html>
