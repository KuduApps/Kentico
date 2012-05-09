/*
* Initialize global variables
*/
jQuery(function() {
    InitResizers();
});

function InitGlobalVariables() {
    window.mainBlockElem = jQuery('.DialogMainBlock');
    window.leftBlockElem = jQuery('.DialogLeftBlock');
    window.separatorElem = jQuery('.DialogTreeAreaSeparator');
    window.rightBlockElem = jQuery('.DialogRightBlock');
    window.resizerElemV = jQuery('.DialogResizerVLine');
    window.resizerElemH = jQuery('.DialogResizerH');
    window.resizerArrowV = jQuery('.DialogResizerArrowV');
    window.propElem = jQuery('.DialogProperties');
    window.previewObj = jQuery('.DialogPropertiesPreview');
    window.propertiesFullObj = jQuery('.DialogPropertiesFullSize');
    window.webContentObj = jQuery('.DialogWebContent');
    window.webPropertiesObj = jQuery('.DialogWebProperties');
    window.menuElem = jQuery('.DialogMenu');
    window.viewElem = jQuery('.DialogViewContent');
    window.titleElem = jQuery('.DialogHeader');
    window.bodyObj = jQuery('body');
    if (jQuery('.DialogMainBlock').length > 0) {
        window.containerElem = jQuery('.DialogMainBlock').offsetParent();
    }
    else {
        window.containerElem = window.bodyObj;
    }
    window.isSafari = bodyObj.hasClass('Safari');
    window.isGecko = bodyObj.hasClass('Gecko');
    window.isOpera = bodyObj.hasClass('Opera');
    window.isRTL = bodyObj.hasClass('RTL');
    window.resizerLineHeight = 7;
}

function InitResizers() {
    jQuery('.DialogResizerArrowH').unbind('click');
    jQuery('.DialogResizerArrowH').click(function() {
        InitGlobalVariables();
        ResetListItemWidth();
        var thisElem = jQuery(this);
        var imgUrl = thisElem.css('background-image');
        imgUrl = imgUrl.replace('/RTL/Design/', '/Design/');
        imgUrl = imgUrl.replace(/\/minimize\./i, '/##status##.');
        imgUrl = imgUrl.replace(/\/maximize\./i, '/##status##.');
        var minimizeLTR = imgUrl.replace('/##status##.', '/minimize.');
        var maximizeLTR = imgUrl.replace('/##status##.', '/maximize.');
        var minimizeRTL = minimizeLTR.replace('/Design/', '/RTL/Design/');
        var maximizeRTL = maximizeLTR.replace('/Design/', '/RTL/Design/');
        var parentWidth = '233px';
        var thisSideUp = '233px';
        var thisSideDown = '0px';
        if (isRTL) {
            jQuery('.JqueryUITabs').css('position', 'static');
            if (thisElem.hasClass('ResizerDown')) {
                var width = mainBlockElem.innerWidth() - separatorElem.outerWidth() - leftBlockElem.outerWidth();
                rightBlockElem.width(width);
                resizerElemH.css('margin-right', '0px');
                leftBlockElem.css('margin-right', '0px');
                thisElem.css({
                    'right': thisSideUp,
                    'background-image': minimizeRTL
                });
                if ((!isSafari) && (!isOpera)) {
                    rightBlockElem.css('margin-right', '243px');
                }
                thisElem.removeClass('ResizerDown');
            }
            else {
                var width = mainBlockElem.innerWidth() - 10;
                rightBlockElem.width(width);
                leftBlockElem.css('margin-right', '-' + parentWidth);
                resizerElemH.css('margin-right', '-' + parentWidth);
                thisElem.css({
                    'right': thisSideDown,
                    'background-image': maximizeRTL
                });
                if ((!isSafari) && (!isOpera)) {
                    rightBlockElem.css('margin-right', '10px');
                }
                thisElem.addClass('ResizerDown');
            }
            setTimeout("jQuery('.JqueryUITabs').css('position', 'relative');", 100);
        }
        else {
            if (thisElem.hasClass('ResizerDown')) {
                if ((!isSafari) && (!isGecko) && (!isOpera)) {
                    rightBlockElem.css('margin-left', parentWidth);
                }
                resizerElemH.css('margin-left', '0px');
                leftBlockElem.css('margin-left', '0px');
                thisElem.css({
                    'left': thisSideUp,
                    'background-image': minimizeLTR
                });
                thisElem.removeClass('ResizerDown');
            }
            else {
                if ((!isSafari) && (!isGecko) && (!isOpera)) {
                    rightBlockElem.css('margin-left', '0px');
                }
                resizerElemH.css('margin-left', '-' + parentWidth);
                leftBlockElem.css('margin-left', '-' + parentWidth);
                thisElem.css({
                    'left': thisSideDown,
                    'background-image': maximizeLTR
                });
                thisElem.addClass('ResizerDown');
            }
        }
        SetListItemWidth();
        SetPreviewBox();
    });

    jQuery('.DialogResizerArrowV').unbind('click');
    jQuery('.DialogResizerArrowV').click(function() {
        jQuery('.DialogResizerV').css('position', 'static');
        InitGlobalVariables();
        var thisElem = jQuery(this);
        var imgUrl = thisElem.css('background-image');
        imgUrl = imgUrl.replace(/\/minimize\./i, '/##status##.');
        imgUrl = imgUrl.replace(/\/maximize\./i, '/##status##.');
        var maximize = imgUrl.replace('/##status##.', '/minimize.');
        var minimize = imgUrl.replace('/##status##.', '/maximize.');

        if (thisElem.hasClass('ResizerDown')) {
            viewElem.css('height', mainBlockElem.innerHeight() - propElem.outerHeight() - menuElem.outerHeight() - resizerLineHeight);
            propElem.css('display', 'block');
            thisElem.css('background-image', minimize);
            thisElem.removeClass('ResizerDown');
        }
        else {
            viewElem.css('height', mainBlockElem.innerHeight() - menuElem.outerHeight() - resizerLineHeight);
            propElem.css('display', 'none');
            thisElem.css('background-image', maximize);
            thisElem.addClass('ResizerDown');
        }
        setTimeout("jQuery('.DialogResizerV').css('position', 'relative');", 100);        
    });
}

