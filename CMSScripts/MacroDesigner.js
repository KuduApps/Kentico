var UNIQUEID = 0;

function OnDropGroup(sender, value) {
    alert(value.get_container());
    alert(value.get_droppedItem());
    alert(value.get_position());
}


function addGroup(elem, isMovable) {
    if (elem != null) {

        var id = elem.getAttribute('id') + '_' + UNIQUEID++;
        var headerClass = (isMovable ? 'DDHandle' : '');
        var itemClass = (isMovable ? 'DDItem ' : '') + 'MacroDesignerGroup';

        var handleElem = document.createElement('div');
        handleElem.setAttribute('id', id + '_handle');
        handleElem.setAttribute('class', headerClass);
        handleElem.innerHTML = '<div class="MacroDesignerHeader"><input type="button" name="btnAddExpr" value="Add expression" onclick="addExpr(this.parentNode.parentNode.parentNode);" />&nbsp;<input type="button" name="btnAddGroup" value="Add group" onclick="addGroup(this.parentNode.parentNode.parentNode, true);" /></div>';

        var childElem = document.createElement('div');
        childElem.setAttribute('id', id);
        childElem.setAttribute('class', itemClass);
        childElem.appendChild(handleElem);

        elem.appendChild(childElem);

        // Add first expression
        //addExpr(childElem);
    }
}

function addExpr(elem) {
    if (elem != null) {

        var id = elem.getAttribute('id') + '_' + UNIQUEID++;
        var itemElem = document.createElement('div');

        var handleElem = document.createElement('div');
        handleElem.setAttribute('id', id + '_handle');
        handleElem.setAttribute('class', 'DDHandle');
        handleElem.innerHTML = '<input type=text name="leftExpr"><input type=text name="rightExpr">';
        itemElem.appendChild(handleElem);

        itemElem.setAttribute('id', id);
        itemElem.setAttribute('class', 'DDItem MacroDesignerExpr');
        elem.appendChild(itemElem);

        InitDragAndDrop();
    }
}

function InitDragAndDrop() {
    if (window.recursiveDragAndDrop) {
        window.recursiveDragAndDrop = true;
    }
    if (window.lastDragAndDropBehavior) {
        lastDragAndDropBehavior._initializeDraggableItems();
    }
}