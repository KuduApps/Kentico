// Initialize buttons visibility
function ShowButtons(portal, reusable) {
    var inherited = (document.getElementById('SelectedTemplateId').value == '0');
    var templateExists = false;

    var txtElem = document.getElementById('txtTemplate');
    if ((txtElem != null) && (txtElem.value != "")) {
        templateExists = true;
    }

    if (editTemplatePropertiesElemStyle != null) {
        editTemplatePropertiesElemStyle.display = ((inherited || reusable && !allowEditShared) ? 'none' : 'inline');
    }
    
    var portalAndReusable = (portal && reusable);
    
    if (cloneElemStyle != null) {
        cloneElemStyle.display = (portalAndReusable ? 'inline' : 'none');
    }
    if (saveElemStyle != null) {
        saveElemStyle.display = ((!portal || !templateExists) ? 'none' : 'inline');
    }
    
    document.getElementById('isPortal').value = (portal ? 'true' : 'false');
    document.getElementById('isReusable').value = (reusable ? 'true' : 'false');
 
    return false;
}

function SelectTemplate(templateId, templateName, portal, reusable) {
    if (templateId != 0) {
        document.getElementById('SelectedTemplateId').value = templateId;
        document.getElementById('InheritedTemplateId').value = 0;

        if (templateName != null) {
            document.getElementById('txtTemplate').value = templateName;
        }

        ShowButtons(portal, reusable);
    }
}

// btnSelect onclick()
function OnSelectPageTemplate(templateId, templateName, selectorId, portal, reusable) {
    if (templateId != 0) {
        SelectTemplate(templateId, templateName, portal, reusable);

        if (inheritElemStyle != null) {
            inheritElemStyle.display = 'inline';
        }
    }
}

function NoTemplateSelected() {
    if (cloneElemStyle != null) {
        cloneElemStyle.display = 'none';
    }
    if (layoutElemStyle != null) {
        layoutElemStyle.display = 'none';
    }
    if (inheritElemStyle != null) {
        inheritElemStyle.display = 'inline';
    }
    if (editTemplatePropertiesElemStyle != null) {
        editTemplatePropertiesElemStyle.display = 'none';
    }
}

// btnInherit onclick()
function pressedInherit(inheritedTemplateId) {
    // ShowButtons() is called in code behind
    document.getElementById('SelectedTemplateId').value = 0;
    document.getElementById('InheritedTemplateId').value = inheritedTemplateId;
    document.getElementById('TextTemplate').value = document.getElementById('txtTemplate').value;
    
    if (inheritElemStyle != null) {
        inheritElemStyle.display = 'none';
    }
    if (editTemplatePropertiesElemStyle != null) {
        editTemplatePropertiesElemStyle.display = 'none';
    }
    return false;
}

function hideInherit() {
    if (inheritElemStyle != null) {
        inheritElemStyle.display = 'none';
    }
    if (editTemplatePropertiesElemStyle != null) {
        editTemplatePropertiesElemStyle.display = 'inline';
    }
}

// btnClone onclick()
function pressedClone(selectedTemplateId) {
    // ShowButtons() is called in code behind 
    document.getElementById('SelectedTemplateId').value = selectedTemplateId;
    document.getElementById('InheritedTemplateId').value = 0;
    document.getElementById('TextTemplate').value = document.getElementById('txtTemplate').value;

    if (inheritElemStyle != null) {
        inheritElemStyle.display = 'inline';
    }
    
    ShowButtons(true, false);
}

// btnSave onclick()
function ReceiveNewTemplateData(DisplayName, Category, Description, pageTemplateId, lIsPortal, lIsReusable) {
    if ((DisplayName != "") && (Category != 0))  // description can be empty
    {
        document.getElementById('TemplateDisplayName').value = DisplayName;
        document.getElementById('TemplateDescription').value = Description;
        document.getElementById('TemplateCategory').value = Category;

        document.getElementById('txtTemplate').value = DisplayName;
        document.getElementById('SelectedTemplateId').value = pageTemplateId;

        ShowButtons(true, true);
    }
}

function SetTemplateName(name) {
    document.getElementById('txtTemplate').value = name;
}

// Remembers template name.
function RememberTemplate(templateName) {
    document.getElementById('TextTemplate').value = templateName;
}
