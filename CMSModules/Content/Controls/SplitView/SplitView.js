// Frames variables
var FSP_f1S = null;
var FSP_f1Url = null;
var FSP_f1vS = null;
var FSP_f2S = null;
var FSP_f2Url = null;
var FSP_bUrl = null;
var FSP_sUrl = null;
var FSP_fsS = null;
var FSP_mFramesetS = null;
var FSP_vFramesetS = null;
var FSP_tHeight = 0;
var FSP_synchronize = 0;
var FSP_displaySplitMode = 0;
var FSP_ModeEnum =
{
    // Vertical split view
    Vertical: { value: 0, name: "Vertical" },
    // Horizontal split view
    Horizontal: { value: 1, name: "Horizontal" }
}

var FSP_mode = FSP_ModeEnum.Vertical.value;

// Toolbar variables
var FSP_syncId = null;
var FSP_selectId = null;
var FSP_culture = null;
var FSP_checkSyncImgUrl = null;
var FSP_uncheckSyncImgUrl = null;
var FSP_divHorizontalId = null;
var FSP_divVerticalId = null;

// Binding variables for scrolling
var FSP_frameF1 = null;
var FSP_frameF2 = null;
var FSP_bindElement1 = null;
var FSP_bindElement2 = null;
var FSP_bindEventOnBody = false;
var FSP_lastScrollTopF1 = 0;
var FSP_lastScrollTopF2 = 0;
var FSP_lastScrollLeftF1 = 0;
var FSP_lastScrollLeftF2 = 0;


// Initializes frame elements and binds 'scroll' events.
function InitSyncScroll(frameElement, body, refreshSameCulture, unbind) {
    if (frameElement != null) {
        FSP_bindEventOnBody = body;

        // URL prefix of 'frame2'
        var urlPrefix = FSP_cntPref.substring(0, FSP_cntPref.indexOf('##c##'));
        var url = frameElement.contentWindow.location.href;
        // Indicates if frameElement is 'frame2'
        var isFrame2 = (url.toLowerCase().search(urlPrefix.toLowerCase()) >= 0);

        // Set frame1
        if ((FSP_frameF1 == null) && !isFrame2) {
            FSP_frameF1 = frameElement;
        }
        // Set frame2
        else if ((FSP_frameF2 == null) && isFrame2) {
            FSP_frameF2 = frameElement;
        }

        // Bind 'scroll' events
        if ((FSP_frameF1 != null) && (FSP_frameF2 != null)) {
            var frameName = null;
            if (refreshSameCulture) {
                if (!isFrame2) {
                    frameName = 'frame2';
                }
                else {
                    if (FSP_mode == FSP_ModeEnum.Horizontal.value) {
                        frameName = 'frame1';
                    } else if (FSP_mode == FSP_ModeEnum.Vertical.value) {
                        frameName = 'frame1Vertical';
                    }
                }
            }

            if (refreshSameCulture) {
                FSP_RefreshSameCulture(frameName, unbind);
            }
            else {
                FSP_BindScrollElements();
            }
        }
    }
}


