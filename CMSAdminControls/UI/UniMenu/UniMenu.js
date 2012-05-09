(function($) {
    $.screenLeft = function(element) {
        var left = $(window).scrollLeft(), elem = $(element);
        return left > elem.offset().left;
    };
    $.screenRight = function(element) {
        var win = $(window), right = win.width() + win.scrollLeft(), elem = $(element);
        return right < elem.offset().left + elem.outerWidth();
    };
    $.extend($.expr[':'], {
        "screenLeft": function(a) { return $.screenLeft(a) },
        "screenRight": function(a) { return $.screenRight(a) }
    });
})(jQuery);

window.rightScroller = null, window.leftScroller = null;
function initRibbonScrollers() {
    // Create left slider element
    if (leftScroller === null) {
        leftScroller = $j('<div id="leftScroller" class="ContentMenuSlider LeftSlider" />').appendTo($j('body'));
        leftScroller.click(scrollLeft);
    }

    // Create right slider element
    if (rightScroller === null) {
        rightScroller = $j('<div id="rightScroller" class="ContentMenuSlider RightSlider" />').appendTo($j('body'));
        rightScroller.click(scrollRight);
    }

    // Check left and right groups
    var leftGroups = $j('.ContentMenuGroup').filter(':screenLeft:last');
    var rightGroups = $j('.ContentMenuGroup').filter(':screenRight:first');

    // Hide or show sliders
    if (leftGroups.length) { leftScroller.fadeIn() } else { leftScroller.fadeOut() }
    if (rightGroups.length) { rightScroller.fadeIn() } else { rightScroller.fadeOut() }
}

function scrollRight() {
    var r = $j('.ContentMenuGroup').filter(':screenRight:first');
    if (r.length) {
        var scroll = $j(window).scrollLeft() + window.scrollRightStep;
        $j('html, body').stop().animate({ scrollLeft: scroll }, 200, initRibbonScrollers);
    }
}

function scrollLeft() {
    var l = $j('.ContentMenuGroup').filter(':screenLeft:last');
    if (l.length) {
        var scroll = $j(window).scrollLeft() + window.scrollLeftStep;
        $j('html, body').stop().animate({ scrollLeft: scroll }, 200, initRibbonScrollers);
    }
}

function initializeButtonWidth() {
    $j('.SmallButton').each(function() {
        var j = $j(this), w = j.parentsUntil('td').first().innerWidth();
        j.parentsUntil('table').first().find('.MiddleSmallButton').width(w - 6);
    });
}

function selectMenuButton(buttonName) {
    if (typeof (SelectButton) === 'function') {
        SelectButton($j('div[name=' + buttonName + ']').get(0));
    }
}

$j(function() {
    initializeButtonWidth();
    initRibbonScrollers();
    $j(window).resize(function() {
        if (bodyElem.hasClass('RTL') && bodyElem.hasClass('IE7')) {
            // IE7 RTL scrolling bug
            $j('html, body').scrollLeft(50000);
        }
        else {
            $j('html, body').scrollLeft(0);
        }
        initRibbonScrollers()
    });

    // Append mouse wheel event on menu
    $j('.ContentMenu').mousewheel(function(event, delta) {
        if (delta > 0) { scrollLeft(); }
        else { scrollRight(); }
    });

    // Initialize scrolling step sizes variables
    window.scrollLeftStep = -200;
    window.scrollRightStep = 200;
    var bodyElem = $j('body');

    // Switch step sizes for IE8/9 bug in RTL
    if (bodyElem.hasClass('RTL') && (bodyElem.hasClass('IE8') || bodyElem.hasClass('IE9'))) {
        window.scrollLeftStep = 200;
        window.scrollRightStep = -200;
    }
});