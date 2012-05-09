<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_BizForms_Tools_BizForm_Edit_Autoresponder"
    Theme="Default" ValidateRequest="false" EnableEventValidation="false" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master"
    CodeFile="BizForm_Edit_Autoresponder.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/PageElements/PageTitle.ascx" TagName="PageTitle"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/AdminControls/Controls/MetaFiles/FileList.ascx" TagName="FileList"
    TagPrefix="cms" %>
<asp:Content ID="aS" runat="server" ContentPlaceHolderID="plcActions">
    <asp:LinkButton ID="lnkSave" runat="server" CssClass="ContentSaveLinkButton" OnClick="lnkSave_Click">
        <asp:Image ID="imgSave" runat="server" CssClass="NewItemImage" />
        <%=mSave%>
    </asp:LinkButton>
</asp:Content>
<asp:Content ID="plcContent" runat="server" ContentPlaceHolderID="plcContent">
    <asp:Panel ID="pnlUsers" runat="server" CssClass="TabsPageContent">
        <asp:Label ID="lblError" runat="server" CssClass="ErrorLabel" EnableViewState="false"
            Visible="false"></asp:Label>
        <asp:Label ID="lblInfo" runat="server" CssClass="InfoLabel" EnableViewState="false"
            Visible="false"></asp:Label>
        <table>
            <tr>
                <td style="padding-right: 5px;">
                    <cms:LocalizedLabel ID="lblEmailField" runat="server" EnableViewState="False" ResourceString="bizform_edit_autoresponder.lblemailfield" />
                </td>
                <td>
                    <asp:DropDownList ID="drpEmailField" runat="server" CssClass="DropDownField" AutoPostBack="true" />
                </td>
            </tr>
            <asp:PlaceHolder ID="pnlCustomLayout" runat="server">
                <tr>
                    <td>
                        <cms:LocalizedLabel ID="lblEmailFrom" runat="server" EnableViewState="False" ResourceString="bizform_edit_autoresponder.lblEmailFrom" />
                    </td>
                    <td>
                        <cms:CMSTextBox ID="txtEmailFrom" runat="server" MaxLength="200" CssClass="TextBoxField" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <cms:LocalizedLabel ID="lblEmailSubject" runat="server" EnableViewState="False" ResourceString="general.subject"
                            DisplayColon="true" />
                    </td>
                    <td>
                        <cms:CMSTextBox ID="txtEmailSubject" runat="server" MaxLength="200" CssClass="TextBoxField" />
                    </td>
                </tr>
        </table>
        <br />
        <table>
            <tr>
                <td colspan="2" class="GenerateButtonPadding">
                    <cms:LocalizedButton ID="btnGenerateLayout" runat="server" OnClientClick="SetContent(GenerateTableLayout()); return false;"
                        CssClass="XLongButton" ResourceString="Bizform_Edit_Autoresponder.btnGenerateLayout" />
                </td>
            </tr>
            <tr>
                <td rowspan="2">
                    <cms:CMSHtmlEditor ID="htmlEditor" runat="server" Width="550px" Height="300px" />
                </td>
                <td style="vertical-align: top; padding-left: 7px;" class="RightColumn">
                    <cms:LocalizedLabel ID="lblAvailableFields" runat="server" EnableViewState="false"
                        CssClass="AvailableFieldsTitle" ResourceString="Bizform_Edit_Autoresponder.AvailableFields" />
                    <asp:ListBox ID="lstAvailableFields" runat="server" CssClass="FieldsList" Width="152px" />
                </td>
            </tr>
            <tr>
                <td style="vertical-align: bottom; padding-bottom: 3px;" class="RightColumn">
                    <table cellspacing="0" cellpadding="1">
                        <tr>
                            <td>
                                <cms:LocalizedButton ID="btnInsertLabel" runat="server" CssClass="LongButton" ResourceString="Bizform_Edit_Autoresponder.btnInsertLabel" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <cms:LocalizedButton ID="btnInsertInput" runat="server" CssClass="LongButton" ResourceString="Bizform_Edit_Autoresponder.btnInsertInput" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            </asp:PlaceHolder>
        </table>
        <br />
        <cms:PageTitle ID="AttachmentTitle" runat="server" Visible="false" TitleCssClass="SubTitleHeader" />
        <br />
        <cms:FileList ID="AttachmentList" runat="server" Visible="false" />
        <asp:Literal ID="ltlConfirmDelete" runat="server" />
    </asp:Panel>

    <script type="text/javascript">
        //<![CDATA[
        // Insert desired HTML at the current cursor position of the CK editor
        function InsertHTML(htmlString) {
            // Get the editor instance that we want to interact with.
            var oEditor = CKEDITOR.instances['<%=htmlEditor.ClientID%>'];

            // Check the active editing mode.
            if (oEditor.mode == 'wysiwyg') {
                // Insert the desired HTML.
                oEditor.insertHtml(htmlString);
            }
            else
                alert('You must be on WYSIWYG mode!');
        }


        // Set content of the CK editor - replace the actual one
        function SetContent(newContent) {
            // Get the editor instance that we want to interact with.
            var oEditor = CKEDITOR.instances['<%=htmlEditor.ClientID%>'];

            // Set the editor content (replace the actual one).
            oEditor.setData(newContent);
        }


        function PasteImage(imageurl) {
            imageurl = '<img src="' + imageurl + '" />';
            return InsertHTML(imageurl);
        }

        // Returns HTML code with standard table layout
        function GenerateTableLayout() {
            var tableLayout = "";

            // indicates whether any row definition was added to the table
            var rowAdded = false;

            // list of attributes
            var list = document.getElementById("<%=lstAvailableFields.ClientID%>");

            // attributes count
            var optionsCount = list.options.length;

            for (var i = 0; i < optionsCount; i++) {
                tableLayout += "<tr><td>$$label:" + list.options[i].value + "$$</td><td>$$value:" + list.options[i].value + "$$</td></tr>";
                rowAdded = true;
            }

            if (rowAdded) {
                tableLayout = "<table><tbody>" + tableLayout + "</tbody></table>";
            }

            return tableLayout;
        }


        // Insert desired HTML at the current cursor position of the CK editor if it is not already inserted 
        function InsertAtCursorPosition(htmlString) {
            InsertHTML(htmlString);
        }


        function ConfirmDelete() {
            return confirm(document.getElementById('confirmdelete').value);
        }
        //]]>
    </script>

</asp:Content>
