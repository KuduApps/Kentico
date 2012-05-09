var elemMinimize;
var elemMaximize;
var elemMinimizeAll;
var elemMaximizeAll;
var elemBorder;
var originalSizeElem;
var minSizes;
var originalSizes;

function InitResizer() {
    elemMinimize = document.getElementById('imgMinimize');
    elemMaximize = document.getElementById('imgMaximize');
    elemMinimizeAll = document.getElementById('imgMinimizeAll');
    elemMaximizeAll = document.getElementById('imgMaximizeAll');
    elemBorder = document.getElementById('resizerBorder');
    originalSizeElem = document.getElementById('originalSize');

    if (window.minSize != undefined) {
        minSizes = minSize.split(';');
    }

    if (window.originalSizeElem != undefined) {
        originalSizes = originalSizeElem.value.split(';');
    }
}

function ResizerGetParent() {
    var par = window;
    var level = parentLevel;

    while (level > 0) {
        par = par.parent;
        level--;
    }

    return par;
}

function Minimize() {
    if (window.parentLevel == undefined) { return; }

    var index = 0;
    var par = ResizerGetParent();
    // Minimalize only if not minimized
    if (elemMinimize.style.display != 'none') {
        for (index = 0; index < minSizes.length; index++) {
            var minSize = minSizes[index];
            if (minSize && (minSize != '')) {
                var fs = par.document.getElementById(framesetName);
                if (fs == null) {
                    var framesets = par.document.getElementsByTagName('frameset');
                    if (framesets.length > 0) {
                        fs = framesets[0];
                    }
                }
                if (fs) {
                    if (resizeVertical) {
                        originalSizes[index] = fs.rows;
                        fs.rows = minSize;
                    }
                    else {
                        originalSizes[index] = fs.cols;
                        fs.cols = minSize;
                    }

                    elemMinimize.style.display = 'none';
                    elemMaximize.style.display = 'inline';
                    elemBorder.style.display = 'block';
                }
            }

            par = par.parent;
        }
    }
}

function Maximize() {
    if (window.parentLevel == undefined) { return; }

    var index = 0;
    var par = ResizerGetParent();
    // Maximize only if not maximized
    if (elemMaximize.style.display != 'none') {
        for (index = 0; index < minSizes.length; index++) {
            var originalSize = originalSizes[index];
            if (originalSize && (originalSize != '')) {
                var fs = par.document.getElementById(framesetName);
                if (fs == null) {
                    var framesets = par.document.getElementsByTagName('frameset');
                    if (framesets.length > 0) {
                        fs = framesets[0];
                    }
                }
                if (fs) {
                    if (resizeVertical) {
                        fs.rows = originalSize;
                    }
                    else {
                        if (/\%$/.test(originalSize)) {
                            originalSize = originalSize.replace(/[0-9]+,/, '*,');
                        }
                        fs.cols = originalSize;
                    }

                    elemMinimize.style.display = 'inline';
                    elemMaximize.style.display = 'none';
                    elemBorder.style.display = 'none';
                }
            }

            par = par.parent;
        }
    }
}

function MinimizeAll(wnd) {
    if (wnd == null) {
        elemMinimizeAll.style.display = 'none';
        elemMaximizeAll.style.display = 'inline';

        wnd = top.window;
    }

    if (wnd.Minimize) {
        wnd.Minimize();
    }
    else {
        for (var i = 0; i < wnd.frames.length; i++) {
            MinimizeAll(wnd.frames[i]);
        }
    }
}

function MaximizeAll(wnd) {
    if (wnd == null) {
        elemMinimizeAll.style.display = 'inline';
        elemMaximizeAll.style.display = 'none';

        wnd = top.window;
        window.requestWindow = window;
    }

    if (wnd.Maximize) {
        wnd.Maximize();
    }
    else {
        for (var i = 0; i < wnd.frames.length; i++) {
            if (window.requestWindow != wnd) {
                MaximizeAll(wnd.frames[i]);
            }
        }
    }
}

