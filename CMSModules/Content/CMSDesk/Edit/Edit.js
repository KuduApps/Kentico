
// Selects the node within the tree
function SelectNode(nodeId) {
    parent.SelectNode(nodeId);
}

function RefreshTree(expandNodeId, selectNodeId) {
    // Update tree
    parent.RefreshTree(expandNodeId, selectNodeId);
}

function RefreshNode(expandNodeId, selectNodeId) {
    RefreshTree(expandNodeId, selectNodeId);
}

// Passive refresh
function PassiveRefresh(nodeId, parentNodeId, newName) {
    parent.PassiveRefresh(nodeId, parentNodeId, newName);
}

// Frames refresh
function FramesRefresh(refreshTree, selectNodeId) {
    if (parent.FramesRefresh) {
        parent.FramesRefresh(refreshTree, selectNodeId);
    }
}

// Create another
function CreateAnother() {
    parent.CreateAnother();
}

// Spell check
function SpellCheck(spellURL) {
    checkSpelling(spellURL);
}

// Refresh frame in split mode
function SplitModeRefreshFrame() {
    parent.SplitModeRefreshFrame();
}