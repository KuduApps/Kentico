<%@ Page Title="Search" Language="C#" MasterPageFile="~/CMSMasterPages/UI/Dialogs/ModalDialogPage.master"
    Theme="Default" AutoEventWireup="true" CodeFile="Search.aspx.cs" Inherits="CMSAdminControls_CodeMirror_dialogs_Search" %>

<asp:Content ID="plcContentContent" ContentPlaceHolderID="plcContent" runat="server">

    <script type="text/javascript">
        var editor = null, lastPos = null, lastQuery = null, marked = null;
        
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
            lastQuery = text; lastPos = cursor.to();

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
            
            var text = document.getElementById('<%= txtSearchFor.ClientID %>').value;
            var matchCase = document.forms[0]['<%= chkMatchCase.ClientID %>'].checked;

            if (!search(editor, text, matchCase)) {
                window.alert(editor.toolbar.getLocalizedString("TextNotFound", true) + "\n\n" + text);
            }

            document.forms[0]['<%= btnSearch.ClientID %>'].focus();

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
                <cms:LocalizedLabel ID="lblSearchFor" runat="server" EnableViewState="false" ResourceString="cmssearchdialog.searchfor"
                    AssociatedControlID="txtSearchFor" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtSearchFor" runat="server" EnableViewState="false" Text="" Width="98%" />
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
        focusOnTextBox('<%= txtSearchFor.ClientID %>');
    </script>

</asp:Content>
<asp:Content ID="plcFooterContent" ContentPlaceHolderID="plcFooter" runat="server">
    <div class="FloatRight">
        <cms:LocalizedButton ID="btnSearch" runat="server" EnableViewState="false" CssClass="SubmitButton"
            OnClientClick="findText(); return false;" ResourceString="general.search" />
        <cms:LocalizedButton ID="btnClose" runat="server" EnableViewState="false" CssClass="SubmitButton"
            OnClientClick="window.close();" ResourceString="general.close" />
    </div>
</asp:Content>
