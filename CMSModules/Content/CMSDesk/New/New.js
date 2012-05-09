// Select node in CMSDesk-Content tree
function TreeSelectNode(nodeId) {
    parent.SelectNode(nodeId);
}

// Refresh node in CMSDesk-Content tree
function TreeRefreshNode(nodeId, selectNodeId) {
    parent.RefreshNode(nodeId, selectNodeId);
}

// Refresh tree with current node selected
function TreeRefresh() {
    parent.TreeRefresh();
}