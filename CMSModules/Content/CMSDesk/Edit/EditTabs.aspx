<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Content_CMSDesk_Edit_EditTabs"
    Theme="Default" CodeFile="EditTabs.aspx.cs" %>
<%@ Register src="~/CMSAdminControls/UI/PageElements/Help.ascx" tagname="Help" tagprefix="cms" %>

<%@ Register TagPrefix="cms" Namespace="CMS.UIControls" Assembly="CMS.UIControls" %>
<%@ Register Src="~/CMSAdminControls/UI/PageElements/FrameResizer.ascx" TagName="FrameResizer" TagPrefix="cms" %>
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
        .TabsTabs
        {
            padding-top: 0px !important;
        }
    </style>
</head>
<body class="<%=mBodyClass%>">
    <form id="form1" runat="server">
    <cms:FrameResizer ID="allResizer" runat="server" All="true" />
    <asp:Panel runat="server" ID="pnlLeft" CssClass="FullTabsLeft" EnableViewState="false">
        &nbsp;
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlPropTabs" CssClass="TabsTabs LightTabs" EnableViewState="false">
        <asp:Panel runat="server" ID="pnlWhite" CssClass="Tabs">
            <cms:UITabs ID="tabsModes" ShortID="t" runat="server" UseClientScript="true" ModuleName="CMS.Content" />
        </asp:Panel>
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlRight" CssClass="FullTabsRight">
        <div class="RightAlign" style="padding: 3px 5px 0px 5px;">
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td style="padding: 1px 10px 0px 10px; vertical-align: top;">
                        <div id="divDesign" style="display: none;">
                            <asp:CheckBox runat="server" ID="chkWebParts" />
                        </div>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <cms:Help ID="helpElem" runat="server" TopicName="page_tab" HelpName="helpTopic"
                            EnableViewState="false" />
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
    <asp:Panel runat="server" ID="PanelSeparator" CssClass="HeaderSeparator">
        &nbsp;
    </asp:Panel>

    <script type="text/javascript">
        //<![CDATA[
        function SetTabsContext(mode) {
            if (mode == 'design') {
                document.getElementById('divDesign').style.display = 'block';
            } else {
                document.getElementById('divDesign').style.display = 'none';
            }
        }

        function RefreshContent() {
            parent.frames['contenteditview'].document.location.replace(parent.frames['contenteditview'].document.location);
        }
        //]]>
    </script>

    <asp:Literal runat="server" ID="ltlScript" EnableViewState="false" />
    </form>
</body>
</html>
