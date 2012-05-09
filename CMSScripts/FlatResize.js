//<![CDATA[
// Variables used for calculating the number of displayed items
var uniFlatItems = null;
var uniFlatItemsCount = 0;
var uniFlatFirstItem = null;
// Initial body height
var initHeigh = document.body.clientHeight;
// Maximal timer calls. 10 = 2 second
var maxTimerCount = 40;
// Current timer count
var timerCounter = 0;
// Offset value
var offsetValue = 1;
// Minimal height
var minHeight = 370;

var itemSelector = null;
var selectorTree = null;
var uniFlatContent = null;
var uniFlatSearchPanel = null;
var uniFlatPager = null;
var selectorFlatDescription = null;

// After DOM ready
$j(document.body).ready(initializeResize);

// Initialize resize
function initializeResize() {

    // Get items
    getItems(false);
    // Initial resize
    resizeareainternal();
    // get the number of items which can fit into the page
    uniFlatItemsCount = getItemsCount();
    // Create timer
    //setTimeout(resizeChecker, 200);
    $j(window).resize(function() { resizeareainternal(); });
}


// Check whether document is ready and set correct values
function resizeChecker() {
    
    // Check whether page is ready => body height contains correct values
    if (document.body.clientHeight != initHeigh) {

        // Resize elements
        resizearea();

        // Set window resize handler
        $j(window).resize(function() { resizeareainternal(); });
    }
    else {

        // Check whether current call is smaller than max. calls count
        if (timerCounter < maxTimerCount) {
            timerCounter++;
            setTimeout(resizeChecker, 200);
        }
        else {
            // Set window resize handler
            $j(window).resize(function() { resizeareainternal(); });
            // Clear counter
            timerCounter = 0;
        }
    }
}

// Resize elements handler
function resizearea() {
    getItems(true);
    resizeareainternal();
}


// Resize elements
function resizeareainternal() {
    var selectorOffsetTop = itemSelector.offset();
    if (selectorOffsetTop == null) {
        return;
    }

    // Main height
    height = document.body.clientHeight - selectorOffsetTop.top - $j("div .CopyLayoutPanel").outerHeight(true) - $j("#__ButtonsArea").outerHeight(true) - offsetValue;
    
    // Set minimal height
    if (height < minHeight) {
        height = minHeight;
    }
    
    // Selector container
    itemSelector.css("height", height);
    // Tree
    selectorTree.css("height", height);
    // Flat content height
    uniFlatContent.css("height", height - uniFlatSearchPanel.outerHeight(true) - selectorFlatDescription.outerHeight(true) - uniFlatPager.outerHeight(true));
}


// Ensures selector objects
function getItems(forceLoad) {
    if (forceLoad || (itemSelector == null)) {
        itemSelector = $j("div .ItemSelector");
        selectorTree = $j("div .SelectorTree");
        uniFlatContent = $j("div .UniFlatContent");
        uniFlatItems = $j("div .SelectorFlatItems");
        uniFlatFirstItem = $j("div .SelectorEnvelope:first");
        uniFlatSearchPanel = $j("div  .UniFlatSearchPanel");
        uniFlatPager = $j("div .UniFlatPager");
        selectorFlatDescription = $j("div .SelectorFlatDescription");
    }
}

// Gets a number which represents the max count o items which can fit into the "uniFlatContent" div
function getItemsCount() {
    if ((uniFlatItems == null) || (uniFlatFirstItem == null) || (uniFlatContent == null)) {
        return 0;
    }

    var cols = uniFlatItems.width() / uniFlatFirstItem.outerWidth(true) | 0;
    var rows = uniFlatContent.height() / uniFlatFirstItem.outerHeight(true) | 0;
    return cols * rows;
}

//]]>
