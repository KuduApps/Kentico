// Saves the document
function SaveDocument(nodeId, createAnother) {
    if ((parent.frames[controlFrame] != null) && (parent.frames[controlFrame].SaveDocument != null)) {
        parent.frames[controlFrame].SaveDocument(nodeId, createAnother);
    }
    else {
        LocalSave(createAnother);
    }
}

// Approve
function Approve(nodeId) {
    if ((parent.frames[controlFrame] != null) && (parent.frames[controlFrame].Approve != null)) {
        parent.frames[controlFrame].Approve(nodeId);
    }
    else {
        LocalApprove();
    }
}

// Reject
function Reject(nodeId) {
    if ((parent.frames[controlFrame] != null) && (parent.frames[controlFrame].Reject != null)) {
        parent.frames[controlFrame].Reject(nodeId);
    }
    else {
        LocalReject();
    }
}

// Check in
function CheckIn(nodeId) {
    if ((parent.frames[controlFrame] != null) && (parent.frames[controlFrame].CheckIn != null)) {
        parent.frames[controlFrame].CheckIn(nodeId);
    }
    else {
        LocalCheckIn();
    }
}

// Spell check
function SpellCheck(spellURL) {
    if ((parent.frames[controlFrame] != null) && (parent.frames[controlFrame].SpellCheck != null)) {
        parent.frames[controlFrame].SpellCheck(spellURL);
    }
}

function UndoCheckoutConfirmation() {

}
