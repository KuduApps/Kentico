function AutoTitle(wnd) {
    try {
        if (wnd == null) wnd = window;
        var ttl = '';
        var lasttp = null;

        while (true) {
            var d = wnd.document
            var tp = d.titlePart;
            if (tp != null) tp = tp.replace(/^\s+|\s+$/g, '')
            if ((tp != null) && (lasttp != tp)) {
                lasttp = tp;
                ttl = tp + (ttl != '' ? ' / ' + ttl : '')
            } else {
                d.titlePart = lasttp;
            }
            wnd.document.title = ttl;
            if ((wnd == window.top) || (wnd.parent == null) || (wnd == wnd.parent)) {
                break;
            }
            wnd = wnd.parent;
        }
    } catch (e) { }
}

function UnSetTitle() {
    try {
        var w = window;
        var d = w.document;
        while (d.titlePart == part) {
            if ((w == null) || (w == w.parent)) break;
            w = w.parent;
            d = w.document;
        }
        AutoTitle(w);
        if (oldUnload != null) {
            return oldUnload();
        }
    } catch (e) { }
};

if (!window.ttlUnloadSet) {
    window.oldUnload = window.onunload;
    window.onunload = UnSetTitle;
    window.ttlUnloadSet = true;
}