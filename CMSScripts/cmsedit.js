window.cmsResizeIntervalIsSet = false;
var cmsDragEditableItem = '';

function InitializePage() {
    if (IsIE(8)) {
        // Ensure initialize page after load
        AddEvent(window, 'load', function() {
            InitializePageStart();
        });
    }
    else {
        InitializePageStart();
    }
}

function InitializePageStart() {
    window.cmsHeader = null;
    window.cmsHeaderPad = null;
    window.cmsFooter = null;
    window.cmsFooterPad = null;

    InitializeHeader();
    InitializeFooter();

    if (((window.cmsFooter != null)
	    && (window.cmsFooter != null))
	    || (!window.cmsResizeIntervalIsSet)) {
        window.resizeInterval = setInterval('if (window.ResizeToolbar) { ResizeToolbar(); }', 300);
        window.cmsResizeIntervalIsSet = true;
    }

    // Ensure header width on resize
    AddEvent(window, 'resize', function() {
        EnsureToolbarWidth();
    });
}

function InitializeHeader() {
    try {
        var docObj = (window.frames['pageview'] ? window.frames['pageview'].document : window.document);
        window.cmsHeader = docObj.getElementById('CMSHeaderDiv');
        if (window.cmsHeader != null) {
            if (window.cmsHeader.style.position !== 'fixed') {
                window.cmsHeader.style.position = 'fixed';
                window.cmsHeader.style.top = '0';
                window.cmsHeader.style.left = '0';
                window.cmsHeader.style.right = '0';
                window.cmsHeader.style.bottom = 'auto';
                window.cmsHeader.style.zIndex = '10000';
                window.cmsHeader.style.overflow = 'hidden';
                EnsureToolbarWidth();
            }
            window.cmsHeaderPad = docObj.getElementById('CMSHeaderPad');
            if (window.cmsHeaderPad == null) {
                // Create new padding div for header
                window.cmsHeaderPad = docObj.createElement('div');
                window.cmsHeaderPad.id = 'CMSHeaderPad';
                window.cmsHeaderPad.style.height = '0px';
                window.cmsHeaderPad.style.lineHeight = '0px';
                window.cmsHeader.parentNode.insertBefore(window.cmsHeaderPad, window.cmsHeader);

                CheckBackgroundPosition(docObj.body);
            }
        }
    } catch (err) {
    }
}

function InitializeFooter() {
    try {
        var docObj = (window.frames['pageview'] ? window.frames['pageview'].document : window.document);

        window.cmsFooter = docObj.getElementById('CMSFooterDiv');
        if (window.cmsFooter != null) {
            if (window.cmsFooter.style.position !== 'fixed') {
                window.cmsFooter.style.position = 'fixed';
                window.cmsFooter.style.bottom = '0';
                window.cmsFooter.style.left = '0';
                window.cmsFooter.style.width = '100%';
                window.cmsFooter.style.overflow = 'hidden';
                window.cmsFooter.style.zIndex = '10000';
            }
            window.cmsFooterPad = docObj.getElementById('CMSFooterPad');
            if (window.cmsFooterPad == null) {
                // Create new padding div for header
                window.cmsFooterPad = docObj.createElement('div');
                window.cmsFooterPad.id = 'CMSFooterPad';
                window.cmsFooterPad.style.height = '0px';
                // Append at the end of page
                docObj.body.appendChild(window.cmsFooterPad);
            }
        }
    } catch (err) {
    }
}

function CheckBackgroundPosition(element) {
    var bgTopPos = '', bgLeftPos = '';
    if (document.defaultView && document.defaultView.getComputedStyle) {
        var bgPos = document.defaultView.getComputedStyle(element, '').getPropertyValue('background-position');
        bgLeftPos = bgPos.match(/^[\w-%]+/)[0];
        bgTopPos = bgPos.match(/[\w-%]+$/)[0];
    } else if (element.currentStyle) { // IE
        bgTopPos = element.currentStyle['backgroundPositionY'];
        bgLeftPos = element.currentStyle['backgroundPositionX'];
    }

    var pixelsPosition = /px$/.test(bgTopPos);
    if ((bgTopPos === '0%') || (pixelsPosition) || (bgTopPos === 'top')) {
        window.moveBodyBg = true;
        window.bodyBgLeftPos = bgLeftPos;

        if (pixelsPosition) {
            window.bodyBgTopPos = parseInt(bgTopPos, 10);
        }
        else {
            window.bodyBgTopPos = 0;
        }
    }
}