// Binds 'scroll' event on elements.
function FSP_BindScrollElements() {

    FSP_ClearLastValues();

    // Browser version
    var browserVersion = $j.browser.version;
    // Indicates if browser is Internet explorer
    var isIE = $j.browser.msie;
    // Indicates if browser is Internet explorer version 8
    var isIE8 = (browserVersion >= 8.0) && (browserVersion < 9.0);

    // Internet explorer
    if (isIE && !isIE8 && FSP_bindEventOnBody) {
        if (FSP_bindElement1 == null) {
            FSP_bindElement1 = $j($j(FSP_frameF1).get(0).contentWindow.document.body);
        }
        if (FSP_bindElement2 == null) {
            FSP_bindElement2 = $j($j(FSP_frameF2).get(0).contentWindow.document.body);
        }
    }
    // Others explorers
    else {
        // Get content window of frame1 and frame2
        if (FSP_bindElement1 == null) {
            FSP_bindElement1 = $j($j(FSP_frameF1).get(0).contentWindow);
        }
        if (FSP_bindElement2 == null) {
            FSP_bindElement2 = $j($j(FSP_frameF2).get(0).contentWindow);
        }
    }

    // Bind 'scroll' events
    FSP_bindElement1.scroll(function() {
        try {
            if (FSP_IsSyncEnabled()) {
                var scrollTopF1 = FSP_bindElement1.scrollTop();
                var scrollLeftF1 = FSP_bindElement1.scrollLeft();

                if (FSP_lastScrollTopF2 != scrollTopF1) {
                    FSP_lastScrollTopF2 = scrollTopF1;
                    FSP_bindElement2.scrollTop(scrollTopF1);
                }

                if (FSP_lastScrollLeftF2 != scrollLeftF1) {
                    FSP_lastScrollLeftF2 = scrollLeftF1;
                    FSP_bindElement2.scrollLeft(scrollLeftF1);
                }
            }
        }
        catch (ex) {
        }
    });

    FSP_bindElement2.scroll(function() {
        try {
            if (FSP_IsSyncEnabled()) {
                var scrollTopF2 = FSP_bindElement2.scrollTop();
                var scrollLeftF2 = FSP_bindElement2.scrollLeft();

                if (FSP_lastScrollTopF1 != scrollTopF2) {
                    FSP_lastScrollTopF1 = scrollTopF2;
                    FSP_bindElement1.scrollTop(scrollTopF2);
                }

                if (FSP_lastScrollLeftF1 != scrollLeftF2) {
                    FSP_lastScrollLeftF1 = scrollLeftF2;
                    FSP_bindElement1.scrollLeft(scrollLeftF2);
                }
            }
        }
        catch (ex) {
        }
    });
}


// Clears last values for scrolling.
function FSP_ClearLastValues() {
    FSP_lastScrollTopF1 = 0;
    FSP_lastScrollTopF2 = 0;
    FSP_lastScrollLeftF1 = 0;
    FSP_lastScrollLeftF2 = 0;
}


// Indicates if synchronization is enabled.
function FSP_IsSyncEnabled() {
    var synchronizeScrollbar = FSP_synchronize;
    try {
        // Get value 'Synchronization' from cookie
        var cookieValue = FSP_GetValueFromCookie(3);
        synchronizeScrollbar = (cookieValue.toLowerCase() == "1");
    }
    catch (ex) {
    }

    return synchronizeScrollbar;
}


// Refresh the second frame in the same culture.
function FSP_RefreshSameCulture(frameName, unbind) {
    if (frames[frameName] != null) {
        if ((frameName == 'frame1') || (frameName == 'frame1Vertical')) {
            FSP_frameF1 = null;
            FSP_bindElement1 = null;
            if (unbind) {
                FSP_frameF2 = null;
                FSP_bindElement2 = null;
            }
        }
        else if (frameName == 'frame2') {
            FSP_frameF2 = null;
            FSP_bindElement2 = null;
            if (unbind) {
                FSP_frameF1 = null;
                FSP_bindElement1 = null;
            }
        }

        // Refresh the second frame
        frames[frameName].location.replace(frames[frameName].location.href);
    }
}


// Initialize global variables.
function FSP_Init(frm1Url, frm2Url, emptyUrl, sepUrl, mainFrmId, frm1Id, frm1VId, frm2Id, vFrmId, frmSId, tHeight, mode, synchronize, displaySplitMode) {
    // Init selectors
    FSP_f1S = '#' + frm1Id;
    FSP_f1vS = '#' + frm1VId;
    FSP_f2S = '#' + frm2Id;
    FSP_fsS = '#' + frmSId;
    FSP_mFramesetS = '#' + mainFrmId;
    FSP_vFramesetS = '#' + vFrmId;

    FSP_f1Url = frm1Url;
    FSP_f2Url = frm2Url;
    FSP_bUrl = emptyUrl;
    FSP_sUrl = sepUrl;

    FSP_tHeight = tHeight;
    FSP_synchronize = synchronize;
    FSP_displaySplitMode = displaySplitMode;
    FSP_SetMode(mode);
}


