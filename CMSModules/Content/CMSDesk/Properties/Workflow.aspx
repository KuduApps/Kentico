<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Content_CMSDesk_Properties_Workflow"
    Theme="Default" CodeFile="Workflow.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/PageElements/Help.ascx" TagName="Help" TagPrefix="cms" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Properties - Workflow</title>
    <style type="text/css">
        body
        {
            margin: 0px;
            padding: 0px;
            height: 100%;
        }
        .UniGridGrid, .UniGridPager
        {
            width: 100% !important;
        }
    </style>

    <script type="text/javascript">
        //<![CDATA[
        function RefreshTree(expandNodeId, selectNodeId) {
            // Update tree
            parent.RefreshTree(expandNodeId, selectNodeId);
        }
        //]]>
    </script>

</head>
<body class="VerticalTabsBody <%=mBodyClass%>">
    <form id="form1" runat="server">
    <asp:PlaceHolder runat="server" ID="plcManager" />
    <asp:Panel runat="server" ID="pnlBody" CssClass="VerticalTabsPageBody">
        <asp:Panel ID="pnlMenu" runat="server" CssClass="ContentEditMenu">
            <table width="100%">
                <tr>
                    <td>
                        <div style="height: 24px; padding: 5px;">
                        </div>
                    </td>
                    <td class="TextRight">
                        <cms:Help ID="helpElem" runat="server" TopicName="workflow" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlContent" runat="server" CssClass="PageContent">
            <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false" />
            <asp:Panel ID="pnlWorkflow" runat="server">
                <table width="97%">
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblWorkflowName" runat="Server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <cms:LocalizedLabel ID="lblApprove" runat="server" CssClass="FormGroupHeader" EnableViewState="false"
                                ResourceString="WorfklowProperties.Approve" />
                        </td>
                        <td>
                            <cms:LocalizedLabel ID="lblSteps" runat="server" CssClass="FormGroupHeader" EnableViewState="false"
                                ResourceString="WorfklowProperties.Steps" />
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top;">
                            <asp:Panel ID="pnlForm" runat="server">
                                <table>
                                    <tr>
                                        <td style="vertical-align: top;">
                                            <cms:LocalizedLabel ID="lblComment" runat="server" EnableViewState="false" ResourceString="WorfklowProperties.Comment" />
                                        </td>
                                        <td>
                                            <cms:CMSTextBox ID="txtComment" runat="server" TextMode="MultiLine" CssClass="TextAreaField" />
                                        </td>
                                    </tr>
                                    <asp:PlaceHolder ID="plcSendMail" runat="server" Visible="false">
                                        <tr>
                                            <td>
                                                <cms:LocalizedLabel ID="lblSendMail" runat="server" EnableViewState="false" ResourceString="WorfklowProperties.SendMail" />
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="chkSendMail" runat="server" Checked="True" />
                                            </td>
                                        </tr>
                                    </asp:PlaceHolder>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            <cms:LocalizedButton ID="btnApprove" runat="server" CssClass="SubmitButton" OnClick="btnApprove_Click"
                                                EnableViewState="false" ResourceString="general.approve" />
                                            <cms:LocalizedButton ID="btnReject" runat="server" CssClass="SubmitButton" OnClick="btnReject_Click"
                                                EnableViewState="false" ResourceString="general.reject" />
                                            <cms:LocalizedButton ID="btnArchive" runat="server" CssClass="SubmitButton" OnClick="btnArchive_Click"
                                                EnableViewState="false" ResourceString="general.Archive" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                        <td style="vertical-align: top; width: 300px;">
                            <cms:UniGrid ID="gridSteps" runat="server" GridName="workflowsteps.xml" OrderBy="StepOrder"
                                IsLiveSite="false" DelayedReload="true" ShowActionsMenu="false" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <cms:LocalizedLabel ID="lblHistory" runat="server" CssClass="FormGroupHeader" EnableViewState="false"
                                ResourceString="WorfklowProperties.History" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <cms:UniGrid ID="gridHistory" runat="server" GridName="workflowhistory.xml" OrderBy="WorkflowHistoryID DESC"
                                IsLiveSite="false" DelayedReload="true" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:Panel>
    </asp:Panel>
    </form>
</body>
</html>
