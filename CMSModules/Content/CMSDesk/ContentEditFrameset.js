var IsCMSDesk = true;

function PassiveRefresh(nodeId, parentNodeId) {
    if ((nodeId != null) && (parentNodeId != null)) {
        parent.frames['contenttree'].RefreshNode(parentNodeId, nodeId);
    }
}

function RefreshNode(nodeId, selectNodeId) {
    // Update language selector
    parent.frames['contentmenu'].SelectNode(nodeId);
    // Update tree
    parent.frames['contenttree'].RefreshNode(nodeId, selectNodeId);
    return false;
}

function RefreshTree(expandNodeId, selectNodeId) {
    // Update tree
    parent.frames['contenttree'].RefreshTree(expandNodeId, selectNodeId);
}

function SelectNode(nodeId) {
    parent.frames['contenttree'].SelectNode(nodeId, null);
}

function NewDocument(parentNodeId, className) {
    if (parentNodeId != 0) {
        parent.frames['contentmenu'].NewDocument(parentNodeId, className);
        parent.frames['contenttree'].RefreshNode(parentNodeId, parentNodeId);
    }
}

function DeleteDocument(nodeId) {
    if (nodeId != 0) {
        parent.frames['contentmenu'].DeleteDocument(nodeId);
        parent.frames['contenttree'].RefreshNode(nodeId, nodeId);
    }
}

function EditDocument(nodeId) {
    if (nodeId != 0) {
        parent.frames['contentmenu'].EditDocument(nodeId);
        parent.frames['contenttree'].RefreshNode(nodeId, nodeId);
    }
}

function SetTabsContext(mode) {
    if (window.frames['contenteditheader'].SetTabsContext) {
        window.frames['contenteditheader'].SetTabsContext(mode);
    }
}

// Refresh tree with current node selected
function TreeRefresh() {
    RefreshTree(parent.frames['contenttree'].currentNodeId, null);
}

function CloseSplitMode() {
    parent.frames['contentmenu'].CloseSplitMode();
}

function CheckChanges() {
    toReturn = true;
    if (window.frames['contenteditview'].CheckChanges) {
        toReturn = window.frames['contenteditview'].CheckChanges();
    }
    return toReturn;
}

function SplitModeRefreshFrame() {
    parent.SplitModeRefreshFrame();
}