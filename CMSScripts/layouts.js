var firstLayoutX = 0;
var firstLayoutY = 0;

var initialWidth = 0;
var initialHeight = 0;

var currentWidth = 0;
var currentHeight = 0;

var currentLayoutElem = null;
var layoutOverlayElem = null;

var layoutInfoElem = null;
var layoutInfoText = null;

function IsWebKit() {
    var s = navigator.userAgent.toLowerCase() + '';
    if (s.indexOf('applewebkit/') >= 0) {
        return true;
    }
    return false;
}

function Get(elemId) {
    return document.getElementById(elemId);
}

function GetElementY(oElement) {
    var floats = false;
    var iReturnValue = 0;
    while (oElement != null) {
        if (oElement.style) {
            // Check floating
            var floatValue = ((oElement.style.cssFloat != null) ? oElement.style.cssFloat : oElement.style.styleFloat);
            if ((floatValue == 'left') || (floatValue == 'right')) {
                floats = true;
            }
            if (floats && (oElement != document.documentElement) && !IsWebKit()) {
                iReturnValue -= oElement.scrollTop;
            }

            if (oElement.style.position == 'absolute') {
                return iReturnValue;
            }

            iReturnValue += oElement.offsetTop;

            if (oElement.style.position == 'fixed') {
                return iReturnValue + document.documentElement.scrollTop;
            }
        }

        // Check parents behind the offset
        if (oElement.parentNode != oElement.offsetParent) {
            var parent = oElement.parentNode;
            while ((parent != null) && (parent != oElement.offsetParent)) {
                if (parent.style) {
                    // Check floating
                    var floatValue = ((parent.style.cssFloat != null) ? parent.style.cssFloat : parent.style.styleFloat);
                    if ((floatValue == 'left') || (floatValue == 'right')) {
                        floats = true;
                    }
                    if (floats && (parent != document.documentElement)) {
                        iReturnValue -= parent.scrollTop;
                    }
                }

                parent = parent.parentNode;
            }
        }

        oElement = oElement.offsetParent;
    }

    return iReturnValue;
}

function GetElementX(oElement) {
    var floats = false;
    var iReturnValue = 0;
    while (oElement != null) {
        if (oElement.style) {
            // Check floating
            var floatValue = ((oElement.style.cssFloat != null) ? oElement.style.cssFloat : oElement.style.styleFloat);
            if ((floatValue == 'left') || (floatValue == 'right')) {
                floats = true;
            }
            if (floats && (oElement != document.documentElement) && !IsWebKit()) {
                iReturnValue -= oElement.scrollLeft;
            }

            if (oElement.style.position == 'absolute') {
                return iReturnValue;
            }

            iReturnValue += oElement.offsetLeft;
        }

        // Check parents behind the offset
        if (oElement.parentNode != oElement.offsetParent) {
            var parent = oElement.parentNode;
            while ((parent != null) && (parent != oElement.offsetParent)) {
                if (parent.style) {
                    // Check floating
                    var floatValue = ((parent.style.cssFloat != null) ? parent.style.cssFloat : parent.style.styleFloat);
                    if ((floatValue == 'left') || (floatValue == 'right')) {
                        floats = true;
                    }
                    if (floats && (parent != document.documentElement)) {
                        iReturnValue -= parent.scrollLeft;
                    }
                }

                parent = parent.parentNode;
            }
        }

        oElement = oElement.offsetParent;
    }

    return iReturnValue;
}

function UpdateLayoutInfo(el) {
    if (el != null) {
        var x = GetElementX(el);
        var y = GetElementY(el);
        var w = el.offsetWidth;
        var h = el.offsetHeight;

        layoutInfoElem.style.top = y + 'px';
        layoutInfoElem.style.left = x + 'px';

        layoutInfoElem.style.width = (w - 2) + 'px';
        layoutInfoElem.style.height = (h - 2) + 'px';

        //layoutInfoText.innerHTML = w + ' x ' + h;
        layoutInfoText.innerHTML = (w == initialWidth ? w : '<strong>' + w + '</strong>') + ' x ' + (h == initialHeight ? h : '<strong>' + h + '</strong>');

        layoutInfoText.style.top = (y + h / 2 - layoutInfoText.offsetHeight / 2) + 'px';
        layoutInfoText.style.left = (x + w / 2 - layoutInfoText.offsetWidth / 2) + 'px';
    }
}

function GetLeftButton() {
    var s = navigator.userAgent.toLowerCase() + '';
    if ((s.indexOf('gecko/') >= 0) || (s.indexOf('opera/') >= 0) || (s.indexOf('chrome/') >= 0) || (s.match(/msie 9/) != null)) {
        return 0;
    }
    else {
        return 1;
    }
}