// Show horizontal layout.
function FSP_HorizontalLayout() {
    if (FSP_mode != FSP_ModeEnum.Horizontal.value) {
        var $f1 = $j(FSP_f1S);
        var $fv1 = $j(FSP_f1vS);
        var $fstm = $j(FSP_mFramesetS);
        var $fstv = $j(FSP_vFramesetS);
        var $fsts = $j(FSP_fsS);

        // Set icon 'layout' class
        FSP_SetLayoutClass(false);

        FSP_UnbindSrolling();
        FSP_frameF1 = null;
        FSP_bindElement1 = null;

        $f1.attr('src', FSP_f1Url);
        $fv1.attr('src', FSP_bUrl);
        $fsts.attr('src', FSP_bUrl);
        $fstm.attr('rows', '*,' + FSP_tHeight + ',*');

        if (isRTL) {
            $fstv.attr('cols', '100%,0%,0%');
        }
        else {
            $fstv.attr('cols', '0%,0%,100%');
        }

        // Set split mode
        FSP_SetMode(FSP_ModeEnum.Horizontal.value);

        // Set cookie
        FSP_SetCookie();
    }
}


// Show vertical layout.
function FSP_VerticalLayout() {
    if (FSP_mode != FSP_ModeEnum.Vertical.value) {
        var $f1 = $j(FSP_f1S);
        var $fv1 = $j(FSP_f1vS);
        var $fstm = $j(FSP_mFramesetS);
        var $fstv = $j(FSP_vFramesetS);
        var $fsts = $j(FSP_fsS);

        // Set icon 'layout' class
        FSP_SetLayoutClass(true);

        FSP_UnbindSrolling();
        FSP_frameF1 = null;
        FSP_bindElement1 = null;

        $fv1.attr('src', FSP_f1Url);
        $f1.attr('src', FSP_bUrl);
        $fsts.attr('src', FSP_sUrl);

        $fstm.attr('rows', '0,' + FSP_tHeight + ',*');
        $fstv.attr('cols', '*,12,*');

        // Set split mode
        FSP_SetMode(FSP_ModeEnum.Vertical.value);

        // Set cookie
        FSP_SetCookie();
    }
}


// Set class for icon 'layout' (horizontal, vertical).
function FSP_SetLayoutClass(vertical) {
    // Get toolbar frame
    var frameToolbar = frames['toolbarframe'];
    if (frameToolbar != null) {
        // Set class
        var divH = $j(frameToolbar.document.getElementById(FSP_divHorizontalId));
        var divV = $j(frameToolbar.document.getElementById(FSP_divVerticalId));
        if ((divH != null) && (divV != null)) {
            var classButton = 'Button';
            var classButtonSelected = 'Button Selected';

            if (vertical) {
                divH.removeClass().addClass(classButton);
                divV.removeClass().addClass(classButtonSelected);
            }
            else {
                divH.removeClass().addClass(classButtonSelected);
                divV.removeClass().addClass(classButton);
            }
        }
    }
}


// Unbind 'scroll' events.
function FSP_UnbindSrolling() {
    try {
        if (FSP_bindElement1 != null) {
            $j(FSP_bindElement1).unbind('scroll');
        }

        if (FSP_bindElement2 != null) {
            $j(FSP_bindElement2).unbind('scroll');
        }
    }
    catch (ex) {
    }
}


// Synchonizes scrollbars
function FSP_SynchronizeToolbar() {
    if (FSP_synchronize == 1) {
        FSP_synchronize = 0
    }
    else {
        FSP_synchronize = 1;
    }

    // Get toolbar frame
    var frameToolbar = frames['toolbarframe'];
    if (frameToolbar != null) {
        // Set image src
        var imageSync = frameToolbar.document.getElementById(FSP_syncId);
        if (imageSync != null) {
            imageSync.src = FSP_synchronize ? FSP_checkSyncImgUrl : FSP_uncheckSyncImgUrl;
        }
    }

    // Synchronize scroolbars
    if ((FSP_bindElement1 != null) && (FSP_bindElement2 != null)) {
        FSP_bindElement2.scrollTop(FSP_bindElement1.scrollTop());
        FSP_bindElement2.scrollLeft(FSP_bindElement1.scrollLeft());
    }

    FSP_SetCookie();
}


// Sets split mode.
function FSP_SetMode(mode) {
    FSP_mode = Number(mode);
}


