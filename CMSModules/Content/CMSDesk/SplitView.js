var IsCMSDesk = true; 

function PassiveRefresh(nodeId, parentNodeId) {
    parent.PassiveRefresh(nodeId, parentNodeId);
}

function RefreshNode(nodeId, selectNodeId) {
    return parent.RefreshNode(nodeId, selectNodeId);
}

function RefreshTree(expandNodeId, selectNodeId) {
    parent.RefreshTree(expandNodeId, selectNodeId);
}

function SelectNode(nodeId) {
    parent.SelectNode(nodeId, null);
}

function NewDocument(parentNodeId, className) {
    parent.NewDocument(parentNodeId, className);
}

function DeleteDocument(nodeId) {
    parent.DeleteDocument(nodeId);
}

function EditDocument(nodeId) {
    parent.EditDocument(nodeId);
}

function SetTabsContext(mode) {
    parent.SetTabsContext(mode);
}