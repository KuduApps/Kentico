var menuTimers = new Array();
var menuElements = new Array();
var dynamicMenus = new Array();
var menuLevels = new Array(10);
var loadingContent = new Array();
var mouseOverTimers = new Array();
var timerParameters = new Array();
var menuParameters = new Array();

for (i = 0; i < 10; i++) {
    menuLevels[i] = new Array();
}

var x = 0;
var y = 0;
var show = true;
var currentMenuId = null;
var currentElem = null;
var cursorOnMenu = false;

function DisableRight() { return ((window.event != null) ? (window.event.button != 2) : false) }
function DoNothing() { return false; }

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

function GetMouseX(ev) {
    return ev.clientX + GetScrollX();
}

function GetMouseY(ev) {
    return ev.clientY + GetScrollY();
}

function GetElementY(oElement,checkRelative) {
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

            if (checkRelative) {
                var currentStyle = null;
                if (oElement.currentStyle) {
                    currentStyle = oElement.currentStyle;
                }
                else if (window.getComputedStyle) {
                    currentStyle = window.getComputedStyle(oElement, null);
                }

                if (currentStyle != null) {
                    // Check position
                    var positionValue = currentStyle['position'];
                    if ((positionValue == 'relative')) {
                        return iReturnValue;
                    }
                }
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

function GetElementX(oElement,checkRelative) {
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

            if (checkRelative) {
                var currentStyle = null;
                if (oElement.currentStyle) {
                    currentStyle = oElement.currentStyle;
                }
                else if (window.getComputedStyle) {
                    currentStyle = window.getComputedStyle(oElement, null);
                }

                if (currentStyle != null) {
                    // Check position
                    var positionValue = currentStyle['position'];
                    if ((positionValue == 'relative')) {
                        return iReturnValue;
                    }
                }
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

function IsIE() {
    return jQuery.browser.msie;
}

function IsGecko() {
    return jQuery.browser.mozilla;
}

function IsWebKit() {
    return jQuery.browser.webkit;
}

function IsIE7() {
    return jQuery.browser.msie && (parseInt(jQuery.browser.version, 10) === 7);
}

function IsOpera() {
    return jQuery.browser.opera;
}

function IsW3C() {
    return (IsGecko() || IsOpera() || IsWebKit());
}

function GetLeftButton() {
    return IsW3C() ? 0 : 1;
}

function GetRightButton() {
    return 2;
}

function PrepareContextMenu(ev) {
    if (ev != null) {
        var menuElem = document.getElementById(currentMenuId);
        if (menuElem != null) {
            if ((document.getElementById(currentMenuId + "_horizontal").value == "Cursor")) {
                x = GetMouseX(ev) + 1;
            }
            if ((document.getElementById(currentMenuId + "_vertical").value == "Cursor")) {
                y = GetMouseY(ev) + 1;
            }

            show = false;
            menuButtons = document.getElementById(currentMenuId + "_button").value;
            if (((menuButtons == "Right") || (menuButtons == "Both")) && (ev.button == GetRightButton())) {
                show = true;
            }
            if (((menuButtons == "Left") || (menuButtons == "Both")) && (ev.button == GetLeftButton())) {
                show = true;
            }
        }
    }
}

function ReturnFalse() {
    return false;
}

function CM_Disable(elem) {
    if (elem != null) {
        elem.oncontextmenu = ReturnFalse;
    }
}

function InitContextMenu(menuId, elem) {
    currentMenuId = menuId;
    if (elem != null) {
        if (!elem.contextMenuInitialized) {
            elem.contextMenuInitialized = true;
            elem.onmousedown = PrepareContextMenu;
            elem.oncontextmenu = ReturnFalse;
        }
    }
}

function ContextMenu(menuId, elem, param, forceShow,mousex, mousey, checkRelative) {
    var menuElem = document.getElementById(menuId);
    if (menuElem != null) {
        if (forceShow) {
            show = true;
        }
        if (elem != null) {
            currentElem = elem;
        }

        if (mouseOverTimers[menuId] != null) {
            clearInterval(mouseOverTimers[menuId]);
            mouseOverTimers[menuId] = null;
        }

        if (currentMenuId != null) {
            var menuLevel = parseInt(document.getElementById(currentMenuId + "_level").value);
            HideContextMenus(menuLevel);
        }

        HideContextMenu(menuId);

        if (window.event != null) {
            mousex = GetMouseX(window.event);
            mousey = GetMouseY(window.event);

            if (!forceShow) {
                show = false;
                var menuButtons = document.getElementById(currentMenuId + "_button").value;
                if (((menuButtons == "Right") || (menuButtons == "Both")) && (window.event.button == GetRightButton())) {
                    show = true;
                }
                if (((menuButtons == "Left") || (menuButtons == "Both")) && (window.event.button == GetLeftButton())) {
                    show = true;
                }
            }
        }

        if ((mousex != null) && (document.getElementById(menuId + "_horizontal").value == "Cursor")) {
            x = mousex;
        }
        if ((mousey != null) && (document.getElementById(menuId + "_vertical").value == "Cursor")) {
            y = mousey;
        }

        switch (document.getElementById(menuId + "_vertical").value) {
            case "Top":
                y = GetElementY(currentElem, checkRelative);
                break;

            case "Bottom":
                y = GetElementY(currentElem, checkRelative) + currentElem.offsetHeight;
                break;

            case "Middle":
                y = GetElementY(currentElem, checkRelative) + currentElem.offsetHeight / 2;
                break;
        }

        var rtl = (document.body.className.indexOf('RTL') >= 0);
        switch (document.getElementById(menuId + "_horizontal").value) {
            case "Left":
                x = GetElementX(currentElem, checkRelative);
                if (rtl) {
                    if (IsIE7()) {
                        // Icon offset of content tree menu for IE7
                        if (menuId == "nodeMenu" && menuLevel == 0) {
                            x += 16;
                            break;
                        }
                    }
                    x += currentElem.offsetWidth;
                }
                break;

            case "Right":
                x = GetElementX(currentElem, checkRelative);
                if (!rtl) {
                    x += currentElem.offsetWidth;
                }
                break;

            case "Center":
                x = GetElementX(currentElem, checkRelative) + currentElem.offsetWidth / 2;
                break;

            case "Cursor":
                if (!rtl) {
                    x++;
                }
                else {
                    x--;
                }
                break;
        }

        if (rtl) {
            x -= parseInt(document.getElementById(menuId + "_offsetx").value);
        }
        else {
            x += parseInt(document.getElementById(menuId + "_offsetx").value);
        }
        y += parseInt(document.getElementById(menuId + "_offsety").value);

        if (show) {
            SetContextMenuParameter(menuId, param);
            var isDynamicMenu = (dynamicMenus[menuId] != null);
            if (isDynamicMenu) {
                var loading = loadingContent[menuId];
                if (loading != null) {
                    menuElem.innerHTML = loading;
                }
                else {
                    menuElem.innerHTML = "";
                }
                dynamicMenus[menuId](param);
            }
            menuElem.style.display = 'block';
            if (rtl) {
                x -= menuElem.offsetWidth;
            }
            menuElem.style.left = x + 'px';
            menuElem.style.top = y + 'px';

            if (!isDynamicMenu) {
                SetHideTimeout(menuId);
                CM_Visible(menuElem.id);
            }
            var activecss = document.getElementById(menuId + '_activecss').value;
            var inactivecss = document.getElementById(menuId + '_inactivecss').value;
            var activecssoffset = document.getElementById(menuId + '_activecssoffset').value;
            var activeElem = currentElem;

            var updateCss = true;
            for (var lvl = 0; lvl < activecssoffset; lvl++) {
                if (activeElem.parentNode != null) {
                    activeElem = activeElem.parentNode;
                }
                else {
                    updateCss = false;
                }
            }
            if (activecss != '' && updateCss) {
                if ((inactivecss == null) || (inactivecss == '')) {
                    inactivecss = activeElem.className;
                }
                activeElem.oldClassName = inactivecss;
                activeElem.className = activecss;
            }
            menuElements[menuId] = currentElem;
        }
    }
}

function CM_Visible(menuId) {
    var visibleHandler = eval('window.' + menuId + '_ContextMenuVisible');
    if (visibleHandler) {
        eval('window.' + menuId + '_ContextMenuVisible();');
    }
}

function CM_Close(menuId, timeout) {
    menuTimers[menuId] = setTimeout("HideContextMenu('" + menuId + "', true);", timeout);
}

function CM_Cancel(menuId) {
    if (menuTimers[menuId] != null) {
        clearTimeout(menuTimers[menuId]);
        menuTimers[menuId] = null;
    }
}

function HideContextMenu(menuId, hideSubmenus) {
    var menuElem = document.getElementById(menuId);
    if (menuElem != null) {
        if (menuTimers[menuId] != 'undefined') {
            if (hideSubmenus) {
                var menuLevel = parseInt(document.getElementById(menuId + "_level").value);
                HideContextMenus(menuLevel + 1);
            }

            CM_Cancel(menuId);

            if (mouseOverTimers[menuId] != null) {
                clearInterval(mouseOverTimers[menuId]);
                mouseOverTimers[menuId] = null;
            }
            if (menuElements[menuId] != null) {

                var activecssoffset = document.getElementById(menuId + '_activecssoffset').value;
                var activeElem = menuElements[menuId];
                var updateCss = true;
                for (var lvl = 0; lvl < activecssoffset; lvl++) {
                    if (activeElem.parentNode != null) {
                        activeElem = activeElem.parentNode;
                    }
                    else {
                        updateCss = false;
                    }
                }
                if (updateCss) {
                    activeElem.className = (activeElem.oldClassName != null) ? activeElem.oldClassName : '';
                }

                menuElements[menuId] = null;
            }

            var menuElem = document.getElementById(menuId);
            if (menuElem != null) {
                menuElem.style.display = 'none';
            }
        }
    }
}

function SetContextMenuParameter(menuId, param) {
    menuParameters[menuId] = param;
    var paramElem = document.getElementById(menuId + '_parameter');
    if (paramElem != null) {
        paramElem.value = param;
    }
	
    if (typeof (OnSetContextMenuParameter) == 'function') {
        OnSetContextMenuParameter(menuId, param);
    } 
	
    return null;
}

function GetContextMenuParameter(menuId) {
    return menuParameters[menuId];

    var paramElem = document.getElementById(menuId + '_parameter');
    if (paramElem != null) {
        return paramElem.value;
    }
    return null;
}

function CM_Receive(rvalue, context) {
    jQuery('#' + context).html(rvalue);
    CM_Visible(context);
    SetHideTimeout(context);
}

function SetHideTimeout(context) {
    menuTimers[context] = setTimeout("HideContextMenu('" + context + "');", 2000);
}

function HideContextMenus(startLevel) {
    for (i = menuLevels.length - 1; i >= startLevel; i--) {
        var level = menuLevels[i];
        for (j = 0; j < level.length; j++) {
            HideContextMenu(level[j]);
        }
    }
}

function HideAllContextMenus() {
    if (!cursorOnMenu) {
        HideContextMenus(0);
    }
}

function CM_Out(menuId, elem, param) {
    if (mouseOverTimers[menuId] != null) {
        clearInterval(mouseOverTimers[menuId]);
        mouseOverTimers[menuId] = null;
    }

    cursorOnMenu = false;
    return false;
}

function CM_Over(menuId, elem, param) {
    InitContextMenu(menuId, elem);

    var menuElem = document.getElementById(menuId);
    if (menuElem != null) {
        if (document.getElementById(menuId + "_mouseover").value == "1") {
            currentElem = elem;

            if ((document.getElementById(menuId).style.display != 'none') && (timerParameters[menuId] != param)) {
                HideContextMenu(menuId, true);
            }

            if (document.getElementById(menuId).style.display == 'none') {
                timerParameters[menuId] = null;
                if (mouseOverTimers[menuId] == null) {
                    timerParameters[menuId] = param;
                    var events = ""; 
                    if (window.event != null) {
                        events += ", true";
                        events += ", " + GetMouseX(window.event);
                        events += ", " + GetMouseY(window.event);
                    }
                    mouseOverTimers[menuId] = setInterval("ContextMenu('" + menuId + "', null, '" + param + "'" + events + ");", 1000);
                }
            }
        }
    }

    cursorOnMenu = true;
    return false;
}

function CM_Up(menuId, elem, param, checkRelative) {
    ContextMenu(menuId, elem, param, null, null, null, checkRelative);

    return false;
}

function RegisterAsyncCloser(closer) {
    if ((typeof (Sys) != 'undefined') && (typeof (Sys.WebForms) != 'undefined') && (typeof (Sys.WebForms.PageRequestManager) != 'undefined')) {
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(closer);
    }
}

if (typeof (Sys) != "undefined" && typeof (Sys.Application) != "undefined") {
    Sys.Application.notifyScriptLoaded();
}