function GetSelected(hdnId, hdnAnchors, hdnIds, editorId) {
    var selElem = GetSelectedItem(editorId);
    var selected = '';
    if (selElem) {
        for (var i in selElem) {
            selected += i + '|' + selElem[i] + '|';
        }
    }
    if (selected.length > 0) {
        selected = selected.substring(0, selected.length - 1);
        var hdnElement = document.getElementById(hdnId);
        if (hdnElement) {
            hdnElement.value = selected;
        }
    }
    if (window.GetAnchorNames) {
        var aAnchors = window.GetAnchorNames();
        if ((aAnchors != null) && (aAnchors.length > 0)) {
            var sAnchors = '';
            for (i = 0; i < aAnchors.length; i++) {
                sAnchors += escape(aAnchors[i]) + '|';
            }
            if (sAnchors.length > 0) {
                sAnchors = sAnchors.substring(0, sAnchors.length - 1);
                var eAnchors = document.getElementById(hdnAnchors);
                if (eAnchors) {
                    eAnchors.value = sAnchors;
                }
            }
        }
    }
    if (window.GetIds) {
        var aIds = window.GetIds();
        if ((aIds != null) && (aIds.length > 0)) {
            var sIds = '';
            for (i = 0; i < aIds.length; i++) {
                sIds += escape(aIds[i]) + '|';
            }
            if (sIds.length > 0) {
                sIds = sIds.substring(0, sIds.length - 1);
                var eIds = document.getElementById(hdnIds);
                if (eIds) {
                    eIds.value = sIds;
                }
            }
        }
    }
    DoHiddenPostback();
}

// Design methods
function SetListItemWidth() {
    // Dialog list name row IE6 
    var listItemsObj = jQuery('.DialogListItem');
    if (listItemsObj.length > 0) {
        var listItemCell = listItemsObj.parent();
        listItemsObj.width(listItemCell.width());
    }
}

function ResetListItemWidth() {
    var listItemsObj = jQuery('.DialogListItem');
    if (listItemsObj.length > 0) {
        listItemsObj.width(100);
    }
}

function SetPreviewBox() {
    if (previewObj.length > 0) {
        var previewTd = previewObj.parents('td');
        if (previewTd.is(':visible')) {
            var previewWidth = 0;
            if (previewTd.length > 0) {
                previewWidth = previewTd.width() - 20;
            }
            if (previewWidth > 80) {
                if (webContentObj.length > 0) {
                    previewObj.width(previewWidth);
                    previewObj.height(webPropertiesObj.height() - 60);
                }
                else {
                    // Width
                    previewObj.width(previewWidth);
                    // Height
                    if (propertiesFullObj.length > 0) {
                        previewObj.height(mainBlockElem.height() - 140);
                    }
                    else {
                        previewObj.height(210);
                    }
                }
                previewObj.css({ 'visibility': 'visible', 'display': 'block' });
            }
        }
        else {
            // Delayed display if jTabs not yet loaded
            setTimeout(SetPreviewBox, 100);
        }
    }
}

