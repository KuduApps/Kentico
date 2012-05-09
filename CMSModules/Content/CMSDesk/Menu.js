//var selNodeElem = document.getElementById('selectedNodeId');
//var curModeElem = document.getElementById('currentMode');
//var imagesUrl = document.getElementById('imagesUrl').value;

// Gets the current node Id
function GetSelectedNodeId() {
    return selNodeElem.value;
}

// Sets the current node Id
function SetSelectedNodeId(nodeId) {
    selNodeElem.value = nodeId;
}

// Gets the current node Id
function GetCurrentMode() {
    return curModeElem.value;
}

// Sets the current node Id
function SetCurrentMode(mode) {
    curModeElem.value = mode;
}

// ACTIONS

// Processes the node selection
function SelectNode(nodeId) {
    if (nodeId != null) {
        SetSelectedNodeId(nodeId);
    }
    DisplayDocument();
}

// Mode set action - sets the current editing mode
function SetMode(mode) {
    if (!CheckChanges()) {
        return false;
    }
    SetModeIcon(mode);
    SetCurrentMode(mode);
    DisplayDocument();
    return true;
}

// Displays the selected document in the current mode
function DisplayDocument() {
    mode = GetCurrentMode();
    nodeId = GetSelectedNodeId();
    if (mode == 'edit') {
        parent.frames['contentview'].location.href = contentDir + "contenteditframeset.aspx?nodeid=" + nodeId;
    }
    else if (mode == 'preview') {
        parent.frames['contentview'].location.href = contentDir + "documentframeset.aspx?action=preview&nodeid=" + nodeId;
    }
    else if (mode == 'livesite') {
        parent.frames['contentview'].location.href = contentDir + "documentframeset.aspx?action=livesite&nodeid=" + nodeId;
    }
    else if (mode == 'listing') {
        parent.frames['contentview'].location.href = contentDir + "documentframeset.aspx?action=listing&nodeid=" + nodeId;
    }
}


// Not allowed action
function NotAllowed(baseUrl, action) {
}


// New document action
function NewDocument(parentNodeId, classId) {
    if (parentNodeId != 0) {
        SetMode('edit');
        parent.frames['contentview'].location.href = contentDir + "documentframeset.aspx?action=new&nodeid=" + parentNodeId + "&classid=" + classId;
    }
}


// Particular document delete action
function DeleteDocument(nodeId) {
    if (nodeId > 0) {
        SetSelectedNodeId(nodeId);
        DeleteItem();
    }
}


// Switches to editing mode of particular document
function EditDocument(nodeId) {
    SetMode('edit');
    parent.frames['contentview'].location.href = contentDir + "contenteditframeset.aspx?mode=editform&nodeid=" + nodeId;
}


// New item action
function NewItem() {
    if (!CheckChanges()) {
        return false;
    }

    if (GetSelectedNodeId() != 0) {
        SetMode('edit');
        parent.frames['contentview'].location.href = "documentframeset.aspx?action=new&nodeid=" + GetSelectedNodeId();
    }
}

// Delete item action
function DeleteItem() {
    if (!CheckChanges()) {
        return false;
    }

    if (GetSelectedNodeId() != 0) {
        parent.frames['contentview'].location.href = contentDir + "documentframeset.aspx?action=delete&nodeid=" + GetSelectedNodeId();
    }
}

// Move UP item action
function MoveUp() {
    if (!CheckChanges()) {
        return false;
    }

    if (GetSelectedNodeId() != 0) {
        parent.frames['contenttree'].MoveUp(GetSelectedNodeId());
    }
}

// Move DOWN item action
function MoveDown() {
    if (!CheckChanges()) {
        return false;
    }

    if (GetSelectedNodeId() != 0) {
        parent.frames['contenttree'].MoveDown(GetSelectedNodeId());
    }
}

// Performs the copy node action
function CopyNode(nodeId, targetId) {
    if (!CheckChanges()) {
        return false;
    }

    parent.frames['contenttree'].CopyNode(nodeId, targetId);
}

// Performs the move node action
function MoveNode(nodeId, targetId) {
    if (!CheckChanges()) {
        return false;
    }

    parent.frames['contenttree'].MoveNode(nodeId, targetId);
}

// Performs the change language action
function ChangeLanguage(selectElem) {
    if (!CheckChanges()) {
        return false;
    }

    parent.frames['contenttree'].ChangeLanguage(GetSelectedNodeId(), selectElem.value);
}

// Performs the change language action
function ChangeLanguageByCode(cultureCode) {
    if (!CheckChanges()) {
        return false;
    }

    parent.frames['contenttree'].ChangeLanguage(GetSelectedNodeId(), cultureCode);
}

// Changes the split view
function ChangeSplitMode() {
    if (!CheckChanges()) {
        return false;
    }

    var newCookieValue = "1|#|Vertical|1";

    try {
        var cookieValue = $j.cookie('CMSSplitMode');
        var values = cookieValue.split('|');
        if (values.length == 4) {
            var displaySplitMode = values[0];
            if (displaySplitMode == '1'){
                displaySplitMode = '0';
            }
            else {
                displaySplitMode = '1';
            }
            // display split mode|split mode culture|split mode layout|synchronize scrollbars
            newCookieValue = displaySplitMode + "|" + values[1] + "|" + values[2] + "|" + values[3];
        }
    }
    catch (e) {
        // Use default value
    }

    // Set new values to cookies
    $j.cookie("CMSSplitMode", newCookieValue.toString(), { expires: 365, path: '/' });

    parent.frames['contentview'].location.replace(parent.frames['contentview'].location);
}

// Close the split view
function CloseSplitMode() {
    if (!CheckChanges()) {
        return false;
    }

    // Click the toggle button
    $j(".Toggle").click();
}

function CheckChanges() {
    try {
        if (parent.frames['contentview'].CheckChanges) {
            return parent.frames['contentview'].CheckChanges();
        }
    }
    catch (ex) {
    }

    return true;
}

// Performs the change language action
function OpenSearch() {
    if (!CheckChanges()) {
        return false;
    }

    parent.frames['contentview'].location.href = "./search/default.aspx";
}

// Maximilize the content area from main menu
function FullScreen() {

    if ((parent.frames['contentmenu'].Minimize) && (parent.frames['contenttree'].Minimize)) {
        parent.frames['contentmenu'].Minimize();
        parent.frames['contenttree'].Minimize();
    }
}