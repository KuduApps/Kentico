<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master"
    Title="Staging - Tasks" Inherits="CMSModules_Staging_Tools_Tasks_Tasks" Theme="Default"
    CodeFile="Tasks.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/PageElements/PageTitle.ascx" TagName="PageTitle"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/AsyncControl.ascx" TagName="AsyncControl" TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/AsyncBackground.ascx" TagName="AsyncBackground"
    TagPrefix="cms" %>
<asp:Content ID="cntBeforeBody" ContentPlaceHolderID="plcBeforeBody" runat="server">

    <script type="text/javascript">
        //<![CDATA[
        // Check specified checkboxes
        function SelectCheckboxes(checkbox) {
            var checkboxes = document.getElementsByTagName("input");
            for (var i = 0; i < checkboxes.length; i++) {
                if (checkboxes[i].className == 'chckbox') {
                    if (checkbox.checked) {
                        checkboxes[i].checked = true;
                    }
                    else {
                        checkboxes[i].checked = false;
                    }
                }
            }
            return false;
        }
        //]]>
    </script>

    <asp:Panel ID="pnlMenu" runat="server" CssClass="PageHeaderLine">
        <table cellpadding="0" cellspacing="0">
            <tr>
                <td class="MenuItemEdit" style="height: 19px">
                    <asp:LinkButton ID="btnCurrent" runat="server" OnClick="btnCurrent_Click">
                        <asp:Image ID="imgCurrent" runat="server" />
                        <%=syncCurrent%>
                    </asp:LinkButton>
                </td>
                <td class="MenuItemEdit" style="height: 19px">
                    <asp:LinkButton ID="btnSubtree" runat="server" OnClick="btnSubtree_Click">
                        <asp:Image ID="imgSubtree" runat="server" />
                        <%=syncSubtree%>
                    </asp:LinkButton>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
<asp:Content ID="cntBody" ContentPlaceHolderID="plcContent" runat="server">
    <asp:Panel runat="server" ID="pnlLog" Visible="false">
        <cms:AsyncBackground ID="backgroundElem" runat="server" />
        <div class="AsyncLogArea">
            <asp:Panel ID="pnlAsyncBody" runat="server" CssClass="PageBody">
                <asp:Panel ID="pnlTitle" runat="server" CssClass="PageHeader">
                    <cms:PageTitle ID="titleElem" runat="server" />
                </asp:Panel>
                <asp:Panel ID="pnlCancel" runat="server" CssClass="PageHeaderLine">
                    <cms:CMSButton runat="server" CssClass="SubmitButton" ID="btnCancel" />
                </asp:Panel>
                <asp:Panel ID="pnlAsyncContent" runat="server" CssClass="PageContent">
                    <cms:AsyncControl ID="ctlAsync" runat="server" />
                </asp:Panel>
            </asp:Panel>
        </div>
    </asp:Panel>
    <asp:PlaceHolder ID="plcContent" runat="server">
        <asp:Label ID="lblInfo" runat="server" CssClass="InfoLabel" EnableViewState="false" />
        <asp:Label ID="lblError" runat="server" CssClass="ErrorLabel" EnableViewState="false" />
        <asp:Panel ID="pnlTasksGrid" runat="server" Visible="true">
            <cms:UniGrid ID="tasksUniGrid" runat="server" GridName="~/CMSModules/Staging/Tools/Tasks/Tasks.xml"
                IsLiveSite="false" OrderBy="TaskTime, TaskID" ExportFileName="staging_task" />
        </asp:Panel>
        <br />
        <asp:Panel ID="pnlFooter" runat="server" Style="clear: both;">
            <table style="width: 100%;">
                <tr>
                    <td>
                        <cms:LocalizedButton runat="server" ID="btnSyncSelected" CssClass="LongButton" OnClick="btnSyncSelected_Click"
                            ResourceString="Tasks.SyncSelected" EnableViewState="false" /><cms:LocalizedButton
                                runat="server" ID="btnSyncAll" CssClass="LongButton" OnClick="btnSyncAll_Click"
                                ResourceString="Tasks.SyncAll" EnableViewState="false" />
                    </td>
                    <td class="TextRight">
                        <cms:LocalizedButton runat="server" ID="btnDeleteSelected" CssClass="LongButton"
                            OnClick="btnDeleteSelected_Click" ResourceString="Tasks.DeleteSelected" EnableViewState="false" /><cms:LocalizedButton
                                runat="server" ID="btnDeleteAll" CssClass="LongButton" OnClick="btnDeleteAll_Click"
                                ResourceString="Tasks.DeleteAll" EnableViewState="false" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />
    </asp:PlaceHolder>
    <cms:CMSButton ID="btnSyncComplete" runat="server" Visible="false" />

    <script type="text/javascript">
        //<![CDATA[
        function ViewTask(taskId) {
            modalDialog('View.aspx?taskid=' + taskId, 'viewtask', 700, 500);
        }
        //]]>
    </script>

</asp:Content>
