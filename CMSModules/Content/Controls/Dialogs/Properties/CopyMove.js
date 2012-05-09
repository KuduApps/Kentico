var wopener = (window.top.opener ? window.top.opener : window.top.dialogArguments);
if (wopener == null) {
    wopener = opener;
}

// Select node in CMSDesk-Content tree
function TreeSelectNode(nodeId) {
    if (wopener.parent != null) {
        if (wopener.parent.frames['contenttree'] != null) {
            if (wopener.parent.frames['contenttree'].SelectNode != null) {
                wopener.parent.frames['contenttree'].SelectNode(nodeId);

            }
        }
    }
    // Opened from newpage
    if (wopener.TreeSelectNode != null) {
        wopener.TreeSelectNode(nodeId);
    }
}

// Refresh node in CMSDesk-Content tree
function TreeRefreshNode(nodeId, selectNodeId) {
    if (wopener.parent != null) {
        if (wopener.parent.frames['contenttree'] != null) {
            if (wopener.parent.frames['contenttree'].RefreshNode != null) {
                wopener.parent.frames['contenttree'].RefreshNode(nodeId, selectNodeId);
            }
        }
    }
    // Opened from newpage
    if (wopener.TreeRefreshNode != null) {
        wopener.TreeRefreshNode(nodeId);
    }
}

// Refresh listing after multiple action
function RefreshListing() {
    if (wopener != null) {
        if (wopener.RefreshGrid != null) {
            wopener.RefreshGrid();
        }
    }
}

// Initialize gray overlay in dialog
function InitializeLog() {
    var header = window.top.frames['insertHeader'];
    var content = window.top.frames['insertContent'];
    var footer = window.top.frames['insertFooter'];
    if (header != null) {
        var headerOverlay = header.document.createElement('DIV');
        headerOverlay.id = 'headerOverlay';
        headerOverlay.style.zIndex = '2500';
        headerOverlay.className = 'AsyncLogBackground';
        header.document.body.insertBefore(headerOverlay, header.document.body.firstChild);
    }
    if (footer != null) {
        var footerOverlay = footer.document.createElement('DIV');
        footerOverlay.id = 'footerOverlay';
        footerOverlay.style.zIndex = '2500';
        footerOverlay.className = 'AsyncLogBackground';
        footer.document.body.insertBefore(footerOverlay, footer.document.body.firstChild);
    }
    if (window.parent.expandIframe) {
        window.parent.expandIframe();
    }
}

// Remove gray overlay in dialog
function DestroyLog() {
    var header = window.top.frames['insertHeader'];
    var content = window.top.frames['insertContent'];
    var footer = window.top.frames['insertFooter'];
    if (header != null) {
        var headerOverlay = header.document.getElementById('headerOverlay');
        if (headerOverlay != null) {
            header.document.body.removeChild(headerOverlay);
        }
    }
    if (footer != null) {
        var footerOverlay = footer.document.getElementById('footerOverlay');
        if (footerOverlay != null) {
            footer.document.body.removeChild(footerOverlay);
        }
    }
    if (window.parent.collapseIframe) {
        window.parent.collapseIframe();
    }
}

// Clear UniGrid selection in opener window
function ClearSelection() {
    var wopener = window.top.wopener;
    if (wopener != null) {
        if (wopener.ClearSelection) {
            wopener.ClearSelection();
        }
    }
}

// Content tree refresh
function TreeRefresh() {
    if (wopener.parent != null) {
        if (wopener.parent.frames['contenttree'] != null) {
            if (wopener.parent.frames['contenttree'].RefreshTree != null) {
                wopener.parent.frames['contenttree'].RefreshTree(wopener.parent.frames['contenttree'].currentNodeId, null);
            }
        }
    }
    if (wopener.TreeRefresh != null) {
        wopener.TreeRefresh();
    }
}