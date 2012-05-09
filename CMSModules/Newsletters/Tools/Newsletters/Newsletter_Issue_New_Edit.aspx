<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Newsletter_Issue_New_Edit.aspx.cs"
    Inherits="CMSModules_Newsletters_Tools_Newsletters_Newsletter_Issue_New_Edit"
    Theme="Default" EnableEventValidation="false" ValidateRequest="false" MasterPageFile="~/CMSMasterPages/UI/Dialogs/ModalDialogPage.master"
    Title="Newsletter - New issue" %>

<%@ Register Src="~/CMSAdminControls/UI/PageElements/PageTitle.ascx" TagPrefix="cms"
    TagName="PageTitle" %>
<%@ Register Src="Newsletter_ContentEditorHeader.ascx" TagPrefix="cms" TagName="Newsletter_ContentEditorHeader" %>
<%@ Register Src="Newsletter_ContentEditorFooter.ascx" TagPrefix="cms" TagName="Newsletter_ContentEditorFooter" %>
<%@ Register Src="Newsletter_ContentEditor.ascx" TagPrefix="cms" TagName="Newsletter_ContentEditor" %>
<%@ Register Src="~/CMSModules/AdminControls/Controls/MetaFiles/FileList.ascx" TagPrefix="cms"
    TagName="FileList" %>
<%@ Register Src="~/CMSAdminControls/Wizard/Header.ascx" TagPrefix="cms" TagName="WizardHeader" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">

    <script type="text/javascript">
        //<![CDATA[
        var iframeHeight = '350px';
        //]]>
    </script>

    <ajaxToolkit:ToolkitScriptManager ID="manScript" runat="server" />
    <div style="padding: 10px;">
        <asp:Panel runat="server" ID="pnlInfo">
            <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
                Visible="false" />
            <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
                Visible="false" />
        </asp:Panel>
        <table class="GlobalWizard NewsletterWizard" cellspacing="0">
            <tr class="Top">
                <td class="Left">
                    &nbsp;
                </td>
                <td class="Center">
                    <cms:WizardHeader ID="ucHeader" runat="server" />
                </td>
                <td class="Right">
                    &nbsp;
                </td>
            </tr>
            <tr class="Middle">
                <td class="Center" colspan="3">
                    <div id="wzdBody">
                        <table class="Wizard" cellspacing="0" cellpadding="0" border="0" style="border-collapse: collapse;">
                            <tbody>
                                <tr style="height: 100%;">
                                    <td>
                                        <div class="NewsletterWizardStep" style="height: 610px">
                                            <table style="width: 96%" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <div class="NewsletterWizardSubject">
                                                            <table cellpadding="0" width="100%">
                                                                <tr>
                                                                    <td style="text-align: left">
                                                                        <cms:LocalizedLabel ID="lblSubject" runat="server" CssClass="FieldLabel" ResourceString="general.subject"
                                                                            DisplayColon="true" EnableViewState="false" Style="padding: 0 5px; display: inline"
                                                                            AssociatedControlID="txtSubject" />
                                                                        <cms:CMSTextBox ID="txtSubject" runat="server" CssClass="TextBoxField" MaxLength="450"
                                                                            Width="535" />
                                                                    </td>
                                                                    <td style="text-align: right">
                                                                        <asp:CheckBox runat="server" ID="chkShowInArchive" CssClass="ContentCheckBox" Style="padding: 0 5px"
                                                                            TextAlign="Left" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <cms:Newsletter_ContentEditorHeader ID="contentHeader" runat="server" />
                                                        <cms:Newsletter_ContentEditor ID="contentBody" runat="server" IsNewIssue="true" />
                                                        <cms:Newsletter_ContentEditorFooter ID="contentFooter" runat="server" />
                                                        <br />
                                                        <cms:PageTitle ID="AttachmentTitle" runat="server" TitleCssClass="SubTitleHeader" />
                                                        <br />
                                                        <asp:Label ID="lblAttInfo" runat="server" EnableViewState="false" CssClass="InfoLabel" />
                                                        <div class="ClearBorder">
                                                            <cms:FileList ID="AttachmentList" runat="server" Visible="false" />
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="ButtonRow">
                                        <div id="buttonsDiv">
                                            <cms:CMSButton ID="btnSave" runat="server" CssClass="SubmitButton" /><cms:CMSButton
                                                ID="btnNext" runat="server" CssClass="SubmitButton" />
                                        </div>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
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
    </div>
    <asp:HiddenField runat="server" ID="hdnNext" />
    <cms:CMSButton runat="server" ID="btnNextHidden" OnClick="btnNext_Click" CssClass="HiddenButton" />
    <cms:CMSButton runat="server" ID="btnSaveHidden" OnClick="btnSave_Click" CssClass="HiddenButton" />

    <script type="text/javascript">
        //<![CDATA[
        function PasteImage(imageurl) {
            imageHtml = '<img src="' + imageurl + '" />';
            return window.frames['iframeContent'].InsertHTML(imageHtml);
        }

        function RefreshPage() {
            wopener.RefreshPage();
        }
        //]]>
    </script>

</asp:Content>
<asp:Content ID="cntFooter" runat="server" ContentPlaceHolderID="plcFooter">
    <div class="FloatRight">
        <cms:CMSButton ID="btnClose" runat="server" CssClass="SubmitButton" OnClientClick="window.close();RefreshPage();return false;" />
    </div>
</asp:Content>
