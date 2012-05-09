// Refresh action
function RefreshTree(nodeId, selectNodeId) {
    if (parent != null) {
        if (parent.parent != null) {
            if (parent.parent.frames['contenttree'] != null) {
                if (parent.parent.frames['contenttree'].RefreshTree != null) {
                    parent.parent.frames['contenttree'].RefreshTree(nodeId, selectNodeId);
                }
            }
        }
    }
}

// Selects the node within the tree
function SelectNode(nodeId) {
    if (parent != null) {
        if (parent.parent != null) {
            if (parent.parent.frames['contenttree'] != null) {
                if (parent.parent.frames['contenttree'].SelectNode != null) {
                    parent.parent.frames['contenttree'].SelectNode(nodeId, null);
                }
            }
        }
    }
}

// Display the document
function DisplayDocument(nodeId) {
    if (parent != null) {
        if (parent.parent != null) {
            if (parent.parent.frames['contentmenu'] != null) {
                if (parent.parent.frames['contentmenu'].SelectNode != null) {
                    parent.parent.frames['contentmenu'].SelectNode(nodeId);
                }
            }
        }
    }
}