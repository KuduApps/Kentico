<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Newsletters_Tools_Templates_NewsletterTemplate_Edit"
    Theme="Default" ValidateRequest="false" EnableEventValidation="false" CodeFile="NewsletterTemplate_Edit.aspx.cs" %>

<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextBox"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/PageElements/PageTitle.ascx" TagName="PageTitle"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSFormControls/Inputs/LargeTextArea.ascx" TagName="LargeTextArea"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/AdminControls/Controls/MetaFiles/FileList.ascx" TagName="FileList"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/Macros/MacroSelector.ascx" TagName="MacroSelector"
    TagPrefix="cms" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Tools - Newsletter template edit</title>
    <style type="text/css">
        body
        {
            margin: 0px;
            padding: 0px;
            height: 100%;
        }
    </style>
</head>
<body class="TabsBody <%=mBodyClass%>">
    <form id="form1" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="manScript" runat="server" />
    <asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />
    <asp:Panel ID="pnlBody" runat="server" CssClass="TabsPageBody">
        <asp:Panel runat="server" ID="pnlScroll" CssClass="TabsPageScrollArea">
            <asp:Panel ID="pnlUsers" runat="server" CssClass="TabsPageContent">
                <asp:Panel ID="pnlHeader" runat="server" CssClass="PageHeader">
                    <cms:PageTitle ID="PageTitle" runat="server" HelpTopicName="newsletter_edit" />
                </asp:Panel>
                <asp:Panel ID="pnlActions" runat="server" CssClass="PageHeaderLine" EnableViewState="false">
                    <asp:LinkButton ID="lnkSave" runat="server" CssClass="ContentSaveLinkButton" OnClick="lnkSave_Click">
                        <asp:Image ID="imgSave" runat="server" CssClass="NewItemImage" />
                        <%=mSave%>
                    </asp:LinkButton>
                    &nbsp;&nbsp;
                    <asp:LinkButton ID="lnkSpellCheck" runat="server" CssClass="ContentSaveLinkButton">
                        <asp:Image ID="imgSpellCheck" runat="server" CssClass="NewItemImage" />
                        <%=mSpellCheck%>
                    </asp:LinkButton>
                </asp:Panel>
                <asp:Panel ID="pnlContent" runat="server" CssClass="PageContent">
                    <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
                        Visible="false" />
                    <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
                        Visible="false" />
                    <table style="vertical-align: top" cellspacing="5">
                        <tr>
                            <td class="FieldLabel" style="width: auto;">
                                <cms:LocalizedLabel runat="server" ID="lblTemplateDisplayName" EnableViewState="false"
                                    ResourceString="general.displayname" DisplayColon="true" />
                            </td>
                            <td>
                                <cms:LocalizableTextBox ID="txtTemplateDisplayName" runat="server" CssClass="TextBoxField"
                                    MaxLength="250" />
                                <cms:CMSRequiredFieldValidator ID="rfvTemplateDisplayName" runat="server" ControlToValidate="txtTemplateDisplayName:textbox"
                                    Display="dynamic" EnableViewState="false"></cms:CMSRequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="FieldLabel">
                                <cms:LocalizedLabel runat="server" ID="lblTemplateName" EnableViewState="false" ResourceString="general.codename"
                                    DisplayColon="true" />
                            </td>
                            <td>
                                <cms:CMSTextBox ID="txtTemplateName" runat="server" CssClass="TextBoxField" MaxLength="250" />
                                <cms:CMSRequiredFieldValidator ID="rfvTemplateName" runat="server" ControlToValidate="txtTemplateName"
                                    Display="dynamic" EnableViewState="false"></cms:CMSRequiredFieldValidator>
                            </td>
                        </tr>
                        <asp:PlaceHolder runat="server" ID="plcSubject" EnableViewState="false" Visible="false">
                            <tr>
                                <td class="FieldLabel">
                                    <cms:LocalizedLabel runat="server" ID="lblTemplateSubject" ResourceString="general.subject"
                                        DisplayColon="true" EnableViewState="false" />
                                </td>
                                <td>
                                    <cms:CMSTextBox ID="txtTemplateSubject" runat="server" CssClass="TextBoxField" MaxLength="250" />
                                </td>
                            </tr>
                        </asp:PlaceHolder>
                        <tr>
                            <td colspan="2">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="FieldLabel">
                                <cms:LocalizedLabel ID="lblTemplateHeader" runat="server" Text="Label" ResourceString="general.htmlheader"
                                    DisplayColon="true" EnableViewState="false" />
                            </td>
                            <td>
                                <cms:LargeTextArea ID="txtTemplateHeader" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="FieldLabel" style="vertical-align: top; padding-top: 5px;">
                                <cms:LocalizedLabel ID="lblTemplateBody" runat="server" ResourceString="general.body"
                                    DisplayColon="true" EnableViewState="false" />
                            </td>
                            <td class="EditingFormControl" style="width: auto;">
                                <cms:CMSHtmlEditor ID="htmlTemplateBody" runat="server" Width="770px" Height="400px" />
                                <table width="100%" cellspacing="0" cellpadding="2">
                                    <tr>
                                        <td style="vertical-align: top;">
                                            <cms:LocalizedLabel ID="lblInsertField" runat="server" EnableViewState="false" ResourceString="NewsletterTemplate_Edit.TemplateInsertFieldLabel" /><br />
                                            <asp:DropDownList ID="lstInsertField" runat="server" CssClass="SourceFieldDropDown" />
                                            <cms:LocalizedButton ID="btnInsertField" runat="server" CssClass="ContentButton"
                                                OnClientClick="InsertAtCursorPosition('{%' + document.getElementById('lstInsertField').value + '%}'); return false;"
                                                ResourceString="NewsletterTemplate_Edit.Insert" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top;">
                                            <cms:MacroSelector ID="macroSelectorElm" runat="server" IsLiveSite="false" />
                                        </td>
                                    </tr>
                                    <asp:PlaceHolder ID="pnlEditableRegion" runat="server" Visible="false">
                                        <tr>
                                            <td colspan="2">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table cellspacing="0" cellspacing="0">
                                                    <tr>
                                                        <td>
                                                            <cms:LocalizedLabel ID="lblInsertEditableRegion" runat="server" Visible="false" EnableViewState="false"
                                                                ResourceString="NewsletterTemplate_Edit.TemplateInsertEditRegLabel" />
                                                        </td>
                                                        <td>
                                                            <cms:LocalizedLabel ID="lblWidth" runat="server" EnableViewState="false" ResourceString="NewsletterTemplate_Edit.TemplateEditRegWidthLabel" />
                                                        </td>
                                                        <td>
                                                            <cms:LocalizedLabel ID="lblHeight" runat="server" EnableViewState="false" ResourceString="NewsletterTemplate_Edit.TemplateEditRegHeightLabel" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="vertical-align: top;">
                                                            <cms:CMSTextBox ID="txtName" runat="server" />
                                                        </td>
                                                        <td style="vertical-align: top;">
                                                            <cms:CMSTextBox ID="txtWidth" runat="server" CssClass="ShortTextBox" />
                                                        </td>
                                                        <td>
                                                            <cms:CMSTextBox ID="txtHeight" runat="server" CssClass="ShortTextBox" /><cms:LocalizedButton
                                                                ID="btnInsertEditableRegion" runat="server" CssClass="ContentButton" OnClientClick="InsertEditableRegion(); return false;"
                                                                Visible="false" ResourceString="NewsletterTemplate_Edit.Insert" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </asp:PlaceHolder>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="FieldLabel">
                                <cms:LocalizedLabel ID="lblTemplateFooter" runat="server" ResourceString="general.htmlfooter"
                                    DisplayColon="true" EnableViewState="false" />
                            </td>
                            <td>
                                <cms:LargeTextArea ID="txtTemplateFooter" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="FieldLabel" style="vertical-align: top; padding-top: 5px;">
                                <cms:LocalizedLabel ID="lblTemplateStyleSheetText" runat="server" EnableViewState="false"
                                    ResourceString="NewsletterTemplate_Edit.TemplateStylesheetTextLabel" />
                            </td>
                            <td>
                                <cms:ExtendedTextArea ID="txtTemplateStyleSheetText" runat="server" EnableViewState="false"
                                    EditorMode="Advanced" Language="CSS" Height="200px" Width="770px" /><br />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                            </td>
                        </tr>
                    </table>
                    <br />
                    <cms:PageTitle ID="AttachmentTitle" runat="server" TitleCssClass="SubTitleHeader" />
                    <br />
                    <div class="ClearBorder">
                        <cms:FileList ID="AttachmentList" runat="server" />
                    </div>
                </asp:Panel>
            </asp:Panel>
        </asp:Panel>
    </asp:Panel>
    </form>

    <script type="text/javascript">
        // Insert desired HTML at the current cursor position of the HTML editor
        function InsertHTML(htmlString) {
            // Get the editor instance that we want to interact with.
            var oEditor = CKEDITOR.instances['htmlTemplateBody'];
            // Check the active editing mode.
            if (oEditor.mode == 'wysiwyg') {
                // Insert the desired HTML.
                oEditor.insertHtml(htmlString);
            }
            else
                alert('You must be on WYSIWYG mode!');
            return false;
        }

        function PasteImage(imageurl) {
            imageurl = '<img src="' + imageurl + '" />';
            return InsertHTML(imageurl);
        }

        // Insert desired HTML at the current cursor position of the CK editor if it is not already inserted 
        function InsertAtCursorPosition(htmlString) {
            InsertHTML(htmlString);
            return false;
        }

        function InsertEditableRegion() {
            if (document.getElementById('txtName').value == '') {
                alert(emptyNameMsg);
                return;
            }

            var region = "$$";
            region += document.getElementById('txtName').value + ":";
            region += document.getElementById('txtWidth').value + ":";
            region += document.getElementById('txtHeight').value + "$$";
            InsertHTML(region);
        }
            
    </script>

</body>
</html>
