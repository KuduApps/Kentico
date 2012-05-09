<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Log.aspx.cs" Inherits="CMSModules_Integration_Pages_Administration_Log"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/Dialogs/ModalDialogPage.master"
    Title="Integration - Synchronization log" %>

<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<%@ Register Namespace="CMS.UIControls.UniGridConfig" TagPrefix="ug" Assembly="CMS.UIControls" %>
<asp:Content ID="cntControls" runat="server" ContentPlaceHolderID="plcControls">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td style="vertical-align: middle;">
                <asp:Label runat="server" ID="lblInfo" EnableViewState="false" />
            </td>
            <td class="TextRight">
                <cms:LocalizedButton runat="server" ID="btnClear" CssClass="LongButton" OnClick="btnClear_Click"
                    EnableViewState="false" ResourceString="Task.LogClear" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <div class="PageContent">
        <cms:UniGrid ID="gridLog" runat="server" OrderBy="SyncLogTime DESC" IsLiveSite="false"
            ObjectType="integration.synclog">
            <GridActions>
                <ug:Action Name="delete" Caption="$General.Delete$" Icon="Delete.png" Confirmation="$General.ConfirmDelete$" />
            </GridActions>
            <GridColumns>
                <ug:Column Source="SyncLogTime" Caption="$SyncLog.Columns.Time$" Wrap="false" />
                <ug:Column Source="SyncLogErrorMessage" Caption="$SyncLog.Columns.Error$" IsText="true"
                    Width="100%" />
            </GridColumns>
        </cms:UniGrid>
    </div>
</asp:Content>
<asp:Content ID="cntFooter" ContentPlaceHolderID="plcFooter" runat="server" EnableViewState="false">
    <div class="FloatRight">
        <cms:LocalizedButton ID="btnClose" runat="server" CssClass="SubmitButton" ResourceString="general.close"
            OnClientClick="window.close(); return false;" EnableViewState="false" />
    </div>
</asp:Content>
