// Bug fix for "__pendingCallbacks is not defined"
function WebForm_CallbackComplete_SyncFixed() {
    if (window.__pendingCallbacks) {
        for (var i = 0; i < window.__pendingCallbacks.length; i++) {
            callbackObject = window.__pendingCallbacks[i];
            if (callbackObject && callbackObject.xmlRequest && (callbackObject.xmlRequest.readyState == 4)) {
                if (!window.__pendingCallbacks[i].async) {
                    __synchronousCallBackIndex = -1;
                }
                window.__pendingCallbacks[i] = null;

                var callbackFrameID = "__CALLBACKFRAME" + i;
                var xmlRequestFrame = document.getElementById(callbackFrameID);
                if (xmlRequestFrame) {
                    xmlRequestFrame.parentNode.removeChild(xmlRequestFrame);
                }

                WebForm_ExecuteCallback(callbackObject);
            }
        }
    }
}

// Page completeness checking on postback
var notLoadedText = 'The page is not yet fully loaded, please wait until it loads.';
var originalUnload;

function __doPostBackWithCheck(eventTarget, eventArgument) {
    var editorsLoaded = true;
    if ((typeof (CKEDITOR) != 'undefined') && ((CKEDITOR.status == 'unloaded') || (CKEDITOR.status == 'loading'))) {
        editorsLoaded = false;
    }

    if (document.pageLoaded && editorsLoaded) {
        originalUnload = document.onunload;

        originalPostback(eventTarget, eventArgument);
    }
    else {
        alert(notLoadedText);
    }
}

function __OnUnload() {
    document.pageLoaded = false;

    originalUnload();
}


// CK editor enhancements
var disabledCKEditors = disabledCKEditors || [];

function CKeditor_OnComplete(editorInstance) {
    if (disabledCKEditors[editorInstance.name]) {
        DisableCKeditor(editorInstance);
    }
    editorInstance.resetDirty();
}

function DisableCKeditor(editorInstance) {
    // Desable key input
    editorInstance.on('key', function(event) {
        event.cancel();
    }, null, null, 0);
    editorInstance.on('selectionChange', function(event) {
        event.cancel();
    }, null, null, 0);
    editorInstance.on('state', function(event) {
        event.cancel();
    }, null, null, 0);
    
    editorInstance.document.$.body.disabled = true;
    if (CKEDITOR.env.ie) {
        editorInstance.document.$.body.contentEditable = false;
    } else {
        editorInstance.document.$.designMode = "off";
    }
    // Disable all commands in wysiwyg mode.
    var commands = editorInstance._.commands;
    for (var name in commands) {
        commands[name].disable();
    }
}

if (typeof (Sys) != "undefined" && typeof (Sys.Application) != "undefined") {
    Sys.Application.notifyScriptLoaded();
}
