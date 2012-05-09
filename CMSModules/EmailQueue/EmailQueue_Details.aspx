<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CMSMasterPages/UI/Dialogs/ModalDialogPage.master"
    Title="EmailQueue - Details" Inherits="CMSModules_EmailQueue_EmailQueue_Details"
    Theme="Default" CodeFile="EmailQueue_Details.aspx.cs" %>

<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <cms:LocalizedLabel ID="lblInfo" runat="server" CssClass="InfoLabel" EnableViewState="false"
        Visible="false" ResourceString="emailqueue.details.emailalreadysent" />
    <asp:PlaceHolder ID="plcDetails" runat="server">
        <div class="PageContent">
            <table border="0" cellpadding="0" cellspacing="5" class="tblEventLogDetail">
                <tr>
                    <td>
                        <cms:LocalizedLabel ID="lblFrom" runat="server" ResourceString="emailqueue.detail.from"
                            DisplayColon="true" EnableViewState="false" />
                    </td>
                    <td>
                        <asp:Label ID="lblFromValue" runat="server" CssClass="FieldLabel" EnableViewState="false" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <cms:LocalizedLabel ID="lblTo" runat="server" ResourceString="emailqueue.detail.to"
                            DisplayColon="true" EnableViewState="false" />
                    </td>
                    <td>
                        <asp:Label ID="lblToValue" runat="server" CssClass="FieldLabel" EnableViewState="false" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <cms:LocalizedLabel ID="lblCc" runat="server" ResourceString="emailqueue.detail.cc"
                            DisplayColon="true" EnableViewState="false" />
                    </td>
                    <td>
                        <asp:Label ID="lblCcValue" runat="server" CssClass="FieldLabel" EnableViewState="false" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <cms:LocalizedLabel ID="lblBcc" runat="server" ResourceString="emailqueue.detail.bcc"
                            DisplayColon="true" EnableViewState="false" />
                    </td>
                    <td>
                        <asp:Label ID="lblBccValue" runat="server" CssClass="FieldLabel" EnableViewState="false" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <cms:LocalizedLabel ID="lblSubject" runat="server" ResourceString="general.subject"
                            DisplayColon="true" EnableViewState="false" />
                    </td>
                    <td>
                        <asp:Label ID="lblSubjectValue" runat="server" CssClass="FieldLabel" EnableViewState="false" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align: top">
                        <cms:LocalizedLabel ID="lblBody" runat="server" ResourceString="emailqueue.detail.body"
                            DisplayColon="true" EnableViewState="false" />
                    </td>
                    <td>
                        <asp:Literal ID="ltlBodyValue" runat="server" EnableViewState="false" Visible="false" />
                        <cms:CMSHtmlEditor ID="htmlTemplateBody" runat="server" Width="770px" Height="300px"
                            Visible="false" Enabled="false" ToolbarSet="Disabled" />
                    </td>
                </tr>
                <asp:PlaceHolder ID="plcAttachments" runat="server">
                    <tr>
                        <td style="vertical-align: top">
                            <cms:LocalizedLabel ID="lblAttachments" runat="server" ResourceString="emailqueue.detail.attachments"
                                DisplayColon="true" EnableViewState="false" />
                        </td>
                        <td>
                            <asp:Panel ID="pnlAttachmentsList" runat="server" />
                        </td>
                    </tr>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="plcErrorMessage" runat="server">
                    <tr>
                        <td>
                            <cms:LocalizedLabel ID="lblErorrMessage" runat="server" ResourceString="emailqueue.detail.errormessage"
                                DisplayColon="true" EnableViewState="false" />
                        </td>
                        <td>
                            <asp:Label ID="lblErrorMessageValue" runat="server" CssClass="FieldLabel" EnableViewState="false" />
                        </td>
                    </tr>
                </asp:PlaceHolder>
            </table>
        </div>
    </asp:PlaceHolder>
</asp:Content>
<asp:Content ID="cntFooter" ContentPlaceHolderID="plcFooter" runat="server">
    <div class="FloatLeft">
        <cms:CMSButton runat="server" ID="btnPrevious" CssClass="SubmitButton" Enabled="false"
            EnableViewState="false" /><cms:CMSButton runat="server" ID="btnNext" CssClass="SubmitButton"
                Enabled="false" EnableViewState="false" /></div>
    <div class="FloatRight">
        <cms:LocalizedButton ID="btnClose" runat="server" CssClass="SubmitButton" ResourceString="general.close"
            OnClientClick="window.close();return false;" EnableViewState="false" />
    </div>
</asp:Content>