function GetLeftButton() {
    var s = navigator.userAgent.toLowerCase() + '';
    if ((s.indexOf('gecko/') >= 0) || (s.indexOf('opera/') >= 0) || (s.indexOf('safari/') >= 0) || IsIE9()) {
        return 0;
    }
    else {
        return 1;
    }
}

var lastResizeX = 0;
var currentFrameSize = 0;

var resizingFrame = false;
var nextWindow = null;

function StopResizeFrame(ev) {
    if (window != document.originalWindow) {
        return document.originalWindow.StopResizeFrame(ev);
    }

    resizingFrame = false;
    return false;
}

function IsWebKit() {
    var s = navigator.userAgent.toLowerCase() + '';
    return (s.indexOf('applewebkit/') >= 0);
}

function IsIE9() {
    var s = navigator.userAgent.toLowerCase() + '';
    return (s.indexOf('msie 9') >= 0);
}

function StartResizeFrame(ev) {
    if (ev == null) {
        ev = window.event;
    }

    if (ev.button == GetLeftButton()) {
        lastResizeX = (ev.x ? ev.x : ev.clientX);
        currentFrameSize = document.body.clientWidth;

        resizingFrame = true;
    }

    if (IsWebKit() || IsIE9()) {
        InitResizerWindows(window.parent, window);
        return false;
    }
    else {
        InitResizerWindows(window, window);
    }
}

function InitResizerWindows(wnd, originalWindow) {
    try {
        if (window.location.host == originalWindow.location.host) {
            if (wnd.frames.length > 0) {
                for (var i = 0; i < wnd.frames.length; i++) {
                    InitResizerWindows(wnd.frames[i], originalWindow);
                }
            }

            wnd.document.originalWindow = originalWindow;

            wnd.document.originalmousemove = wnd.document.body.onmousemove;
            wnd.document.originalmouseup = wnd.document.body.onmouseup;

            wnd.document.body.onmouseup = StopResizeFrame;
            wnd.document.body.onmousemove = ResizeFrame;
        }
    }
    catch (ex) {
    }
}

function RestoreResizerWindows(wnd) {
    try {
        if (window.location.host == originalWindow.location.host) {
            if (wnd.frames.length > 0) {
                for (var i = 0; i < wnd.frames.length; i++) {
                    RestoreResizerWindows(wnd.frames[i]);
                }
            }

            wnd.document.body.onmouseup = wnd.document.originalmouseup;
            wnd.document.body.onmousemove = wnd.document.originalmousemove;

            wnd.document.originalWindow = null;
        }
    }
    catch (ex) {
    }
}

function ResizeFrame(ev, wnd) {
    if (ev == null) {
        ev = window.event;
    }

    if (window != document.originalWindow) {
        return document.originalWindow.ResizeFrame(ev, window);
    }

    if (resizingFrame) {
        if (ev.button == GetLeftButton()) {
            var rtl = (document.body.className.indexOf('RTL') >= 0);
            if (nextWindow == null) {
                nextWindow = false;
            }

            var newX = (ev.x ? ev.x : ev.clientX);
            var changeX = newX - lastResizeX;

            if ((changeX > 50) || (changeX < -50)) {
                lastResizeX = newX;
                nextWindow = (changeX > 50);
            }

            var changeX = newX - lastResizeX;
            if ((changeX < 50) && (changeX > -50)) {
                var fs = window.parent.document.getElementById(framesetName);
                if (fs) {
                    if (rtl) {
                        currentFrameSize -= changeX;
                        fs.cols = fs.cols.replace(/[^,]+$/, currentFrameSize);
                    } else {
                        currentFrameSize += changeX;
                        fs.cols = fs.cols.replace(/^[^,]+/, currentFrameSize);
                    }
                }

                if (!rtl) {
                    lastResizeX = newX;
                }
            }
        }
        else {
            resizingFrame = false;
        }
    }
}

function InitFrameResizer(elem) {
    if (elem != null) {
        if (!elem.resizerInitialized) {
            elem.resizerInitialized = true;

            elem.onmousedown = StartResizeFrame;
            elem.onmouseup = StopResizeFrame;
        }
    }
}
