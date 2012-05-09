
function Close() {
    window.close();
}

function PassiveRefresh() {
    if (wopener.PassiveRefresh) {
        wopener.PassiveRefresh();
        Refresh();
    }
}

function Refresh() {
    if (wopener.UpdatePage) {
        wopener.UpdatePage();
    }
    else {
        wopener.location.replace(wopener.location);
    }
}

function InitRefresh(clientId, fullRefresh, refreshTree, guid, action) {
    eval("if (wopener.InitRefresh_" + clientId + "){wopener.InitRefresh_" + clientId + "('', " + fullRefresh + ", " + refreshTree + ", 'attachmentguid|" + guid + "', '" + action + "'); } else { if(" + fullRefresh + "){ FullRefresh('" + clientId + "','attachmentguid|" + guid + "'); } else { Refresh(); } }");
}

// Refresh using postback
function FullRefresh(clientId, guid) {
    eval("if (wopener.FullPageRefresh_" + clientId + "){ wopener.FullPageRefresh_" + clientId + "('" + guid + "')}else{Refresh();}");
}

// Show or hide properties 
function ShowProperties(showProperties, elementId, labelId) {
    var divProperties = document.getElementById('divProperties');
    var imageFrame = parent.frames['imageFrame'];

    if ((divProperties != null) && (imageFrame != null) && (elementId != null)) {
        if (showProperties) {
            divProperties.style.display = 'block';
            imageFrame.frameElement.style.display = 'none';
            document.getElementById(elementId).value = 'true';
        }
        else {
            divProperties.style.display = 'none';
            imageFrame.frameElement.style.display = 'block';
            document.getElementById(elementId).value = 'false';
            // Hide info label of properties
            var labelInfo = document.getElementById(labelId);
            if (labelInfo != null) {
                labelInfo.style.display = 'none';
            }
        }
    }
}

function UnloadTrigger(e) {
    if (wopener.EditDialogStateUpdate) {
        wopener.EditDialogStateUpdate('false');
    }
    if (!window.skipCloseConfirm && window.discardChangesConfirmation) {

        e = e || window.event;

        //e.cancelBubble is supported by IE - this will kill the bubbling process.
        e.cancelBubble = true;
        e.returnValue = window.discardChangesConfirmation;
        //e.stopPropagation works in Firefox.
        if (e.stopPropagation) {
            e.stopPropagation();
            e.preventDefault();
        }

        //return works for Chrome and Safari
        return window.discardChangesConfirmation;
    }
}

function UpdateTrimCoords(c) {
    txtCropX.val(c.x);
    txtCropY.val(c.y);
    txtCropWidth.val(c.w);
    txtCropHeight.val(c.h);
}

function UpdateTrim() {
    var x1 = parseInt(txtCropX.val(), 10);
    var y1 = parseInt(txtCropY.val(), 10);
    var x2 = x1 + parseInt(txtCropWidth.val(), 10);
    var y2 = y1 + parseInt(txtCropHeight.val(), 10);

    if ((frames['imageFrame'] != null) && frames['imageFrame'].updateTrim) {
        frames['imageFrame'].updateTrim(x1, y1, x2, y2);
    }
}

function OnKeyDown(e) {
    if (!window.keyPressed) {
        var e = window.event || e
        var code = e.keyCode || e.which;

        // Shift
        if (code == 16) {
            UpdateTrim();
            if (chkCropLock.checked) {
                LockAspectRatio(false);
            }
            else {
                LockAspectRatio(true);
            }
            window.keyPressed = true;
        }
        // Ctrl
        if (code == 17) {
            var width = parseInt(txtCropWidth.val(), 10);
            var height = parseInt(txtCropHeight.val(), 10);
            var max = Math.max(width, height);

            txtCropWidth.val(max);
            txtCropHeight.val(max);

            UpdateTrim();
            LockAspectRatio(true);
            window.keyPressed = true;
        }
    }
}

function OnKeyUp(e) {
    if (window.keyPressed) {
        var e = window.event || e
        var code = e.keyCode || e.which;
        // Shift
        if (code == 16) {
            UpdateTrim();
            if (chkCropLock.checked) {
                LockAspectRatio(true);
            }
            else {
                LockAspectRatio(false);
            }
            window.keyPressed = false;
        }
        // Ctrl
        if (code == 17) {
            UpdateTrim();
            if (!chkCropLock.checked) {
                LockAspectRatio(false);
            }
            window.keyPressed = false;
        }
    }
}

