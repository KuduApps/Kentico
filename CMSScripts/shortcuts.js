var allowShortcuts = true;

$j.ctrl = function(key, callback, args) {
    $j(document).keydown(function(e) {
        if (!args) {
            // IE fix
            args = [];
        }
        if ((e.keyCode == key.charCodeAt(0)) && e.ctrlKey) {
            callback.apply(this, args);
            return false;
        }
    });
};

$j.ctrl('S', function() {
    if (!window.disableShortcuts) {
        if (window.SaveDocument) {
            SaveDocument();
        }
        else if ((window.parent != null) && window.parent.SaveDocument) {
            window.parent.SaveDocument();
        }
    }
});

if (typeof (Sys) != "undefined" && typeof (Sys.Application) != "undefined") {
    Sys.Application.notifyScriptLoaded();
}