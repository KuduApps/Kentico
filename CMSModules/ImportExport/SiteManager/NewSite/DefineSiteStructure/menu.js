// Gets the current node Id
function GetSelectedNodeId() {
    if (selNodeElem.value == null || selNodeElem.value == 0) {
        if (parent.frames['definestructureview'].GetSelectedNodeId) {
            return parent.frames['definestructureview'].GetSelectedNodeId();
        }
    }
    else {
        return selNodeElem.value;
    }
}

// Sets the current node Id
function SetSelectedNodeId(nodeId) {
    selNodeElem.value = nodeId;
}

// Processes the node selection
function SelectNode(nodeId) {
    SetSelectedNodeId(nodeId);
    DisplayInfo();
}

// Displays the selected document in the current mode
function DisplayInfo() {
    parent.frames['definestructureview'].location.href = "main.aspx?action=edit&nodeid=" + GetSelectedNodeId() + "&sitename=" + siteName;
}

// New item action
function NewItem() {
    if (GetSelectedNodeId() != 0) {
        parent.frames['definestructureview'].location.href = "main.aspx?action=new&nodeid=" + GetSelectedNodeId() + "&sitename=" + siteName;
    }
}

// Delete item action
function DeleteItem() {
    if (GetSelectedNodeId() != 0) {
        if (confirm(delConfirmation) == true) {
            parent.frames['definestructuretree'].DeleteItem(GetSelectedNodeId());
        }
    }
}

// Move UP item action
function MoveUp() {
    if (GetSelectedNodeId() != 0) {
        parent.frames['definestructuretree'].MoveUp(GetSelectedNodeId());
    }
}

// Move DOWN item action
function MoveDown() {
    if (GetSelectedNodeId() != 0) {
        parent.frames['definestructuretree'].MoveDown(GetSelectedNodeId());
    }
}    