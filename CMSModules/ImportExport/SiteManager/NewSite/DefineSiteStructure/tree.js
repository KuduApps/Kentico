var currentNode = null;
var currentNodeId = 0;

// Refresh node action
function RefreshNode(nodeId, selectNodeId) {
    if (selectNodeId == null) {
        selectNodeId = currentNodeId;
    }
    var siteName;
    var siteElem = document.getElementById('siteName');
    if (siteElem) {
        siteName = siteElem.value;
    }
    document.location.replace(treeUrl + "?expandnodeid=" + nodeId + "&nodeid=" + selectNodeId + "&sitename=" + siteName);
}

// Select node action
function SelectNode(nodeId, nodeElem) {
    if ((currentNode != null) && (nodeElem != null)) {
        currentNode.className = 'ContentTreeItem';
    }

    parent.frames['definestructuremenu'].SelectNode(nodeId);
    currentNodeId = nodeId;

    if (nodeElem != null) {
        currentNode = nodeElem;
        if (currentNode != null) {
            currentNode.className = 'ContentTreeSelectedItem';
        }
    }
}

// Delete item action
function DeleteItem(nodeId) {
    if (nodeId != 0) {
        ProcessRequest('delete', nodeId);
    }
}

// Move UP item action
function MoveUp(nodeId) {
    if (nodeId != 0) {
        ProcessRequest('moveup', nodeId);
    }
}

// Move DOWN item action
function MoveDown(nodeId) {
    if (nodeId != 0) {
        ProcessRequest('movedown', nodeId);
    }
}    