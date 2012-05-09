// Delete item action
function DeleteItem(nodeId, parentNodeId) {
    if (nodeId != 0) {
        parent.location.href = "../DocumentFrameset.aspx?action=delete&multiple=true&nodeid=" + nodeId;
    }
}

// Edit item action
function EditItem(nodeId, parentNodeId) {
    if (nodeId != 0) {
        parent.parent.frames['contentmenu'].SelectNode(nodeId);
        parent.parent.frames['contentmenu'].SetMode('edit');
        parent.parent.frames['contenttree'].RefreshNode(parentNodeId, nodeId);
    }
}

// Select item action
function SelectItem(nodeId, parentNodeId) {
    if (nodeId != 0) {
        parent.parent.frames['contentmenu'].SelectNode(nodeId);
        parent.parent.frames['contenttree'].RefreshNode(parentNodeId, nodeId);
    }
}

// Redirect item
function RedirectItem(nodeId, culture) {
    if (parent != null) {
        if (parent.parent != null) {
            if (parent.parent.parent != null) {
                parent.parent.parent.location.href = "../../../../CMSDesk/default.aspx?section=content&action=edit&mode=editform&nodeid=" + nodeId + "&culture=" + culture;
            }
        }
    }
}