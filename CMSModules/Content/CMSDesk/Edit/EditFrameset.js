var IsCMSDesk = true;
var toolbarSize = 76;

function ShowToolbar() {
    frmSet = document.getElementById('editFrameset');
    if (frmSet != null) {
        frmSet.rows = '43, ' + toolbarSize + ', *';
    }
}

function PassiveRefresh(nodeId, parentNodeId, newName) {
    FramesRefresh(false, nodeId);

    if ((nodeId != null) && (parentNodeId != null)) {
        parent.PassiveRefresh(nodeId, parentNodeId);
    }
    if ((newName != null) && parent.AutoTitle) {
        parent.part = newName;
        parent.document.titlePart = newName;
        parent.AutoTitle();
    }
}

function FramesRefresh(refreshTree, selectNodeId) {
    if (frames['editheader'] != null) {
        frames['editheader'].location.replace(frames['editheader'].location);
    }
    if (refreshTree) {
        RefreshTree(selectNodeId, selectNodeId);
    }
}

function RefreshNode(nodeId, selectNodeId) {
    parent.RefreshNode(nodeId, selectNodeId);
    return false;
}

function RefreshTree(expandNodeId, selectNodeId) {
    // Update tree
    if (parent.name == 'cmsdesktop') {
        // Special case for 'New culture version'
        parent.frames['contenttree'].RefreshTree(expandNodeId, selectNodeId);
    }
    else {
        parent.RefreshTree(expandNodeId, selectNodeId);
    }
}

function SelectNode(nodeId) {
    if (parent.name == 'cmsdesktop') {
        // Special case for 'New culture version'
        parent.frames['contenttree'].SelectNode(nodeId);
    }
    else {
        parent.SelectNode(nodeId);
    }
}

function CreateAnother() {
    window.location.replace(window.location.href);
}

function NewDocument(parentNodeId, className) {
    parent.NewDocument(parentNodeId, className);
}

function NotAllowed(baseUrl, action) {
    parent.location.replace(baseUrl + '/CMSModules/Content/CMSDesk/NotAllowed.aspx?action=' + action);
}

function DeleteDocument(nodeId) {
    parent.DeleteDocument(nodeId);
}

function EditDocument(nodeId) {
    parent.EditDocument(nodeId);
}

function FileCreated(nodeId, parentNodeId, closeWindow) {
    if (wopener != null) {
        wopener.FileCreated(nodeId, parentNodeId, closeWindow);
    }
}

function CheckChanges() {
    if (window.frames['editview'].CheckChanges) {
        return window.frames['editview'].CheckChanges();
    }
    else {
        return true;
    }
}

function SetTabsContext(mode) {
    parent.SetTabsContext(mode);
}

var wopener = window.dialogArguments;
if (wopener == null) {
    wopener = opener;
}

function InitSplitViewSyncScroll(frameElement, body, refreshSameCulture, unbind) {
    if (parent.InitSplitViewSyncScroll) {
        parent.InitSplitViewSyncScroll(frameElement, body, refreshSameCulture, unbind);
    }
}

function CloseSplitMode() {
    parent.CloseSplitMode();
}

// Refresh frame in split mode
function SplitModeRefreshFrame() {
    parent.SplitModeRefreshFrame();
}