function LockAspectRatio(lock) {
    var width = 0, height = 0;
    if (lock) {
        width = parseInt(txtCropWidth.val(), 10);
        height = parseInt(txtCropHeight.val(), 10);
    }
    if ((frames['imageFrame'] != null) && frames['imageFrame'].lockAspectRatio) {
        frames['imageFrame'].lockAspectRatio(lock, width, height);
    }
}

function InitCrop() {
    if ((frames['imageFrame'] != null) && frames['imageFrame'].initCrop) {
        frames['imageFrame'].initCrop();
    }
    else {
        setTimeout('InitCrop()', 100);
    }
}

function DestroyCrop() {
    if ((frames['imageFrame'] != null) && frames['imageFrame'].destroyCrop) {
        frames['imageFrame'].destroyCrop();
    }
}

function InitializeEditor() {
    window.keyPressed = false;
    window.initializeCrop = false;
    window.txtCropX = jQuery('input[id$=txtCropX]');
    window.txtCropY = jQuery('input[id$=txtCropY]');
    window.txtCropWidth = jQuery('input[id$=txtCropWidth]');
    window.txtCropHeight = jQuery('input[id$=txtCropHeight]');
    window.chkCropLock = jQuery('input[id$=chkCropLock]')[0];

    window.onbeforeunload = UnloadTrigger;

    // Initialize crop after postback.
    if (jQuery('.MenuHeaderItemSelected').find('.Trim').length && jQuery('input[id$=btnCrop]:enabled').length) {
        window.initializeCrop = true;
    }

    jQuery('.HeaderInner').click(function() {
        // Initialize only if OK button for crop is enabled
        if (jQuery(this).hasClass('Trim') && jQuery('input[id$=btnCrop]:enabled').length) {
            InitCrop();
            jQuery(window)
                .keydown(function(e) { OnKeyDown(e) })
                .keyup(function(e) { OnKeyUp(e) });

            jQuery(frames['imageFrame'].document)
                .keydown(function(e) { OnKeyDown(e) })
                .keyup(function(e) { OnKeyUp(e) });
        }
        else {
            DestroyCrop();
            // Clear events
            jQuery(window).unbind('keydown').unbind('keyup');
            jQuery(frames['imageFrame'].document).unbind('keydown').unbind('keyup');
        }
    });

    if (jQuery.browser.msie) {
        jQuery('input[id$=chkCropLock]').click(function() {
            LockAspectRatio(this.checked);
        });
    }
    else {
        jQuery('input[id$=chkCropLock]').change(function() {
            LockAspectRatio(this.checked);
        });
    }

    jQuery('input[id$=txtCropX], input[id$=txtCropY], input[id$=txtCropWidth], input[id$=txtCropHeight]').change(function() {
        UpdateTrim();
    });

    jQuery('input[id$=btnCropReset]').click(function() {
        LockAspectRatio(false);
        DestroyCrop();
        InitCrop();

        txtCropX.val('0');
        txtCropY.val('0');
        txtCropWidth.val('0');
        txtCropHeight.val('0');
        chkCropLock.checked = false;
        jQuery('span[id$=lblCropError]').hide();

        return false;
    });

    jQuery('input[id$=btnCropOk]').click(function() {
        var x = parseInt(txtCropX.val(), 10);
        var y = parseInt(txtCropY.val(), 10);
        var w = parseInt(txtCropWidth.val(), 10);
        var h = parseInt(txtCropHeight.val(), 10);

        if ((x >= 0) && (y >= 0) && (w > 0) && (h > 0)) {
            ApplyTrim();
        }
        return false;
    });

    // IE
    jQuery('a[id$=lblRotate90Left]').click(function() { window.skipCloseConfirm = true; });
    jQuery('a[id$=lblRotate90Right]').click(function() { window.skipCloseConfirm = true; });
    jQuery('a[id$=lblFlipHorizontal]').click(function() { window.skipCloseConfirm = true; });
    jQuery('a[id$=lblFlipVertical]').click(function() { window.skipCloseConfirm = true; });
    jQuery('a[id$=lblBtnGrayscale]').click(function() { window.skipCloseConfirm = true; });
}