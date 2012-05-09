function US_InitDropDown(control) {
    if (control != null) {
        control.originalValue = control.value;
    }
}

function SetVal(id, val) {
    if ((id != null) && (id != '')) {
        var el = document.getElementById(id);
        if (el != null) {
            el.value = val
        }
    }
}

function US_ItemChanged(control, clientId, restoredValue) {
    if (control != null) {
        var editButton = document.getElementById(clientId + '_btnDropEdit');
        if (editButton != null) {
            setTimeout('BTN_Enable(\"' + clientId + '_btnDropEdit\");', 0);
        }

        var disableButton = false;
        var result = true;
        var restoreSelection = false;

        if ((control.value == '-1') || (control.value == '0')) {
            disableButton = true;
        }
        else if (control.value == '-2') {
            setTimeout('US_SelectionDialog_' + clientId + '();', 1);
            disableButton = true;
            restoreSelection = true;
            result = false;
        }
        else if (control.value == '-3') {
            setTimeout('US_NewItem_' + clientId + '(\'' + control.value + '\');', 1);
            disableButton = false;
            restoreSelection = true;
            result = false;
        }
        else if (control.value == 'default') {
            result = false;
        }

        if (restoreSelection) {
            if (control.originalValue != null) {
                control.value = control.originalValue;
                if ((control.value == '-2') || (control.value == '-1') || (control.value == '0')) {
                    disableButton = true;
                }
            }
        }
        else {
            control.originalValue = control.value;
        }
        if (disableButton && (editButton != null)) {
            setTimeout('BTN_Disable(\"' + clientId + '_btnDropEdit\");', 0);
        }
        return result;
    }
    return true;
}

function US_SetItems(items, names, hiddenFieldID, txtClientID, hidValue, fireOnChanged) {
    SetVal(txtClientID, decodeURIComponent(names));
    SetVal(hiddenFieldID, items);
    SetVal(hidValue, items);
    
    return false;
}

function US_ProcessItem(clientId, valuesSeparator, chkbox, changeChecked) {
    var itemsElem = document.getElementById(clientId + '_hiddenSelected');
    var items = itemsElem.value;
    var item = chkbox.id.substr(36);
    if (changeChecked) {
        chkbox.checked = !chkbox.checked;
    }
    if (chkbox.checked) {
        if (items == '') {
            items = valuesSeparator + item + valuesSeparator;
        }
        else if (items.toLowerCase().indexOf(valuesSeparator + item.toLowerCase() + valuesSeparator) < 0) {
            items += item + valuesSeparator;
        }
    }
    else {
        var re = new RegExp(valuesSeparator + item + valuesSeparator, 'i');
        items = items.replace(re, valuesSeparator);
    }
    itemsElem.value = items;
}

function US_SelectAllItems(clientId, valuesSeparator, checkbox, checkboxClass) {
    var checkboxes = document.getElementsByTagName('input');
    for (var j = 0; j < checkboxes.length; j++) {
        var chkbox = checkboxes[j];
        if (chkbox.className == checkboxClass) {
            if (checkbox.checked) { chkbox.checked = true; }
            else { chkbox.checked = false; }

            US_ProcessItem(clientId, valuesSeparator, chkbox);
        }
    }
}