// Initializes variables for toolbar.
function FSP_ToolbarInit(selectorId, syncId, checkSyncImgUrl, uncheckSyncImgUrl, divHorizontalId, divVerticalId) {
    FSP_selectId = selectorId;
    FSP_syncId = syncId;
    FSP_checkSyncImgUrl = checkSyncImgUrl;
    FSP_uncheckSyncImgUrl = uncheckSyncImgUrl;
    FSP_divHorizontalId = divHorizontalId;
    FSP_divVerticalId = divVerticalId;
}


// Change culture
function FSP_ChangeCulture(control) {
    var f1 = null;
    if (FSP_mode == FSP_ModeEnum.Horizontal.value) {
        f1 = $j(frame1).get(0);
    }
    if (FSP_mode == FSP_ModeEnum.Vertical.value) {
        f1 = $j(frame1Vertical).get(0);
    }

    if (f1 != null) {
        var urlF1 = f1.location.toString().toLowerCase();
        if (control != null) {
            FSP_culture = control.value;
        }
        else {
            FSP_SetCulture(true);
        }

        // New url
        var newUrl = FSP_appPref + FSP_cntPref.replace('##c##', FSP_culture) + urlF1.replace(FSP_appPref.toLowerCase(), '');

        if (control != null) {
            // Set new value
            control.originalValue = FSP_culture;
            // Set cookie
            FSP_SetCookie();
        }

        // Get frame2 window
        var f2 = $j(frame2).get(0);

        // Reload frame
        FSP_RefreshFrame(f2.name, newUrl);
    }
}


// Refreshs frame.
function FSP_RefreshFrame(frameName, newUrl) {
    FSP_UnbindSrolling();
    FSP_frameF2 = null;
    FSP_bindElement2 = null;

    if (frames[frameName] != null) {
        frames[frameName].location.replace(newUrl);
    }
}


function FSP_SplitModeRefreshFrame() {
    FSP_ChangeCulture(null);
}


// Set culture to variable 'FSP_culture'.
function FSP_SetCulture(changeText) {
    var frameToolbar = frames['toolbarframe'];
    if (frameToolbar != null) {
        var selector = frameToolbar.document.getElementById(FSP_selectId);
        if (selector != null) {
            FSP_culture = selector.value;
            if (changeText) {
                try {
                    var $sel = $j(selector);
                    // Get culture name with suffix
                    var text = $sel.find('option').filter(':selected').text();
                    // Remove suffix
                    text = text.substring(0, text.indexOf('(') - 1);
                    // Set only culture name
                    $sel.find('option').filter(':selected').text(text);
                } catch (ex) {
                }
            }
        }
        else {
            return;
        }
    }
}


// Set values to cookies.
function FSP_SetCookie() {
    FSP_SetCulture(false);

    var layoutMode = FSP_ModeEnum.None;

    switch (FSP_mode) {
        case FSP_ModeEnum.Horizontal.value:
            layoutMode = FSP_ModeEnum.Horizontal.name;
            break;

        case FSP_ModeEnum.Vertical.value:
            layoutMode = FSP_ModeEnum.Vertical.name;
            break;
    }

    // New cookie value
    var newCookieValue = FSP_displaySplitMode + "|" + FSP_culture + "|" + layoutMode + "|" + FSP_synchronize;
    // Set new values to cookies
    $j.cookie("CMSSplitMode", newCookieValue.toString(), { expires: 365, path: '/' });
}


// Gets value from cookie.
function FSP_GetValueFromCookie(index) {
    var cookieValue = $j.cookie('CMSSplitMode');
    var values = cookieValue.split('|');
    return values[index];
}


// Close split mode.
function FSP_CloseSplitMode() {
    // Save cookie
    FSP_SetCookie();

    // Close split mode
    parent.CloseSplitMode();
}


// Check changes.
function CheckChanges(frameName) {
    toReturn = true;
    if (frameName == null) {
        if (window.frames['frame1'].CheckChanges) {
            toReturn = window.frames['frame1'].CheckChanges();
        }
        if (window.frames['frame1Vertical'].CheckChanges) {
            toReturn = window.frames['frame1Vertical'].CheckChanges();
        }
        if (window.frames['frame2'].CheckChanges) {
            toReturn &= window.frames['frame2'].CheckChanges();
        }
    }
    else {
        if ((window.frames[frameName] != null) && window.frames[frameName].CheckChanges) {
            toReturn = window.frames[frameName].CheckChanges();
        }
    }

    return toReturn;
}