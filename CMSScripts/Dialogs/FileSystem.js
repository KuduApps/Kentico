function InsertSelectedItem(obj) {
    var wopener = (window.top.opener ? window.top.opener : window.top.dialogArguments);
    if ((wopener) && (obj)) {
        var path = null;
        if ((obj.item_path) && (obj.item_path != '')) {
            path = obj.item_path;
        }
        if ((obj.editor_clientid != null) && (obj.editor_clientid != '')) {
            var editor = wopener.document.getElementById(obj.editor_clientid);
            if (editor != null) {
                if (path != null) {
                    if (editor.value != null) {
                        editor.value = path;
                        editor.onchange();
                        
                    }
                }
            }
        }
    }
}

function GetSelectedItem(editorId) {
    var obj = null;
    if ((editorId) && (editorId != '')) {
        var wopener = (window.top.opener ? window.top.opener : window.top.dialogArguments);
        if (wopener) {
            var editor = wopener.document.getElementById(editorId);
            if ((editor != null) && (editor.value) && (editor.value != '')) {
                obj = new Object();
                obj.item_path = editor.value;
                
            }
        }
    }
    return obj;
}