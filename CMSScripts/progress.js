var oldIndicatorSubmit = null;
var boxCss = { 'opacity': '0.7' };
var progressImageUrl = null;
var ie = 0//@cc_on + @_jscript_version;
var progressIsRunning = false;

function GetScrollX() {
    if (typeof (window.pageXOffset) == 'number') {
        return window.pageXOffset;
    }
    else if (document.body && (document.body.scrollLeft || document.body.scrollTop)) {
        return document.body.scrollLeft;
    }
    else if (document.documentElement && (document.documentElement.scrollLeft || document.documentElement.scrollTop)) {
        return document.documentElement.scrollLeft;
    }

    return 0;
}

function GetScrollY() {
    if (typeof (window.pageYOffset) == 'number') {
        return window.pageYOffset;
    }
    else if (document.body && (document.body.scrollLeft || document.body.scrollTop)) {
        return document.body.scrollTop;
    }
    else if (document.documentElement && (document.documentElement.scrollLeft || document.documentElement.scrollTop)) {
        return document.documentElement.scrollTop;
    }

    return 0;
}

function ProgressIsRunning(window) {
    if (window.progressIsRunning) {
        return true;
    }
    else {
        var isRunning = false;
        for (var i = 0; i < window.frames.length; i++) {
            if (window.frames[i].ProgressIsRunning) {
                if (window.frames[i].ProgressIsRunning(window.frames[i])) {
                    isRunning = true;
                }
            }
            if (isRunning) {
                break;
            }
        }
        return isRunning;
    }
}

function ShowPassiveIndicator() {
    var runningUnder = ProgressIsRunning(window);
    if (!progressIsRunning & !runningUnder) {
        progressIsRunning = true;
        HideIndicator(true);
        if (window.ClearToolbar) {
            window.ClearToolbar();
        }

        if (window.jQuery) {
            progressIsRunning = true;
            // Menu box
            var box = $j('<div class="IndicatorBox SaveIndicator PassiveIndicator"></div>');
            box.css(boxCss);
            $j(document.body).append(box);
        }
    }
}

function ShowIndicator(notRecursive, isIFrame) {
    if (!progressIsRunning) {
        progressIsRunning = true;
        HideIndicator(true);
        if (window.ClearToolbar) {
            window.ClearToolbar();
        }

        if (window.jQuery) {
            progressIsRunning = true;
            var viewBody = $j(document.body);
            var viewBox = $j(document.createElement('div')).attr('class', 'IndicatorBox ActiveIndicator');
            var viewBack = $j(document.createElement('div')).css(boxCss).attr('class', 'SaveIndicator');
            viewBack.height($j(document).height());
            var contentDiv = $j(document.createElement('div'));
            var img = $j(document.createElement('img')).attr('src', progressImageUrl);
            var txt = "Please wait ...";
            if (window.progressText != null) {
                txt = window.progressText;
            }
            var txt = $j(document.createElement('div')).html('<strong>' + txt + '</strong>');

            contentDiv.append(img);
            contentDiv.append(txt);

            viewBox.append(contentDiv);
            viewBox.append(viewBack);
            viewBody.prepend(viewBox);

            var contentWidth = 200;
            var contentHeight = 100;

            var rtl = (document.body.className.indexOf('RTL') >= 0);
            var leftValue = GetScrollX();
            if (rtl && ie) {
                leftValue = -document.documentElement.scrollWidth + viewBody.innerWidth() + GetScrollX();
            }

            var contentDivCss = {
                'width': '100%',
                'height': contentHeight + 'px',
                'text-align': 'center',
                'position': 'absolute',
                'z-index': '10005',
                'top': viewBody.innerHeight() / 2 - contentHeight / 2 + GetScrollY(),
                'left': leftValue
            };
            contentDiv.css(contentDivCss);

            var positionCss = null;
            if (isIFrame) {
                positionCss = {
                    'top': '0px',
                    'right': '0px',
                    'bottom': '0px',
                    'left': '0px'
                };
            }
            else if (!ie) {
                positionCss = {
                    'top': '0px',
                    'right': '0px',
                    'bottom': '0px',
                    'left': '0px',
                    'position': 'fixed'
                };
            }
            else {
                positionCss = {
                    'top': '0px',
                    'right': '0px',
                    'bottom': '0px',
                    'left': '0px'
                };
            }

            viewBack.css(positionCss);

            if (!notRecursive) {
                ShowPassiveIndicatorRecursively(window, 0, false);
            }
        }
    }
}

