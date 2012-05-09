// Insert desired HTML at the current cursor position of the CK editor
function InsertHTML(htmlString) {
    // Get the editor instance that we want to interact with.
    var oEditor = CKEDITOR.instances[ckEditorID];

    // Check the active editing mode.
    if (oEditor.mode == 'wysiwyg') {
        // Insert the desired HTML.
        oEditor.insertHtml(htmlString);
    }
    else {
        alert('You must be on WYSIWYG mode!');
    }
}

// Set content of the CK editor - replace the actual one
function SetContent(newContent) {
    // Get the editor instance that we want to interact with.
    var oEditor = CKEDITOR.instances[ckEditorID];

    // Set the editor content (replace the actual one).
    oEditor.setData(newContent);
}

// Get content of the CK editor in XHTML
function GetContent() {
    // Get the editor instance that we want to interact with.
    var oEditor = CKEDITOR.instances[ckEditorID];

    // Get the editor content in XHTML.
    return oEditor.getData(); 
}


// Returns HTML code with standard table layout
function GenerateTableLayout() {
    var tableLayout = "";

    // indicates whether any row definition was added to the table
    var rowAdded = false;

    // list of attributes
    var list = lstAvailFieldsElem;

    // attributes count
    var optionsCount = list.options.length;

    for (var i = 0; i < optionsCount; i++) {
        tableLayout += "<tr><td>$$label:" + list.options[i].value + "$$</td><td>$$input:" + list.options[i].value + "$$<br/>$$validation:" + list.options[i].value + "$$</td></tr>";
        rowAdded = true;
    }

    if (rowAdded) {
        tableLayout = "<table><tbody>" + tableLayout + "</tbody></table>";
    }

    return tableLayout;
}

// Determines whether specified html string is already in CK editing window or not
function IsInContent(content, htmlString) {
    return (content.toLowerCase().indexOf(htmlString.toLowerCase()) != -1);
}

// Determines whether specified html string is already in CK editing window or not
function IsInContentMoreThanOnce(content, htmlString) {
    return (content.toLowerCase().indexOf(htmlString.toLowerCase()) != content.toLowerCase().lastIndexOf(htmlString.toLowerCase()))
}

// Insert desired HTML at the current cursor position of the CK editor if it is not already inserted 
function InsertAtCursorPosition(htmlString) {
    var content = GetContent();

    // doesnt already exist -> insert
    if (!IsInContent(content, htmlString)) {
        InsertHTML(htmlString);
    }
    // already exists -> alert
    else {
        alert(document.getElementById('alertexist').value + " '" + htmlString + "'");
    }
}

// Checks if field items are only once in CK editor content
function CheckContent() {
    var content = GetContent();

    // list of attributes
    var list = lstAvailFieldsElem;

    // attributes count
    var optionsCount = list.options.length;

    // array of field Items
    var fieldItems = new Array(3);

    // error mesaage to display
    var errorMessage = "";

    fieldItems[0] = "label:";
    fieldItems[1] = "input:";
    fieldItems[2] = "validation:";


    // for each field
    for (var i = 0; i < optionsCount; i++) {
        // for each field item
        for (var j = 0; j < 3; j++) {
            // string to check
            htmlString = "$$" + fieldItems[j] + list.options[i].value + "$$";

            if (IsInContentMoreThanOnce(content, htmlString)) {
                if (errorMessage == "") {
                    errorMessage = document.getElementById('alertexistfinal').value + "\n";
                }
                errorMessage += "'" + htmlString + "', ";
            }
        }
    }

    htmlString = "$$submitbutton$$";
    if (IsInContentMoreThanOnce(content, htmlString)) {
        if (errorMessage == "") {
            errorMessage = document.getElementById('alertexistfinal').value + "\n";
        }
        errorMessage += "'" + htmlString + "', ";
    }

    if (errorMessage != "") {
        // remove ending comma ", " from error string                
        errorMessage = errorMessage.substring(0, errorMessage.length - 2);

        // display error message
        alert(errorMessage);

        // avoid sending form data          
        return false;
    }
    else {
        // send form data
        return true;
    }


}

function ConfirmDelete() {
    return confirm(document.getElementById('confirmdelete').value);
}                      