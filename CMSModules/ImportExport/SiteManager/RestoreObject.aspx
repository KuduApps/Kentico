<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_ImportExport_SiteManager_RestoreObject"
    Theme="Default" ValidateRequest="false" EnableEventValidation="false" MasterPageFile="~/CMSMasterPages/UI/Dialogs/ModalDialogPage.master"
    Title="Export single object" CodeFile="RestoreObject.aspx.cs" %>

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
                <td>
                    <cms:LocalizedLabel ID="lblBeta" runat="server" Visible="false" CssClass="ErrorLabel"
                        EnableViewState="false" />
                    <asp:Label ID="lblIntro" runat="server" CssClass="ContentLabel" EnableViewState="false" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblError" runat="server" CssClass="ErrorLabel" EnableViewState="false" />
                    <asp:ListBox runat="server" ID="lstImports" CssClass="ContentListBoxLow" Width="450"
                        Enabled="false" />
                    <asp:Panel runat="server" ID="pnlLeftActions" CssClass="PageHeaderItemLeft" Style="padding: 8px 5px">
                        <asp:Image runat="server" ID="imgDelete" CssClass="DeletePackageImage" /><cms:LocalizedLinkButton
                            runat="server" ID="btnDelete" CssClass="DeletePackageLink" ResourceString="importconfiguration.deletepackage"
                            OnClick="btnDelete_Click" />
                    </asp:Panel>
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
    <asp:Panel runat="server" ID="pnlContent" CssClass="PageContent" Visible="false">
        <table>
            <tr>
                <td style="vertical-align: top;">
                    <asp:Label ID="lblResult" runat="server" CssClass="ContentLabel" EnableViewState="false" />
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
