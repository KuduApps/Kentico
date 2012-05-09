<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_ImportExport_SiteManager_ExportObject"
    Theme="Default" ValidateRequest="false" EnableEventValidation="false" MasterPageFile="~/CMSMasterPages/UI/Dialogs/ModalDialogPage.master"
    Title="Export single object" CodeFile="ExportObject.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/AsyncControl.ascx" TagName="AsyncControl" TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/System/ActivityBar.ascx" TagName="ActivityBar"
    TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">

    <script type="text/javascript">
        //<![CDATA[
        var importTimerId = 0;

        // End timer function
        function StopTimer() {
            if (importTimerId) {
                clearInterval(importTimerId);
                importTimerId = 0;

                if (window.HideActivity) {
                    window.HideActivity();
                }
            }
        }

        // Start timer function
        function StartTimer() {
            if (window.Activity) {
                importTimerId = setInterval("window.Activity()", 500);
            }
        }
        //]]>
    </script>

    <asp:Panel runat="server" ID="pnlDetails" CssClass="PageContent" Visible="true">
        <table>
            <tr>
                <td colspan="2">
                    <cms:LocalizedLabel ID="lblBeta" runat="server" Visible="false" CssClass="ErrorLabel"
                        EnableViewState="false" />
                    <asp:Label ID="lblIntro" runat="server" CssClass="ContentLabel" EnableViewState="false" />
                </td>
            </tr>
            <asp:PlaceHolder ID="plcExportDetails" runat="server">
                <tr>
                    <td style="white-space: nowrap; vertical-align: middle;">
                        <cms:LocalizedLabel ID="lblFileName" runat="server" CssClass="ContentGroupHeader"
                            EnableViewState="false" ResourceString="general.filename" Font-Bold="true" />&nbsp;
                    </td>
                    <td style="width: 100%">
                        <cms:CMSTextBox ID="txtFileName" runat="server" Width="600" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="lblError" runat="server" CssClass="ErrorLabel" EnableViewState="false" />
                    </td>
                </tr>
            </asp:PlaceHolder>
        </table>
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlContent" CssClass="PageContent" Visible="false">
        <table>
            <tr>
                <td style="vertical-align: top;">
                    <asp:Label ID="lblResult" runat="server" CssClass="ContentLabel" EnableViewState="false" />
                </td>
            </tr>
            <tr>
                <td style="vertical-align: top;">
                    <asp:HyperLink ID="lnkDownload" runat="server" Visible="false" Target="_blank" EnableViewState="false" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnlProgress" CssClass="PageContent" runat="Server" Visible="false"
        EnableViewState="false">
        <table>
            <tr>
                <td style="vertical-align: top;">
                    <asp:Label ID="lblProgress" runat="server" CssClass="ContentLabel" /><br />
                    <br />
                    <cms:ActivityBar ID="ucActivityBar" runat="server" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <cms:AsyncControl ID="ucAsyncControl" runat="server" />
    <asp:Literal ID="ltlScript" runat="Server" EnableViewState="false" />
</asp:Content>
<asp:Content ID="cntFooter" ContentPlaceHolderID="plcFooter" runat="server">
    <div class="FloatRight">
        <cms:LocalizedButton ID="btnOk" runat="server" CssClass="SubmitButton" /><cms:LocalizedButton
            ID="btnCancel" runat="server" CssClass="SubmitButton" ResourceString="general.close"
            OnClientClick="window.close(); return false;" />
    </div>
</asp:Content>
