var currentNodeId = 0;

// Refresh node action
function RefreshTree(nodeId, selectNodeId) {
    if (selectNodeId == null) {
        selectNodeId = currentNodeId;
    }
    if (window.treeUrl) {
        parent.frames['contentmenu'].SetSelectedNodeId(selectNodeId);
        ProcessRequest('refresh', selectNodeId, nodeId);
    }
}

function RefreshNode(nodeId, selectNodeId) {
    RefreshTree(nodeId, selectNodeId);
}

// Select node action
function SelectNode(nodeId, nodeElem) {
    if (nodeElem == null) {
        // Current node is original
        nodeElem = currentNode;
    }

    if (window.suppressOnClick) {
        window.suppressOnClick = false;
        return false;
    }

    if (!CheckChanges()) {
        return false;
    }

    if ((currentNode != null) && (nodeElem != null) && (nodeId != currentNodeId)) {
        currentNode.className = 'ContentTreeItem';
    }

    parent.frames['contentmenu'].SelectNode(nodeId);
    currentNodeId = nodeId;

    if (nodeElem != null) {
        currentNode = nodeElem;
        if (currentNode != null) {
            currentNode.className = 'ContentTreeSelectedItem';
        }
    }
}


// Display listing
function Listing(nodeId) {
    refreshTree = (currentNodeId != nodeId);

    parent.frames['contentmenu'].SetMode('listing');
    SelectNode(nodeId);

    if (refreshTree) {
        RefreshNode(nodeId, nodeId);
    }
}

// Initialize the window focus to the selected node
function InitFocus() {
    if (window.selectedJump != undefined) {
        selectedJump.focus();
    }
}

// New item action
function NewItem(nodeId, classId, refreshTree) {
    if (nodeId != 0) {
        parent.frames['contentmenu'].SetMode('edit');

        if ((classId != null) && (classId == '-1')) {
            // Unable to create a link through context menu
            //url = "contenteditframeset.aspx?action=newlink&nodeid=" + nodeId;
            url = "documentframeset.aspx?action=newlink&nodeid=" + nodeId;
        }
        else {
            url = "documentframeset.aspx?action=new&nodeid=" + nodeId;
            if (classId != null) {
                url += "&classid=" + classId;
            }
        }
        parent.frames['contentview'].location.href = url;

        if (refreshTree) {
            RefreshNode(0, nodeId);
        }
    }
}


// New AB Test variant action
function NewVariant(nodeId, refreshTree) {
    if (nodeId != 0) {
        parent.frames['contentmenu'].SetMode('edit');
        url = "documentframeset.aspx?action=newvariant&nodeid=" + nodeId;
        parent.frames['contentview'].location.href = url;

        if (refreshTree) {
            RefreshNode(0, nodeId);
        }
    }
}

// Delete item action
function DeleteItem(nodeId, refreshTree) {
    if (nodeId != 0) {
        parent.frames['contentview'].location.href = "documentframeset.aspx?action=delete&nodeid=" + nodeId;

        if (refreshTree) {
            RefreshNode(0, nodeId);
        }
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

// Move item action
function MoveItem(nodeId) {
    if (nodeId != 0) {
        // Display node selection dialog in move mode
        HideContextMenu('nodeMenu');
        modalDialog(parent.frames['contenttree'].document.getElementById('hdnMoveNodeUrl').value, 'contentselectnode', '90%', '85%');
    }
}

// Copy item action
function CopyItem(nodeId) {
    if (nodeId != 0) {
        // Display node selection dialog in copy mode
        HideContextMenu('nodeMenu');
        modalDialog(parent.frames['contenttree'].document.getElementById('hdnCopyNodeUrl').value, 'contentselectnode', '90%', '85%');
    }
}

// Performs the copy node action
function CopyNode(nodeId, targetId) {
    parent.frames['contentrequest'].location.href = requestPageUrl + "?action=copy&nodeid=" + nodeId + "&targetid=" + targetId;
}

// Performs the move node action
function MoveNode(nodeId, targetId) {
    parent.frames['contentrequest'].location.href = requestPageUrl + "?action=move&nodeid=" + nodeId + "&targetid=" + targetId;
}

// Displays the document properties page
function Properties(nodeId, tab) {
    if (nodeId != 0) {
        RefreshNode(0, nodeId);

        // Display the dialog
        parent.frames['contentview'].location.href = contentDir + "contenteditframeset.aspx?action=properties&tab=" + tab + "&nodeid=" + nodeId;
    }
}

// Performs the sort node action
function SortAlphaAsc(nodeId) {
    if (nodeId > 0) {
        ProcessRequest('sortalphaasc', nodeId);
    }
}

// Performs the sort node action
function SortAlphaDesc(nodeId) {
    if (nodeId > 0) {
        ProcessRequest('sortalphadesc', nodeId);
    }
}

// Performs the sort node action
function SortDateAsc(nodeId) {
    if (nodeId > 0) {
        ProcessRequest('sortdateasc', nodeId);
    }
}

// Performs the sort node action
function SortDateDesc(nodeId) {
    if (nodeId > 0) {
        ProcessRequest('sortdatedesc', nodeId);
    }
}

// Moves the node to the top
function MoveTop(nodeId) {
    if (nodeId > 0) {
        ProcessRequest('movetop', nodeId);
    }
}

// Moves the node to the top
function MoveBottom(nodeId) {
    if (nodeId != 0) {
        ProcessRequest('movebottom', nodeId);
    }
}

// Changes the language
function ChangeLanguage(nodeId, language) {
    ProcessRequest('setculture', nodeId, language);
}

function CheckChanges() {
    if (parent.frames['contentmenu'].CheckChanges) {
        return parent.frames['contentmenu'].CheckChanges();
    }
    else {
        return true;
    }
}

function DragOperation(nodeId, targetNodeId, operation) {
    parent.frames['contentview'].location.href = 'DragOperation.aspx?action=' + operation + '&nodeid=' + nodeId + '&targetnodeid=' + targetNodeId;
}

function CancelDragOperation() {
    if (parent.frames['contentview'].CancelDragOperation) {
        parent.frames['contentview'].CancelDragOperation();
    }
}

function ShowRefreshIcon() {
    if ((refreshIconID != null) && (document.getElementById(refreshIconID) != null)) {
        document.getElementById(refreshIconID).style.visibility = "visible";
    }
}

function HideRefreshIcon() {
    if ((refreshIconID != null) && (document.getElementById(refreshIconID) != null)) {
        document.getElementById(refreshIconID).style.visibility = "hidden";
    }
}