function InitializeDesign() {

    InitGlobalVariables();
    ResetListItemWidth();

    var menuHeight = menuElem.outerHeight();
    // Dialog view content height
    if (viewElem.length > 0) {
        var mainBlockHeight = containerElem.height() - titleElem.outerHeight();
        mainBlockElem.height(mainBlockHeight);
        var mainHeight = mainBlockHeight - menuHeight;
        var propHeight = propElem.outerHeight();
        if (resizerArrowV.hasClass('ResizerDown')) {
            viewElem.height(mainHeight - resizerLineHeight);
        }
        else
            if (viewElem.height() != (mainHeight - propHeight)) {
            viewElem.height(mainHeight - propHeight - resizerLineHeight);
        }
    }
    // Dialog tree height
    var treeAreaObj = jQuery('.DialogTreeArea');
    var siteBlockObj = jQuery('.DialogSiteBlock');
    var mediaLibraryBlockObj = jQuery('.DialogMediaLibraryBlock');
    if (mediaLibraryBlockObj.length == 0) {
        mediaLibraryBlockObj = jQuery('.MediaLibraryFolderActions');
    }
    var mediaLibraryTreeBlockObj = jQuery('.DialogMediaLibraryTreeArea');

    if (mainBlockElem.length > 0) {
        var treeHeight = 0;
        if (siteBlockObj.length > 0) {
            // Content tree
            treeHeight = mainBlockElem.innerHeight() - menuHeight - siteBlockObj.outerHeight();
        }
        else
            if (mediaLibraryBlockObj.length > 0) {
            // Media library tree
            treeHeight = mainBlockElem.innerHeight() - menuHeight - mediaLibraryBlockObj.outerHeight();
        }
        else {
            // Copy/Move tree
            treeHeight = mainBlockElem.innerHeight() - menuHeight;
        }
        if (rightBlockElem.length > 0) {
            var rightWidth = 0;
            if (jQuery('.DialogResizerArrowH').hasClass('ResizerDown')) {
                rightWidth = mainBlockElem.innerWidth() - 10;
            }
            else {
                rightWidth = mainBlockElem.innerWidth() - treeAreaObj.outerWidth() - separatorElem.outerWidth();
                // Fix IE6 double margin bug
                if ($j.browser.msie && jQuery.browser.version == '6.0') {
                    rightWidth = rightWidth - 5;
                }
            }
            if (rightBlockElem.width() != rightWidth) {
                rightBlockElem.width(rightWidth);
            }
        }
        if ((treeAreaObj.length > 0) && (treeAreaObj.height() != treeHeight)) {
            treeAreaObj.height(treeHeight);
        }
        if (mediaLibraryTreeBlockObj.length > 0) {
            var diferent = mediaLibraryTreeBlockObj.outerHeight() - mediaLibraryTreeBlockObj.height();
            if (mediaLibraryTreeBlockObj.height() != (treeHeight - diferent)) {
                mediaLibraryTreeBlockObj.height(treeHeight - diferent);
            }
        }
        else {
            var contentTreeObj = jQuery('.ContentTree');
            var diferent = contentTreeObj.outerHeight() - contentTreeObj.height();
            treeHeight = treeHeight - diferent;
            if (contentTreeObj.height() != treeHeight) {
                contentTreeObj.height(treeHeight);
            }
        }
    }

    // Dialog preview box
    SetPreviewBox();

    // Dialog list name row IE6
    SetListItemWidth();

    // Ensure preview box size
    jQuery('a[href$=tabImageGeneral]').click(SetPreviewBox);
    jQuery('a[href$=tabFlashGeneral]').click(SetPreviewBox);
    jQuery('a[href$=tabVideoGeneral]').click(SetPreviewBox);
}


// YouTube properties handling
function Loading(preview, loading) {
    jQuery('.YouTubeLoader').remove();
    var loader = null;
    if (jQuery('.YouTubeUrl')[0].value == '') {
        loader = jQuery('<div class="YouTubeLoader"><span>' + preview + '</span></div>');
    }
    else {
        loader = jQuery('<div class="YouTubeLoader"><span>' + loading + '</span></div>');
    }
    jQuery('div.YouTubeBox').append(loader);
    window.youTubeLoaded = false;
}

function onYouTubePlayerReady(playerId) {
    if (!window.youTubeLoaded) {
        jQuery('div.YouTubeLoader').hide();
        window.youTubeLoaded = true;
    }
}