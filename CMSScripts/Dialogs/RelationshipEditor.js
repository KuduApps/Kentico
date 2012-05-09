
function InsertSelectedItem(obj) {
    var wopener = (window.top.opener ? window.top.opener : window.top.dialogArguments);
    if ((wopener) && (obj)) {
        if ((obj.editor_clientid != null) && (obj.editor_clientid != '')) {
            var split = obj.editor_clientid.split(';');
            if (split.length == 2) {
                var editorClientId = split[0];
                var hiddenFieldId = split[1];

                // Get elements
                var editor = wopener.document.getElementById(editorClientId);
                var hiddenField = wopener.document.getElementById(hiddenFieldId);

                // Set node alias path
                if (editor != null) {
                    editor.value = obj.doc_nodealiaspath;
                }

                // Set node id
                if (hiddenFieldId != null) {
                    hiddenField.value = obj.doc_targetnodeid;
                }

                // Refresh
                if (wopener.RefreshRelatedPanel) {
                    wopener.RefreshRelatedPanel(editorClientId);
                }
            }
        }
    }
}

function GetSelectedItem(editorId) {

}