function ShowPassiveIndicatorRecursively(sender, iteration, showMain) {
    if (iteration > 10) {
        return;
    }

    if ((parent != null) && (parent != window)) {
        var frameSets = parent.document.getElementsByTagName("frameset");
        var hasFrameSets = (frameSets != null) && (frameSets.length > 0);

        if (!hasFrameSets) {
            // IFRAME
            if (showMain && parent.isMainWindow && parent.ShowIndicator) {
                parent.ShowIndicator(true, true);
                showMain = false;
            }
            else if (parent.ShowPassiveIndicator) {
                parent.ShowPassiveIndicator();
            }
        }
        else {
            for (var i = 0; i < parent.frames.length; i++) {
                var frame = parent.frames[i];
                if ((frame != sender) && frame.ShowPassiveIndicator) {
                    if (showMain && frame.isMainWindow) {
                        frame.ShowIndicator(true, true);
                        showMain = false;
                    }
                    else {
                        frame.ShowPassiveIndicator();
                    }
                }
            }
        }

        if (parent != sender && parent.ShowPassiveIndicatorRecursively) {
            showMain = parent.ShowPassiveIndicatorRecursively(parent, iteration + 1, showMain);
        }
    }

    return showMain;
}

function HideIndicator(second) {
    progressIsRunning = false;
    if (window.jQuery) {
        // Remove menu save indicator
        var indicator = $j('.IndicatorBox');
        if ((indicator != null) && (indicator.length > 0)) {
            indicator.remove();
        }

        // Second hide for IE + quick click
        if (!second) {
            setTimeout('HideIndicator(true);', 300);
        }
    }
}

function HideIndicatorRecursively(sender, iteration) {
    if (iteration > 10) {
        return;
    }

    if ((parent != null) && (parent != sender)) {
        for (var i = 0; i < parent.frames.length; i++) {
            var frame = parent.frames[i];
            if ((frame != sender) && frame.HideIndicator) {
                frame.HideIndicator();
            }
        }

        if (parent.HideIndicator) {
            parent.HideIndicator();
        }

        if ((parent != sender) && (parent.HideIndicatorRecursively)) {
            parent.HideIndicatorRecursively(parent, iteration + 1);
        }
    }
}

function SubmitIndicatorPage() {
    var proceed = false;
    if (typeof (Sys) == 'undefined') {
        proceed = true;
    }
    else {
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if ((prm._postBackSettings === null) || !prm._postBackSettings.async) {
            proceed = true;
        }
    }

    if (window.ValidatorOnSubmit && !window.ValidatorOnSubmit()) {
        return false;
    }

    if (proceed) {
        disableShortcuts = true;

        if (window.isSideWindow) {
            ShowPassiveIndicator();
            ShowPassiveIndicatorRecursively(window, 0, true);
        }
        else {
            ShowIndicator();
        }

        if (oldIndicatorSubmit != null) {
            return oldIndicatorSubmit();
        }
    }

    return true;
}

function InitIndicator() {
    progressIsRunning = false;
    if (document.body != null) {
        if (window.theForm != null) {
            oldIndicatorSubmit = theForm.onsubmit;
            theForm.onsubmit = SubmitIndicatorPage;
        }

        if (window.imagesUrl != null) {
            var rtl = (document.body.className.indexOf('RTL') >= 0);
            if (rtl) {
                imagesUrl += "RTL/";
            }
            progressImageUrl = imagesUrl + 'Design/Preloaders/preload64.gif';
            pic1 = new Image();
            pic1.src = progressImageUrl;
        }

        HideIndicatorRecursively(window, 0);
    }
}

InitIndicator();

if (typeof (Sys) != "undefined" && typeof (Sys.Application) != "undefined") {
    Sys.Application.notifyScriptLoaded();
}