function TrimString(inputString) {
    if (inputString) {
        return inputString.replace(/^\s*/, '').replace(/\s*$/, '');
    }
    return '';
}

function GetHeight() {
    var headerHeight = -1;
    if (window.cmsHeader) {
        if (TrimString(window.cmsHeader.innerHTML) !== '') {
            headerHeight = window.cmsHeader.offsetHeight;
        } else {
            headerHeight = 0;
        }
    }
    var footerHeight = -1;
    if (window.cmsFooter) {
        if (TrimString(window.cmsFooter.innerHTML) !== '') {
            footerHeight = window.cmsFooter.offsetHeight;
        } else {
            footerHeight = 0;
        }
    }
    // if height was not set try to initialize
    if ((headerHeight == -1) && (footerHeight == -1)) {
        InitializePage();
        headerHeight = 0;
        footerHeight = 0;
    }
    return { 'header': headerHeight, 'footer': footerHeight };
}

function ResizeToolbar() {
    try {
        var height = GetHeight();
        if (window.cmsHeaderPad) {
            window.cmsHeaderPad.style.height = height.header + 'px';
            if (window.moveBodyBg) {
                var pos = window.bodyBgLeftPos + ' ' + (window.bodyBgTopPos + height.header) + 'px';
                window.document.body.style.backgroundPosition = pos;
            }
        }
        if (window.cmsFooterPad) {
            window.cmsFooterPad.style.height = height.footer + 'px';
        }
    } catch (err) {
        InitializeHeader();
        InitializeFooter();
    }
}

function ShowToolbar() {
    InitializeHeader();
    InitializeFooter();
    ResizeToolbar();
}

function ClearToolbar() {
    try {
        if (!window.cmsHeader) {
            InitializeHeader();
        }
        if (window.cmsHeader) {
            window.cmsHeader.style.display = 'none';
            window.cmsHeader.style.height = '';
            window.cmsHeader = null;
            window.cmsHeaderPad = null;
        }
        ResizeToolbar();
    } catch (err) {
    }
}

function EnsureToolbarWidth() {
    if (window.cmsHeader) {
        if (IsIE()) {
            window.cmsHeader.style.width = document.documentElement.clientWidth + 'px';
        } else {
            window.cmsHeader.style.width = '100%';
        }
    }
}

function SaveDocument(nodeId, createAnother) {
    ClearToolbar();
    try {
        if (window.frames['pageview'].SaveDocument) {
            window.frames['pageview'].SaveDocument(nodeId, createAnother);
        } else {
            alert(notAllowedAction);
        }
    } catch (err) {
        alert(notAllowedAction);
    }
}

function Approve(nodeId) {
    ClearToolbar();
    try {
        if (window.frames['pageview'].Approve) {
            window.frames['pageview'].Approve(nodeId);
        } else {
            alert(notAllowedAction);
        }
    } catch (err) {
        alert(notAllowedAction);
    }
}

function Reject(nodeId) {
    ClearToolbar();

    if (window.frames['pageview'].Reject) {
        window.frames['pageview'].Reject(nodeId);
    }
}

function CheckIn(nodeId) {
    ClearToolbar();
    try {
        if (window.frames['pageview'].CheckIn) {
            window.frames['pageview'].CheckIn(nodeId);
        } else {
            alert(notAllowedAction);
        }
    } catch (err) {
        alert(notAllowedAction);
    }
}

function PassiveRefresh(nodeId, parentNodeId, newName) {
    if (parent.PassiveRefresh) {
        parent.PassiveRefresh(nodeId, parentNodeId, newName);
    }
}

function FramesRefresh(refreshTree, selectNodeId) {
    if (parent.FramesRefresh) {
        parent.FramesRefresh(refreshTree, selectNodeId);
    }
}

function RefreshTree(nodeId, selectNodeId) {
    if (parent.RefreshTree) {
        return parent.RefreshTree(nodeId, selectNodeId);
    }
    return false;
}

function RefreshNode(nodeId, selectNodeId) {
    if (parent.RefreshNode) {
        return parent.RefreshNode(nodeId, selectNodeId);
    }
    return false;
}

