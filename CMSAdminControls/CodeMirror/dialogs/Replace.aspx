<%@ Page Title="Replace" Language="C#" MasterPageFile="~/CMSMasterPages/UI/Dialogs/ModalDialogPage.master"
    Theme="Default" AutoEventWireup="true" CodeFile="Replace.aspx.cs" Inherits="CMSAdminControls_CodeMirror_dialogs_Replace" %>

<asp:Content ID="plcContentContent" ContentPlaceHolderID="plcContent" runat="server">

    <script type="text/javascript">
        var editor = null, lastPos = null, lastQuery = null, lastFindPos = null, marked = null;

        function unmark() {
            for (var i = 0; i < marked.length; ++i) {
                marked[i]();
            }
            marked.length = 0;
        }

        function search(editor, text, matchCase) {
            unmark();
            if (!text) return true;
            for (var cursor = editor.getSearchCursor(text, null, !matchCase); cursor.findNext(); ) {
                marked.push(editor.markText(cursor.from(), cursor.to(), "CodeMirror-searched"));
            }

            if (lastQuery != text) lastPos = null;
            var cursor = editor.getSearchCursor(text, lastPos || editor.getCursor(), !matchCase);
            if (!cursor.findNext()) {
                cursor = editor.getSearchCursor(text, null, !matchCase);
                if (!cursor.findNext()) return false;
            }
            
            editor.setSelection(cursor.from(), cursor.to());
            lastQuery = text; lastPos = cursor.to(); lastFindPos = cursor.from();

            return true;
        }

        function findText() {
            if (editor == null) {
                if (window.location.search.indexOf("editorName=") != 1)
                    return false;

                var editorName = window.location.search.substring(12, window.location.search.length);
                editor = window.opener[editorName];
                marked = editor.marked;
            }
                        
            var text = document.getElementById('<%= txtFindWhat.ClientID %>').value;
            var matchCase = document.forms[0]['<%= chkMatchCase.ClientID %>'].checked;

            if (!search(editor, text, matchCase)) {
                window.alert(editor.toolbar.getLocalizedString("TextNotFound", true) + "\n\n" + text);
            }

            document.forms[0]['<%= btnFind.ClientID %>'].focus();

            return false;
        }

        function replace(editor, text, replace, matchCase, all) {
            unmark(editor);
            
            if (!text) return;

            if (lastQuery != text) {
                lastPos = null;
                lastFindPos = null;
            }
            
            var count = 0;
            if (!all) {
                var findPos = lastFindPos || lastPos || editor.getCursor();
                var cursor = editor.getSearchCursor(text, findPos, !matchCase);
                if (cursor.findNext()) {
                    cursor.replace(replace);
                    if (cursor.findNext()) {
                        editor.setSelection(cursor.from(), cursor.to());
                    }
                    lastPos = cursor.to();
                    lastFindPos = cursor.from();

                    count++;
                }
                else {
                    lastFindPos = null;
                }
            }
            else {
                for (var cursor = editor.getSearchCursor(text, { line: 0, ch: 0 }, !matchCase); cursor.findNext(); ) {
                    cursor.replace(replace);
                    count++;
                }

                lastFindPos = null;
            }

            lastQuery = text;

            return count;
        }

        function replaceText() {
            if (window.location.search.indexOf("editorName=") != 1)
                return false;

            var editorName = window.location.search.substring(12, window.location.search.length);
            var editor = window.opener[editorName];

            var from = document.getElementById('<%= txtFindWhat.ClientID %>').value;
            var to = document.getElementById('<%= txtReplaceWith.ClientID %>').value;
            var replaceAll = document.forms[0]['<%= rblReplaceMode.UniqueID %>'][1].checked;
            var matchCase = document.forms[0]['<%= chkMatchCase.ClientID %>'].checked;

            var replaced = replace(editor, from, to, matchCase, replaceAll);
            if (replaced == 0) {
                window.alert(editor.toolbar.getLocalizedString("TextNotFound", true) + "\n\n" + from);
            }
            else if (replaceAll) {
                window.alert(replaced + " " + editor.toolbar.getLocalizedString("XOccurencesReplaced", false));
            }

            return false;
        }

        function focusOnTextBox(textBoxId) {
            var txtBox = document.getElementById(textBoxId);
            if (txtBox != null) {
                try { txtBox.focus(); }
                catch (e) { }
            }
        }
    </script>

    <table class="SHEditorDialogTable">
        <col />
        <tr>
            <td>
                <cms:LocalizedLabel ID="lblFindWhat" runat="server" EnableViewState="false" ResourceString="cmsreplacedialog.findwhat"
                    DisplayColon="true" AssociatedControlID="txtFindWhat" />
            </td>
            <td>
                <cms:CMSTextBox runat="server" ID="txtFindWhat" EnableViewState="false" Text="" Width="98%" />
            </td>
        </tr>
        <tr>
            <td>
                <cms:LocalizedLabel ID="lblReplaceWith" runat="server" EnableViewState="false" ResourceString="cmsreplacedialog.replacewith"
                    DisplayColon="true" AssociatedControlID="txtReplaceWith" />
            </td>
            <td>
                <cms:CMSTextBox runat="server" ID="txtReplaceWith" EnableViewState="false" Text="" Width="98%" />
            </td>
        </tr>
        <tr>
            <td>
                <cms:LocalizedLabel ID="lblReplaceMode" runat="server" EnableViewState="false" ResourceString="cmsreplacedialog.replacemode"
                    DisplayColon="true" />
            </td>
            <td>
                <cms:LocalizedRadioButtonList ID="rblReplaceMode" runat="server" EnableViewState="false"
                    RepeatDirection="Horizontal" UseResourceStrings="true">
                    <asp:ListItem Text="cmsreplacedialog.replacemode.replace" Value="down" Selected="True" />
                    <asp:ListItem Text="cmsreplacedialog.replacemode.replaceall" Value="up" />
                </cms:LocalizedRadioButtonList>
            </td>
        </tr>
        <tr>
            <td>
                <cms:LocalizedLabel ID="lblMatchCase" runat="server" EnableViewState="false" ResourceString="general.matchcase"
                    DisplayColon="true" />
            </td>
            <td>
                <asp:CheckBox ID="chkMatchCase" runat="server" EnableViewState="false" />
            </td>
        </tr>
    </table>

    <script type="text/javascript">
        focusOnTextBox('<%= txtFindWhat.ClientID %>');
    </script>

</asp:Content>
<asp:Content ID="plcFooterContent" ContentPlaceHolderID="plcFooter" runat="server">
    <div class="FloatRight">
        <cms:LocalizedButton ID="btnFind" runat="server" EnableViewState="false" CssClass="SubmitButton"
            OnClientClick="findText(); return false;" ResourceString="general.find" />
        <cms:LocalizedButton ID="btnReplace" runat="server" EnableViewState="false" CssClass="SubmitButton"
            OnClientClick="replaceText(); return false;" ResourceString="general.replace" />
        <cms:LocalizedButton ID="btnClose" runat="server" EnableViewState="false" CssClass="SubmitButton"
            OnClientClick="window.close();" ResourceString="general.close" />
    </div>
</asp:Content>
