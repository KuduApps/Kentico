<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master"
    Inherits="CMSModules_Staging_Tools_Objects_Tasks" Theme="Default" Title="Staging - Tasks"
    CodeFile="Tasks.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/PageElements/PageTitle.ascx" TagName="PageTitle"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/AsyncControl.ascx" TagName="AsyncControl" TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/AsyncBackground.ascx" TagName="AsyncBackground"
    TagPrefix="cms" %>
<asp:Content ID="cntHeader" ContentPlaceHolderID="plcBeforeBody" runat="server">
    <asp:PlaceHolder runat="server" ID="plcTasks">
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
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </asp:PlaceHolder>
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
    <asp:Panel runat="server" ID="pnlNotLogged" Visible="false" CssClass="PageContent">
        <asp:Label ID="lblNotLogged" runat="server" EnableViewState="false" CssClass="InfoLabel" />
    </asp:Panel>
    <asp:PlaceHolder ID="plcContent" runat="server">
        <asp:Label ID="lblInfo" runat="server" CssClass="InfoLabel" EnableViewState="false" />
        <asp:Label ID="lblError" runat="server" CssClass="ErrorLabel" EnableViewState="false" />
        <cms:UniGrid ID="gridTasks" runat="server" GridName="~/CMSModules/Staging/Tools/Objects/Tasks.xml"
            IsLiveSite="false" OrderBy="TaskTime, TaskID" DelayedReload="false" ExportFileName="staging_task" />
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
    </asp:PlaceHolder>
</asp:Content>
