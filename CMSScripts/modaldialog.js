function modalDialog(url, name, width, height, otherParams, noWopener, forceModal) {
    win = window; var dHeight = height; var dWidth = width;
    if (width.toString().indexOf('%') != -1) {
        dWidth = Math.round(screen.width * parseInt(width, 10) / 100);
    }
    if (height.toString().indexOf('%') != -1) {
        dHeight = Math.round(screen.height * parseInt(height, 10) / 100);
    }
    var s = navigator.userAgent.toLowerCase().match(/msie (\d+)/);
    var ieVersion = (s ? parseInt(s[1], 10) : 0);
    var ieEngine = document.documentMode || 0;

    if (document.all && (navigator.appName != 'Opera') && (((ieVersion < 9) && !((ieVersion == 7) && (ieEngine == 9))) || forceModal)) {
        if (otherParams == undefined) {
            otherParams = 'resizable:yes;scroll:no';
        }
        var vopenwork = true;
        if (!noWopener) {
            try { win = wopener.window; } catch (e) { vopenwork = false; }
        }
        if (ieVersion < 7) {
            dWidth += 4; dHeight += 58;
        }
        var pars = 'dialogWidth:' + dWidth + 'px;dialogHeight:' + dHeight + 'px;' + otherParams;
        try {
            win.showModalDialog(url, this, pars);
        }
        catch (e) {
            if (vopenwork) {
                window.showModalDialog(url, this, pars);
            }
        }
    } else {
        if (otherParams == undefined) {
            otherParams = 'toolbar=no,directories=no,menubar=no,modal=yes,dependent=yes,resizable=yes';
        }
        oWindow = win.open(url, name, 'width=' + dWidth + ',height=' + dHeight + ',' + otherParams);
        if (oWindow) {
            oWindow.opener = this; oWindow.focus();
        }
    }
}