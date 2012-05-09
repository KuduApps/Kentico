function HideModalBackground(backgroundid) {
    var backgroundElem = $j('#' + backgroundid);
    if (backgroundElem != null) {
        backgroundElem.hide();
        if (resizeBackgroundHandler != null) {
            $j(window).unbind('resize', resizeBackgroundHandler);
            resizeBackgroundHandler = null;
        }
    }
}

function HideModalPopup(popupid, backgroundid) {
    if ($j.browser.msie && $j.browser.version.substr(0, 1) < 7) {
        $j(window).unbind('scroll');
    }

    var popupObj = $j('#' + popupid);
    if (popupObj != null) {
        popupObj.hide();
        if (keepDialogAccessibleHandler != null) {
            $j(window).unbind('resize', keepDialogAccessibleHandler);
            keepDialogAccessibleHandler = null;
        }
    }
    // Hide background
    HideModalBackground(backgroundid);

    // Show hidden drop down for IE6
    if ($j.browser.msie && $j.browser.version.substr(0, 1) < 7) {
        var hiddenSelects = $j('select[popuphide="true"]');
        hiddenSelects.removeAttr('popuphide');
        hiddenSelects.show();
    }
}

var resizeBackgroundHandler = null;
var keepDialogAccessibleHandler = null;

function ShowModalBackground(backgroundid) {
    var backgroundElem = $j('#' + backgroundid);
    if (backgroundElem != null) {
        backgroundElem.width(GetWinWidth());
        backgroundElem.height(GetWinHeight());
        if ($j.browser.msie && $j.browser.version.substr(0, 1) < 7) {
            backgroundElem.css('position', 'absolute');
        }
        else {
            backgroundElem.css('position', 'fixed');
        }
        backgroundElem.css('overflow', 'hidden');

        if (resizeBackgroundHandler != null) {
            $j(window).unbind('resize', resizeBackgroundHandler);
            resizeBackgroundHandler = null;
        }
        resizeBackgroundHandler = function() { backgroundElem.width(GetWinWidth()); backgroundElem.height(GetWinHeight()); };

        $j(window).resize(resizeBackgroundHandler);
        backgroundElem.show();
    }
}

var imgsToLoad = 0;

function ShowModalPopup(popupid, backgroundid) {
    var popupObj = $j('#' + popupid);
    // Hide drop down fields for ie6
    if ($j.browser.msie && $j.browser.version.substr(0, 1) < 7) {
        var visibleSelects = $j("select:visible");
        visibleSelects.attr('popuphide', 'true');
        visibleSelects.hide();
    }

    // Clear dialog initial size
    dialogInitWidth = -1;
    if (popupObj != null) {
        // Show background
        ShowModalBackground(backgroundid);
        // Default setting due to IE6 support
        popupObj.css('position', 'absolute');

        keepDialogAccessibleHandler = function() { keepDialogAccesible(popupid); };

        popupObj.css('height', 'auto');
        popupObj.css('width', 'auto');

        $j(window).resize(keepDialogAccessibleHandler);

        var images = popupObj.find("img");
        imgsToLoad = images.length;
        if (imgsToLoad > 0) {

            images.each(function() {
                $j(this).error(function() { imgsToLoad--; });
                WaitForImageToLoad(popupid, this);
            });
        }
        else {
            ShowDialog(popupid);
        }

        if ($j.browser.msie && $j.browser.version.substr(0, 1) < 7) {
            $j(window).unbind('scroll');
            $j(window).scroll(function() { popupObj.AlignCenter(); });
        }
    }
}

function WaitForImageToLoad(popupid, img) {
    if ((imgsToLoad <= 0) || IsImageLoaded(img) || IsInputTypeImageLoaded(img)) {
        imgsToLoad--;
        if (imgsToLoad <= 0) {
            ShowDialog(popupid);
        }
    }
    else {
        setTimeout(function() { WaitForImageToLoad(popupid, img); }, 20);
    }
}

function IsImageLoaded(img) {
    if (!img.complete) {
        return false;
    }
    if (typeof img.naturalWidth != 'undefined' && img.naturalWidth == 0) {
        return false;
    }
    return true;
}

function IsInputTypeImageLoaded(img) {
    if ((typeof img.size != 'undefined') && (img.size != 0) && (img.tagName.toLowerCase() == "input")) {
        return true;
    }
    return false;
}

function ShowDialog(popupid) {
    var popupObj = $j('#' + popupid);
    popupObj.AlignCenter();
    popupObj.show();
    keepDialogAccesible(popupid);
}

var dialogInitWidth = -1;
var dialogInitHeight = -1;
var scrollableInitWidth = -1;
var scrollableInitHeight = -1;
var resizableDialog = false;

