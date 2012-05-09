<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_BizForms_Tools_BizForm_Edit_NotificationEmail"
    Theme="Default" ValidateRequest="false" CodeFile="BizForm_Edit_NotificationEmail.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" enableviewstate="false">
    <title>Bizform - Notification e-mail</title>
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
    <asp:Panel ID="pnlBody" runat="server" CssClass="TabsPageBody">
        <asp:Panel runat="server" ID="pnlScroll" CssClass="TabsPageScrollArea">
            <asp:Panel ID="pnlUsers" runat="server" CssClass="TabsPageContent">
                <asp:Panel runat="server" ID="pnlMenu" CssClass="PageHeaderLine">
                    <asp:LinkButton ID="lnkSave" runat="server" CssClass="ContentSaveLinkButton" OnClick="lnkSave_Click">
                        <asp:Image ID="imgSave" runat="server" CssClass="NewItemImage" />
                        <%=mSave%>
                    </asp:LinkButton>
                </asp:Panel>
                <asp:Panel ID="pnlContent" runat="server" CssClass="PageContent">
                    <asp:Label ID="lblError" runat="server" CssClass="ErrorLabel" EnableViewState="false"
                        Visible="false"></asp:Label>
                    <asp:Label ID="lblInfo" runat="server" CssClass="InfoLabel" EnableViewState="false"
                        Visible="false"></asp:Label>
                    <asp:CheckBox ID="chkSendToEmail" runat="server" AutoPostBack="true" OnCheckedChanged="chkSendToEmail_CheckedChanged" /><br />
                    <br />
                    <table>
                        <tr>
                            <td>
                                <cms:LocalizedLabel ID="lblFromEmail" runat="server" EnableViewState="False" ResourceString="BizFormGeneral.lblFromEmail" />
                            </td>
                            <td>
                                <cms:CMSTextBox ID="txtFromEmail" runat="server" MaxLength="200" CssClass="TextBoxField" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <cms:LocalizedLabel ID="lblToEmail" runat="server" EnableViewState="False" ResourceString="BizFormGeneral.ToEmail" />
                            </td>
                            <td>
                                <cms:CMSTextBox ID="txtToEmail" runat="server" MaxLength="200" CssClass="TextBoxField" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <cms:LocalizedLabel ID="lblSubject" runat="server" EnableViewState="False" ResourceString="general.subject"
                                    DisplayColon="true" />
                            </td>
                            <td>
                                <cms:CMSTextBox ID="txtSubject" runat="server" MaxLength="250" CssClass="TextBoxField" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkAttachDocs" runat="server" AutoPostBack="false" />
                            </td>
                        </tr>
                    </table>
                    <div>
                        <asp:CheckBox ID="chkCustomLayout" runat="server" AutoPostBack="true" OnCheckedChanged="chkCustomLayout_CheckedChanged" />
                    </div>
                    <asp:Panel ID="pnlCustomLayout" runat="server">
                        <table cellpadding="0" cellspacing="0" border="0">
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
                                    <asp:ListBox ID="lstAvailableFields" runat="server" CssClass="FieldsList" Height="230" />
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align: bottom;" class="RightColumn">
                                    <table cellspacing="0" cellpadding="1">
                                        <tr>
                                            <td>
                                                <cms:LocalizedButton ID="btnInsertLabel" runat="server" CssClass="LongButton" OnClientClick="InsertAtCursorPosition('$$label:' + document.getElementById('lstAvailableFields').value + '$$'); return false;"
                                                    ResourceString="Bizform_Edit_Autoresponder.btnInsertLabel" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <cms:LocalizedButton ID="btnInsertInput" runat="server" CssClass="LongButton" OnClientClick="InsertAtCursorPosition('$$value:' + document.getElementById('lstAvailableFields').value + '$$'); return false;"
                                                    ResourceString="Bizform_Edit_Autoresponder.btnInsertInput" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Literal ID="ltlConfirmDelete" runat="server" />
                </asp:Panel>
            </asp:Panel>
        </asp:Panel>
    </asp:Panel>
    </form>

    <script type="text/javascript">
        //<![CDATA[
        // Insert desired HTML at the current cursor position of the CK editor
        function InsertHTML(htmlString) {
            // Get the editor instance that we want to interact with.
            var oEditor = CKEDITOR.instances['htmlEditor'];

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
            var oEditor = CKEDITOR.instances['htmlEditor'];

            // Set the editor content (replace the actual one).
            oEditor.setData(newContent);
        }


        // Returns HTML code with standard table layout
        function GenerateTableLayout() {
            var tableLayout = "";

            // indicates whether any row definition was added to the table
            var rowAdded = false;

            // list of attributes
            var list = document.getElementById("lstAvailableFields");

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

</body>
</html>