function SelectNode(nodeId) {
    if (parent.SelectNode) {
        return parent.SelectNode(nodeId);
    }
}

function CreateAnother() {
    if (parent.CreateAnother) {
        parent.CreateAnother();
    }
}

// Edit mode actions
function NotAllowed(baseUrl, action) {
    if (parent.NotAllowed) {
        parent.NotAllowed(baseUrl, action);
    }
}

function NewDocument(parentNodeId, className) {
    if (parent.NewDocument) {
        parent.NewDocument(parentNodeId, className);
    }
}

function DeleteDocument(nodeId) {
    if (parent.DeleteDocument) {
        parent.DeleteDocument(nodeId);
    }
}

function EditDocument(nodeId) {
    if (parent.EditDocument) {
        parent.EditDocument(nodeId);
    }
}

function SpellCheck(spellURL) {
    try {
        if (window.frames['pageview'].SpellCheck) {
            window.frames['pageview'].SpellCheck(spellURL);
        } else {
            alert(notAllowedAction);
        }
    } catch (err) {
        alert(notAllowedAction);
    }
}

// File created
function FileCreated(nodeId, parentNodeId, closeWindow) {
    if (parent.FileCreated) {
        parent.FileCreated(nodeId, parentNodeId, closeWindow);
    }
}

function CheckChanges() {
    if ((window.frames['pageview'] != null) && window.frames['pageview'].CheckChanges) {
        return window.frames['pageview'].CheckChanges();
    }
    return true;
}

function FocusFrame() {
    var fr = document.getElementById("pageview");
    try {
        if (document.all)
        //IE
        {
            fr.document.body.focus();
        } else
        //Firefox
        {
            fr.contentDocument.body.focus();
        }
    } catch (err) {
    }
}

function AddEvent(elem, type, eventHandle) {
    if (elem == null || elem == undefined)
        return;
    if (elem.addEventListener) {
        elem.addEventListener(type, eventHandle, false);
    } else if (elem.attachEvent) {
        elem.attachEvent("on" + type, eventHandle);
    }
}

function IsIE(versionNum) {
    if (/MSIE (\d+\.\d+);/.test(navigator.userAgent)) {
        if (versionNum) {
            var ieversion = new Number(RegExp.$1)
            return (ieversion === versionNum)
        }
        return true;
    }
    return false;
}

// Refresh frame in split mode
function SplitModeRefreshFrame() {
    parent.SplitModeRefreshFrame();
}

// Rename the textarea element (ID and NAME parameter) when moving widgets between zones
function CKRenameWidgetTextareas(zoneId, targetZoneId) {
    if ((cmsDragEditableItem != null)
        && (typeof (CKEDITOR) != "undefined")
        && (CKEDITOR != null)
        && (CKEDITOR.instances != null)) {
        var items = cmsDragEditableItem.split(';');
        $j.each(items, function(number) {
            var id = items[number];
            var oldInstance = CKEDITOR.instances[id];
            var config = null;
            if (oldInstance) {
                config = oldInstance.config;
                oldInstance.destroy();
            }

            var el = document.getElementById(id);
            if (el != null) {
                var re = new RegExp(zoneId);
                id = el.id.replace(re, targetZoneId);
                el.id = id;
                el.name = el.name.replace(re, targetZoneId);
                var jObj = $j(el);
                jObj.val(jObj.data('value'));
                jObj.html(jObj.val());
                el.defaultValue = jObj.html();
                if (typeof (CKReplace) == 'function') {
                    // Reload the CKEditor
                    CKReplace(id, config);
                }
            }
        });
    }
    cmsDragEditableItem = '';
}

function BeforeDropWebPart(container, item, position) {
    // Build a list of textareas (and store their value) which are being dragged because textareas loose their value when removed and inserted into DOM.
    cmsDragEditableItem = '';
    if ((typeof (CKEDITOR) != "undefined")
        && (CKEDITOR != null)
        && (CKEDITOR.instances != null)) {

        $j('textarea', item).each(function() {
            var jObj = $j(this);
            jObj.val(CKEDITOR.instances[jObj.attr('id')].getData());
            jObj.data('value', jObj.val());
            cmsDragEditableItem += jObj.attr('id') + ';';
        });
    }
}