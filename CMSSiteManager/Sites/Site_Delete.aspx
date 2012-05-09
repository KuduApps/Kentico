<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSSiteManager_Sites_Site_Delete"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Site Manager - Delete Site"
    CodeFile="Site_Delete.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/Wizard/Header.ascx" TagName="WizardHeader" TagPrefix="cms" %>
<asp:Content ContentPlaceHolderID="plcContent" runat="server">

    <script type="text/javascript" language="javascript">
        //<![CDATA[
        var timerId = 0;
        var i = 0;

        function GetStateAction() {
            if (i++ >= 10) {
                i = 0;
                try {
                    GetState(false);
                }
                catch (ex) {
                }
            }
        }

        // End timer function
        function StopStateTimer() {
            if (timerId) {
                clearInterval(timerId);
                timerId = 0;
            }
        }

        // Start timer function
        function StartStateTimer() {
            timerId = setInterval("GetStateAction()", 100);
        }
        //]]>
    </script>

    <asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />
    <asp:Panel ID="pnlConfirmation" runat="server" CssClass="PageContent">
        <table class="GlobalWizard" border="0" cellpadding="0" cellspacing="0">
            <tr class="Top">
                <td class="Left">
                    &nbsp;
                </td>
                <td class="Center" style="width: 840px;">
                    <cms:WizardHeader ID="ucHeaderConfirm" runat="server" TitleVisible="false" DescriptionVisible="false" />
                </td>
                <td class="Right">
                    &nbsp;
                </td>
            </tr>
            <tr class="Middle">
                <td class="Center" colspan="3">
                    <asp:Panel runat="server" ID="Panel1" CssClass="GlobalWizardStep" Height="280">
                        <div style="height: 280px;">
                            <div>
                                <strong>
                                    <asp:Label ID="lblConfirmation" runat="server" CssClass="ContentLabel" EnableViewState="false" />
                                </strong>
                                <br />
                                <div>
                                    <div>
                                        <cms:LocalizedCheckBox ID="chkDeleteDocumentAttachments" runat="server" ResourceString="DeleteSite.DocumentAttachments"
                                            EnableViewState="false" Checked="true" />
                                    </div>
                                    <div>
                                        <cms:LocalizedCheckBox ID="chkDeleteMetaFiles" runat="Server" ResourceString="DeleteSite.MetaFiles"
                                            EnableViewState="false" Checked="true" />
                                    </div>
                                    <div>
                                        <cms:LocalizedCheckBox ID="chkDeleteMediaFiles" runat="Server" ResourceString="DeleteSite.MediaFiles"
                                            EnableViewState="false" Checked="true" />
                                    </div>
                                </div>
                            </div>
                            <br />
                        </div>
                    </asp:Panel>
                    <div class="WizardButtons">
                        <cms:CMSButton ID="btnYes" runat="server" CssClass="SubmitButton" /><cms:CMSButton
                            ID="btnNo" runat="server" CssClass="SubmitButton" />
                    </div>
                </td>
            </tr>
            <tr class="Bottom">
                <td class="Left">
                    &nbsp;
                </td>
                <td class="Center">
                    &nbsp;
                </td>
                <td class="Right">
                    &nbsp;
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnlDeleteSite" runat="server" CssClass="PageContent" Visible="false">
        <table class="GlobalWizard" border="0" cellpadding="0" cellspacing="0">
            <tr class="Top">
                <td class="Left">
                    &nbsp;
                </td>
                <td class="Center" style="width: 840px;">
                    <cms:WizardHeader ID="ucHeader" runat="server" TitleVisible="false" DescriptionVisible="false" />
                </td>
                <td class="Right">
                    &nbsp;
                </td>
            </tr>
            <tr class="Middle">
                <td class="Center" colspan="3">
                    <asp:Panel ID="pnlExportPanel" runat="server">
                        <asp:Panel runat="server" ID="pnlContent" CssClass="GlobalWizardStep" Height="280">
                            <div style="height: 280px;">
                                <asp:Label runat="server" ID="lblFinish" CssClass="InfoLabel" EnableViewState="true" />
                                <asp:Label runat="server" ID="lblLog" CssClass="InfoLabel" EnableViewState="true" />
                            </div>
                        </asp:Panel>
                    </asp:Panel>
                    <div class="WizardButtons">
                        <cms:CMSButton ID="btnOk" runat="server" CssClass="SubmitButton" Enabled="false"
                            RenderScript="true" />
                    </div>
                </td>
            </tr>
            <tr class="Bottom">
                <td class="Left">
                    &nbsp;
                </td>
                <td class="Center">
                    &nbsp;
                </td>
                <td class="Right">
                    &nbsp;
                </td>
            </tr>
        </table>
        <asp:Label ID="lblWarning" runat="server" CssClass="ErrorLabel" /><br />
        <br />
        <asp:Label ID="lblError" runat="server" CssClass="ErrorLabel" /><br />
    </asp:Panel>
    <asp:HiddenField ID="hdnLog" runat="server" EnableViewState="false" />
</asp:Content>
