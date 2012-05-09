
function InsertSelectedItem(obj) {
    var wopener = (window.top.opener ? window.top.opener : window.top.dialogArguments);
    if ((wopener) && (obj)) {
        if ((obj.editor_clientid != null) && (obj.editor_clientid != '')) {
            var editor = wopener.document.getElementById(obj.editor_clientid);
            if (editor != null) {
                if ((obj.doc_targetnodeid != null) && (obj.doc_targetnodeid != 'undefined') && (obj.doc_targetnodeid != '')) {
                    editor.value = obj.doc_targetnodeid;
                }
                else {
                    editor.value = obj.doc_nodealiaspath;
                }
                if (editor.onchange != null) {
                    editor.onchange(window.event);
                }
            }
        }
    }
}

function GetSelectedItem(editorId) {

}
