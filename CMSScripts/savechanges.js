var frameset = false;
var changed = false;
var allowSubmit = false;

var oldSubmit = null;
var oldLoad = null;

var iframes = null;
var confirmChanges = true;

function AllowChanged(elementId) {
    if (window.checkChangedFields == null) {
        return true;
    }

    for (var i = 0; i < checkChangedFields.length; i++) {
        if (checkChangedFields[i] == elementId) {
            return true;
        }
    }
    return false;
}

function Changed() {
    changed = true;
}

function NotChanged() {
    changed = false;
}

function AllowSubmit() {
    allowSubmit = true;
}

function UnloadPage(evt) {
    if (DataChanged() && !allowSubmit && confirmChanges) {
        return confirmLeave;
    }
}

function CheckChanges() {
    if (DataChanged()) {
        if (confirm(confirmLeave)) {
            changed = false;
            ResetEditorsIsDirty();
            return true;
        }
        else {
            return false;
        }
    }
    else {
        return true;
    }
}

function SubmitAction() {
    if (DataChanged() && !allowSubmit) {
        document.getElementById('saveChanges').value = '1';
    }
    else {
        document.getElementById('saveChanges').value = '0';
    }
    changed = false;
    allowSubmit = true;
    return true;
}

function SubmitPage() {
    if (!DataChanged() || allowSubmit || SubmitAction()) {
        if ((oldSubmit != null) && (oldSubmit != SubmitPage)) {
            oldSubmit();
        }
    }
}

function InitChangesLoad() {
    if (oldLoad != null)
    {
        oldLoad();
    }
    if (window.theForm != null) {
        oldSubmit = theForm.onsubmit;
        theForm.onsubmit = SubmitPage;

        for (i = 0; i < theForm.elements.length; i++) {
            var elem = theForm.elements[i];
            if (AllowChanged(elem.id)) {
                var check = false;
                switch (elem.tagName) {
                    case 'INPUT':
                        check = (elem.type == 'text') || (elem.type == 'checkbox') || (elem.type == 'radio') || (elem.type == 'file');
                        break;

                    case 'TEXTAREA':
                    case 'SELECT':
                        check = true;
                        break;
                }

                if (check) {
                    if (elem.onchange == undefined) {
                        elem.onchange = Changed;
                    }
                    if (elem.onkeyup == undefined) {
                        elem.onkeyup = Changed;
                    }
                }
            }
        }
    }
}

function InitChanges() {
    if (oldLoad == null) {
        oldLoad = window.onload;
        window.onload = InitChangesLoad;
    }
}

function DataChanged() {
    return changed || EditorsChanged();
}

function FullTrim(text) {
    return text.replace(/\s+/g, "");
}

function EditorsChanged() {
    try {
        if ((typeof (CKEDITOR) != 'undefined') && (CKEDITOR.instances != null)) {
            for (var name in CKEDITOR.instances) {
                if (AllowChanged(name)) {
                    var oEditor = CKEDITOR.instances[name];
                    if (oEditor.checkDirty()) {
                        var prevValue = document.getElementById(name).value;
                        var oldText = FullTrim(prevValue);

                        var newText = FullTrim(oEditor.getData());
                        if (oldText != newText) {
                            return true;
                        }
                    }
                }
            }
        }
    }
    catch (ex) {
    }
    return false;
}

function ResetEditorsIsDirty() {
    try {
        if ((typeof (CKEDITOR) != 'undefined') && (CKEDITOR.instances != null)) {
            for (var name in CKEDITOR.instances) {
                if (AllowChanged(name)) {
                    var oEditor = CKEDITOR.instances[name];
                    if (oEditor.checkDirty()) {
                        oEditor.resetDirty();
                    }
                }
            }
        }
    }
    catch (ex) {
    }
    return false;
}

if (typeof (Sys) != "undefined" && typeof (Sys.Application) != "undefined") {
    Sys.Application.notifyScriptLoaded();
}