function keepDialogAccesible(popupid) {
    var popupObj = $j('#' + popupid);

    if (popupObj.is(":visible")) {

        var scrollable = popupObj.find('.DialogScrollableContent');
        var isChanged = false;

        if (scrollable.length > 0) {

            if (dialogInitWidth == -1) {
                dialogInitWidth = popupObj.width();
                dialogInitHeight = popupObj.height();
                scrollableInitWidth = scrollable.width();
                scrollableInitHeight = scrollable.height();
            }

            scrollable.css('overflow', '');
            popupObj.css('height', dialogInitHeight + 'px');
            popupObj.css('width', dialogInitWidth + 'px');

            if (resizableDialog) {
                scrollable.css('height', '');
                scrollable.css('height', scrollable.height() + 'px');
            }
            else {
                scrollable.css('height', scrollableInitHeight + 'px');
            }
            scrollable.css('width', scrollableInitWidth + 'px');

            var poshei = $j(window).height() - (popupObj.height() - scrollable.height()) - 20;
            var poswi = $j(window).width() - (popupObj.width() - scrollable.width()) - 20;

            if ($j(window).height() < popupObj.height()) {
                scrollable.height(poshei + 'px');
                scrollable.css('overflow', 'auto');
                popupObj.height($j(window).height() - 20 + 'px');
                isChanged = true;
            }

            if ($j(window).width() < popupObj.width()) {
                scrollable.width(poswi + 'px');
                scrollable.css('overflow', 'auto');
                popupObj.width($j(window).width() - 20 + 'px');
                isChanged = true;
            }
        }
        else {
            if (popupObj.css('overflow') == 'auto') {
                var popObjBasic = popupObj.get(0);
                popupObj.width(popObjBasic.scrollWidth + 'px');
                popupObj.height(popObjBasic.scrollHeight + 'px');
                popupObj.css('overflow', "");
            }

            if ($j(window).height() < popupObj.height()) {
                popupObj.height($j(window).height() - 20 + 'px');
                popupObj.css('overflow', 'auto');
                isChanged = true;
            }
            if ($j(window).width() < popupObj.width()) {
                popupObj.width($j(window).width() - 20 + 'px');
                popupObj.css('overflow', 'auto');
                isChanged = true;
            }
        }
        if (isChanged) {
            popupObj.AlignCenter();
        }
    }
}

function RegisterClickHandler(targetcontrolid, fn) {
    $j(document).ready(function() {
        var target = $j('#' + targetcontrolid);
        if (target != null) {
            target.click(fn);
        }
    });
}

function GetWinHeight() {
    var docHeight = $j(document).height();
    var winHeight = $j(window).height();
    if ((docHeight > winHeight) && (($j.browser.msie && $j.browser.version.substr(0, 1) < 7))) {
        return docHeight;
    }
    else {
        return winHeight;
    }
}

function GetWinWidth() {
    var docWidth = $j(document).width();
    var winWidth = $j(window).width();
    if ((docWidth > winWidth) && (($j.browser.msie && $j.browser.version.substr(0, 1) < 7))) {
        return docWidth;
    }
    else {
        return winWidth;
    }
}

/* Fixes position for all browsers */
(function(F) {
    F.fn.AlignCenter = function(B) {
        var C = { ignorechildren: true, showPopup: true }; var D = F.extend({}, C, B); var E = F(this); E.css({ zIndex: 9998 }); if (D.showPopup) { E.show(); } F(document).ready(function() { PosElem(); }); F(window).resize(function() { PosElem(); }); function PosElem() {
            var a = 0; var b = 0; if (D.ignorechildren) { a = E.height(); b = E.width(); } else { var c = E.children(); for (var i = 0; i < c.length; i++) { if (c[i].style.display != 'none') { a = c[i].clientHeight; b = c[i].clientWidth; } } } var d = E.css("margin"); var e = E.css("padding"); if (d != null) { d = d.replace(/auto/gi, '0'); d = d.replace(/px/gi, ''); d = d.replace(/pt/gi, ''); } var f = ""; if (d != "" && d != null) { var g = e.split(' '); if (g.length == 1) { var h = parseInt(g[0]); f = new Array(h, h, h, h); } else if (g.length == 2) { var j = parseInt(g[0]); var k = parseInt(g[1]); f = new Array(j, k, j, k); } else if (g.length == 3) { var l = parseInt(g[0]); var m = parseInt(g[1]); var n = parseInt(g[2]); f = new Array(l, m, n, m); } else if (g.length == 4) { var l = parseInt(g[0]); var m = parseInt(g[1]); var o = parseInt(g[2]); var p = parseInt(g[3]); f = new Array(l, m, n, p); } } var k = 0; var j = 0; if (f != "NaN") { if (f.length > 0) { k = f[1] + f[3]; j = f[0] + f[2]; } } if (e != null) { e = e.replace(/auto/gi, '0'); e = e.replace(/px/gi, ''); e = e.replace(/pt/gi, ''); } var q = ""; if (e != "" && e != null) { var r = e.split(' '); if (r.length == 1) { var s = parseInt(r[0]); q = new Array(s, s, s, s); } else if (r.length == 2) { var t = parseInt(r[0]); var u = parseInt(r[1]); q = new Array(t, u, t, u) } else if (r.length == 3) { var v = parseInt(r[0]); var w = parseInt(r[1]); var x = parseInt(r[2]); q = new Array(v, w, x, w); } else if (r.length == 4) { var v = parseInt(r[0]); var w = parseInt(r[1]); var x = parseInt(r[2]); var y = parseInt(r[3]); q = new Array(v, w, x, y); } } var u = 0; var t = 0; if (q != "NaN") { if (q.length > 0) { u = q[1] + q[3]; t = q[0] + q[2]; } } if (j == "NaN" || isNaN(j)) { j = 0; } if (t == "NaN" || isNaN(t)) { t = 0; } var z = F(window).height(); var A = F(window).width(); var rt = ((z - (a + j + t)) / 2); var rl = ((A - (b + k + u)) / 2); if (F.browser.msie && F.browser.version.substr(0, 1) < 7) {
                E.css("position", "absolute"); rt = rt + F(document).scrollTop(); var rl = rl + F(document).scrollLeft();
            } else { E.css("position", "fixed"); } E.css("height", a + "px"); E.css("width", b + "px"); E.css("top", rt + "px"); E.css("left", rl + "px");
        }
    };
})(jQuery);

if (typeof (Sys) != "undefined" && typeof (Sys.Application) != "undefined") {
    Sys.Application.notifyScriptLoaded();
}