function InitHorizontalResizer(ev, elem, clientId, elementId, propertyName, inverted, infoId) {
    if (ev == null) ev = window.event;

    if (ev.button == GetLeftButton()) {
        firstLayoutX = ev.clientX;
        firstLayoutY = ev.clientY;

        currentLayoutElem = elem;
        if (infoId == null) infoId = elementId;

        if (elem.horzProperty == null) {
            elem.horzProperty = propertyName;
            elem.horzClientId = clientId;
            elem.horzElementId = elementId;
            elem.horzInverted = inverted;
            elem.horzInfoId = infoId;
        }

        var el = Get(clientId + '_' + elementId);
        currentWidth = initialWidth = el.offsetWidth;
        currentHeight = initialHeight = el.offsetHeight;

        var infoElem = Get(clientId + '_' + infoId)
        EnsureOverlay('e-resize', infoElem);
    }
}

function InitVerticalResizer(ev, elem, clientId, elementId, propertyName, infoId) {
    if (ev == null) ev = window.event;

    if (ev.button == GetLeftButton()) {
        firstLayoutX = ev.clientX;
        firstLayoutY = ev.clientY;

        currentLayoutElem = elem;
        if (infoId == null) infoId = elementId;

        if (elem.vertProperty == null) {
            elem.vertProperty = propertyName;
            elem.vertClientId = clientId;
            elem.vertElementId = elementId;
            elem.vertInfoId = infoId;
        }

        var el = Get(clientId + '_' + elementId);
        currentWidth = initialWidth = el.offsetWidth;
        currentHeight = initialHeight = el.offsetHeight;

        var infoElem = Get(clientId + '_' + infoId)
        EnsureOverlay('n-resize', infoElem);
    }
}

function LayoutMouseMove(ev) {
    if (ev == null) ev = window.event;

    var el = currentLayoutElem;
    if (el != null) {
        if (el.horzProperty != null) {
            var x = ev.clientX;
            var dx = firstLayoutX - x;
            if (el.horzInverted) {
                dx = -dx;
            }
            var elem = Get(el.horzClientId + '_' + el.horzElementId);
            currentWidth = initialWidth - dx;
            elem.style.width = currentWidth + 'px';

            var infoElem = Get(el.horzClientId + '_' + el.horzInfoId);
            UpdateLayoutInfo(infoElem);
        }

        if (el.vertProperty != null) {
            var y = ev.clientY;
            var dy = firstLayoutY - y;
            var elem = Get(el.vertClientId + '_' + el.vertElementId);
            currentHeight = initialHeight - dy;
            elem.style.height = currentHeight + 'px';

            var infoElem = Get(el.vertClientId + '_' + el.vertInfoId);
            UpdateLayoutInfo(infoElem);
        }
    }

    //preventDefault();
    ev.returnValue = false;
    return false;
}

function LayoutMouseUp(ev) {
    if (currentLayoutElem != null) {
        elem = currentLayoutElem;

        if ((elem.vertProperty != null) && (currentHeight != initialHeight)) {
            SetWebPartProperty(elem.vertClientId, elem.vertProperty, currentHeight + 'px');
        }

        if ((elem.horzProperty != null) && (currentWidth != initialWidth)) {
            SetWebPartProperty(elem.horzClientId, elem.horzProperty, currentWidth + 'px');
        }
    }

    currentLayoutElem = null;
    firstLayoutX = 0;
    firstLayoutY = 0;
    initialWidth = 0;
    initialHeight = 0;

    HideOverlay();

    return false;
}

function EnsureOverlay(cursor, el) {
    if (layoutInfoElem == null) {
        layoutInfoElem = document.createElement('div');
        layoutInfoElem.className = 'LayoutInfoOverlay';
        layoutInfoElem.innerHTML = '&nbsp;';

        document.body.appendChild(layoutInfoElem);

        layoutInfoText = document.createElement('div');
        layoutInfoText.className = 'LayoutInfoText';
        layoutInfoText.innerHTML = '&nbsp;';

        document.body.appendChild(layoutInfoText);
    }

    if (layoutOverlayElem == null) {
        layoutOverlayElem = document.createElement('div');
        layoutOverlayElem.className = 'LayoutOverlay';
        layoutOverlayElem.innerHTML = '&nbsp;';

        layoutOverlayElem.onmousemove = LayoutMouseMove;
        layoutOverlayElem.onmouseup = LayoutMouseUp;

        document.body.appendChild(layoutOverlayElem);
    }

    layoutOverlayElem.style.display = 'block';
    layoutOverlayElem.style.cursor = cursor;

    layoutInfoElem.style.display = 'block';
    layoutInfoText.style.display = 'block';

    UpdateLayoutInfo(el);
}

function HideOverlay() {
    layoutOverlayElem.style.display = 'none';
    layoutInfoElem.style.display = 'none';
    layoutInfoText.style.display = 'none';
}

