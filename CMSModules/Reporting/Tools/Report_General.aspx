<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Reporting_Tools_Report_General"
    Theme="Default" ValidateRequest="false" EnableEventValidation="false" MasterPageFile="~/CMSMasterPages/UI/EmptyPage.master"
    CodeFile="Report_General.aspx.cs" %>

<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextBox" TagPrefix="cms" %>

<%@ Register Src="~/CMSAdminControls/UI/PageElements/PageTitle.ascx" TagName="PageTitle"
    TagPrefix="cms" %>
<%@ Register Src="ItemsList.ascx" TagName="ItemsList" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/AdminControls/Controls/MetaFiles/FileList.ascx" TagName="FileList"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Reporting/FormControls/SelectReportCategory.ascx"
    TagName="SelectReportCategory" TagPrefix="cms" %>
<asp:Content ContentPlaceHolderID="plcContent" ID="content" runat="server">

    <script type="text/javascript">
        //<![CDATA[
        // Insert desired HTML at the current cursor position of the HTML editor
        function InsertHTML(htmlString) {
            // Get the editor instance that we want to interact with.
            var oEditor = CKEDITOR.instances[reporting_htmlTemplateBody];
            // Check the active editing mode.
            if (oEditor.mode == 'wysiwyg') {
                // Insert the desired HTML.
                oEditor.insertHtml(htmlString);
            }
            else
                alert('You must be in WYSIWYG mode!');
            return false;
        }

        function PasteImage(imageurl) {
            imageurl = '<img src="' + imageurl + '" />';
            return InsertHTML(imageurl);
        }
        //]]>
    </script>

    <asp:Literal runat="server" ID="ltlScript" />
    <asp:Panel runat="server" ID="pnlMenu" CssClass="TabsEditMenu">
        <table>
            <tr>
                <td>
                    <%--OnClientClick="return CheckContent();--%>
                    <asp:LinkButton ID="lnkSave" runat="server" CssClass="MenuItemEdit" OnClick="lnkSave_Click"
                        EnableViewState="false">
                        <asp:Image ID="imgSave" runat="server" EnableViewState="false" />
                        <%=mSave%>
                    </asp:LinkButton>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnlContent" runat="server" CssClass="PageContent">
        <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
            Visible="false" />
        <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
            Visible="false" />
        <table style="vertical-align: top; width: 100%;">
            <tr>
                <td class="FieldLabel">
                    <asp:Label runat="server" ID="lblReportDisplayName" EnableViewState="false" />
                </td>
                <td style="width: 100%">
                    <cms:LocalizableTextBox ID="txtReportDisplayName" runat="server" CssClass="TextBoxField" MaxLength="440"
                        EnableViewState="false" />
                    <cms:CMSRequiredFieldValidator ID="rfvReportDisplayName" runat="server" ErrorMessage=""
                        ControlToValidate="txtReportDisplayName:textbox" Display="Dynamic" EnableViewState="false"></cms:CMSRequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="FieldLabel">
                    <asp:Label runat="server" ID="lblReportName" EnableViewState="false" />
                </td>
                <td>
                    <cms:CMSTextBox ID="txtReportName" runat="server" CssClass="TextBoxField" MaxLength="100"
                        EnableViewState="false" />
                    <cms:CMSRequiredFieldValidator ID="rfvReportName" runat="server" ErrorMessage="" ControlToValidate="txtReportName"
                        Display="Dynamic" EnableViewState="false" />
                </td>
            </tr>
            <tr>
                <td class="FieldLabel">
                    <asp:Label runat="server" ID="lblReportCategory" EnableViewState="false" />
                </td>
                <td>
                    <cms:SelectReportCategory ID="selectCategory" runat="server" IsLiveSite="false" />
                </td>
            </tr>
            <tr>
                <td class="FieldLabel">
                    <asp:Label runat="server" ID="lblReportAccess" EnableViewState="false" />
                </td>
                <td>
                    <asp:CheckBox CssClass="CheckBoxMovedLeft" ID="chkReportAccess" runat="server" Checked="True" />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label runat="server" ID="lblLayout" EnableViewState="false" />
                    <cms:CMSHtmlEditor ID="htmlTemplateBody" runat="server" Width="90%" Height="435px"
                        ToolbarSet="Reporting" />
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td class="PageHeaderLine">
                    <asp:Label runat="server" ID="lblGraphs" EnableViewState="false" />
                </td>
                <td class="PageHeaderLine">
                    <cms:ItemsList ID="ilGraphs" runat="server" IsLiveSite="false" />
                </td>
            </tr>
            <tr>
                <td class="PageHeaderLine">
                    <asp:Label runat="server" ID="lblHtmlGraphs" EnableViewState="false" />
                </td>
                <td class="PageHeaderLine">
                    <cms:ItemsList ID="ilHtmlGraphs" runat="server" IsLiveSite="false" />
                </td>
            </tr>
            <tr>
                <td class="PageHeaderLine">
                    <asp:Label runat="server" ID="lblTables" EnableViewState="false" />
                </td>
                <td class="PageHeaderLine">
                    <cms:ItemsList ID="ilTables" runat="server" IsLiveSite="false" />
                </td>
            </tr>
            <tr>
                <td class="PageHeaderLine">
                    <asp:Label runat="server" ID="lblValues" EnableViewState="false" />
                </td>
                <td class="PageHeaderLine">
                    <cms:ItemsList ID="ilValues" runat="server" IsLiveSite="false" />
                </td>
            </tr>
        </table>
        <br />
        <br />
        <br />
        <cms:PageTitle ID="AttachmentTitle" runat="server" TitleText="Attachments" TitleCssClass="SubTitleHeader" />
        <br />
        <cms:FileList ID="attachmentList" runat="server" />
    </asp:Panel>
    <cms:CMSButton runat="server" ID="btnHdnReload" EnableViewState="false" CssClass="HiddenButton" />
</asp:Content>
