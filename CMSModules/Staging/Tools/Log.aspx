<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Staging_Tools_Log"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/Dialogs/ModalDialogPage.master"
    Title="Staging - Synchronization log" CodeFile="Log.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
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
        <cms:UniGrid ID="gridLog" runat="server" GridName="SyncLog.xml" OrderBy="SyncLogTime DESC"
            IsLiveSite="false" />
    </div>
</asp:Content>
<asp:Content ID="cntFooter" ContentPlaceHolderID="plcFooter" runat="server" EnableViewState="false">
    <div class="FloatRight">
        <cms:LocalizedButton ID="btnClose" runat="server" CssClass="SubmitButton" ResourceString="general.close"
            OnClientClick="window.close(); return false;" EnableViewState="false" />
    </div>
</asp:Content>
